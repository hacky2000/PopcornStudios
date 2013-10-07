using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT.Decisions
{
    public class SubCondition
    {
        public bool ConditionTrue
        {
            get
            {
                if (SubConditions != null && SubConditions.Length > 0)
                {
                    if (Relation == ConditionRelation.OR)
                    {
                        foreach (var con in SubConditions)
                        {//OR关系一个成功则全部成功
                            if (con.ConditionTrue)
                                return true;
                        }
                    }
                    else
                    {
                        foreach (var con in SubConditions)
                        {//AND关系一个失败则全部失败
                            if (con.ConditionTrue == false)
                                return false;
                        }                        
                    }
                }

                return m_selfCondition;
            }
        }

        private bool m_selfCondition = false;

        public bool SelfCondition
        {
            get { return m_selfCondition; }
            internal set { m_selfCondition = value; }
        }

        public void AddOneSecondDatas(int second, ParameterRawData[] rawDatas)
        {
            if (this.SubConditions != null && this.SubConditions.Length > 0)
            {
                foreach (var con in this.SubConditions)
                    con.AddOneSecondDatas(second, rawDatas);

                //如果有子条件则自身不算
                return;
            }
            else
            {
                if (this.IsConditionMatch(second, rawDatas))
                {
                    this.SelfCondition = true;
                }
                else
                {
                    this.SelfCondition = false;
                }
            }
        }

        private bool IsConditionMatch(int second, ParameterRawData[] rawDatas)
        {
            throw new NotImplementedException();
        }

        public ConditionRelation Relation
        {
            get;
            set;
        }

        public SubCondition[] SubConditions
        {
            get;
            set;
        }
    }
}
