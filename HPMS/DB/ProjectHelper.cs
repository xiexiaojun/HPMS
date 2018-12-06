using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using HPMS.Config;
using HPMS.Util;
using Tool;
using _32p_analyze;

namespace HPMS.DB
{
    public class ProjectHelper
    {
        public static Project Find(string pn, bool dbMode)
        {
            Project ret;
            if (dbMode)
            {
                string savePath = "config\\" + pn + ".xml";
                ret = (Project)LocalConfig.GetObjFromXmlFile(savePath, typeof(Project));
            }
            else
            {
                var temp = ProjectDao.Find(pn);
                ret = temp.Count == 1 ? temp[0] : null;
            }

            return ret;
        }
    }

    public class ProjectDao
    {
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
                               "Skew, Customer,FreSpecFilePath) VALUES (" +
                               "?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";
            OleDbParameter[] b = new OleDbParameter[23];
            
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


            int insertCount = DbHelperOleDb.ExecuteSql(insertSql, b);
            if (insertCount == 1)
            {
                ret = true;
            }
            else
            {
                msg = "添加新料号失败";
            }
            return ret;
        }

        public static List<Project> Find(string pn)
        {
            List<Project> ret = new List<Project>();
            string querySql = "SELECT ID, PN, Customer, PNCustomer, Length, AWG, Diff, Single, Tdr, DiffPair, " +
                              "NextPair, FextPair, ReportTempletePath, RomFileMode, RomFilePath," +
                              " RomWrite, SwitchFilePath, FreSpec,FreSpecFilePath, FrePoints, Tdd11, Tdd22," +
                              " ILD, Skew FROM  HPMS_Project Where PN = ? ";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", pn);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
               Project project=new Project();
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
               ret.Add(project);
            }

            return ret;
        }

        public static bool Update(Project project, ref string msg)
        {
            bool ret = false;
            string updateSql ="UPDATE HPMS_Project SET" +
                              " PNCustomer = ?, Length = ?, AWG = ?, Diff = ?, Single = ?," +
                              "Tdr = ?,DiffPair = ?,NextPair = ?,FextPair = ?,ReportTempletePath = ?," +
                              "RomFileMode = ?,RomFilePath = ?,RomWrite = ?,SwitchFilePath = ?,FreSpec = ?," +
                              "FrePoints = ?,Tdd11 = ?,Tdd22 = ?,ILD = ?,Skew = ?,Customer = ?,FreSpecFilePath = ? where PN = ? ";
            OleDbParameter[] b = new OleDbParameter[23];
           
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
            b[22] = new OleDbParameter("22", project.Pn);

            int updateCount = DbHelperOleDb.ExecuteSql(updateSql, b);
            if (updateCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static bool Delete(string pn)
        {
            bool ret = false;
            string deleteSql = "DELETE FROM dbo.HPMS_Project WHERE PN = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", pn);
            int delCount = DbHelperOleDb.ExecuteSql(deleteSql,b);
            if (delCount == 1)
            {
                ret = true;
            }
            return ret;
        }
    }
}
