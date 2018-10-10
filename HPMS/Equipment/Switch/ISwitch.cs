using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPMS.Equipment.Switch
{
    interface ISwitch
    {
        bool CloseAll(ref string errMsg);
        bool Open(int switchIndex, ref string errMsg);
    }
}
