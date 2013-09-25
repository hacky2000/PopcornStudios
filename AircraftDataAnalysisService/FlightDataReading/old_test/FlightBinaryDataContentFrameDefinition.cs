using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.old_test
{
    public class FlightBinaryDataContentFrameDefinition
    {
        public int BytesCount
        {
            get;
            set;
        }

        public FlightBinaryDataContentSegmentDefinition[] Segments
        {
            get;
            set;
        }
    }
}
