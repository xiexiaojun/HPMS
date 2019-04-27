


using System.IO.Ports;
using System.Threading;

namespace VirtualSwitch
{
    /// <summary>
    /// MCU Visa方式控制
    /// </summary>
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

        /// <summary>
        /// 关闭所有通道
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
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

        /// <summary>
        /// 开启开关矩阵配置中的指定行或列的开关,由构造函数来决定打开一行或一列
        /// </summary>
        /// <param name="switchIndex">行/列索引</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 开启由对应开关组的串口指令指定的开关
        /// </summary>
        /// <param name="switchNum">要写入的byte[]数组</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
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

      
    }
}
