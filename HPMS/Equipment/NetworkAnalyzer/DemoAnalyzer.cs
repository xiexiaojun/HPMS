using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPMS.Equipment.NetworkAnalyzer
{
    public class DemoAnalyzer:INetworkAnalyzer
    {
        public bool SaveSnp(string saveFilePath, int switchIndex, ref string msg)
        {
            Thread.Sleep(5000);
            return true;
        }

        public bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            return true;
        }
    }
}
