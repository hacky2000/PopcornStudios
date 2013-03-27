using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopcornStudio.ScheduleInterop
{
    /// <summary>
    /// 日程表顶级对象
    /// </summary>
    public class Schedule : ModelBase
    {
        private DateTime m_startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return m_startDate; }
            set
            {
                m_startDate = value;
                this.OnPropertyChanged("StartDate");
            }
        }

        private string m_ScheduleTableName = string.Empty;

        public string ScheduleTableName
        {
            get { return m_ScheduleTableName; }
            set
            {
                m_ScheduleTableName = value;
                this.OnPropertyChanged("ScheduleTableName");
            }
        }

        public ScheduleWeek CurrentWeek
        {
            get
            {
                return null;
            }
        }
    }
}
