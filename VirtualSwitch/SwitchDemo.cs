using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSwitchTool.Switch;

namespace VirtualSwitch
{
    public class SwitchDemo:ISwitch
    {
        public bool CloseAll(ref string errMsg)
        {
            return true;
        }

        public bool Open(int switchIndex, ref string errMsg)
        {
            return true;
        }

        public bool Open(byte[] switchNum, ref string errMsg)
        {
            return true;
        }
    }
}
