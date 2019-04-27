using System;
using System.Data;
using HPMS.Code.DB;
using HPMS.Code.Log;
using HPMS.Code.RightsControl;

namespace HPMS.Code.Utility
{
    /// <summary>
    /// 存放全局变量
    /// </summary>

    public class Gloabal
    {
        public static RightsWrapper GRightsWrapper;//权限控制类
        public static User GUser;                   //当前登录用户
        public static string SoftVersion;                  //软件版本
        public static string ExpireDate;                   //过期日期

        public static string freSpecFilePath = @"temp\\Freq Spec.txt";
        public static string timeSpecFilePath = @"temp\\Impedance Spec.txt";
        public static DBUtil.IDbAccess GDatabase;


        private static string sqlServer = "Data Source=172.20.23.107,1433;Initial Catalog=HPMSTest;User ID=sa;Password=data'songyy;";
        private static string sqlite = "Data Source=config\\Local.db;Password=figoba;";

        public static bool SetDb(bool dbModeLocal)
        {
            try
            {
                Gloabal.GDatabase = dbModeLocal ? DBUtil.IDBFactory.CreateIDB(sqlite, "SQLITE") : DBUtil.IDBFactory.CreateIDB(sqlServer, "SQLSERVER");
                TableChange();
                return true;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("database ini error",e);
                throw;
            }
           
        }

        private static bool DbHasColumn(string tableName,string columnName)
        {
            //sqlite查询表字段
            string getColumns = string.Format("pragma table_info('{0}')",tableName);
            var columnTable = GDatabase.GetDataTable(getColumns);
            foreach (DataRow variable in columnTable.Rows)
            {
                if ((string)variable["name"] == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        private static void InsertColumn(string tableName, string columnName, string type)
        {
            string insertColumn = String.Format("ALTER TABLE '{0}' ADD  '{1}' {2}",tableName,columnName,type);
            if (!DbHasColumn(tableName, columnName))
            {
                int a=GDatabase.ExecuteSql(insertColumn);
            }
        }

        private static void TableChange()
        {
            InsertColumn("HPMS_Project", "Srevert", "BOOLEAN");
            InsertColumn("HPMS_Project", "Trevert", "BOOLEAN");
            InsertColumn("HPMS_Project", "Report", "BOOLEAN");
           
        }

    }
}
