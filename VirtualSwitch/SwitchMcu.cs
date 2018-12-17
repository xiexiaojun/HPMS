


using System.IO.Ports;
using System.Threading;

namespace VirtualSwitch
{
    public class SwitchMcu:ISwitch
    {
        private bool[,] _switchArrays;
        private string _visaAddress;
        private int _responseTime;
        private SerialPort _serialPort;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="switchArrays">开关矩阵</param>
        /// <param name="visaAddress">开关visa地址</param>
        /// <param name="responseTime">响应时间，单位ms</param>
        public SwitchMcu(bool[,] switchArrays,string visaAddress,int responseTime=500 )
        {
            if (responseTime == 0)
                responseTime = 500;
            this._switchArrays = switchArrays;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="visaAddress">开关visa地址</param>
        /// <param name="responseTime">响应时间，单位ms</param>
        public SwitchMcu(string visaAddress, int responseTime=500)
        {
            if (responseTime == 0)
                responseTime = 500;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
        }
        public SwitchMcu(SerialPort sp, int responseTime = 500)
        {
            if (responseTime == 0)
                responseTime = 500;
            this._serialPort = sp;
            this._responseTime = responseTime;
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
            errMsg = retErrMsg.Msg+retErrMsg.ErrorCode;
            if (retErrMsg.Result)
            {
                Thread.Sleep(_responseTime);
            }
            return retErrMsg.Result;
        }

        public bool OpenS(int switchIndex, ref string errMsg)
        {
            // CloseAll(ref errMsg);
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(this._switchArrays, switchIndex);
            SPConnect();
            _serialPort.Write(writeBytes, 0, writeBytes.Length);
            return true;
        }

        private void SPConnect()
        {
            if (_serialPort != null&&!_serialPort.IsOpen)
            {
                _serialPort.BaudRate = 115200;
                _serialPort.WriteTimeout = 1000;
                _serialPort.BaudRate = 115200;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.ReadBufferSize = 1024;
                _serialPort.WriteBufferSize = 4096;
                _serialPort.Parity = Parity.None;
                _serialPort.Open();
                
            }
        }

        public bool Open(byte[] switchNum, ref string errMsg)
        {
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(switchNum);
            ErrMsg retErrMsg = VisaSerial.WriteData(writeBytes, _visaAddress);
            errMsg = retErrMsg.Msg + retErrMsg.ErrorCode;
            if (retErrMsg.Result)
            {
                Thread.Sleep(_responseTime);
            }
            return retErrMsg.Result;
        }

        public bool OpenS(byte[] switchNum, ref string errMsg)
        {
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(switchNum);
            SPConnect();
            _serialPort.Write(writeBytes, 0, writeBytes.Length);
            return true;
        }
    }
}
