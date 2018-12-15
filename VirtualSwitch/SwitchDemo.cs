using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSwitch
{
    public class SwitchDemo:ISwitch
    {
        public SwitchDemo()
        {

        }
        public bool CloseAll(ref string errMsg)
        {
            errMsg = "";
            return true;
        }

        public bool Open(int switchIndex, ref string errMsg)
        {
            errMsg = "";
            return true;
        }

        public bool Open(byte[] switchNum, ref string errMsg)
        {
            errMsg = "";
            return true;
        }

        public bool OpenS(int switchIndex, ref string errMsg)
        {
            throw new NotImplementedException();
        }

        public bool OpenS(byte[] switchNum, ref string errMsg)
        {
            throw new NotImplementedException();
        }
    }
}
