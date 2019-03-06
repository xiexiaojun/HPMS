using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HPMS.Code.Draw;

namespace HPMS.Code.Utility
{
    public class Convert
    {
        public static string List2Str(List<string> source)
        {
            return String.Join(",", source.ToArray());
        }

        public static List<string> Str2List(string source)
        {
            string[] separater = new[] { "," };
            return source.Split(separater, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string FormatMsg(string msgIn)
        {
            return DateTime.Now.ToString() + "    " + msgIn;
        }

        public static string SlashRepalce(string source)
        {
            StringBuilder rBuilder = new StringBuilder(source);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                rBuilder.Replace(rInvalidChar.ToString(), "-");
            return rBuilder.ToString();
        }

        public static  bool Judge(Dictionary<string, plotData> spec,plotData data,string testItem)
        {
            Dictionary<string, bool> ret = new Dictionary<string, bool>();
              bool blUpper = true;
                bool blLower = true;
                if (spec.ContainsKey(testItem + "_UPPER"))
                {
                    plotData specSingle = spec[testItem + "_UPPER"];
                    blUpper = edgeTestLine(data, specSingle, true);
                }
                if (spec.ContainsKey(testItem + "_LOWER"))
                {
                    plotData specSingle = spec[testItem + "_LOWER"];
                    blLower = edgeTestLine(data, specSingle, false);
                }

                return blUpper&&blLower;
        }

        public static Dictionary<string, bool> Judge(Dictionary<string, plotData> spec, Dictionary<string, plotData[]> data)
        {
            Dictionary<string, bool> ret = new Dictionary<string, bool>();
            foreach (var item in data)
            {
                bool blUpper = true;
                bool blLower = true;
                if (spec.ContainsKey(item.Key + "_UPPER"))
                {
                    plotData specSingle = spec[item.Key + "_UPPER"];
                    blUpper = edgeTest(item.Value, specSingle, true);
                }
                if (spec.ContainsKey(item.Key + "_LOWER"))
                {
                    plotData specSingle = spec[item.Key + "_LOWER"];
                    blLower = edgeTest(item.Value, specSingle, false);
                }
                ret.Add(item.Key, blUpper && blLower);
            }
            return ret;
        }

        #region 等同labview边界测试函数
        public static bool edgeTest(plotData[] data, plotData spec, bool isUpper)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!edgeTestLine(data[i], spec, isUpper))
                {
                    return false;
                }

            }

            return true;
        }
        private static bool edgeTestLine(plotData data, plotData spec, bool isUpper)
        {
            for (int i = 0; i < data.xData.Length; i++)
            {
                if (!edgeTestPoint(data.xData[i], data.yData[i], spec, isUpper))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool edgeTestPoint(double x, double y, plotData spec, bool isUpper)
        {
            for (int i = 0; i < spec.xData.Length; i++)
            {
                if (Math.Abs(x - spec.xData[i]) < float.Epsilon)
                {
                    if (isUpper)
                    {
                        if (y >= spec.yData[i])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (y <= spec.yData[i])
                        {
                            return false;
                        }
                    }

                }

            }
            return true;
        }
        #endregion
    }
}
