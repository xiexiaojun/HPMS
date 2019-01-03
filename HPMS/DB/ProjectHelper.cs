using System;
using System.Collections.Generic;
using System.Data;
using HPMS.Config;
using HPMS.Util;
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
                               "ProductTypeR, Power, Description,CalFilePath,Keypoint,Srevert,Trevert) VALUES (" +
                               "@pn,@pncustomer,@length,@awg,@diff," +
                               "@single,@tdr,@diffpair,@nextpair,@fextpair," +
                               "@reporttempletepath,@romfilemode,@romfilepath,@romwrite,@switchfilepath," +
                               "@frespec,@frepoints,@tdd11,@tdd22,@ild," +
                               "@skew,@customer,@frespecfilepath,@speed,@producttypel," +
                               "@producttyper,@power,@description,@calfilepath,@keypoint,@srevert,@trevert) ";

            IDataParameter[] b = new IDataParameter[32];

            b[0] = Gloabal.GDatabase.CreatePara("pn", project.Pn);
            b[1] = Gloabal.GDatabase.CreatePara("pncustomer", project.PnCustomer);
            b[2] = Gloabal.GDatabase.CreatePara("length", project.Length);
            b[3] = Gloabal.GDatabase.CreatePara("awg", project.Awg);
            b[4] = Gloabal.GDatabase.CreatePara("diff", Util.Convert.List2Str(project.Diff));

            b[5] = Gloabal.GDatabase.CreatePara("single", Util.Convert.List2Str(project.Single));
            b[6] = Gloabal.GDatabase.CreatePara("tdr", Util.Convert.List2Str(project.Tdr));
            b[7] = Gloabal.GDatabase.CreatePara("diffpair", Util.Convert.List2Str(project.DiffPair));
            b[8] = Gloabal.GDatabase.CreatePara("nextpair", Util.Convert.List2Str(project.NextPair));
            b[9] = Gloabal.GDatabase.CreatePara("fextpair", Util.Convert.List2Str(project.FextPair));

            b[10] = Gloabal.GDatabase.CreatePara("reporttempletepath", project.ReportTempletePath);
            b[11] = Gloabal.GDatabase.CreatePara("romfilemode", project.RomFileMode.ToString());
            b[12] = Gloabal.GDatabase.CreatePara("romfilepath", project.RomFilePath);
            b[13] = Gloabal.GDatabase.CreatePara("romwrite", project.RomWrite);
            b[14] = Gloabal.GDatabase.CreatePara("switchfilepath", project.SwitchFilePath);

            b[15] = Gloabal.GDatabase.CreatePara("frespec", project.FreSpec);
            b[16] = Gloabal.GDatabase.CreatePara("frepoints", project.FrePoints);
            b[17] = Gloabal.GDatabase.CreatePara("tdd11", Serializer.Object2Json(project.Tdd11));
            b[18] = Gloabal.GDatabase.CreatePara("tdd22", Serializer.Object2Json(project.Tdd22));
            b[19] = Gloabal.GDatabase.CreatePara("ild", project.Ild.ToString());

            b[20] = Gloabal.GDatabase.CreatePara("skew", project.Skew);
            b[21] = Gloabal.GDatabase.CreatePara("customer", project.Customer);
            b[22] = Gloabal.GDatabase.CreatePara("frespecfilepath", project.FreSpecFilePath);
            b[23] = Gloabal.GDatabase.CreatePara("speed", project.Speed);
            b[24] = Gloabal.GDatabase.CreatePara("producttypel", project.ProductTypeL);


            b[25] = Gloabal.GDatabase.CreatePara("producttyper", project.ProductTypeR);
            b[26] = Gloabal.GDatabase.CreatePara("power", project.Power);
            b[27] = Gloabal.GDatabase.CreatePara("description", project.Description);
            b[28] = Gloabal.GDatabase.CreatePara("calfilepath", project.CalFilePath);
            b[29] = Gloabal.GDatabase.CreatePara("keypoint", project.KeyPoint);
            b[30] = Gloabal.GDatabase.CreatePara("srevert", project.Srevert);
            b[31] = Gloabal.GDatabase.CreatePara("trevert", project.Trevert);
         



                int insertCount = Gloabal.GDatabase.ExecuteSql(insertSql, b);
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
           string querySql = "SELECT ID, PN, Customer, PNCustomer, Length, AWG, Diff, Single, Tdr, DiffPair, " +
                              "NextPair, FextPair, ReportTempletePath, RomFileMode, RomFilePath," +
                              " RomWrite, SwitchFilePath, FreSpec,FreSpecFilePath, FrePoints, Tdd11, Tdd22," +
                              " ILD, Skew,Speed, ProductTypeL, ProductTypeR, " +
                              "Power, Description,CalFilePath,Keypoint,Srevert,Trevert FROM  HPMS_Project Where PN = @pn and isFast is Null";

                IDbDataParameter[] b = new IDbDataParameter[1];
                b[0] = Gloabal.GDatabase.CreatePara("pn", pn);
                DataTable table = Gloabal.GDatabase.GetDataTable(querySql, b);
           

            return GetProjectlistFromTable(table);
        }

        public static List<Project> Findfast()
        {
            string querySql = "SELECT ID, PN, Customer, PNCustomer, Length, AWG, Diff, Single, Tdr, DiffPair, " +
                              "NextPair, FextPair, ReportTempletePath, RomFileMode, RomFilePath," +
                              " RomWrite, SwitchFilePath, FreSpec,FreSpecFilePath, FrePoints, Tdd11, Tdd22," +
                              " ILD, Skew,Speed, ProductTypeL, ProductTypeR, " +
                              "Power, Description,CalFilePath,Keypoint,Srevert,Trevert FROM  HPMS_Project Where isFast ='fast'";
            var table = Gloabal.GDatabase.GetDataTable(querySql);
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
                project.Srevert = GetDbValue(tempRow["Srevert"],false);
                project.Trevert = GetDbValue(tempRow["Trevert"], false);
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
                              " PNCustomer = @pncustomer, Length = @length, AWG = @awg, Diff = @diff, Single = @single," +
                              "Tdr = @tdr,DiffPair = @diffpair,NextPair = @nextpair,FextPair = @fextpair,ReportTempletePath = @reporttempletepath," +
                              "RomFileMode = @romfilemode,RomFilePath = @romfilepath,RomWrite = @romwrite,SwitchFilePath = @switchfilepath,FreSpec = @frespec," +
                              "FrePoints = @frepoints,Tdd11 =@tdd11,Tdd22 = @tdd22,ILD = @ild,Skew = @skew," +
                              "Customer = @customer,FreSpecFilePath = @frespecfilepath, Speed = @speed,ProductTypeL = @producttypel,ProductTypeR = @producttyper," +
                              "Power = @power, Description = @description,CalFilePath = @calfilepath,Keypoint = @keypoint,Srevert=@srevert,Trevert=@trevert where PN = @pn ";

            IDataParameter[] b = new IDataParameter[32];

            b[0] = Gloabal.GDatabase.CreatePara("pn", project.Pn);
            b[1] = Gloabal.GDatabase.CreatePara("pncustomer", project.PnCustomer);
            b[2] = Gloabal.GDatabase.CreatePara("length", project.Length);
            b[3] = Gloabal.GDatabase.CreatePara("awg", project.Awg);
            b[4] = Gloabal.GDatabase.CreatePara("diff", Util.Convert.List2Str(project.Diff));

            b[5] = Gloabal.GDatabase.CreatePara("single", Util.Convert.List2Str(project.Single));
            b[6] = Gloabal.GDatabase.CreatePara("tdr", Util.Convert.List2Str(project.Tdr));
            b[7] = Gloabal.GDatabase.CreatePara("diffpair", Util.Convert.List2Str(project.DiffPair));
            b[8] = Gloabal.GDatabase.CreatePara("nextpair", Util.Convert.List2Str(project.NextPair));
            b[9] = Gloabal.GDatabase.CreatePara("fextpair", Util.Convert.List2Str(project.FextPair));

            b[10] = Gloabal.GDatabase.CreatePara("reporttempletepath", project.ReportTempletePath);
            b[11] = Gloabal.GDatabase.CreatePara("romfilemode", project.RomFileMode.ToString());
            b[12] = Gloabal.GDatabase.CreatePara("romfilepath", project.RomFilePath);
            b[13] = Gloabal.GDatabase.CreatePara("romwrite", project.RomWrite);
            b[14] = Gloabal.GDatabase.CreatePara("switchfilepath", project.SwitchFilePath);

            b[15] = Gloabal.GDatabase.CreatePara("frespec", project.FreSpec);
            b[16] = Gloabal.GDatabase.CreatePara("frepoints", project.FrePoints);
            b[17] = Gloabal.GDatabase.CreatePara("tdd11", Serializer.Object2Json(project.Tdd11));
            b[18] = Gloabal.GDatabase.CreatePara("tdd22", Serializer.Object2Json(project.Tdd22));
            b[19] = Gloabal.GDatabase.CreatePara("ild", project.Ild.ToString());

            b[20] = Gloabal.GDatabase.CreatePara("skew", project.Skew);
            b[21] = Gloabal.GDatabase.CreatePara("customer", project.Customer);
            b[22] = Gloabal.GDatabase.CreatePara("frespecfilepath", project.FreSpecFilePath);
            b[23] = Gloabal.GDatabase.CreatePara("speed", project.Speed);
            b[24] = Gloabal.GDatabase.CreatePara("producttypel", project.ProductTypeL);


            b[25] = Gloabal.GDatabase.CreatePara("producttyper", project.ProductTypeR);
            b[26] = Gloabal.GDatabase.CreatePara("power", project.Power);
            b[27] = Gloabal.GDatabase.CreatePara("description", project.Description);
            b[28] = Gloabal.GDatabase.CreatePara("calfilepath", project.CalFilePath);
            b[29] = Gloabal.GDatabase.CreatePara("keypoint", project.KeyPoint);
            b[30] = Gloabal.GDatabase.CreatePara("srevert", project.Srevert);
            b[31] = Gloabal.GDatabase.CreatePara("trevert", project.Trevert);



            int updateCount = Gloabal.GDatabase.ExecuteSql(updateSql, b);
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

                IDataParameter[] b = new IDataParameter[1];
                b[0] = Gloabal.GDatabase.CreatePara("0", pn);
                int delCount = Gloabal.GDatabase.ExecuteSql(deleteSql, b);
                if (delCount == 1)
                {
                    ret = true;
                }
          
          
            return ret;
        }
    }
}
