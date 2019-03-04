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

        ~RsZnb()
        {
            if (mbSession != null)
            {
                mbSession.Dispose();
            }
        }

        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            throw new NotImplementedException();
        }

        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            throw new NotImplementedException();
        }

        public override bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            throw new NotImplementedException();
        }

        public override bool LoadCalFile(string calFilePath, ref string msg)
        {
            throw new NotImplementedException();
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
                Console.WriteLine(e);
                return false;
            }

        }

    }
}
