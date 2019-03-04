using System;
using System.Threading;
using VirtualSwitch;

namespace VirtualVNA.NetworkAnalyzer
{
    public class DemoAnalyzer:NetworkAnalyzer
    {
        private ISwitch _iswitch;
        private string _visaAddress;
        private int _responseTime;
        public DemoAnalyzer(ISwitch iSwitch,string visaAddress,int responseTime=2000)
        {
            if (responseTime == 0)
                responseTime = 2000;
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
            //Connect();

        }
        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            //Thread.Sleep(3000);
           // throw new Exception("Test error");
            return true;
        }

        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
           // Thread.Sleep(3000);
           // throw new Exception("Test error");
            return true;
        }

        public override bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            return true;
        }

        public override bool LoadCalFile(string calFilePath, ref string msg)
        {
            return true;
        }
    }
}
