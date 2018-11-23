


namespace VirtualSwitch
{
    public class SwitchMcu:ISwitch
    {
        private bool[,] _switchArrays;
        private string _visaAddress;
        public SwitchMcu(bool[,] switchArrays,string visaAddress)
        {
            this._switchArrays = switchArrays;
            this._visaAddress = visaAddress;
        }
        public SwitchMcu( string visaAddress)
        {
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
           // CloseAll(ref errMsg);
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(this._switchArrays, switchIndex);
            ErrMsg retErrMsg = VisaSerial.WriteData(writeBytes, _visaAddress);
            errMsg = retErrMsg.Msg;
            return retErrMsg.Result;
        }

        public bool Open(byte[] switchNum, ref string errMsg)
        {
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(switchNum);
            ErrMsg retErrMsg = VisaSerial.WriteData(writeBytes, _visaAddress);
            errMsg = retErrMsg.Msg;
            return retErrMsg.Result;
        }
    }
}
