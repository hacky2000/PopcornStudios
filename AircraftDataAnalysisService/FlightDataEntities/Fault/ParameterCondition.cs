﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightDataEntities.Fault
{
    public class ParameterCondition
    {
        public FlyParameter Parameter { get; set; }
        public ICondition Condition { get; set; }
    }
}
