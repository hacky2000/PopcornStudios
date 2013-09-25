using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.old_test
{
    public class FlightBinaryDataContentSegmentDefinition
    {
        public int BytesCount
        {
            get;
            set;
        }

        public string SegmentName
        {
            get;
            set;
        }

        public string DataTypeStr
        {
            get;
            set;
        }
    }
}
