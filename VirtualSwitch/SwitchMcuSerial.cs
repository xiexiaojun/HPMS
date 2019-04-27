using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualSwitch
{
    /// <summary>
    /// MCU串口方式控制
    /// </summary>
    class SwitchMcuSerial:ISwitch
    {
        private bool[,] _switchArrays;
        private int _responseTime;
        private SerialPort _serialPort;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sp">串口对象</param>
        /// <param name="responseTime">响应时间，单位ms</param>
        public SwitchMcuSerial(SerialPort sp, int responseTime = 500)
        {
            if (responseTime == 0)
                responseTime = 500;
            this._serialPort = sp;
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
            ErrMsg ret = WriteData(closeAllBytes);
            errMsg = ret.Msg;
            return ret.Result;
            
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
            SpConnect();
            ErrMsg ret = WriteData(writeBytes);
            errMsg = ret.Msg;
            return ret.Result;
        }

        private void SpConnect()
        {
            if (_serialPort != null && !_serialPort.IsOpen)
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

        /// <summary>
        /// 开启由对应开关组的串口指令指定的开关
        /// </summary>
        /// <param name="switchNum">要写入的byte[]数组</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        public bool Open(byte[] switchNum, ref string errMsg)
        {
            byte[] writeBytes = SwitchUtil.GetMcuFormatBytes(switchNum);
            SpConnect();
            ErrMsg ret = WriteData(writeBytes);
            errMsg = ret.Msg;
            return ret.Result;
        }

        private ErrMsg WriteData(byte[]buffer)
        {
            ErrMsg ret=new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            try
            {
                _serialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                ret.ErrorCode = e.HResult;
                ret.Msg = e.Message;
                ret.Result = false;
            }

            return ret;
        }
    }
}
