using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar;
using HPMS.Equipment.Enum;
using HPMS.Util;

namespace HPMS.Config
{
    public class Theme
    {
        public eStyle EStyle { get; set; }
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

    public class Project
    {
        public DataTable FreSpec { get; set; }

        //public bool SaveToDb()
        //{
        //    DataTable a = this.FreSpec;
        //}
    }
}
