using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class Level1FlightRecord
    {
        public string ParameterID
        {
            get;
            set;
        }

        public int StartSecond
        {
            get;
            set;
        }

        public int EndSecond
        {
            get;
            set;
        }

        public float[] Values
        {
            get;
            set;
        }
    }
}
