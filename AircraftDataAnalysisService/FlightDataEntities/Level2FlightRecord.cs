﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    public class Level2FlightRecord
    {
        /// <summary>
        /// 记录ID，用作关联、定位
        /// </summary>
        public ObjectId ID
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行参数ID，用作关联
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 起始秒数，从开始飞行的时间开始算
        /// </summary>
        public int StartSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 结束秒数，从开始飞行的时间算
        /// </summary>
        public int EndSecond
        {
            get;
            set;
        }

        /// <summary>
        /// 整个段中的平均值（精简前）
        /// </summary>
        public float AvgValue
        {
            get;
            set;
        }

        /// <summary>
        /// 整个段中的最小值（精简前）
        /// </summary>
        public float MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// 整个段中的最大值（精简前）
        /// </summary>
        public float MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 整个段中的记录点个数（精简前）
        /// 必须保留Count值，因为用于计算出SUM
        /// SUM = count * AvgValue
        /// </summary>
        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// 第一层飞行记录数据，经过一定处理，比如每秒钟只保留一个点
        /// </summary>
        public Level1FlightRecord[] Level1FlightRecords
        {
            get;
            set;
        }
    }
}
