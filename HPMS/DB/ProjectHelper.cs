using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using HPMS.Config;
using Tool;
using _32p_analyze;

namespace HPMS.DB
{
    public class ProjectHelper
    {
       

        public static Project Find(string pn)
        {
                var temp = ProjectDao.Find(pn);
                return temp.Count == 1 ? temp[0] : null;
         }
    }

    public class ProjectDao
    {
        public static bool DbMode=true;
        public static bool Add(Project project,ref string msg)
        {
            
            bool ret = false;
            if (Find(project.Pn).Count > 0)
            {
                msg = "相同料号已经存在";
                return false;
            }

            string insertSql = "INSERT INTO HPMS_Project (" +
                               "PN, PNCustomer, Length, AWG, Diff, " +
                               "Single, Tdr, DiffPair, NextPair, FextPair, " +
                               "ReportTempletePath, RomFileMode, RomFilePath, RomWrite, SwitchFilePath, " +
                               "FreSpec, FrePoints, Tdd11, Tdd22, ILD, " +
                               "Skew, Customer,FreSpecFilePath, Speed, ProductTypeL, " +
                               "ProductTypeR, Power, Description,CalFilePath,Keypoint) VALUES (" +
                               "?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";
            if (!DbMode)
            {
                OleDbParameter[] b = new OleDbParameter[30];

                b[0] = new OleDbParameter("0", project.Pn);
                b[1] = new OleDbParameter("1", project.PnCustomer);
                b[2] = new OleDbParameter("2", project.Length);
                b[3] = new OleDbParameter("3", project.Awg);
                b[4] = new OleDbParameter("4", Util.Convert.List2Str(project.Diff));

                b[5] = new OleDbParameter("5", Util.Convert.List2Str(project.Single));
                b[6] = new OleDbParameter("6", Util.Convert.List2Str(project.Tdr));
                b[7] = new OleDbParameter("7", Util.Convert.List2Str(project.DiffPair));
                b[8] = new OleDbParameter("8", Util.Convert.List2Str(project.NextPair));
                b[9] = new OleDbParameter("9", Util.Convert.List2Str(project.FextPair));

                b[10] = new OleDbParameter("10", project.ReportTempletePath);
                b[11] = new OleDbParameter("11", project.RomFileMode.ToString());
                b[12] = new OleDbParameter("12", project.RomFilePath);
                b[13] = new OleDbParameter("13", project.RomWrite);
                b[14] = new OleDbParameter("14", project.SwitchFilePath);

                b[15] = new OleDbParameter("15", project.FreSpec);
                b[16] = new OleDbParameter("16", project.FrePoints);
                b[17] = new OleDbParameter("17", Serializer.Object2Json(project.Tdd11));
                b[18] = new OleDbParameter("18", Serializer.Object2Json(project.Tdd22));
                b[19] = new OleDbParameter("19", project.Ild.ToString());

                b[20] = new OleDbParameter("20", project.Skew);
                b[21] = new OleDbParameter("21", project.Customer);
                b[22] = new OleDbParameter("22", project.FreSpecFilePath);
                b[23] = new OleDbParameter("23", project.Speed);
                b[24] = new OleDbParameter("24", project.ProductTypeL);

                b[25] = new OleDbParameter("25", project.ProductTypeR);
                b[26] = new OleDbParameter("26", project.Power);
                b[27] = new OleDbParameter("27", project.Description);
                b[28] = new OleDbParameter("28", project.CalFilePath);
                b[29] = new OleDbParameter("29", project.KeyPoint);
               


                int insertCount = DbHelperOleDb.ExecuteSql(insertSql, b);
                if (insertCount == 1)
                {
                    ret = true;
                }
                else
                {
                    msg = "添加新料号失败";
                }
            }
            else
            {
                SQLiteParameter[] b = new SQLiteParameter[30];

                b[0] = new SQLiteParameter("0", project.Pn);
                b[1] = new SQLiteParameter("1", project.PnCustomer);
                b[2] = new SQLiteParameter("2", project.Length);
                b[3] = new SQLiteParameter("3", project.Awg);
                b[4] = new SQLiteParameter("4", Util.Convert.List2Str(project.Diff));

                b[5] = new SQLiteParameter("5", Util.Convert.List2Str(project.Single));
                b[6] = new SQLiteParameter("6", Util.Convert.List2Str(project.Tdr));
                b[7] = new SQLiteParameter("7", Util.Convert.List2Str(project.DiffPair));
                b[8] = new SQLiteParameter("8", Util.Convert.List2Str(project.NextPair));
                b[9] = new SQLiteParameter("9", Util.Convert.List2Str(project.FextPair));

                b[10] = new SQLiteParameter("10", project.ReportTempletePath);
                b[11] = new SQLiteParameter("11", project.RomFileMode.ToString());
                b[12] = new SQLiteParameter("12", project.RomFilePath);
                b[13] = new SQLiteParameter("13", project.RomWrite);
                b[14] = new SQLiteParameter("14", project.SwitchFilePath);

                b[15] = new SQLiteParameter("15", project.FreSpec);
                b[16] = new SQLiteParameter("16", project.FrePoints);
                b[17] = new SQLiteParameter("17", Serializer.Object2Json(project.Tdd11));
                b[18] = new SQLiteParameter("18", Serializer.Object2Json(project.Tdd22));
                b[19] = new SQLiteParameter("19", project.Ild.ToString());

                b[20] = new SQLiteParameter("20", project.Skew);
                b[21] = new SQLiteParameter("21", project.Customer);
                b[22] = new SQLiteParameter("22", project.FreSpecFilePath);
                b[23] = new SQLiteParameter("23", project.Speed);
                b[24] = new SQLiteParameter("24", project.ProductTypeL);

                b[25] = new SQLiteParameter("25", project.ProductTypeR);
                b[26] = new SQLiteParameter("26", project.Power);
                b[27] = new SQLiteParameter("27", project.Description);
                b[28] = new SQLiteParameter("28", project.CalFilePath);
                b[29] = new SQLiteParameter("29", project.KeyPoint);
                int insertCount = SqlLite.ExecuteNonQuery(insertSql, b);
                if (insertCount == 1)
                {
                    ret = true;
                }
                else
                {
                    msg = "添加新料号失败";
                }
            }

           
            return ret;
        }

