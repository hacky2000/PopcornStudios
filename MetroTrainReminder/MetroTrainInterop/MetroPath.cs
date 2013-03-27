using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.MetroTrainInterop
{
    public class MetroPath : ModelBase
    {
        public MetroPath()
            : base()
        {
            //构造查找路线的策略
            this.m_pathCalcStrategy = new ThroughPathCalcStrategy(this);
        }

        private string m_cityName = string.Empty;

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get { return m_cityName; }
            set
            {
                m_cityName = value;
                this.OnPropertyChanged("CityName");
            }
        }

        private MetroStation m_startStation = null;

        private MetroStation m_endStation = null;

        public MetroStation EndStation
        {
            get
            {
                return m_endStation;
            }
            set
            {
                m_endStation = value;
                this.OnPropertyChanged();
            }
        }

        public MetroStation StartStation
        {
            get { return m_startStation; }
            set
            {
                m_startStation = value;
                this.OnPropertyChanged();
            }
        }

        public string StartStationName
        {
            get
            {
                return m_startStation == null ? string.Empty : m_startStation.StationName;
            }
        }

        public string EndStationName
        {
            get
            {
                return this.m_endStation == null ? string.Empty : m_endStation.StationName;
            }
        }

        /// <summary>
        /// 需要重新计算路径
        /// </summary>
        private void OnPropertyChanged()
        {
            this.EvaluateThroughPaths();

            base.OnPropertyChanged(string.Empty);
        }

        private IThroughPathCalcStrategy m_pathCalcStrategy = null;

        private void EvaluateThroughPaths()
        {
            if (this.m_pathCalcStrategy != null)
            {
                this.m_throughPaths = this.m_pathCalcStrategy.CalcThroughPaths();
                this.SelectedPathIndex = 0;
            }
            else
            {
                this.SelectedPathIndex = -1;
            }
        }

        public double SelectedPrice
        {
            get
            {
                if (this.m_selectedPathIndex >= 0
                    && this.ThroughPaths != null && this.ThroughPaths.Count() > m_selectedPathIndex)
                {
                    ThroughPath path = this.ThroughPaths[this.SelectedPathIndex];
                    return path.Price;
                }

                return 0;
            }
        }

        private int m_selectedPathIndex = -1;

        public int SelectedPathIndex
        {
            get { return m_selectedPathIndex; }
            set
            {
                m_selectedPathIndex = value;
                base.OnPropertyChanged("SelectedPathIndex");
                this.OnPropertyChanged("SelectedPrice");
            }
        }

        private List<ThroughPath> m_throughPaths = new List<ThroughPath>();

        public List<ThroughPath> ThroughPaths
        {
            get
            {
                return m_throughPaths;
            }
        }
    }
}
