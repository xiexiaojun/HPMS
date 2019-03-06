using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using HPMS.Code.Config;
using HPMS.Code.Draw;
using HPMS.Code.Utility;
using Newtonsoft.Json;
using Tool;
using _32p_analyze;

namespace HPMS.Code.Core
{

    public enum ItemType
    {
        Loss=0,
        Next=1,
        Fext=2,
        Last
    }

   
    /// <summary>
    /// 测试项目转换
    /// </summary>
    public class TestConfig
    {
        public List<string>AnalyzeItems { set; get; }
        public List<Pair> Pairs { set; get; }
        public ItemType ItemType { set; get; }
        public List<TdrParam>TdrParams { set; get; }
        public Dictionary<string,float[]>KeyPoint { set; get; }
        public bool Sreverse { set; get; }
        public bool Treverse { set; get; }
        public IldSpec IldSpec { set; get; }
    }

    /// <summary>
    /// 图形相关参数
    /// </summary>
    public struct TestChart
    {
        public Dictionary<string, object> ChartDic;
        public AChart AChart;
    }

    /// <summary>
    /// 窗体界面更新操作函数
    /// </summary>
    public struct FormUi
    {
        public Action<string> AddStatus;
        public Action<int, bool> ProgressDisplay;
        public Action<string, ClbType> SetCheckItem;
        public Action<string> SetResult;
        public Action<Dictionary<string, List<PairData>>> SetKeyPointList;
        public Func<bool> StopEnabbled;

    }

    /// <summary>
    /// 文件路径保存设定
    /// </summary>
    public struct Savepath
    {
        public string SnpFilePath;
        public string TxtFilePath;
        public string Sn;
        public string XmlPath;
        public string ReportTempletePath;
    }

    /// <summary>
    /// checklistbox 项目分类
    /// </summary>
    public enum ClbType
    {
        TestItem,
        DiffPair,
        NextPair,
        FextPair
    }
    public struct Pair
    {
        public string PairName;
        public byte[] SwitchIndex;
        public int ProgressValue;
    }

    public struct KeyPoint
    {
        public double[] X;
        public double[] Y;

    }

    public class TestUtil
    {
        public static Dictionary<string, string> GetTestItem()
        {
            Dictionary<string,string>resources=new Dictionary<string, string>();
            resources.Clear();
            var content = File.ReadAllText("config\\testItemGroup.json", Encoding.UTF8);
            if (!string.IsNullOrEmpty(content))
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                foreach (string key in dict.Keys)
                {
                    //遍历集合如果语言资源键值不存在，则创建，否则更新
                    if (!resources.ContainsKey(key))
                    {
                        resources.Add(key, dict[key]);
                    }
                    else
                    {
                        resources[key] = dict[key];
                    }
                }
            }

            return resources;
        }