        public static List<Project> Find(string pn)
        {
           string querySql = "SELECT ID, PN, Customer, PNCustomer, Length, AWG, Diff, Single, Tdr, DiffPair, " +
                              "NextPair, FextPair, ReportTempletePath, RomFileMode, RomFilePath," +
                              " RomWrite, SwitchFilePath, FreSpec,FreSpecFilePath, FrePoints, Tdd11, Tdd22," +
                              " ILD, Skew,Speed, ProductTypeL, ProductTypeR, " +
                              "Power, Description,CalFilePath,Keypoint FROM  HPMS_Project Where PN = ? and isFast is Null";
            DataTable table;
            if (!DbMode)
            {
                OleDbParameter[] b = new OleDbParameter[1];
                b[0] = new OleDbParameter("0", pn);
                DataSet dataSet = DbHelperOleDb.Query(querySql, b);
                table = dataSet.Tables[0];
            }
            else
            {
                SQLiteParameter[] b = new SQLiteParameter[1];
                b[0] = new SQLiteParameter("0", pn);
                table = SqlLite.ExecuteDataTable(querySql, b);
              
            }
          

          

            return GetProjectlistFromTable(table);
        }

        public static List<Project> Findfast()
        {
            string querySql = "SELECT ID, PN, Customer, PNCustomer, Length, AWG, Diff, Single, Tdr, DiffPair, " +
                              "NextPair, FextPair, ReportTempletePath, RomFileMode, RomFilePath," +
                              " RomWrite, SwitchFilePath, FreSpec,FreSpecFilePath, FrePoints, Tdd11, Tdd22," +
                              " ILD, Skew,Speed, ProductTypeL, ProductTypeR, " +
                              "Power, Description,CalFilePath,Keypoint FROM  HPMS_Project Where isFast ='fast'";
            var table = DbMode ? SqlLite.ExecuteDataTable(querySql) : DbHelperOleDb.Query(querySql).Tables[0];
          return GetProjectlistFromTable(table);
        }

