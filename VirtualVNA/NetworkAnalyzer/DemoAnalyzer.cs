using System.Threading;

namespace VirtualVNA.NetworkAnalyzer
{
    public class DemoAnalyzer:INetworkAnalyzer
    {
        public bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            Thread.Sleep(2000);
            return true;
        }

        public bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            Thread.Sleep(2000);
            return true;
        }

        public bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            return true;
        }
    }
}
