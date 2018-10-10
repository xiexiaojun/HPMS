using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.VisaNS;

namespace HPMS.Equipment.Visa
{
    public struct ErrMsg
    {
        public bool Result;
        public int ErrorCode;
        public string Msg;

    }
    public class VisaSerial
    {
        public static ErrMsg WriteData(byte[]writeBytes,string visaAddress)
        {
            ErrMsg ret = new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            SerialSession serialSession = (SerialSession)ResourceManager.GetLocalManager().Open(visaAddress, AccessModes.NoLock, 0);
            ret = SetSerial(serialSession, ret);
            ret=FlushIO(serialSession,ret);
            ret=SetIOSize(serialSession,ret);
            ret = WriteIO(serialSession, writeBytes,ret);
            serialSession.Dispose();
            return ret;
        }

        /// <summary>
        /// 设置串口visa参数
        /// </summary>
        /// <param name="serialSession"></param>
        /// <param name="inErrMsg"></param>
        /// <returns></returns>
        private static ErrMsg SetSerial(SerialSession serialSession,ErrMsg inErrMsg)
        {
            if (!inErrMsg.Result)
            {
                return inErrMsg;
            }


            ErrMsg ret=new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            try
            {
                
                serialSession.Timeout = 1000;
                serialSession.BaudRate = 115200;
                serialSession.DataBits = 8;
                serialSession.StopBits = StopBitType.One;
                serialSession.FlowControl = FlowControlTypes.None;
                serialSession.Parity = Parity.None;
                serialSession.TerminationCharacter = 0xA;
                serialSession.TerminationCharacterEnabled = true;
               
            }
            catch (Exception e)
            {
                ret.ErrorCode = 010001;
                ret.Result = false;
                ret.Msg = e.Message;
            }

            return ret;
        }

        /// <summary>
        /// 清空IO缓冲区
        /// </summary>
        /// <param name="serialSession"></param>
        /// <param name="inErrMsg"></param>
        /// <returns></returns>
        private static ErrMsg FlushIO(SerialSession serialSession, ErrMsg inErrMsg)
        {
            if (!inErrMsg.Result)
            {
                return inErrMsg;
            }

            ErrMsg ret = new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            try
            {
                serialSession.Flush(BufferTypes.InBuffer,true);
             
            }
            catch (Exception e)
            {
                ret.ErrorCode = 010001;
                ret.Result = false;
                ret.Msg = e.Message;
            }

            return ret;
        }

        /// <summary>
        /// 设置IO缓冲区
        /// </summary>
        /// <param name="serialSession"></param>
        /// <param name="inErrMsg"></param>
        /// <returns></returns>
        private static ErrMsg SetIOSize(SerialSession serialSession, ErrMsg inErrMsg)
        {
            if (!inErrMsg.Result)
            {
                return inErrMsg;
            }
            ErrMsg ret = new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            try
            {
                serialSession.SetBufferSize(BufferTypes.InBuffer,4096);

            }
            catch (Exception e)
            {
                ret.ErrorCode = 010001;
                ret.Result = false;
                ret.Msg = e.Message;
            }

            return ret;
        }

        /// <summary>
        /// 写入串口数据
        /// </summary>
        /// <param name="serialSession"></param>
        /// <param name="writeBytes"></param>
        /// <param name="inErrMsg"></param>
        /// <returns></returns>
        private static ErrMsg WriteIO(SerialSession serialSession, byte[] writeBytes, ErrMsg inErrMsg)
        {
            if (!inErrMsg.Result)
            {
                return inErrMsg;
            }

            ErrMsg ret = new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
            try
            {
                serialSession.Write(writeBytes);

            }
            catch (Exception e)
            {
                ret.ErrorCode = 010001;
                ret.Result = false;
                ret.Msg = e.Message;
            }

            return ret;
           
        }

    }
}
