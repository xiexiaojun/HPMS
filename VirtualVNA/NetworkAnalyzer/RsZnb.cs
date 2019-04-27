using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NationalInstruments.VisaNS;
using VirtualSwitch;

namespace VirtualVNA.NetworkAnalyzer
{
    /// <summary>
    /// 罗德施瓦茨型号网分控制类
    /// </summary>
    public class RsZnb : NetworkAnalyzer
    {
        private ISwitch _iswitch;
        private MessageBasedSession mbSession;
        private bool _connected = false;
        private string _visaAddress;
        private int _responseTime;
        public RsZnb(ISwitch iSwitch, string visaAddress, int responseTime = 2000)
        {
            if (responseTime == 0)
                responseTime = 2000;
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
            Connect();
        }

        /// <summary>
        /// 析构函数，释放visa资源
        /// </summary>
        ~RsZnb()
        {
            if (mbSession != null)
            {
                mbSession.Dispose();
            }
        }

        /// <summary>
        /// 保存s4p
        /// </summary>
        /// <param name="saveFilePath">s4p文件路径</param>
        /// <param name="switchIndex">开关矩阵索引</param>
        /// <param name="mutiChannel">是否使用多个channel</param>
        /// <param name="nextByTrace">是否串音</param>
        /// <param name="msg">异常信息</param>
        /// <returns>true/false</returns>
        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = mutiChannel ? (switchIndex + 1).ToString() : "1";
            
            if (!Trigger(channel, ref msg))
            {
                return false;
            };

            Thread.Sleep(_responseTime);
            return SaveS4P(saveFilePath, channel, ref msg);
        }

        /// <summary>
        /// 保存s4p
        /// </summary>
        /// <param name="saveFilePath">s4p文件路径</param>
        /// <param name="switchIndex">开关矩阵字节数组</param>
        /// <param name="index">开关矩阵索引</param>
        /// <param name="mutiChannel">是否使用多个channel</param>
        /// <param name="nextByTrace">是否串音</param>
        /// <param name="msg">异常信息</param>
        /// <returns>true/false</returns>
        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = mutiChannel ? (index + 1).ToString() : "1";

            if (!Trigger(channel, ref msg))
            {
                return false;
            };

            Thread.Sleep(_responseTime);
            return SaveS4P(saveFilePath, channel, ref msg);
        }

        /// <summary>
        /// 直接获取测试数据，不保存s4p文件
        /// </summary>
        /// <param name="fre">返回的频率</param>
        /// <param name="db">返回的db值</param>
        /// <param name="switchIndex">开关矩阵索引</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        [Obsolete]
        public override bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 载入校准档案文件
        /// </summary>
        /// <param name="calFilePath">档案文件路径,相对网分设备</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool LoadCalFile(string calFilePath, ref string msg)
        {
            Connect();
            string delAllCalFile = "MEM:DEL:ALL";
            string loadCalFile = String.Format("MMEM:LOAD:STAT 1,'{0}'", ReplaceSlash(calFilePath));
            mbSession.Write(delAllCalFile);
            Thread.Sleep(2000);
            mbSession.Write(loadCalFile);
            return true;
        }

        private void Connect()
        {
            try
            {
                if (!_connected)
                {
                    mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_visaAddress);
                    _connected = true;
                }

            }
            catch (Exception e)
            {

                throw new VnaException("Connect RSZNB fail via Visa Command", e);
            }

        }

        private bool Trigger(string channel, ref string msg)
        {
            //set singleMode
            string singleMode = String.Format("INIT{0}", channel); 
            
            try
            {
                mbSession.Write(singleMode);
                //TODO need to check the status after single command send
                Thread.Sleep(1000);
                return true;

            }
            catch (Exception e)
            {
                msg = "ZNB trigger fail";
                throw new VnaException(msg, e);
             }

        }
        private bool SaveS4P(string saveFilePath, string channel, ref string msg)
        {
         
            //example::MMEMory:STORe:TRACe:PORTs  1, 'c:\\12345\\333.s4p', COMPlex, 1, 2, 3, 4
            string strSaveCommand = String.Format(":MMEMory:STORe:TRACe:PORTs  {0}, '{1}', COMPlex, 1, 2, 3, 4", channel, ReplaceSlash(saveFilePath));

            try
            {
              
                mbSession.Write(strSaveCommand);
                return true;
            }
            catch (Exception e)
            {
                msg = "save s4p file fail";
                throw new VnaException(msg, e);
            }

        }

    }
}
