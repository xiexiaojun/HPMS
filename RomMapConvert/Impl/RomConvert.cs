using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using RomMapConvert.DB;
using RomMapConvert.Interface;


namespace RomMapConvert.Impl
{
    enum RomType
    {
        sfpPlus=0,         //SFP+
        hd=1,               //HD
        qsfpPlus=2,        //QSFP+
        sfpPlusH3C=3,      //SFP+(H3C)
        osfp=4              //OSFP
    }

    /// <summary>
    /// 标签配置
    /// </summary>
    public struct LabelConfig
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer;
        /// <summary>
        /// label描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 年开始的位置
        /// </summary>
        public int YearStart;
        /// <summary>
        /// 年结束的位置
        /// </summary>
        public int YearEnd;
        /// <summary>
        /// 月开始的位置
        /// </summary>
        public int MonthStart;
        /// <summary>
        /// 月结束的位置
        /// </summary>
        public int MonthEnd;
        /// <summary>
        /// 周开始的位置
        /// </summary>
        public int WeekStart;
        /// <summary>
        /// 年结束的位置
        /// </summary>
        public int WeekEnd;
        /// <summary>
        /// 天开始的位置
        /// </summary>
        public int DayStart;
        /// <summary>
        /// 天结束的位置
        /// </summary>
        public int DayEnd;
        /// <summary>
        /// 流水号开始的位置
        /// </summary>
        public int SnStart;
        /// <summary>
        /// 流水号结束的位置
        /// </summary>
        public int SnEnd;
        /// <summary>
        /// 烧录进EEPOM里面的字符串掩码
        /// </summary>
        public string SnFormat;
        /// <summary>
        /// 空白填充用的ASCII码数值
        /// </summary>
        public int DateCode8Value_0X30;
        /// <summary>
        /// 周的开始类型，默认为1月1日为第一周开始
        /// </summary>
        public CalendarWeekRule WeekType;
        /// <summary>
        /// 一周的第几天为第一天，默认为周日
        /// </summary>
        public DayOfWeek WeekStartDay;
        /// <summary>
        /// 对应料号的EEPROM模板，交错数组
        /// </summary>
        public byte[][] TempeleteBytes;
        /// <summary>
        /// 是否两个sn
        /// </summary>
        public bool TwoSn;
        /// <summary>
        /// 标签长度
        /// </summary>
        public int LabelLength;

    }

    struct SnDate
    {
        public string Sn;
        public string Date;
    }

   
    /// <summary>
    /// 转换实现类
    /// </summary>
    public class RomConvert:IConvert
    {
        private string dllVersion = "0.1 alpha";
        /// <summary>
        /// 返回当前dll版本
        /// </summary>
        /// <returns>当前dll版本</returns>
        public string GetVersion()
        {
             return dllVersion;
        }

       
      
        /// <summary>
        /// 根据标签配置，标签返回模板内容
        /// </summary>
        /// <param name="labelConfig">标签配置</param>
        /// <param name="labelSn">标签sn</param>
        /// <returns>交错数组</returns>
        public byte[][] GetRomMapCross(LabelConfig labelConfig, string labelSn)
        {
           SnDate snDate = Label2SnDate(labelSn, labelConfig);           
           return FillBytes(labelConfig.TempeleteBytes, snDate.Sn, snDate.Date, labelConfig.DateCode8Value_0X30, labelConfig.Customer);
        }

        /// <summary>
        /// 根据标签配置，标签返回模板内容
        /// </summary>
        /// <param name="labelConfig">标签配置</param>
        /// <param name="labelSn">sn</param>
        /// <returns>二维数组</returns>
        public byte[,] GetRomMapArray(LabelConfig labelConfig, string labelSn)
        {
            return CrossByte2Array(GetRomMapCross(labelConfig, labelSn));
        }

        /// <summary>
        /// 返回标签配置类，供GetRomMap(LabelConfig labelConfig,string labelSn)使用
        /// </summary>
        /// <param name="pn">料号</param>
        /// <param name="result">获取料号配置是否成功</param>
        /// <param name="msg">错误信息</param>
        /// <returns>标签配置类型</returns>
        public LabelConfig GetLabelConfig(string pn,ref bool result,ref string msg)
        {
            LabelConfig labelConfig=new LabelConfig();
            result=Dao.QueryPn(pn, ref labelConfig, ref msg);
            return labelConfig;
        }

        /// <summary>
        /// 根据料号，标签返回romMap
        /// </summary>
        /// <param name="pn">料号</param>
        /// <param name="labelSn">标签sn</param>
        /// <returns>交错数组</returns>
        public byte[][] GetRomMapCross(string pn, string labelSn)
        {
            bool result = false;
            string msg = "";
            LabelConfig labelConfig = GetLabelConfig(pn, ref result, ref msg);
            return GetRomMapCross(labelConfig, labelSn);
        }

        /// <summary>
        /// 根据料号，标签返回romMap
        /// </summary>
        /// <param name="pn">料号</param>
        /// <param name="sn">标签sn</param>
        /// <returns>二维数组</returns>
        public byte[,] GetRomMapArray(string pn, string sn)
        {
            return CrossByte2Array(GetRomMapCross(pn, sn));
        }

        private byte[,] CrossByte2Array(byte[][]source)
        {
            int row = source[0].Length;
            int col = source.Length;
            byte[,]ret=new byte[row,col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    ret[i, j] = source[j][i];
                }
            }

            return ret;
        }

        private SnDate Label2SnDate(string labelSn, LabelConfig labelConfig)
        {
            SnDate ret = new SnDate();
            switch (labelConfig.Description)
            {
                case "NoSN":
                    ret.Sn = SubRight(labelSn, 4);
                    ret.Date = "20" + SubLeft(SubRight(labelSn, 12), 6);
                    break;
                case "NoDate":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = DateTime.Today.ToString("yyyyMMdd");
                    break;

                case "年月日":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "IOQU");
                    break;

                case "年月日2":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "WXYZ");
                    break;
                case "年月日3":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "WXYZ");
                    break;
                case "年月日4":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "IOQ");
                    break;
                case "年月日5":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "WXYZ");
                    break;
                case "年月日7":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "IOX");
                    break;
                case "年月日8":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDate(labelSn, labelConfig, "IOYZ");
                    break;
                case "年周":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDateFromWeek_YYWW(labelSn, labelConfig);
                    break;
                case "年天":
                    ret.Sn = labelSn.Substring(labelConfig.SnStart, labelConfig.SnEnd - labelConfig.SnStart + 1);
                    ret.Date = GetDateFromDay(labelSn, labelConfig);
                    break;
                default:
                    ret.Sn = "";
                    ret.Date = DateTime.Today.ToString("yyyyMMdd");
                    break;
            }

            ret.Sn=FillSn(ret, labelConfig);
            ret.Date = SubRight(ret.Date, 6);
            return ret;
        }

        private string FillSn(SnDate snDate, LabelConfig labelConfig)
        {
            if (labelConfig.SnFormat.Trim() == "")
            {
                return snDate.Sn;
            }

            string snFormat = labelConfig.SnFormat;

            string[] keyFormats = { "(YMD1)", "(YMD)", "y^", "yy", "(c)Y", "(c)M", "mm", "ww", "(dd)", "(ddd)","(YMD2)","(YMD3)" };
            foreach (string key in keyFormats)
            {
                if (snFormat.Contains(key))
                {
                    snFormat = DateReplace(snFormat, snDate.Date, key, labelConfig.WeekType, labelConfig.WeekStartDay);
                }
            }

            snFormat = SnReplace(snFormat, snDate.Sn);
            return snFormat;
        }


        /// <summary>
        /// (X1-X99)流水替换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sn"></param>
        /// <returns></returns>
        private string SnReplace(string source, string sn)
        {
            string pattern = "\\(X[0-9][0-9]?\\)";
            Match match = Regex.Match(source, pattern);
            string findXn = match.Value;
            if (findXn == "")
            {
                return source;
            }
            string patternXn = "[0-9][0-9]?";
            int XnWidth = int.Parse(Regex.Match(findXn, patternXn).Value);
            string snFormater = sn.PadLeft(XnWidth, '0');
            return source.Replace(findXn, snFormater);
        }
        /// <summary>
        /// 日期替换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <param name="weekType"></param>
        /// <param name="weekStartDay"></param>
        /// <returns></returns>
        private string DateReplace(string source, string date, string key,
            CalendarWeekRule weekType = CalendarWeekRule.FirstDay, DayOfWeek weekStartDay=DayOfWeek.Monday)
        {
            string ret = "";
            string year = "";
            string month = "";
            string day = "";
            string week = "";
            switch(key)
            {
                case   "(YMD1)":
                     year = date.Substring(3, 1);
                     month = Decto13(date.Substring(4, 2));
                     day = Decto32YMD(SubRight(date, 2),"WXYZ");
                    ret=source.Replace("(YMD1)", year + month + day);
                    break;
                case "(YMD)":
                     year = date.Substring(3, 1);
                     month = Decto13(date.Substring(4, 2));
                     day = Decto32YMD(SubRight(date, 2),"IOQ");
                    ret=source.Replace("(YMD)", year + month + day);
                    break;
                case "(YMD2)":
                    year = date.Substring(3, 1);
                    month = Decto13(date.Substring(4, 2));
                    day = Decto32YMD(SubRight(date, 2),"IOX");
                    ret = source.Replace("(YMD2)", year + month + day);
                    break;
                case "(YMD3)":
                    year = date.Substring(3, 1);
                    month = Decto13(date.Substring(4, 2));
                    day = Decto32YMD(SubRight(date, 2),"IOYZ");
                    ret = source.Replace("(YMD3)", year + month + day);
                    break;
                case "y^":
                    year = date.Substring(3, 1);
                    ret = source.Replace("y^", year);
                    break;
                case "yy":
                    year = date.Substring(2, 2);
                    ret = source.Replace("yy", year);
                    break;
                case "(c)Y":
                    //(c)Y中年份减去1946
                    year = (int.Parse(date.Substring(0, 4))-1946).ToString();
                    ret = source.Replace("(c)Y", year);
                    break;
                case "(c)M":
                    //(c)M中月份增加64,再转换为对应的ASCII对应的字母，即1-12月份映射成A-L
                    month = date.Substring(4, 2);
                    month = ((char) (int.Parse(month) + 64)).ToString();
                    ret = source.Replace("(c)M", month);
                    break;
                case "mm":
                    month = date.Substring(4, 2);
                    ret = source.Replace("mm", month);
                    break;
                case "ww":
                    Calendar cal = new GregorianCalendar();
                    DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture);
                    week = cal.GetWeekOfYear(dt, weekType, weekStartDay).ToString("D2");
                    ret = source.Replace("ww", week);
                    break;
                case "(dd)":
                    day = SubRight(date, 2);
                    ret = source.Replace("(dd)", day);
                    break;
                case "(ddd)":
                    Calendar cald = new GregorianCalendar();
                    DateTime dtd = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture);
                    day = cald.GetDayOfYear(dtd).ToString("D3");
                    ret = source.Replace("(ddd)", day);
                    break;
                default:
                    ret = source;
                    break;
            }

            return ret;

        }

        private string S25toDec(string sn)
        {
            NumBase numBase25=new NumBase("0123456789ABCDEFGHIJKLMNO");
            return numBase25.FromString(sn).ToString();
        }

        private string Decto32YMD(string sn,string noUse)
        {
            string usedStr = GetDeletedDateStr(noUse);
            NumBase numBase32 = new NumBase(usedStr);
            return numBase32.ToString(long.Parse(sn));
        }

      

        private string Decto13(string sn)
        {
            NumBase numBase13 = new NumBase("0123456789ABC");
            return numBase13.ToString(long.Parse(sn));
        }

        private string GetDeletedDateStr(string noUse)
        {
            string full = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = noUse.Length;
            for (int i = 0; i < length; i++)
            {
                full = full.Replace(noUse.Substring(i, 1), "");
            }

            return full;
        }

        /// <summary>
        /// 年年天天天
        /// </summary>
        /// <param name="labelSn"></param>
        /// <param name="labelConfig"></param>
        /// <returns></returns>
        private string GetDateFromDay(string labelSn, LabelConfig labelConfig)
        {
            string year = SubLeft(DateTime.Today.ToString("yyyy"), 2) +
                          labelSn.Substring(labelConfig.YearStart, labelConfig.YearEnd - labelConfig.YearStart + 1);
            string day = labelSn.Substring(labelConfig.DayStart, labelConfig.DayEnd - labelConfig.DayStart + 1);
            string week = labelSn.Substring(labelConfig.WeekStart, labelConfig.WeekEnd - labelConfig.WeekStart + 1);
            return new DateTime(int.Parse(year), 1, 1).AddDays(int.Parse(day)-1).ToString("yyyyMMdd");
        }

        /// <summary>
        /// 年年周周
        /// </summary>
        /// <param name="labelSn"></param>
        /// <param name="labelConfig"></param>
        /// <param name="weekStart"></param>
        /// <param name="weekType"></param>
        /// <returns></returns>
        private string GetDateFromWeek_YYWW(string labelSn, LabelConfig labelConfig, DayOfWeek weekStart = DayOfWeek.Monday, CalendarWeekRule weekType = CalendarWeekRule.FirstDay)
        {
            string year = SubLeft(DateTime.Today.ToString("yyyy"), 2) +
                          labelSn.Substring(labelConfig.YearStart, labelConfig.YearEnd - labelConfig.YearStart + 1);
            string week = labelSn.Substring(labelConfig.WeekStart, labelConfig.WeekEnd - labelConfig.WeekStart + 1);
            return Week2Date(int.Parse(year), int.Parse(week), weekStart, weekType);
        }

        /// <summary>
        /// 从周计算日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周数</param>
        /// <param name="weekStart">从周日还是周一开始,默认为周一</param>
        /// <param name="weekType">第一周的规则,基于 FirstDay 值的第一周可以有 1 到 7 天,
        /// 始终根据 FirstFullWeek 值的第一周为七天,基于 FirstFourDayWeek 值的第一周可以
        /// 有四到七天,默认值为FirstDay,参考
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.calendarweekrule?redirectedfrom=MSDN&amp;view=netframework-4.7.2</param>
        /// <returns></returns>
        private string Week2Date(int year, int week, DayOfWeek weekStart = DayOfWeek.Monday, CalendarWeekRule weekType = CalendarWeekRule.FirstDay)
        {
            DateTime firstDay=new DateTime(year,1,1);
            int weekDayEnd;
            int dayWeek1;//第一周第一天
            int weekNo=(int)firstDay.DayOfWeek;
            if (weekType == CalendarWeekRule.FirstDay)
            {
                 weekDayEnd = 7 - weekNo + (int) weekStart;
                dayWeek1 = 1;
            }
            else if (weekType == CalendarWeekRule.FirstFourDayWeek)
            {
                int weekLeft =  (7 - weekNo + (int) weekStart);
                if (weekLeft >= 4)
                {
                    weekDayEnd = weekLeft % 7;
                }
                else
                {
                    weekDayEnd = weekLeft+7;
                }

                dayWeek1 = 1;
            }
            else
            {
                weekDayEnd = 7 - weekNo + (int) weekStart;
                weekDayEnd = weekDayEnd < 7 ? weekDayEnd + 7 : weekDayEnd;
                dayWeek1 = weekDayEnd - 7 + 1;
            }

            if (week >= 2)
            {
                return new DateTime(year, 1, weekDayEnd).AddDays(7 * (week - 2) + 1).ToString("yyyyMMdd");
            }

            return new DateTime(year, 1, dayWeek1).ToString("yyyyMMdd");

        }

        /// <summary>
        /// 计算年月日等类型的日期
        /// </summary>
        /// <param name="labelSn"></param>
        /// <param name="labelConfig"></param>
        /// <param name="noUse">不用的日期</param>
        /// <returns>形如20181114的8位日期</returns>
        private string GetDate(string labelSn, LabelConfig labelConfig,string noUse)
        {
            if (labelConfig.YearStart == labelConfig.YearEnd)
            {
                //年的起始位和结束位一样，则日期类型为年月日
                if (labelConfig.MonthStart != labelConfig.MonthEnd ||
                    labelConfig.MonthStart != labelConfig.MonthEnd)
                {
                    throw new Exception("年月日类型的标签年为一位时,月和天也必须为一位");
                }

                string year = SubLeft(DateTime.Now.Year.ToString(), 3) +
                              labelSn.Substring(labelConfig.YearStart, 1);
                string month = int.Parse(labelSn.Substring(labelConfig.MonthStart,
                    labelConfig.MonthEnd - labelConfig.MonthStart + 1), NumberStyles.HexNumber).ToString("D2");
                string[] days =
                        {
                            //一位天的表示方法
                            "1","2","3","4","5",
                            "6","7","8","9","A",
                            "B","C","D","E","F",
                            "G","H","I","J","K",
                            "L","M","N","O","P",
                            "Q","R","S","T","U",
                            "V","W","X","Y","Z"
                        };
                var dayListTemp = days.ToList();
                for (int i = 0; i < noUse.Length; i++)
                {
                    dayListTemp.Remove(noUse.Substring(i, 1));
                }

                var dayList = dayListTemp.Take(31);
                string dayStr = labelSn.Substring(labelConfig.DayStart, 1);
                int dayIndex = dayList.ToList().IndexOf(dayStr);
                if (dayIndex < 0)
                {
                    throw new Exception("天数字符串:" + dayStr + "不合法");
                }

                string day = (dayIndex + 1).ToString("D2");
                return year + month + day;
            }
            else
            {
                //年的起始位和结束位不一样一样，则日期类型为年年月月日日
                return "20" + labelSn.Substring(labelConfig.YearStart,
                               labelConfig.YearEnd - labelConfig.YearStart + 1) +
                           labelSn.Substring(labelConfig.MonthStart,
                               labelConfig.MonthEnd - labelConfig.MonthStart + 1) +
                           labelSn.Substring(labelConfig.DayStart,
                               labelConfig.DayEnd - labelConfig.DayEnd + 1);
            }
        }

        private string SubRight(string source,int length)
        {
            int sourceLength = source.Length;
            if (length >= sourceLength)
            {
                return source;
            }
            else
            {
                return source.Substring(sourceLength - length, length);
            }
        }

        private string SubLeft(string source, int length)
        {
            int sourceLength = source.Length;
            if (length >= sourceLength)
            {
                return source;
            }
            else
            {
                return source.Substring(0, length);
            }
        }

        private byte[][] FillBytes(byte[][] tempeleteBytes, string sn, string date, int dateCode8Value, string customer)
        {
            int length = tempeleteBytes.Length;
            byte[][]ret=new byte[length][];
            byte[] snIni = Enumerable.Repeat((byte)32, 16).ToArray();
            byte[] dateIni = Enumerable.Repeat((byte)dateCode8Value, 8).ToArray();
            byte[] snBytes = Replace(snIni, System.Text.Encoding.ASCII.GetBytes(sn), 0);
            byte[] dateBytes = Replace(dateIni, System.Text.Encoding.ASCII.GetBytes(date), 0);
            for (int i = 0; i < length; i++)
            {
                ret[i] = FillBytesOp(tempeleteBytes[i], snBytes, dateBytes, customer);
                //todo
                //额外校验位待写入
            }

            return ret;
        }

        private byte[] FillBytesOp(byte[] source, byte[] sn, byte[] date, string customer)
        {
            RomType romType = GetType(source, customer);
            switch (romType)
            {
                case RomType.sfpPlus:
                    return FillBytesSFPPlus(source,sn,date);
                case RomType.hd:
                case RomType.qsfpPlus:
                    return FillBytesHDQSFPPlus(source, sn, date);
                case RomType.osfp:
                    return FillBytesOSFP(source, sn, date);
                case RomType.sfpPlusH3C:
                    return FillBytesSFPPlusH3C(source, sn, date);
                default:
                    return FillBytesSFPPlus(source, sn, date);
            }
        }

        /// <summary>
        /// SFP+
        /// </summary>
        /// <param name="tempeleteByte"></param>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private  byte[] FillBytesSFPPlus(byte[] tempeleteByte,byte[]sn,byte[]date)
        {
            byte checksum63 = GetChecksum(Subset(tempeleteByte, 0, 63));
            byte[] repalcedSnDate = Replace(Replace(tempeleteByte, sn, 68), date, 84);
            byte checksum95 = GetChecksum(Subset(repalcedSnDate, 64, 31));
            return Replace(Replace(repalcedSnDate, new[] {checksum63}, 63), new[] {checksum95}, 95);

        }

        /// <summary>
        /// 根据rom文件的首位来判定类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        private RomType GetType(byte[]source,string customer)
        {
            int firstByte = source[0];
            switch (firstByte)
            {
                case   3:
                    if (customer.Contains("H3C"))
                    {
                        return RomType.sfpPlusH3C;
                    }
                    else
                    {
                        return RomType.sfpPlus;
                    }
                case 24:
                case 25:
                    return RomType.osfp;
                case 13:
                case 17:
                    return RomType.qsfpPlus;
                case 15:
                case 16:
                case 20:
                case 21:
                    return RomType.hd;
                default:
                    return RomType.sfpPlus;
            }
       
        }
        /// <summary>
        /// OSFP
        /// </summary>
        /// <param name="tempeleteByte"></param>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private byte[] FillBytesOSFP(byte[] tempeleteByte, byte[] sn, byte[] date)
        {
            byte[] repalcedSnDate = Replace(Replace(tempeleteByte, sn, 166), date, 182);
            byte checksum222 = GetChecksum(Subset(repalcedSnDate, 128, 94));
            return Replace(repalcedSnDate, new[] { checksum222 }, 222);

        }

        /// <summary>
        /// HD,QSFP+
        /// </summary>
        /// <param name="tempeleteByte"></param>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private byte[] FillBytesHDQSFPPlus(byte[] tempeleteByte, byte[] sn, byte[] date)
        {
            byte checksum191 = GetChecksum(Subset(tempeleteByte, 128, 63));
            byte[] repalcedSnDate = Replace(Replace(tempeleteByte, sn, 196), date, 212);
            byte checksum223 = GetChecksum(Subset(repalcedSnDate, 192, 31));
            return Replace(Replace(repalcedSnDate, new[] { checksum191 }, 191), new[] { checksum223 }, 223);

        }

        /// <summary>
        /// H3C SFP+
        /// </summary>
        /// <param name="tempeleteByte"></param>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private byte[] FillBytesSFPPlusH3C(byte[] tempeleteByte, byte[] sn, byte[] date)
        {
            byte checksum63 = GetChecksum(Subset(tempeleteByte, 0, 63));
            byte[] repalcedSnDate = Replace(Replace(tempeleteByte, sn, 68), date, 84);
            byte checksum95 = GetChecksum(Subset(repalcedSnDate, 64, 31));
            byte checksum127 = GetChecksum(Subset(repalcedSnDate, 96, 31));
            return Replace(Replace(Replace(repalcedSnDate, new[] { checksum63 }, 63), new[] { checksum95 }, 95),new []{checksum127},127);

        }

        /// <summary>
        /// 数组替换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private byte[] Replace(byte[] source, byte[] target, int start)
        {
            int length = source.Length;
            int targetLenth = target.Length;
            if (length <= targetLenth + start)
            {
                throw new Exception("要插入的数组大于目标数组");
            }
            byte[] ret = new byte[length];
            Array.Copy(source,ret,length);
            for (int i = start; i < start+targetLenth; i++)
            {
                ret[i] = target[i - start];
            }

            return ret;

        }

        /// <summary>
        /// 数组截取
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] Subset(byte[] source, int start, int length)
        {
            return source.Skip(start).Take(length).ToArray();
        }

        /// <summary>
        /// 计算数组checksum
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private byte GetChecksum(byte[]source)
        {
            int bCheckSum = 0;
            foreach (byte temp in source)
            {
                bCheckSum = bCheckSum + temp;
            }

            return (byte)(bCheckSum%256);
        }
    }
}