using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightDataEntities.Fault
{
    public interface ICondition
    {
        bool Check(float value);
    }
}
