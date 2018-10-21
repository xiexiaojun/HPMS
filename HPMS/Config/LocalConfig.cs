using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPMS.Equipment.Enum;
using HPMS.Util;

namespace HPMS.Config
{
    public class LocalConfig
    {
        public static object GetObjFromXmlFile(string objXmlPath, Type type)
        {
            return Serializer.FromXmlString(objXmlPath, type);
        }

        public static bool SaveObjToXmlFile<T>(string objXmlPath, T obj) where T:class 
        {
            return Serializer.CreateXML(obj, objXmlPath);
        }
        
    }

    public class Hardware
    {
        //网分visa地址
        public string VisaNetWorkAnalyzer { get; set; }
        //开关盒子的网分地址
        public string VisaSwitchBox { get; set; }
        //网分型号
        public NetworkAnalyzer Analyzer { get; set; }
        //开关盒子型号
        public SwitchBox SwitchBox { get; set; }
        //烧录器型号
        public Adapter Adapter { get; set; }
        //烧录器端口
        public string AdapterPort { get; set; }
    }

    public class TestItem
    {
        //直通差分项目
        public string[]Diff { get; set; }
        //直通单端项目
        public string[]Single { get; set; }
        //阻抗项目
        public string[]Tdr { get; set; }
        //直通对数名称
        public string[]DiffPair { get; set; }
        //近串对数名称
        public string[]NextPair { get; set; }
        //远串对数名称
        public string[]FextPair { get; set; }
    }

    
}
