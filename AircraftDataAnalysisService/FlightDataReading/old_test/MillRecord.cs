using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightDataReading.AircraftModel1
{
    class MillRecord
    {
        public int MillSec { get; set; }

        public int Second { get; set; }

        public string IDKey { get; set; }

        public string Caption { get; set; }

        public string Unit { get; set; }

        public float Value { get; set; }
    }
}
