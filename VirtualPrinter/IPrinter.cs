using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualPrinter
{
    public interface IPrinter
    {
        /// <summary>
        /// 实现打印功能
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        bool Print(string content);
    }
}
