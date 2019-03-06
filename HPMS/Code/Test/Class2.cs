using System;
using HPMS.Code.AOP;

namespace HPMS.Code.Test
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
