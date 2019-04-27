using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EEPROM.Code.Utility;

namespace EEPROM.Code.Entity
{
    public enum TestMode
    {
        [ToChinese("检测")]
        [Description("High Speed Product EEPROM  Check  System")]
        Check,
        [ToChinese("烧录")]
        [Description("High Speed Product EEPROM  Write  System")]
        Write
    }

    public enum BurnAdapter
    {
        [Description("CP2112")]
        CP2112,
        [Description("Luxshare")]
        CP2102,
        [Description("吉阳光电")]
        GYI
    }

    public struct TestResult
    {
        public bool[] WriteResult;
        public bool[] ReadResult;//每个头的结果
        public byte[][] ReadValue;//读出值
        public byte[][] TempalteValue;//模板值
        public bool[] CheckResult;
        public bool HasException;//过程是否发生异常

    }
    public  class DataType
    {
        public static TestMode CurrentTestMode;

       
    }

    public class ProjectConfig
    {
        public BurnAdapter Adapter { set; get; }
      

        public bool Print { set; get; }
       

        public int LabelNum { set; get; }
      
        public string Com { set; get; }

        public int WriteBlock { set; get; }
        public int WriteDelay { set; get; }
       
    }
}
