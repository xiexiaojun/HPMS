using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HPMS.Equipment.Switch;
using HPMS.Log;
using NationalInstruments.VisaNS;


namespace HPMS.Equipment.NetworkAnalyzer
{
    class E5071C:INetworkAnalyzer
    {
        private ISwitch _iswitch;
        private IMessageBasedSession mbSession;
        private bool _connected = false;
        private string _visaAddress;
        private bool _nextByTrace;
        private bool _mutiChannel;
        public E5071C(ISwitch iSwitch,string visaAddress,bool nextByTrace,bool mutiChannel)
        {
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            this._nextByTrace = nextByTrace;
            this._mutiChannel = mutiChannel;
            Connect();

        }
        public bool SaveSnp(string saveFilePath, int switchIndex, ref string msg)
        {
            bool ret = false;
            ret=_iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel =_mutiChannel?(switchIndex + 1).ToString():"1";
            List<string> namList = GetNameList(channel);
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
            Thread.Sleep(500);
            return SaveS4P(saveFilePath, ref msg);
        }

        public bool GetTestData(ref double[]fre,double[]db, int switchIndex, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = (switchIndex + 1).ToString();
            List<string> namList = GetNameList(channel);
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
            Thread.Sleep(500);

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

        private void Connect()
        {
            try
            {
                if (!_connected)
                {
                    mbSession = (IMessageBasedSession)ResourceManager.GetLocalManager().Open(_visaAddress);
                    _connected = true;
                }
              
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("Connect E5071C fail via Visa Command",e);
               // throw;
            }
           
        }

        /// <summary>
        /// get all set test parameters 
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private List<string> GetNameList(string channel)
        {
            //refer to SCPI.CALCulate(Ch).PARameter.COUNt
            List<string> retList=new List<string>();
            string strGetChannelTraces = ":CALC"+channel+":PAR:COUN?";
            mbSession.Write(strGetChannelTraces);
            int traceCount=int.Parse(mbSession.ReadString(100).Trim());
            try
            {
                for (int i = 0; i < traceCount; i++)
                {
                    string strGetChannelTraceSelected = _nextByTrace ? ":CALC" + channel + ":FSIM:BAL:PAR" + (i + 1) + ":BBAL?" :
                        ":CALC" + channel + ":PAR" + (i + 1) + ":DEF?";
                    mbSession.Write(strGetChannelTraceSelected);
                    retList.Add(mbSession.ReadString(100).Trim());

                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("get name list from E5071C fail", e);
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
            string strSlectedTrace = ":CALC" + channel + ":PAR" + (singleTraceNum + 1) + ":SEL";
            try
            {
                mbSession.Write(strSlectedTrace);
                return true;
            }
            catch (Exception e)
            {
               LogHelper.WriteLog("Set trace fail",e);
               return false;
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
                mbSession.Write(strSetHoldMode);
                mbSession.Write(strSetActiveChannel);
                Thread.Sleep(1000);
                mbSession.Write(strSetChannelStateIni);
                Thread.Sleep(500);

                for (int i = 0; i < 10; i++)
                {
                    mbSession.Write(strGetMode);
                    string readTemp = mbSession.ReadString().Trim();
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
                LogHelper.WriteLog(msg,e);
                return false;
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
                mbSession.Write(strSetDataFormat);
                mbSession.Write(strSaveCommand);
                return true;
            }
            catch (Exception e)
            {
                msg = "save s4p file fail";
                LogHelper.WriteLog(msg,e);
                Console.WriteLine(e);
                return false;
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
                mbSession.Write(strGetMeasurePoints);
                points = int.Parse(mbSession.ReadString(100).Trim());
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C measurement points fail";
                LogHelper.WriteLog(msg,e);
                return false;
            }
          
        }

        private bool GetFre(ref double[]fre,ref string msg)
        {
            string strGetFre = ":SENS1:FREQ:DATA?";
            try
            {
                mbSession.Write(strGetFre);
                string temp=mbSession.ReadString(800000);
                fre = StrData2DoubleArray(temp);
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C fre fail";
                LogHelper.WriteLog(msg, e);
                return false;
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
                mbSession.Write(strAbort);
                mbSession.Write(strGetFdata);
                string temp = mbSession.ReadString(800000);
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
                LogHelper.WriteLog(msg, e);
                return false;
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
