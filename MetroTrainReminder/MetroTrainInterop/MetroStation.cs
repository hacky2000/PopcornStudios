using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop
{
    public class MetroStation : ModelBase
    {
        private string m_stationName = string.Empty;

        public string StationName
        {
            get { return m_stationName; }
            set
            {
                m_stationName = value;
                this.OnPropertyChanged("StationName");
            }
        }

    }
}