        public static Dictionary<string, plotData> GetPnSpec(Project pnProject)
        {
           Dictionary<string, plotData> ret = new Dictionary<string, plotData>();
           DataTable dt=Serializer.Json2DataTable(pnProject.FreSpec);
           
           int frePoints = dt.Rows.Count;
            int specNum = dt.Columns.Count;
            for (int i = 1; i < specNum; i++)
            {
                plotData temp = new plotData();
                List<float> x = new List<float>();
                List<float> y = new List<float>();
                string itemName = dt.Columns[i].ColumnName.ToString().ToUpper();
                if (itemName.Contains("OFFSET"))
                {
                    for (int j = 0; j < frePoints; j++)
                    {
                        var cellValue = dt.Rows[j][i];
                        x.Add(float.Parse((string)dt.Rows[j][0]));
                        if (!(cellValue is DBNull))
                        {
                            y.Add(float.Parse((string)cellValue));
                        }
                        else
                        {
                            y.Add(0);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < frePoints; j++)
                    {
                        var cellValue = dt.Rows[j][i];
                        if (!(cellValue is DBNull))
                        {

                            x.Add(float.Parse((string)dt.Rows[j][0]));

                            y.Add(float.Parse((string)cellValue));
                        }
                    }
                }
               
                temp.xData = x.ToArray();
                temp.yData = y.ToArray();
                ret.Add(itemName, temp);
            }
            plotData[] tdd1 = GetTddSpec(pnProject.Tdd11);
            plotData[] tdd2 = GetTddSpec(pnProject.Tdd22);
            
            ret.Add("TDD11_UPPER", tdd1[0]);
            ret.Add("TDD11_LOWER", tdd1[1]);
            ret.Add("TDD22_UPPER", tdd2[0]);
            ret.Add("TDD22_LOWER", tdd2[1]);
            SaveSpec(dt,tdd1,tdd2);
            return ret;
        }

        public static void SaveSpec(DataTable dt, plotData[] tdd11, plotData[] tdd22)
        {
            if (!SaveFreSpec(Gloabal.freSpecFilePath, dt))
            {
                throw new Exception("save fre spec file to:" + Gloabal.freSpecFilePath+" error");
            }
            if (!SaveTddSpec(Gloabal.timeSpecFilePath, tdd11,tdd22))
            {
                throw new Exception("save time spec file to:" + Gloabal.timeSpecFilePath + " error");
            }
           // throw new Exception("test save spec error");
        }


        private static bool SaveFreSpec(string filePath, DataTable dt)
        {
            if (!FileCheck.FilePrepare(filePath))
            {
                return false;
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            int column = dt.Columns.Count;
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            string header = "";
            for (int i = 1; i < column; i++)
            {
                header = header + dt.Columns[i] + "\t";
            }

            header = header.Trim() + Environment.NewLine;
            sw.Write(header);

            foreach (DataRow variable in dt.Rows)
            {
                string line = "";
                for (int i = 1; i < column; i++)
                {
                    line = line + variable[i] + "\t";
                }

                line = line.Substring(0,line.Length-1) + Environment.NewLine;
                sw.Write(line);
            }   
            sw.Close();
            fs.Close();
            return true;
        }

        private static bool SaveTddSpec(string filePath, plotData[] tdd11, plotData[] tdd22)
        {
            if (!FileCheck.FilePrepare(filePath))
            {
                return false;
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);
            System.Globalization.NumberFormatInfo numberFormatInfo =
                (System.Globalization.NumberFormatInfo)System.Globalization.NumberFormatInfo.CurrentInfo.Clone();
            numberFormatInfo.NaNSymbol = "NaN";
            string header = "TDD11_UPPER\tTDD11_LOWER\tTDD22_UPPER\tTDD22_LOWER" + Environment.NewLine;
            sw.Write(header);
            int length = tdd11[0].yData.Length;
            for (int i = 0; i < length; i++)
            {
                string line = tdd11[0].yData[i].ToString(numberFormatInfo) + "\t" + tdd11[1].yData[i].ToString(numberFormatInfo)
                              + "\t" + tdd22[0].yData[i].ToString(numberFormatInfo) + "\t" + tdd22[1].yData[i].ToString(numberFormatInfo) +
                              Environment.NewLine;
                sw.Write(line);
              
            }
            sw.Close();
            fs.Close();
            return true;
        }

        public static plotData[] GetTddSpec(TdrParam tdrParam)
        {
            plotData[] ret = new plotData[2];
            double step = (tdrParam.EndTime - tdrParam.StartTime) / (tdrParam.Points - 1);
            float[] timeArray = new float[tdrParam.Points];

            
            double upperPoint1 = tdrParam.UperTimePoints[0];
            double upperPoint2 = tdrParam.UperTimePoints[1];
            double upperPoint3 = (tdrParam.EndTime - upperPoint2) / 2 + upperPoint2;
            double upperValue1 = tdrParam.UperResi[0];
            double upperValue2 = tdrParam.UperResi[1];

            double lowerPoint1 = tdrParam.LowerTimePoints[0];
            double lowerPoint2 = tdrParam.LowerTimePoints[1];
            double lowerPoint3 = (tdrParam.EndTime - lowerPoint2) / 2 + lowerPoint2;
            double lowerValue1 = tdrParam.LowerResi[0];
            double lowerValue2 = tdrParam.LowerResi[1];


            List<float> x = new List<float>();
            List<float> y = new List<float>();
            double pointX = 0;
            while (pointX < upperPoint1)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.NaN);
                pointX = pointX + step;
            }
            while (pointX <= upperPoint2)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.Parse(upperValue1.ToString()));
                pointX = pointX + step;
            }
            while (pointX <= upperPoint3)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.Parse(upperValue2.ToString()));
                pointX = pointX + step;
            }
            while (pointX <= tdrParam.EndTime+step/2)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.NaN);
                pointX = pointX + step;
            }

            ret[0].xData = x.ToArray();
            ret[0].yData = y.ToArray();

            x.Clear();
            y.Clear();
            pointX = 0;
            while (pointX < lowerPoint1)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.NaN);
                pointX = pointX + step;
            }
            while (pointX <= lowerPoint2)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.Parse(lowerValue1.ToString()));
                pointX = pointX + step;
            }
            while (pointX <= lowerPoint3)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.Parse(lowerValue2.ToString()));
                pointX = pointX + step;
            }
            while (pointX <= tdrParam.EndTime + step / 2)
            {
                x.Add(float.Parse(pointX.ToString()));
                y.Add(float.NaN);
                pointX = pointX + step;
            }
            ret[1].xData = x.ToArray();
            ret[1].yData = y.ToArray();
            return ret;
        }

        public static bool SaveResult_Sample(string filePath,Dictionary<string,string>result,Dictionary<string,string>info)
        {
            string xmlSample = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" +
                             "<general>\n" +
                             "<testResult>\n" +
                             "@testItem@" +
                             "</testResult>\n" +
                             "<information>\n" +
                             "@inforItem@" +
                             "</information>\n" +
                             "</general>";
            string testItem = "<testItem name=\"@key@\" result=\"@value@\"/> ";
            string testItemNode = "";
            foreach (var singleItem in result)
            {
                testItemNode = testItemNode +
                               testItem.Replace("@key@", singleItem.Key).Replace("@value@", singleItem.Value)+Environment.NewLine;
            }

            string infoItem = "<inforItem name=\"@key@\" value=\"@value@\"/>";
            string infoItemNode = "";
            foreach (var singleItem in info)
            {
                infoItemNode = infoItemNode +
                               infoItem.Replace("@key@", singleItem.Key).Replace("@value@", singleItem.Value) + Environment.NewLine;
            }

            string xmlFull = xmlSample.Replace("@testItem@", testItemNode).Replace("@inforItem@", infoItemNode);
            File.WriteAllText(filePath,xmlFull);
            return true;
        }
       
    }
}
