using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    public class FlightParameter
    {
        /// <summary>
        /// 飞行参数ID
        /// </summary>
        public string ParameterID
        {
            get;
            set;
        }

        /// <summary>
        /// 标题（中文展示名）
        /// </summary>
        public string Caption
        {
            get;
            set;
        }

        #region properties
        #endregion
    }
}
