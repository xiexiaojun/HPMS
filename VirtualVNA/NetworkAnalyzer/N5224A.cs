using System;
using System.Linq;
using System.Threading;
using NationalInstruments.VisaNS;
using VirtualSwitch;

namespace VirtualVNA.NetworkAnalyzer
{
    public class N5224A:NetworkAnalyzer
    {
        private ISwitch _iswitch;
        private MessageBasedSession mbSession;
        private bool _connected = false;
        private string _visaAddress;
   
        public N5224A(ISwitch iSwitch,string visaAddress)
        {
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            Connect();

        }

        ~N5224A()
        {
            if (mbSession != null)
            {
                mbSession.Dispose();
            }
            
        }
        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace,ref string msg)
        {
            bool ret = false;
            ret=_iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = mutiChannel ? (switchIndex + 1).ToString() : "1";
            string strSelectTrace = GetNameList(channel,false);


            if (!SelectTrace(channel, strSelectTrace, true, ref msg))
            {
                return false;
            };
            if (!Trigger(channel, ref msg))
            {
                return false;
            };
            Thread.Sleep(500);
            return SaveS4P(saveFilePath,channel, ref msg);
        }

        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace,ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            Connect();
            string channel = mutiChannel ? (index + 1).ToString() : "1";
            string strSelectTrace = GetNameList(channel, false);


            if (!SelectTrace(channel, strSelectTrace, true, ref msg))
            {
                return false;
            };
            if (!Trigger(channel, ref msg))
            {
                return false;
            };
            Thread.Sleep(500);
            return SaveS4P(saveFilePath, channel, ref msg);
        }

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
            string strSelectTrace = GetNameList(channel, true);
           
            if (!SelectTrace(channel, strSelectTrace, false, ref msg))
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

            if (!GetFre(ref fre, ref msg,points))
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
                    mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_visaAddress);
                    _connected = true;
                }
              
            }
            catch (Exception e)
            {
               
                throw new VnaException("Connect N5224A fail via Visa Command",e);
               // ;
            }
           
        }

        /// <summary>
        /// get all set test parameters 
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private string GetNameList(string channel,bool isDiff)
        {

            string ret=null;
            string strGetChannelTraces = ":CALC"+channel+":PAR:CAT?";
            try
            {
                mbSession.Write(strGetChannelTraces);
                string strTemp = mbSession.ReadString(200).Trim('\n').Trim('"');
               // strTemp = strTemp.Substring(1, strTemp.Length - 3);
                string[] tempNames = strTemp.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int cycleCount = tempNames.Length / 2;
                string[] chNames = new string[cycleCount];
                string[] itemNames = new string[cycleCount];

                for (int i = 0; i < cycleCount; i++)
                {
                    chNames[i] = tempNames[i * 2];
                    itemNames[i] = tempNames[i * 2 + 1];
                }

                int singleIndex = -1;
                for (int i = 0; i < cycleCount; i++)
                {
                    if (FindTargetParameter(isDiff, itemNames[i]))
                    {
                        singleIndex = i;
                        ret = chNames[singleIndex];
                        break;
                    }
                }

                if (singleIndex == -1)
                {
                    throw new VnaException("Not Find Any Single Parameter, please open a \"S21\" or \"S11\" parameter, and Try Again! ");
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new VnaException("get name list from N5224 fail", e);
            }
           

            return ret;

           
        }

        /// <summary>
        /// select single trace
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="traceSelected"></param>
        /// <param name="isSingle"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SelectTrace(string channel,string traceSelected,bool isSingle,ref string msg)
        {
         
            string strSlectedTrace = ":CALC" + channel + ":PAR:SEL " +"\""+ traceSelected + "\"";
            try
            {
                mbSession.Write(strSlectedTrace);
                return true;
            }
            catch (Exception e)
            {
               throw new VnaException("Set trace fail",e);
               return false;
            }
           
        }

        /// <summary>
        /// set N5224C trigger mode
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool Trigger(string channel,ref string msg)
        {
            //set singleMode
            string strSetSingleode = "sense" + channel + ":sweep:mode single";
            //this scip command used for get E5071C work mode
            string strGetMode = "sense" + channel + ":sweep:mode?";
            //set holdMode
            string strSetHoldode = "sense" + channel + ":sweep:mode hold";

          

            try
            {
                mbSession.Write(strSetSingleode);
               

                for (int i = 0; i < 10; i++)
                {
                    mbSession.Write(strGetMode);
                    string readTemp = mbSession.ReadString().Trim();
                    if (readTemp.Equals("HOLD"))
                    {
                        break;
                    }

                    Thread.Sleep(200);
                }
                mbSession.Write(strSetHoldode);
                return true;

            }
            catch (Exception e)
            {
                msg = "N5224A trigger fail";
                throw new VnaException(msg,e);
                return false;
            }
           
        }

        /// <summary>
        /// save s4p file to SpecifiedPath
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool SaveS4P(string saveFilePath,string channel,ref string msg)
        {
            //refer to :MMEMory:STORe:SNP
            //this command used for set save data format
            string strSetDataFormat = ":MMEM:STOR:TRAC:FORM:SNP RI";

            string strSaveCommand = "CALC" + channel + ":DATA:SNP:PORTs:Save \'1,2,3,4\', \'" + ReplaceSlash(saveFilePath) + "\'";
              try
            {
                mbSession.Write(strSetDataFormat);
                mbSession.Write(strSaveCommand);
                return true;
            }
            catch (Exception e)
            {
                msg = "save s4p file fail";
                throw new VnaException(msg,e);
                Console.WriteLine(e);
                return false;
            }

        }

     
        private bool FindTargetParameter(bool isDiff, string testParameter)
        {
            if (!isDiff)
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
            string strGetMeasurePoints = "SENSe1:SWEep:POIN?";
            try
            {
                mbSession.Write(strGetMeasurePoints);
                points = int.Parse(mbSession.ReadString(100).Trim());
                return true;
            }
            catch (Exception e)
            {
                msg = "get N5224A measurement points fail";
                throw new VnaException(msg,e);
                return false;
            }
          
        }

        private bool GetFre(ref double[]fre,ref string msg,int points)
        {
            string strGetFre = "sense1:x:values?";
            try
            {
                mbSession.Write(strGetFre);
                string temp=mbSession.ReadString(200000);
                fre = StrData2DoubleArray(temp).Take(points).ToArray();
                return true;
            }
            catch (Exception e)
            {
                msg = "get E5071C fre fail";
                throw new VnaException(msg, e);
                return false;
            }
        }

        private bool GetDb(string channel,int points,ref double[] db, ref string msg)
        {
           

            string strGetFdata = ":CALC" + channel + ":DATA? FDATA";
            try
            {
                
                mbSession.Write(strGetFdata);
                string temp = mbSession.ReadString(200000);
                db = StrData2DoubleArray(temp).Take(points).ToArray();
                
                return true;
            }
            catch (Exception e)
            {
                msg = "get N5224A fre fail";
                throw new VnaException(msg, e);
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
