using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.old_test
{
    public class FlightRawDataWrapper : IDisposable
    {
        public FlightDataHeader Header
        {
            get;
            set;
        }

        public FlightRawDataWrapper(string filePath)
        {
            this.FilePath = filePath;
        }

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

        public IEnumerable<FlightDataContentFrame> ReadFrames(int count)
        {
            return null;
        }

        private BinaryReader m_Reader = null;
        private Stream m_stream = null;
        private object m_syncRoot = new object();

        public void Open()
        {
            if (this.FileInfo == null || this.FileInfo.Exists == false)
            {
                throw new IOException("");
            }

            BufferedStream stream = new BufferedStream(new FileStream(this.FileInfo.FullName, FileMode.Open));
            m_stream = stream;
            m_Reader = new BinaryReader(stream);

            DateTime start = DateTime.Now;

            PHYHeader header = PHYHelper.ReadPHYHeader(m_Reader);

            int secondCount = PHYHelper.GetFlyParamSeconds(header);

            //获取需要读取的飞参列表
            List<FlyParameter> needReadParams = FlyParameter.Parameters.Values.ToList();// FaultHelper.GetFlyParamNoRepetion(faults);

            Dictionary<string, FlyParameter> paramMap = new Dictionary<string, FlyParameter>();
            foreach (FlyParameter fp in needReadParams)
                paramMap.Add(fp.ID, fp);

            //定义记录故障涉及的飞参的所有值
            Dictionary<string, List<float>> paramValues = new Dictionary<string, List<float>>();
            foreach (FlyParameter fp in needReadParams)
                paramValues.Add(fp.ID, new List<float>());

            System.Collections.Concurrent.ConcurrentDictionary<int, Dictionary<string, List<float>>> secsValues
                = new System.Collections.Concurrent.ConcurrentDictionary<int, Dictionary<string, List<float>>>();

            //ID:{0},{Secs:{1},{MIN,MAX,AVG,COUNT}}
            //Dictionary<string, Dictionary<int, Dictionary<string, float>>> summaryValues
            //    = new Dictionary<string, Dictionary<int, Dictionary<string, float>>>();
            Dictionary<string, BsonDocument> summaryValues =
                new Dictionary<string, BsonDocument>();

            foreach (FlyParameter fp in needReadParams)
                summaryValues.Add(fp.ID, new BsonDocument());

            for (int i = 1; i <= secondCount; i++)
            {
                Dictionary<string, List<float>> paramValuesSec = new Dictionary<string, List<float>>();
                foreach (FlyParameter fp in needReadParams)
                    paramValuesSec.Add(fp.ID, new List<float>());

                secsValues.TryAdd(i, paramValuesSec);

                //读取当前秒的飞参值
                foreach (FlyParameter fp in needReadParams)
                {
                    float[] values = PHYHelper.ReadFlyParameter(m_Reader, i, header, fp);

                    float min = values.Min();
                    float max = values.Max();
                    float avg = values.Average();
                    float count = values.Length;
                    //Dictionary<string, float> sumVal = new Dictionary<string, float>();
                    //sumVal.Add("MIN", min);
                    //sumVal.Add("MAX", max);
                    //sumVal.Add("AVG", avg);
                    //sumVal.Add("COUNT", count);
                    //sumVal.Add("Second", i);

                    BsonDocument secValues = summaryValues[fp.ID];
                    if (!secValues.Contains("MIN"))
                        secValues.Add("MIN", new BsonArray());
                    BsonElement e = secValues.GetElement("MIN");
                    e = secValues.GetElement("MIN");
                    var array = e.Value as BsonArray;
                    array.Add(min);

                    if (!secValues.Contains("MAX"))
                        secValues.Add("MAX", new BsonArray());
                    e = secValues.GetElement("MAX");
                    array = e.Value as BsonArray;
                    array.Add(max);

                    if (!secValues.Contains("AVG"))
                        secValues.Add("AVG", new BsonArray());
                    e = secValues.GetElement("AVG");
                    array = e.Value as BsonArray;
                    array.Add(avg);

                    if (!secValues.Contains("COUNT"))
                        secValues.Add("COUNT", new BsonArray());
                    e = secValues.GetElement("COUNT");
                    array = e.Value as BsonArray;
                    array.Add(count);

                    //secValues.Add(i.ToString(), secValues);

                    paramValues[fp.ID].AddRange(values);
                    secsValues[i][fp.ID].AddRange(values);
                }

                //summaryValues.Add(i, secValues);
            }

            foreach (FlyParameter fp in needReadParams)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} : {1}", fp.Caption, paramValues[fp.ID].Count));
                //paramValues.Add(fp.ID, new List<float>());
            }

            //List<MillRecord> records = new List<MillRecord>();

            var connectionString = "mongodb://localhost/?w=1";
            var db = "test_insert";
            var collection = "test_insert_collection_" + DateTime.Now.ToString("YYYYMMDD");
            LogFileToBson bsonHelper = new LogFileToBson(connectionString, db, collection);
            bsonHelper.Connect();

            var bsonHelper2 = new LogFileToBson(connectionString, db, collection);

            //bsonHelper2.Connect();
            //var mongoCollection = bsonHelper2.MongodbCollection;

            //mongoCollection.EnsureIndex(summaryValues.Keys.ToArray());

            ////mongoCollection.AsQueryable<BsonDocument>();
            ////QueryDocument query = new QueryDocument();

            //var provider = new MongoQueryProvider(mongoCollection);
            //var iqueryable = mongoCollection.AsQueryable();//(IQueryable)new MongoQueryable<BsonDocument>(provider);
            //var results = iqueryable.Skip(5).Take(20);

            //foreach (var onerec in results)
            //{
            //    System.Diagnostics.Debug.WriteLine(onerec.ToString());
            //}

            //bsonHelper2.Disconnect();

            int counter = 0;
            System.Collections.Concurrent.ConcurrentBag<BsonDocument> docs
                = new System.Collections.Concurrent.ConcurrentBag<BsonDocument>();

            //int insertTimes = 0;

            TimeSpan span2 = new TimeSpan(0);

            Task createTask = Task.Run(new Action(delegate()
            {
                foreach (int oneKey in secsValues.Keys)
                {
                    BsonDocument ddoc = new BsonDocument();
                    ddoc.Add("AircrfNum", new BsonString("0004"));
                    ddoc.Add("DateTime", new BsonDateTime(DateTime.Now));
                    ddoc.Add("_Year", new BsonInt32(DateTime.Now.Year));
                    ddoc.Add("_Month", new BsonInt32(DateTime.Now.Month));
                    ddoc.Add("_Day", new BsonInt32(DateTime.Now.Day));
                    ddoc.Add("_Hour", new BsonInt32(DateTime.Now.Hour));
                    ddoc.Add("_Minute", new BsonInt32(DateTime.Now.Minute));
                    ddoc.Add("_Second", new BsonInt32(oneKey));
                    ddoc.Add("AircrfInstance", new BsonString("001"));
                    ddoc.AddRange(secsValues[oneKey]);

                    docs.Add(ddoc);
                    //ddoc.Add("SummaryValues",
                    //    new BsonDocument(summaryValues));

                    // bsonHelper.Insert(ddoc);
                    counter++;
                    if (counter % 1000 == 0)
                    {
                        var st = DateTime.Now;
                        bsonHelper.InsertBatch(docs);
                        docs = new System.Collections.Concurrent.ConcurrentBag<BsonDocument>();
                        var ed = DateTime.Now;
                        span2 += ed.Subtract(st);
                        System.Diagnostics.Debug.WriteLine("{1} inserted:{0}", counter, DateTime.Now);
                    }
                }

                if (docs.Count > 0)
                {
                    var st = DateTime.Now;
                    bsonHelper.InsertBatch(docs);
                    var ed = DateTime.Now;
                    span2 += ed.Subtract(st);
                }
                //bsonHelper.Insert(new BsonDocument("SummaryValues", new BsonDocument(summaryValues)));

                var st1 = DateTime.Now;
                bsonHelper.InsertBatch(summaryValues.Values);
                var ed1 = DateTime.Now;
                span2 += ed1.Subtract(st1);
                //foreach (var sumV in summaryValues.Values)
                //{
                //    bsonHelper.InsertBatch(s
                //}

                #region old
                /*
                int[] keyArray = secsValues.Keys.ToArray();

                for(int j = 0 ;j < keyArray.Length ; j++)
                //Parallel.For(0, keyArray.Length,
                //    //Parallel.ForEach(secsValues.Keys, 
                //new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                //   new Action<int>(
                //       delegate(int j)
                       //   foreach (int key in secsValues.Keys)
                       {
                           int key = keyArray[j];
                           foreach (string id in secsValues[key].Keys)
                           {

                               BsonDocument doc = new BsonDocument();
                               int sec = key;
                               string idkey = id;
                               string caption = paramMap[idkey].Caption;
                               string unit = paramMap[idkey].Unit;
                               doc.Add(new BsonElement("IDKey",
                                   new BsonString(idkey)));
                               doc.Add(new BsonElement("Caption",
                                   new BsonString(caption)));
                               doc.Add(new BsonElement("Second",
                                   new BsonInt32(sec)));
                               doc.Add(new BsonElement("Unit",
                                   new BsonString(unit)));
                               doc.Add("Value",
                                   new BsonArray(secsValues[key][id]));
                               

                               //BsonDocument doc2 = new BsonDocument();

                               //List<float> cnts = secsValues[key][id];
                               //int millsecRange = 1000;
                               //for (int i = 0; i < cnts.Count; i++)
                               //{
                               //    int mill = Convert.ToInt32(Decimal.Round(
                               //        Convert.ToDecimal((double)millsecRange * ((double)i / (double)cnts.Count))));
                               //    //doc2
                               //    //doc2.Add(new BsonElement("MillSec",
                               //    //   new BsonInt32(mill)));
                               //    doc2.Add(new BsonElement("Value",
                               //        new BsonArray(
                               //        new BsonDouble(cnts[i])));

                               //    //counter++;
                               //    //if (counter % 10000 == 0)
                               //    //    System.Diagnostics.Debug.WriteLine("{1} inserted:{0}",
                               //    //        counter, DateTime.Now);//.Subtract(middle).TotalMilliseconds);

                               //}

                               //doc.Add("MillSecs", doc2);
                               docs.Add(doc);
                               //bsonHelper.Insert(doc);
                               counter++;
                               if (counter % 1000 == 0)
                               {
                                   bsonHelper.InsertBatch(docs);
                                   docs = new System.Collections.Concurrent.ConcurrentBag<BsonDocument>();

                                   System.Diagnostics.Debug.WriteLine("{1} inserted:{0}",
                                       counter, DateTime.Now);//.Subtract(middle).TotalMilliseconds);
                               }
                           }

                           Dictionary<string, List<float>> tmpVals = null;

                           secsValues.TryRemove(key, out tmpVals);
                       }//));*/
                #endregion
            }));
            createTask.Wait();

            System.Diagnostics.Debug.WriteLine("{1} inserted:{0}", counter, DateTime.Now);
            docs = null;//.Clear();
            bsonHelper.Disconnect();

            DateTime end = DateTime.Now;

            TimeSpan span = end.Subtract(start);
            //TimeSpan span2 = end.Subtract(middle);

            System.Diagnostics.Debug.WriteLine("total millsecs: {0}, mongo: {1}",
                span.TotalMilliseconds, span2.TotalMilliseconds);

            Console.Read();
            // FlightDataHeader h = new FlightDataHeader(){ Segments = new FlightDataContentSegment[] { 
            //    new FlightDataContentSegment[]{ header.

            // this.ReadHeader();
        }

        private void ToMongoDB(List<MillRecord> records)
        {
            var connectionString = "mongodb://localhost/?w=1";
            var db = "test_insert";
            var collection = "test_insert_collection";
            LogFileToBson bsonHelper = new LogFileToBson(connectionString, db, collection);
            bsonHelper.Connect();
            int counter = 0;

            List<BsonDocument> docs = new List<BsonDocument>();
            foreach (MillRecord rc in records)
            {
                counter++;
                BsonDocument doc = new BsonDocument();
                doc.Add(new BsonElement("IDKey",
                    new BsonString(rc.IDKey)));
                doc.Add(new BsonElement("Caption",
                    new BsonString(rc.Caption)));
                doc.Add(new BsonElement("Second",
                    new BsonInt32(rc.Second)));
                doc.Add(new BsonElement("MillSec",
                    new BsonInt32(rc.MillSec)));
                doc.Add(new BsonElement("Unit",
                    new BsonString(rc.Unit)));
                doc.Add(new BsonElement("Value",
                    new BsonDouble(rc.Value)));

                docs.Add(doc);

                if (counter % 10000 == 0)
                {
                    bsonHelper.InsertBatch(docs);
                    docs.Clear();
                    System.Diagnostics.Debug.WriteLine("{1} inserted:{0}", counter, DateTime.Now);
                }
            }

            bsonHelper.InsertBatch(docs);
            System.Diagnostics.Debug.WriteLine("{1} inserted:{0}", counter, DateTime.Now);
            docs.Clear();
            bsonHelper.Disconnect();
        }

        public FlightBinaryDataDefinition Definition
        {
            get;
            set;
        }

        private void ReadHeader()
        {
            FlightBinaryDataHeaderDefinition definition = this.Definition.HeaderDefinition;

            byte[] bytes = m_Reader.ReadBytes(definition.BytesCount);

            FlightDataHeader header = FlightBinaryDataParser.Parse(bytes, definition);

            this.Header = header;
        }

        public bool EndOfStream
        {
            get
            {
                if (this.FileInfo == null
                    || this.FileInfo.Exists == false)
                    return true;

                if (this.m_stream != null)
                    return this.m_stream.Length >= this.m_stream.Position + 1;

                return false;
            }
        }

        public void Dispose()
        {
            if (this.m_Reader != null)
            {
                try
                {
                    this.m_Reader.Close();
                    this.m_Reader.Dispose();
                    this.m_stream.Close();
                    this.m_stream.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    this.m_Reader = null;
                }
            }
        }
    }
}
