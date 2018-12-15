using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSwitch
{
    public class SwitchFake:ISwitch
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
