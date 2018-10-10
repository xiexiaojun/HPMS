﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HPMS.AOP;

namespace HPMS.Test
{
   
        [PermissonCheck(JoinType.Permission)]
        
        public class CalculatorHandler : ContextBoundObject
        {
            //具备标签的方法才能被拦截
            [Permisson("Add")]
           
            public double Add(double x, double y)
            {
                // Console.WriteLine("{0} + {1} = {2}", x, y, x + y);
                return x + y;
            }

        }
    
}
