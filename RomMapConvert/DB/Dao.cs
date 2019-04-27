using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using RomMapConvert.Impl;

namespace RomMapConvert.DB
{
     class Dao
    {
#if DEBUG
        public static string DB_MesDataCenterConnectionString = "Provider=SQLNCLI11;Data Source=172.20.23.107;Persist Security Info=True;User ID=sa;Initial Catalog=MesDataCenter;Password=data'songyy";
        public static string DB_HTPSDBConnectionString = "Provider=SQLNCLI11;Data Source=172.20.23.107;Persist Security Info=True;User ID=sa;Initial Catalog=HTPSDB;Password=data'songyy";
#else
        public static string DB_MesDataCenterConnectionString = "Provider=SQLOLEDB;server=172.20.23.107,1433;Initial Catalog=MesDataCenter;uid=sa;pwd=data'songyy";
        public static string DB_HTPSDBConnectionString = "Provider=SQLOLEDB;server=172.20.23.107,1433;Initial Catalog=HTPSDB;uid=sa;pwd=data'songyy";

#endif

        public static bool QueryPn(string pn, ref LabelConfig labelConfig, ref string msg)
        {

            bool ret = false;
            string queryPnSql = "Select PN,Valid,SWR_Check,SWR_Deadline,SN_Type," +
                              "CreateDate,Description,PE_Valid,DateCode8Format,CustomerName," +
                              "MapFile,Label_length FROM EEPROMManagement WHERE PN = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", pn);
            DataTable dt = DbHelperOleDb.Query(queryPnSql, DB_MesDataCenterConnectionString, b).Tables[0];
            if (dt.Rows.Count == 0)
            {
                msg = "该PN对应的文件还未上传,请联系电子工程师上传文件，联系电话13620065253";
                return false;
            }
            string valid = Object2Str(dt.Rows[0]["Valid"]);
            string swrCheck = Object2Str(dt.Rows[0]["SWR_Check"]);
            string peValid = Object2Str(dt.Rows[0]["PE_Valid"]);
            string swrDeadline = Object2Str(dt.Rows[0]["SWR_Deadline"]);
            if (valid.ToUpper() != "TRUE")
            {
                if (swrCheck.ToUpper() != "TRUE")
                {
                    if (peValid.ToUpper() == "TRUE")
                    {
                        msg = "该PN对应的文件还未经过QC的确认,请联系QC确认，QC联系电话13425162206";
                    }
                    else
                    {
                        msg = "该PN对应的文件既没有确认，也没有发行SWR！请先联系对应PE工程师确认，然后联系QC确认，QC联系电话：13425162206";
                    }

                    return false;
                }

                if (!CheckTimeInRange(swrDeadline))
                {
                    msg = "该PN对应的文件SWR时间已过期！有效日期段为：";
                    return false;
                }
            }


            string snType = (string)dt.Rows[0]["SN_Type"];
            string description = (string)dt.Rows[0]["Description"];
            int labelLength = Object2Int(dt.Rows[0]["Label_length"]);
            byte[] mapFile = (byte[])dt.Rows[0]["MapFile"];
            byte[][] romBytes = GetRomBytes(mapFile, snType, description);

            string queryLabelSql =
                "SELECT labelType, yearStart, yearEnd, monthStart, monthEnd, " +
                "weekStart, weekEnd, DayStart, DayEnd, snStart, " +
                "snEnd, description, snFormat, cableType, preStaData, " +
                "significant, noErrorRetest, twobar  FROM labelconfig WHERE labelType =?";
            b[0] = new OleDbParameter("0", snType);
            DataTable dt1 = DbHelperOleDb.Query(queryLabelSql, DB_HTPSDBConnectionString, b).Tables[0];
            if (dt1.Rows.Count == 0)
            {
                msg = "未能找到对应label类型:" + snType;
                return false;
            }
            labelConfig.Customer = Object2Str(dt.Rows[0]["CustomerName"]);
            labelConfig.Description = Object2Str(dt1.Rows[0]["description"]);
            labelConfig.YearStart = Object2Int(dt1.Rows[0]["yearStart"]);
            labelConfig.YearEnd = Object2Int(dt1.Rows[0]["yearEnd"]);
            labelConfig.MonthStart = Object2Int(dt1.Rows[0]["monthStart"]);
            labelConfig.MonthEnd = Object2Int(dt1.Rows[0]["monthEnd"]);
            labelConfig.WeekStart = Object2Int(dt1.Rows[0]["weekStart"]);
            labelConfig.WeekEnd = Object2Int(dt1.Rows[0]["weekEnd"]);
            labelConfig.DayStart = Object2Int(dt1.Rows[0]["DayStart"]);
            labelConfig.DayEnd = Object2Int(dt1.Rows[0]["DayEnd"]);
            labelConfig.SnStart = Object2Int(dt1.Rows[0]["snStart"]);
            labelConfig.SnEnd = Object2Int(dt1.Rows[0]["snEnd"]);
            labelConfig.SnFormat = Object2Str(dt1.Rows[0]["snFormat"]);
            labelConfig.DateCode8Value_0X30 = (int)dt.Rows[0]["DateCode8Format"];
            labelConfig.TempeleteBytes = romBytes;
            labelConfig.TwoSn= Object2Int(dt1.Rows[0]["twobar"])==1;//为1时表示有两个条码
            labelConfig.LabelLength = labelLength;
            return true;
        }

