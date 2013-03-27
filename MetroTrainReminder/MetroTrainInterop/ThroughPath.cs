using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop
{
    public class ThroughPath
    {
        public ThroughPath()
        {
        }

        public double Price
        {
            get
            {
                return m_price;
            }
            set
            {
                m_price = value;
            }
        }

        private double m_price = 0;

        private ThroughPathNode[] m_throughNodes = new ThroughPathNode[] { };

        public ThroughPathNode[] ThroughNodes
        {
            get { return m_throughNodes; }
            set { m_throughNodes = value; }
        }
    }
}
