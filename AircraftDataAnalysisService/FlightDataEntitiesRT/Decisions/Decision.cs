using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public class Decision : IDecision
    {
        private bool m_isActive = false;
        public bool IsActive
        {
            get { return m_isActive; }
            internal set
            {
                m_isActive = value;
            }
        }

        private int m_activeStartSecond = 0;

        public int ActiveStartSecond
        {
            get { return m_activeStartSecond; }
            internal set
            {
                m_activeStartSecond = value;
            }
        }

        private int m_activeEndSecond = 0;

        public int ActiveEndSecond
        {
            get { return m_activeEndSecond; }
            internal set
            {
                m_activeEndSecond = value;
            }
        }

        private bool m_hasHappend = false;

        public bool HasHappened
        {
            get { return m_hasHappend; }
            internal set { m_hasHappend = value; }
        }

        private int m_happenedSecond = 0;

        public int HappenedSecond
        {
            get { return m_happenedSecond; }
            internal set { m_happenedSecond = value; }
        }

        public void AddOneSecondDatas(int second, ParameterRawData[] rawDatas)
        {
            foreach (var con in this.Conditions)
                con.AddOneSecondDatas(second, rawDatas);

            if (AllConditionTrue() && this.LastTime > 0
                && (second - this.ActiveStartSecond >= this.LastTime))
            {//所有条件都发生，并且大于等于持续时间，则认为真正发生了
                this.HappenedSecond = second;
                HasHappened = true;
            }
            else if (this.AllConditionTrue())//所有条件都满足但是持续时间还不够长
            {//先设置为Active
                if (this.IsActive == false)
                {
                    this.ActiveStartSecond = second;
                    this.IsActive = true;
                }
            }
            else
            {//没有Active了
                if (this.IsActive)
                {
                    this.IsActive = false;
                    this.ActiveEndSecond = second;
                }
            }

            //如果有子条件则自身不算
            return;
        }

        private bool AllConditionTrue()
        {
            if (this.Conditions != null && this.Conditions.Length > 0)
            {
                foreach (var con in this.Conditions)
                {
                    if (con.ConditionTrue == false)
                        return false;
                }
            }
            return true;
        }

        public SubCondition[] Conditions
        {
            get;
            set;
        }

        public int LastTime
        {
            get;
            set;
        }
    }
}
