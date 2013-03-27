using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop.MapTableCalc
{
    public class ThroughLine
    {
        public string Key
        {
            get;
            set;
        }

        public ThroughPath[] ThroughPaths
        {
            get;
            set;
        }
    }
}