        private static List<Project>GetProjectlistFromTable(DataTable dataTable)
        {
            List<Project> ret = new List<Project>();
            foreach (DataRow tempRow in dataTable.Rows)
            {
                Project project = new Project();
                project.Pn = (string)tempRow["PN"];
                project.Customer = (string)tempRow["Customer"];
                project.PnCustomer = (string)tempRow["PNCustomer"];
                project.Length = (int)tempRow["Length"];
                project.Awg = (int)tempRow["AWG"];
                project.Diff = Util.Convert.Str2List((string)tempRow["Diff"]);
                project.Single = Util.Convert.Str2List((string)tempRow["Single"]);
                project.Tdr = Util.Convert.Str2List((string)tempRow["Tdr"]);
                project.DiffPair = Util.Convert.Str2List((string)tempRow["DiffPair"]);
                project.NextPair = Util.Convert.Str2List((string)tempRow["NextPair"]);
                project.FextPair = Util.Convert.Str2List((string)tempRow["FextPair"]);
                project.ReportTempletePath = (string)tempRow["ReportTempletePath"];
                project.RomFileMode = (RomFileMode)Enum.Parse(typeof(RomFileMode), (string)tempRow["RomFileMode"]);
                project.RomFilePath = (string)tempRow["RomFilePath"];
                project.RomWrite = (bool)tempRow["RomWrite"];
                project.SwitchFilePath = (string)tempRow["SwitchFilePath"];
                project.FreSpec = (string)tempRow["FreSpec"];
                project.FreSpecFilePath = (string)tempRow["FreSpecFilePath"];
                project.FrePoints = (int)tempRow["FrePoints"];
                project.Tdd11 = Serializer.Json2Object<TdrParam>((string)tempRow["Tdd11"]);
                project.Tdd22 = Serializer.Json2Object<TdrParam>((string)tempRow["Tdd22"]);
                project.Ild = (IldSpec)Enum.Parse(typeof(IldSpec), (string)tempRow["ILD"]);
                project.Skew = (double)tempRow["Skew"];
                project.Speed = GetDbValue(tempRow["Speed"], "");
                project.ProductTypeL = GetDbValue(tempRow["ProductTypeL"], "");
                project.ProductTypeR = GetDbValue(tempRow["ProductTypeR"], "");
                project.Power = GetDbValue(tempRow["Power"], "");
                project.Description = GetDbValue(tempRow["Description"], "");
                project.CalFilePath = GetDbValue(tempRow["CalFilePath"], "");
                project.KeyPoint = GetDbValue(tempRow["Keypoint"], "");
                ret.Add(project);
            }

            return ret;
        }

        private static T GetDbValue<T>(object db, T defaultValue)
        {
            if (db is DBNull)
            {
                return defaultValue;
            }

            return (T) db;
        }

