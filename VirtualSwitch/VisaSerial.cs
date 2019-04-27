using System;
using NationalInstruments.VisaNS;

namespace VirtualSwitch
{
    /// <summary>
    /// 串口驱动异常信息
    /// </summary>
    public struct ErrMsg
    {
        /// <summary>
        /// 串口操作是否成功
        /// </summary>
        public bool Result;
        /// <summary>
        /// 对应的错误码
        /// </summary>
        public int ErrorCode;
        /// <summary>
        /// 对应的异常信息
        /// </summary>
        public string Msg;

    }
    /// <summary>
    /// 串口对应的visa方式控制类
    /// </summary>
    public class VisaSerial
    {
        private static SerialSession serialSession = null;

        /// <summary>
        /// 析构函数，释放visa资源
        /// </summary>
        ~VisaSerial()
        {
            serialSession.Dispose();
        }

        /// <summary>
        /// 写入串口数据
        /// </summary>
        /// <param name="writeBytes"></param>
        /// <param name="visaAddress"></param>
        /// <returns></returns>
        public static ErrMsg WriteData(byte[]writeBytes,string visaAddress)
        {
            ErrMsg ret = new ErrMsg();
            ret.ErrorCode = -1;
            ret.Result = true;
            ret.Msg = "";
          
                try
                {
                    if (serialSession == null || serialSession.ResourceName != visaAddress)
                    {
                        serialSession = (SerialSession)ResourceManager.GetLocalManager().Open(visaAddress, AccessModes.NoLock, 0);

                    }
                    ret = SetSerial(serialSession, ret);
                    ret = FlushIO(serialSession, ret);
                    ret = SetIOSize(serialSession, ret);
                    ret = WriteIO(serialSession, writeBytes, ret);
                }
                catch (Exception e)
                {
                    ret.ErrorCode = e.HResult;
                    ret.Msg = e.Message;
                    ret.Result = false;
                }
                
           
            
          
            //serialSession.Dispose();
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
                ret.ErrorCode = 010010;
                ret.Result = false;
                ret.Msg = e.Message+e.Source+e.StackTrace;
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
                ret.ErrorCode = 010002;
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
                ret.ErrorCode = 010003;
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
                ret.ErrorCode = 010004;
                ret.Result = false;
                ret.Msg = e.Message;
            }

            return ret;
           
        }

    }
}
