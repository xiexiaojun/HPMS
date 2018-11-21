using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonSwitchTool.Switch
{
    public interface ISwitch
    {
        bool CloseAll(ref string errMsg);
        bool Open(int switchIndex, ref string errMsg);
        bool Open(byte[] switchNum, ref string errMsg);
    }
    enum IndexType
    {
        Row,
        Column
    }
}
