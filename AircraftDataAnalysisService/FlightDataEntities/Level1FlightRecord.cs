using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    public class Level1FlightRecord
    {
        public ObjectId Id
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行秒数（当前是第几秒）
        /// </summary>
        public int FlightSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 当前秒内的采集参数
        /// </summary>
        public float[] Values
        {
            get;
            set;
        }

        public decimal Sum
        {
            get;
            set;
        }

        public float MinValue
        {
            get;
            set;
        }

        public float MaxValue
        {
            get;
            set;
        }

        public float AvgValue
        {
            get;
            set;
        }

        public int ValueCount
        {
            get;
            set;
        }

        //public FlightRawData ToFlightRecordEntity()
        //{
        //    return FlightDataEntityTransform.FromLevel1FlightRecordToFlightRawData(this);
        //}
    }
}
