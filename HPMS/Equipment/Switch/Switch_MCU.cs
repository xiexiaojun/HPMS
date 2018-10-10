using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPMS.Equipment.Visa;

namespace HPMS.Equipment.Switch
{
    class Switch_MCU:ISwitch
    {
        private bool[,] _switchArrays;
        private string _visaAddress;
        public Switch_MCU(bool[,] switchArrays,string visaAddress)
        {
            this._switchArrays = switchArrays;
            this._visaAddress = visaAddress;
        }
        public bool CloseAll(ref string errMsg)
        {
            byte[] closeAllBytes =
            {
                0xEE,
                0x9,
                0x2,
                0x80,
                0x8B,
                0xFF,
                0xFC,
                0xFF,
                0xFF
            };
            ErrMsg retErrMsg = VisaSerial.WriteData(closeAllBytes, _visaAddress);
            errMsg = retErrMsg.Msg;
            return retErrMsg.Result;
        }

        public bool Open(int switchIndex, ref string errMsg)
        {
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(this._switchArrays, switchIndex);
            ErrMsg retErrMsg = VisaSerial.WriteData(writeBytes, _visaAddress);
            errMsg = retErrMsg.Msg;
            return retErrMsg.Result;
        }
    }
}
