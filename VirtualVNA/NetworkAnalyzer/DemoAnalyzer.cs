using System;
using System.Threading;

namespace VirtualVNA.NetworkAnalyzer
{
    public class DemoAnalyzer:NetworkAnalyzer
    {
        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            Thread.Sleep(2000);
           // throw new Exception("Test error");
            return true;
        }

        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            Thread.Sleep(2000);
           // throw new Exception("Test error");
            return true;
        }

        public override bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            return true;
        }
    }
}
