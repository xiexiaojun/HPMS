using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NationalInstruments.VisaNS;
using VirtualSwitch;

namespace VirtualVNA.NetworkAnalyzer
{
    /// <summary>
    /// E5071C型号网分控制类
    /// </summary>
    public class E5071C:NetworkAnalyzer
    {
        private ISwitch _iswitch;
        private MessageBasedSession _mbSession;
        private bool _connected = false;
        private string _visaAddress;
        private int _responseTime;
      
        /// <summary>
        /// E5071C网分构造函数
        /// </summary>
        /// <param name="iSwitch">开关对象</param>
        /// <param name="visaAddress">网分visa地址</param>
        /// <param name="responseTime">响应时间</param>
        public E5071C(ISwitch iSwitch,string visaAddress,int responseTime)
        {
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
          
            Connect();

        }
        /// <summary>
        /// 析构函数,释放visa资源
        /// </summary>
        ~E5071C()
        {
            if (_mbSession != null)
            {
                _mbSession.Dispose();
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
            ret=_iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = mutiChannel ? (switchIndex + 1).ToString() : "1";
            List<string> namList = GetNameList(channel, nextByTrace);
            if (namList.Count == 0)
            {
                msg = "get name list from E5071C fail";
                return false;
            }

            if (!SelectTrace(channel, namList,true, ref msg))
            {
                return false;
            };
            if (!Trigger(channel, ref msg))
            {
                return false;
            };
            Thread.Sleep(_responseTime);
            return SaveS4P(saveFilePath, ref msg);
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
            List<string> namList = GetNameList(channel, nextByTrace);
            if (namList.Count == 0)
            {
                msg = "get name list from E5071C fail";
                return false;
            }

            if (!SelectTrace(channel, namList, true, ref msg))
            {
                return false;
            };
            if (!Trigger(channel, ref msg))
            {
                return false;
            };
            Thread.Sleep(_responseTime);
            return SaveS4P(saveFilePath, ref msg);
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
        public override bool GetTestData(ref double[]fre,double[]db, int switchIndex, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = (switchIndex + 1).ToString();
            List<string> namList = GetNameList(channel,true);
            if (namList.Count == 0)
            {
                msg = "get name list from E5071C fail";
                return false;
            }

            if (!SelectTrace(channel, namList, false, ref msg))
            {
                return false;
            };
            if (!Trigger(channel, ref msg))
            {
                return false;
            };
            Thread.Sleep(_responseTime);

            int points = 0;
            if (!GetMeasurePoints(ref points, ref msg))
            {
                return false;
            }

            if (!GetFre(ref fre, ref msg))
            {
                return false;
            }
            if (!GetDb(channel,points,ref fre, ref msg))
            {
                return false;
            }

            return true;
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
            string loadCalFile = "MMEM:load '" + calFilePath + "'";
            _mbSession.Write(loadCalFile);
            Thread.Sleep(5000);
            string typeLin = ":SENS1:SWE:TYPE LINear";
            _mbSession.Write(typeLin);
            return true;
        }

        private void Connect()
        {
            try
            {
                if (!_connected)
                {
                    _mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_visaAddress);
                    _connected = true;
                }
              
            }
            catch (Exception e)
            {
                throw new VnaException("Connect E5071C fail via Visa Command",e);
               // throw;
            }
           
        }

        /// <summary>
        /// get all set test parameters 
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private List<string> GetNameList(string channel, bool nextByTrace)
        {
            //refer to SCPI.CALCulate(Ch).PARameter.COUNt
            List<string> retList=new List<string>();
            string strGetChannelTraces = ":CALC"+channel+":PAR:COUN?";
            _mbSession.Write(strGetChannelTraces);
            int traceCount=int.Parse(_mbSession.ReadString(100).Trim());
            try
            {
                for (int i = 0; i < traceCount; i++)
                {
                    string strGetChannelTraceSelected = nextByTrace ? ":CALC" + channel + ":FSIM:BAL:PAR" + (i + 1) + ":BBAL?" :
                        ":CALC" + channel + ":PAR" + (i + 1) + ":DEF?";
                    _mbSession.Write(strGetChannelTraceSelected);
                    retList.Add(_mbSession.ReadString(100).Trim());

                }
            }
            catch (Exception e)
            {
                throw new VnaException("get name list from E5071C fail", e);
            }
            return retList;
        }

        /// <summary>
        /// select single trace
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="nameList"></param>
        /// <param name="isSingle"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SelectTrace(string channel,List<string>nameList,bool isSingle,ref string msg)
        {
            int singleTraceNum = 0;
            bool ret = false;
            foreach (string trace in nameList)
            {
                if (FindTargetParameter(isSingle,trace))
                {
                    ret = true;
                    break;
                }

                singleTraceNum++;
            }

            if (!ret)
            {
                msg = isSingle?"Not Find Any Single Parameter, please open a \"S21\" or \"S11\" parameter, and Try Again! ":
                    "Not Find \"Sdd21\"Parameter, please open a \"Sdd21\" parameter, and Try Again! ";
                return false;
            }
            string selectedTrace = ":CALC" + channel + ":PAR" + (singleTraceNum + 1) + ":SEL";
            try
            {
                _mbSession.Write(selectedTrace);
                return true;
            }
            catch (Exception e)
            {
               throw new VnaException("Set trace fail",e);
            }
           
        }

        /// <summary>
        /// set E5071C trigger mode
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool Trigger(string channel,ref string msg)
        {
            //refer to SCPI.INITiate(Ch).CONTinuous
            //this scip command used for set E5071C to hold mode
            string strSetHoldMode = ":INIT" + channel + ":COUT OFF";
            //this scip command used for get E5071C work mode
            string strGetMode = ":INIT" + channel + ":COUT?";

            //refer to SCPI.DISPlay.WINDow(Ch).ACTivate
            //This command specifies selected channel (Ch) as the active channel
            string strSetActiveChannel = ":DISP:WIND" + channel + ":ACT";

            //refer to SCPI.INITiate(Ch).IMMediate
            //This command changes the state of each channel of selected channel (Ch) to the initiation state in the trigger system.
            string strSetChannelStateIni = "INIT" + channel + ":IMM";

            try
            {
                _mbSession.Write(strSetHoldMode);
                _mbSession.Write(strSetActiveChannel);
                Thread.Sleep(1000);
                _mbSession.Write(strSetChannelStateIni);
                Thread.Sleep(500);

                for (int i = 0; i < 10; i++)
                {
                    _mbSession.Write(strGetMode);
                    string readTemp = _mbSession.ReadString().Trim();
                    if (readTemp.Equals("0"))
                    {
                        break;
                    }

                    Thread.Sleep(500);
                }

                return true;

            }
            catch (Exception e)
            {
                msg = "E5071C trigger fail";
                throw new VnaException(msg,e);
            }
           
        }

        /// <summary>
        /// save s4p file to SpecifiedPath
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SaveS4P(string saveFilePath,ref string msg)
        {
            //refer to :MMEMory:STORe:SNP
            //this command used for set save data format
            string strSetDataFormat = ":MMEM:STOR:SNP:FORM RI";

            string strSaveCommand = ":MMEM:STOR:SNP:TYPE:S4P 1,2,3,4;:MMEM:STOR:SNP \"" + saveFilePath + "\"";
            try
            {
                _mbSession.Write(strSetDataFormat);
                _mbSession.Write(strSaveCommand);
                return true;
            }
            catch (Exception e)
            {
                msg = "save s4p file fail";
                throw new VnaException(msg,e);
            }

        }

        private bool FindTargetParameter(bool isSingle, string testParameter)
        {
            if (isSingle)
            {
                return testParameter.Length == 3;
            }
            else
            {
                return testParameter.Equals("SDD21",StringComparison.OrdinalIgnoreCase);
            }
        }
        private bool GetMeasurePoints(ref int points,ref string msg)
        {
            //refer to SCPI.SENSe(Ch).SWEep.POINts
            //This command sets/gets the number of measurement points of selected channel (Ch).
            string strGetMeasurePoints = ":SENS1:SWE:POIN?";
            try
            {
                _mbSession.Write(strGetMeasurePoints);
                points = int.Parse(_mbSession.ReadString(100).Trim());
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C measurement points fail";
                throw new VnaException(msg,e);
            }
          
        }

        private bool GetFre(ref double[]fre,ref string msg)
        {
            string strGetFre = ":SENS1:FREQ:DATA?";
            try
            {
                _mbSession.Write(strGetFre);
                string temp=_mbSession.ReadString(800000);
                fre = StrData2DoubleArray(temp);
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C fre fail";
                throw new VnaException(msg, e);
            }
        }

        private bool GetDb(string channel,int points,ref double[] db, ref string msg)
        {
            //refer to SCPI.ABORt
            //This command aborts the measurement and changes the trigger sequence for all channels to idle state.
            string strAbort = ":ABOR;:FORM:DATA ASC;";

            //refer to SCPI.CALCulate(Ch).SELected.DATA.FDATa
            //This command sets/gets the formatted data array, for the active trace of selected channel (Ch).
            string strGetFdata = ":CALC" + channel + ":data:FDAT?";
            try
            {
                _mbSession.Write(strAbort);
                _mbSession.Write(strGetFdata);
                string temp = _mbSession.ReadString(800000);
                List<double> tempList = StrData2DoubleArray(temp).Take(points*2).ToList();
                List<double>dbList=new List<double>();
                int i = 0;
                foreach (double t in tempList)
                {
                    if (i / 2 == 0)
                    {
                        dbList.Add(t);
                    }

                    i++;
                }

                db = dbList.ToArray();
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C fre fail";
                throw new VnaException(msg, e);
            }
        }

        private double[]StrData2DoubleArray(string source)
        {
            string[] spliteSymbols = {",", Environment.NewLine};
            return source.Split(spliteSymbols, StringSplitOptions.RemoveEmptyEntries).Select(t => double.Parse(t))
                .ToArray();
          
        }
    }
}
