using System;
using System.Collections.Generic;
using DevComponents.DotNetBar;
using Tool;
using VirtualVNA.Enum;
using _32p_analyze;

namespace HPMS.Code.Config
{
    public class Theme
    {
        public eStyle EStyle { get; set; }
        public string Tag { get; set; }
        public string Color { get; set; }
        public bool Customer { get; set; }
    }
    public class LocalConfig
    {
        public static bool SaveTheme(Theme theme)
        {
            string strThemeFilePath = "config\\theme.xml";
            return SaveObjToXmlFile(strThemeFilePath, theme);
        }

        public static Theme LoadTheme()
        {
            string strThemeFilePath = "config\\theme.xml";
            return (Theme)GetObjFromXmlFile(strThemeFilePath, typeof(Theme));
        }
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
        //SNP保存路径
        public string SnpFolder { get; set; }
        //txt保存路径
        public string TxtFolder { get; set; }
        //开关响应时间
        public int SwitchResponseTime { get; set; }
        //网分响应时间
        public int AnalyzerResponseTime { get; set; }
       
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
        //速率
        public string[]Speed { get; set; }
        //产品类型
        public string[]ProductType { get; set; }
        //有源无源
        public string[]Power { set; get; }


    }

    //烧录模式
    public enum RomFileMode
    {
        DB,
        Local
    }

   

    public class TdrParam
    {
        public double StartTime { set; get; }
        public double EndTime { set; get; }
        public int Points { set; get; }
        public double RiseTime { set; get; }
        public double Offset { set; get; }

        public double[] UperTimePoints { set; get; }
        public double[] LowerTimePoints { set; get; }

        public double[] UperResi { set; get; }
        public double[] LowerResi { set; get; }

    }

    [Serializable]
    public class Project
    {
        public string Pn { set; get; }
        public string PnCustomer { set; get; }
        public string Customer { set; get; }
        public int Length { set; get; }
        public int Awg { set; get; }

        
        public List<string>Diff { set; get; }
        public List<string>Single { set; get; }
        public List<string>Tdr { set; get; }
        public List<string>DiffPair { set; get; }
        public List<string>NextPair { set; get; }
        public List<string>FextPair { set; get; }

        public string ReportTempletePath { set; get; }
        public RomFileMode RomFileMode { set; get; }
        public string RomFilePath { set; get; }
        public bool RomWrite { set; get; }
        public string SwitchFilePath { set; get; }
        public string CalFilePath { set; get; }

        public string FreSpec { set; get; }
        public int FrePoints { set; get; }
        public string FreSpecFilePath { set; get; }
        public TdrParam Tdd11 { set; get; }
        public TdrParam Tdd22 { set; get; }
        public IldSpec Ild { set; get; }
        public double Skew { set; get; }

        public string Speed { set; get; }
        public string ProductTypeL { set; get; }
        public string ProductTypeR { set; get; }
        public string Power { set; get; }
        public string Description { set; get; }

        public string KeyPoint { set; get; }

        public bool Srevert { set; get; }
        public bool Trevert { set; get; }


      
    }
}
