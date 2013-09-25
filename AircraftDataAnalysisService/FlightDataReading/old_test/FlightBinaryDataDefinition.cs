using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.old_test
{
    public class FlightBinaryDataDefinition
    {
        public string AircraftModel
        {
            get;
            set;
        }

        public FlightBinaryDataHeaderDefinition HeaderDefinition
        {
            get;
            set;
        }

        public FlightBinaryDataContentFrameDefinition FrameDefinition
        {
            get;
            set;
        }
    }
}
