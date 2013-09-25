using FlightDataEntities;
using FlightDataEntities.Fault;
using FlightDataEntities.PHYFile;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading
{
    public class FlightDataReadingHandler
    {
        public FlightDataReadingHandler(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Read()
        {
            if (this.FileInfo == null || this.FileInfo.Exists == false)
            {
                throw new IOException("无法读取数据源文件。");
            }

            BufferedStream stream = new BufferedStream(new FileStream(this.FileInfo.FullName, FileMode.Open));
            m_stream = stream;
            using (m_Reader = new BinaryReader(stream))
            {
                try
                {
                    DoCore();
                }
                catch (Exception e)
                {//TODO: LOG it
                    if (this.m_stream != null)
                    {
                        this.m_stream.Close();
                        this.m_stream.Dispose();
                        this.m_stream = null;
                    }

                    throw e;
                }
            }

            if (this.m_stream != null)
            {
                this.m_stream.Close();
                this.m_stream.Dispose();
                this.m_stream = null;
            }
        }

        private void DoCore()
        {
            DateTime start = DateTime.Now;

            PHYHeader header = PHYHelper.ReadPHYHeader(m_Reader);
            int secondCount = PHYHelper.GetFlyParamSeconds(header);

            Task task1 = this.DoAddFlightRecordAsync(header);

            string aircraftModel = header.AircrfName;

            //获取需要读取的飞参列表
            List<FlyParameter> needReadParams = FlyParameter.Parameters.Values.ToList();

            Dictionary<string, FlyParameter> paramMap = new Dictionary<string, FlyParameter>();
            foreach (FlyParameter fp in needReadParams)
                paramMap.Add(fp.ID, fp);

            this.InitLevel2Entities(paramMap, secondCount);

            for (int i = 1; i <= secondCount; i++)
            {
                //读取当前秒的飞参值
                foreach (FlyParameter fp in needReadParams)
                {
                    float[] values = PHYHelper.ReadFlyParameter(m_Reader, i, header, fp);
                    FlightDataEntities.FlightRawData dt
                        = new FlightDataEntities.FlightRawData() { ParameterID = fp.ID, Second = i, Values = values };
                    this.PutRawDataToMongoDB(dt);

                    FlightRecordEntity entity = new FlightRecordEntity() { FlightSecond = i, ParameterID = fp.ID, Values = values };
                    this.PutEntityToLevel2Mentities(entity, secondCount);
                }
            }

            Task task2 =
                Task.Run(new Action(delegate()
            {
                this.FlushDataToMongoDB(secondCount);
            }));
            //一边入库一边处理Fault判定

            if (task1 != null)
                task1.Wait();
            if (task2 != null)
                task2.Wait();
        }

        private void InitLevel2Entities(Dictionary<string, FlyParameter> paramMap, int secondCount)
        {
            m_reducedPointsMap = new Dictionary<string, List<FlightRecordPoint>>();
            m_dataHelperMap = new Dictionary<string, Level2DataHelper>();
            m_level2DataMap = new Dictionary<string, Level2FlightRecord>();

            //初始化暂定使用一层
            foreach (string key in paramMap.Keys)
            {
                m_reducedPointsMap.Add(key, new List<FlightRecordPoint>());
                m_dataHelperMap.Add(key,
                    new Level2DataHelper() { MaxValue = float.MinValue, MinValue = float.MaxValue, Count = 0, SumValue = 0 });
            }
        }

        private Dictionary<string, Level2DataHelper> m_dataHelperMap = null;
        private Dictionary<string, Level2FlightRecord> m_level2DataMap = null;
        private Dictionary<string, List<FlightRecordPoint>> m_reducedPointsMap = null;
        //new Dictionary<string, List<FlightRecordPoint>>();

        class Level2DataHelper
        {
            public float MinValue { get; set; }
            public float MaxValue { get; set; }
            public decimal SumValue { get; set; }
            public int Count { get; set; }
        }

        private void PutEntityToLevel2Mentities(FlightRecordEntity entity, int lastSecond)
        {//TODO: 处理修改Level2、精简和……
            if (entity == null || string.IsNullOrEmpty(entity.ParameterID)
                || this.m_level2DataMap == null // || !this.m_level2DataMap.ContainsKey(entity.ParameterID)
                || !m_reducedPointsMap.ContainsKey(entity.ParameterID) || entity.ValueCount == 0)
                return;

            if (m_dataHelperMap.ContainsKey(entity.ParameterID))
            {
                var helper = m_dataHelperMap[entity.ParameterID];
                helper.Count++;
                helper.MinValue = Math.Min(helper.MinValue, entity.MinValue);
                helper.MaxValue = Math.Max(helper.MaxValue, entity.MaxValue);
                helper.SumValue += Convert.ToDecimal(entity.Values.Sum());
            }

            //var level2 = this.m_level2DataMap[entity.ParameterID];
            if (entity.ValueCount == 1
                || (entity.MinValue == entity.AvgValue && entity.MaxValue == entity.AvgValue))
            {//只有一个点
                //或者多个点平均值最大值最小值全等
                //可以认为曲线是平缓的，只记录一个点即可
                FlightRecordPoint point = new FlightRecordPoint()
                {
                    Second = entity.FlightSecond,
                    MillSec = 0,
                    Value = entity.Values[0]
                };
                m_reducedPointsMap[entity.ParameterID].Add(point);

                if (entity.ValueCount > 1 && entity.FlightSecond == lastSecond)
                {//如果超过一个点并且是最后一秒，则再记录最后一个点，否则线可能画不出来
                    FlightRecordPoint point2 = new FlightRecordPoint()
                    {
                        Second = entity.FlightSecond,
                        MillSec = 999,//最后一毫秒
                        Value = entity.Values[entity.ValueCount - 1]
                    };
                    m_reducedPointsMap[entity.ParameterID].Add(point2);
                }

                return;
            }

            //多个点的值有不同的情况，全部入库
            for (int i = 0; i < entity.ValueCount; i++)
            {
                FlightRecordPoint point3 = new FlightRecordPoint()
                {
                    Second = entity.FlightSecond,
                    MillSec = Convert.ToInt32(Decimal.Round(
                        Convert.ToDecimal(1000.0 * (i / entity.ValueCount)))),
                    Value = entity.Values[entity.ValueCount - 1]
                };
                m_reducedPointsMap[entity.ParameterID].Add(point3);
            }
        }

        private void FlushDataToMongoDB(int lastSecond)
        {
            //先处理精简后的Level2数据
            foreach (string key in this.m_reducedPointsMap.Keys)
            {
                var list = m_reducedPointsMap[key];
                Level2FlightRecord record = new Level2FlightRecord() { ParameterID = key };

                var helper = m_dataHelperMap[key];
                record.Count = helper.Count;
                record.StartSecond = 0;
                record.EndSecond = lastSecond;
                record.MinValue = helper.MinValue;
                record.MaxValue = helper.MaxValue;
                record.AvgValue = Convert.ToSingle(helper.SumValue / helper.Count);

                record.Level1FlightRecord = list.ToArray();

                m_level2DataMap.Add(record.ParameterID, record);
            }

            MongoClient dbClient = new MongoClient(this.MongoDBConnectionString);

            MongoServer dbServer = dbClient.GetServer();

            dbServer.Connect();
            MongoDatabase db = dbServer.GetDatabase(this.PreSetAircraftModelName);

            MongoCollection<Level2FlightRecord> level2Collection
                = db.GetCollection<Level2FlightRecord>(AircraftCollections.Level2FlightRecord);

            var start = DateTime.Now;
            int counter = 0;
            foreach (var oneLine in m_level2DataMap.Values)
            {
                counter++;
                level2Collection.Insert(oneLine);
                if (counter % 10 == 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("inputed level2 data count: {0}", counter));
                }
            }
            var end = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format("counter: {0}", counter));
            System.Diagnostics.Debug.WriteLine(string.Format(
                "total mongodb level2 insert millsec: {0}", end.Subtract(start).TotalMilliseconds));

            foreach (string paramId in m_reducedPointsMap.Keys)
            {
                var query = from one in level2Collection.AsQueryable<Level2FlightRecord>()
                            where one.ParameterID == paramId
                            select one;

                if (query != null)
                {
                    //
                }

                //    Query<Level2FlightRecord>.EQ(
                //        e => e.ParameterID, paramId);
                //Level2FlightRecord rec = level2Collection.FindOne(query);
            }
            var end2 = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format(
                "total mongodb level2 select millsec: {0}", end2.Subtract(end).TotalMilliseconds));

            dbServer.Disconnect();

            //DEBUG: 暂时不处理入库
        }

        private void PutRawDataToMongoDB(FlightDataEntities.FlightRawData dt)
        {
            //DEBUG: 暂时不处理入库
        }

        private Task DoAddFlightRecordAsync(PHYHeader header)
        {
            return null;
        }

        private BinaryReader m_Reader = null;
        private Stream m_stream = null;
        private object m_syncRoot = new object();

        private string m_filePath = string.Empty;

        public string FilePath
        {
            get { return m_filePath; }
            set
            {
                m_filePath = value;
                if (string.IsNullOrEmpty(this.m_filePath))
                    this.m_fileInfo = null;
                else this.m_fileInfo = new FileInfo(this.m_filePath);
            }
        }

        private FileInfo m_fileInfo = null;

        public FileInfo FileInfo
        {
            get
            {
                return m_fileInfo;
            }
        }

        public string MongoDBConnectionString { get; set; }

        public string PreSetAircraftModelName { get; set; }
    }
}
