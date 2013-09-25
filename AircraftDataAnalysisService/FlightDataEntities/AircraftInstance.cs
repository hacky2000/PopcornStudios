using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntities
{
    /// <summary>
    /// 机号。某个机型的某一架飞机
    /// </summary>
    public class AircraftInstance
    {
        public AircraftModel AircraftModel
        {
            get;
            set;
        }

        public string AircraftNumber
        {
            get;
            set;
        }
    }
}