        private static bool CheckTimeInRange(string dateTime)
        {
            string[] dt = dateTime.Split(',');
            if (dt.Length != 2)
            {
                return false;
            }

            DateTime start = DateTime.Parse(dt[0]);
            DateTime end = DateTime.Parse(dt[1]);
            bool startCheck = DateTime.Compare(start, DateTime.Now) < 0;
            bool endCheck = DateTime.Compare(end, DateTime.Now) > 0;
            return startCheck && endCheck;
        }

        private static string Object2Str(object obj)
        {
            if (obj == System.DBNull.Value)
            {
                return "";
            }

            return (string)obj;
        }

        private static int Object2Int(object obj)
        {
            int ret = -1;
            if (obj == System.DBNull.Value)
            {
                return ret;
            }
            int.TryParse((string)obj, out ret);
            return ret;
        }

        private static byte[][] GetRomBytes(byte[] mapFile, string snType, string description)
        {
            string str = System.Text.Encoding.Default.GetString(mapFile);
            var bb = str.Split(new[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries).Select(t => t.Split('\t')).ToArray();
            int col = 20;
            int row = bb.Length;
            foreach (string[] s in bb)
            {
                if (s.Length < col)
                {
                    col = s.Length;
                }
            }

            List<byte[]> romMap = new List<byte[]>();
            for (int i = 0; i < col; i++)
            {
                byte[] temp = new Byte[row];
                for (int j = 0; j < row; j++)
                {
                    temp[j] = Convert.ToByte(bb[j][i], 16);
                }
                romMap.Add(temp);

                // romMap[i] = temp;
            }

            string labelTypeDes = (snType + description).ToUpper();
            int copyNum = 0;
            if (labelTypeDes.Contains("4X"))
            {
                copyNum = 3;
            }
            else if (labelTypeDes.Contains("2X"))
            {
                copyNum = 1;
            }
            else
            {
                copyNum = 0;
            }
            byte[] copyCol = romMap[col - 1];
            for (int i = 0; i < copyNum; i++)
            {
                romMap.Add(copyCol);
            }

            return romMap.ToArray();
        }



        public static byte[] File2ByteArray(string saveFilePath, out bool result)
        {
            result = false;
            List<byte> ret = new List<byte>();
            try
            {
                if (File.Exists(saveFilePath))
                {
                    result = true;
                    ret = File.ReadAllBytes(saveFilePath).ToList();
                }

            }
            catch (Exception e)
            {
                // LogHelper.WriteLog("从文件读取成byte[]失败", e);
            }

            return ret.ToArray();
        }

        public static bool ByteArray2File(byte[] byteStream, string saveFilePath)
        {
            bool ret = false;
            try
            {
                string xmlDirectory = Path.GetDirectoryName(saveFilePath);
                if (!Directory.Exists(xmlDirectory))
                {
                    Directory.CreateDirectory(xmlDirectory);
                }

                FileStream fs = new FileStream(saveFilePath, FileMode.OpenOrCreate, FileAccess.Write); //由数据库中的数据形成文件  
                fs.Write(byteStream, 0, byteStream.Length);
                fs.Close();
                ret = true;

            }
            catch (Exception e)
            {
                ret = false;
                //  LogHelper.WriteLog("从byte[]写入到文件失败", e);

            }
            return ret;
        }
    }
}
