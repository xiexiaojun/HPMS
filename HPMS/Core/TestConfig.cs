using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using DevComponents.DotNetBar;
using HPMS.Config;
using HPMS.DB;
using HPMS.Draw;
using HPMS.Util;
using Newtonsoft.Json;

namespace HPMS.Core
{

    public enum ItemType
    {
        Loss=0,
        Next=1,
        Fext=2
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
        public Action<string> AddAction;
        public Action<int, bool> ProgressDisplay;

    }

    public struct Pair
    {
        public string PairName;
        public byte[] SwitchIndex;
        public int ProgressValue;
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
                for (int j = 0; j < frePoints; j++)
                {
                    var cellValue = dt.Rows[j][i];
                    if (!(cellValue is DBNull))
                    {
                    
                        x.Add(float.Parse((string)dt.Rows[j][0]));
                        
                        y.Add(float.Parse((string)cellValue));
                    }
                }
                temp.xData = x.ToArray();
                temp.yData = y.ToArray();
                ret.Add(dt.Columns[i].ColumnName.ToString().ToUpper(), temp);
            }
            plotData[] tdd1 = GetTddSpec(pnProject.Tdd11);
            plotData[] tdd2 = GetTddSpec(pnProject.Tdd22);
            ret.Add("TDD11_UPPER", tdd1[0]);
            ret.Add("TDD11_LOWER", tdd1[1]);
            ret.Add("TDD22_UPPER", tdd2[0]);
            ret.Add("TDD22_LOWER", tdd2[1]);

            return ret;
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
            double pointX = upperPoint1;
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

            ret[0].xData = x.ToArray();
            ret[0].yData = y.ToArray();

            x.Clear();
            y.Clear();
            pointX = lowerPoint1;
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
            ret[1].xData = x.ToArray();
            ret[1].yData = y.ToArray();
            return ret;
        }
       
    }
}