        public static bool Update(Project project, ref string msg)
        {
            bool ret = false;
            string updateSql ="UPDATE HPMS_Project SET" +
                              " PNCustomer = ?, Length = ?, AWG = ?, Diff = ?, Single = ?," +
                              "Tdr = ?,DiffPair = ?,NextPair = ?,FextPair = ?,ReportTempletePath = ?," +
                              "RomFileMode = ?,RomFilePath = ?,RomWrite = ?,SwitchFilePath = ?,FreSpec = ?," +
                              "FrePoints = ?,Tdd11 = ?,Tdd22 = ?,ILD = ?,Skew = ?," +
                              "Customer = ?,FreSpecFilePath = ?, Speed = ?,ProductTypeL = ?,ProductTypeR = ?," +
                              "Power = ?, Description = ?,CalFilePath = ?,Keypoint = ? where PN = ? ";
            if (!DbMode)
            {
                OleDbParameter[] b = new OleDbParameter[30];

                b[0] = new OleDbParameter("0", project.PnCustomer);
                b[1] = new OleDbParameter("1", project.Length);
                b[2] = new OleDbParameter("2", project.Awg);
                b[3] = new OleDbParameter("3", Util.Convert.List2Str(project.Diff));
                b[4] = new OleDbParameter("4", Util.Convert.List2Str(project.Single));

                b[5] = new OleDbParameter("5", Util.Convert.List2Str(project.Tdr));
                b[6] = new OleDbParameter("6", Util.Convert.List2Str(project.DiffPair));
                b[7] = new OleDbParameter("7", Util.Convert.List2Str(project.NextPair));
                b[8] = new OleDbParameter("8", Util.Convert.List2Str(project.FextPair));
                b[9] = new OleDbParameter("9", project.ReportTempletePath);

                b[10] = new OleDbParameter("10", project.RomFileMode.ToString());
                b[11] = new OleDbParameter("11", project.RomFilePath);
                b[12] = new OleDbParameter("12", project.RomWrite);
                b[13] = new OleDbParameter("13", project.SwitchFilePath);
                b[14] = new OleDbParameter("14", project.FreSpec);

                b[15] = new OleDbParameter("15", project.FrePoints);
                b[16] = new OleDbParameter("16", Serializer.Object2Json(project.Tdd11));
                b[17] = new OleDbParameter("17", Serializer.Object2Json(project.Tdd22));
                b[18] = new OleDbParameter("18", project.Ild.ToString());
                b[19] = new OleDbParameter("19", project.Skew);

                b[20] = new OleDbParameter("20", project.Customer);
                b[21] = new OleDbParameter("21", project.FreSpecFilePath);
                b[22] = new OleDbParameter("22", project.Speed);
                b[23] = new OleDbParameter("23", project.ProductTypeL);
                b[24] = new OleDbParameter("24", project.ProductTypeR);
                b[25] = new OleDbParameter("25", project.Power);
                b[26] = new OleDbParameter("26", project.Description);
                b[27] = new OleDbParameter("27", project.CalFilePath);
                b[28] = new OleDbParameter("28", project.KeyPoint);
                b[29] = new OleDbParameter("29", project.Pn);

                int updateCount = DbHelperOleDb.ExecuteSql(updateSql, b);
                if (updateCount == 1)
                {
                    ret = true;
                }
            }
            else
            {
                SQLiteParameter[] b = new SQLiteParameter[30];

                b[0] = new SQLiteParameter("0", project.PnCustomer);
                b[1] = new SQLiteParameter("1", project.Length);
                b[2] = new SQLiteParameter("2", project.Awg);
                b[3] = new SQLiteParameter("3", Util.Convert.List2Str(project.Diff));
                b[4] = new SQLiteParameter("4", Util.Convert.List2Str(project.Single));

                b[5] = new SQLiteParameter("5", Util.Convert.List2Str(project.Tdr));
                b[6] = new SQLiteParameter("6", Util.Convert.List2Str(project.DiffPair));
                b[7] = new SQLiteParameter("7", Util.Convert.List2Str(project.NextPair));
                b[8] = new SQLiteParameter("8", Util.Convert.List2Str(project.FextPair));
                b[9] = new SQLiteParameter("9", project.ReportTempletePath);

                b[10] = new SQLiteParameter("10", project.RomFileMode.ToString());
                b[11] = new SQLiteParameter("11", project.RomFilePath);
                b[12] = new SQLiteParameter("12", project.RomWrite);
                b[13] = new SQLiteParameter("13", project.SwitchFilePath);
                b[14] = new SQLiteParameter("14", project.FreSpec);

                b[15] = new SQLiteParameter("15", project.FrePoints);
                b[16] = new SQLiteParameter("16", Serializer.Object2Json(project.Tdd11));
                b[17] = new SQLiteParameter("17", Serializer.Object2Json(project.Tdd22));
                b[18] = new SQLiteParameter("18", project.Ild.ToString());
                b[19] = new SQLiteParameter("19", project.Skew);

                b[20] = new SQLiteParameter("20", project.Customer);
                b[21] = new SQLiteParameter("21", project.FreSpecFilePath);
                b[22] = new SQLiteParameter("22", project.Speed);
                b[23] = new SQLiteParameter("23", project.ProductTypeL);
                b[24] = new SQLiteParameter("24", project.ProductTypeR);
                b[25] = new SQLiteParameter("25", project.Power);
                b[26] = new SQLiteParameter("26", project.Description);
                b[27] = new SQLiteParameter("27", project.CalFilePath);
                b[28] = new SQLiteParameter("28", project.KeyPoint);
                b[29] = new SQLiteParameter("29", project.Pn);

                int updateCount = SqlLite.ExecuteNonQuery(updateSql, b);
                if (updateCount == 1)
                {
                    ret = true;
                }
            }

         
            return ret;
        }

        public static bool Delete(string pn)
        {
            bool ret = false;
            string deleteSql = "DELETE FROM dbo.HPMS_Project WHERE PN = ?";
            if (!DbMode)
            {
                OleDbParameter[] b = new OleDbParameter[1];
                b[0] = new OleDbParameter("0", pn);
                int delCount = DbHelperOleDb.ExecuteSql(deleteSql, b);
                if (delCount == 1)
                {
                    ret = true;
                }
            }
            else
            {
                SQLiteParameter[] b = new SQLiteParameter[1];
                b[0] = new SQLiteParameter("0", pn);
                int delCount = SqlLite.ExecuteNonQuery(deleteSql, b);
                if (delCount == 1)
                {
                    ret = true;
                }
            }
          
            return ret;
        }
    }
}
