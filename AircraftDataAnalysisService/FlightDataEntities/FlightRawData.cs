using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 飞行参数源数据，未经处理
    /// </summary>
    public class FlightRawData
    {
        public ObjectId Id
        {
            get;
            set;
        }

        public float[] Values
        {
            get;
            set;
        }

        /// <summary>
        /// 当前飞行秒（第几秒）
        /// </summary>
        public int Second
        {
            get;
            set;
        }

        public string ParameterID
        {
            get;
            set;
        }

        //public Level1FlightRecord ToLevel1FlightRecord()
        //{
        //    return FlightDataEntityTransform.FromFlightRawDataToLevel1FlightRecord(this);
        //}
    }
}
