using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace HPMS.DB
{
    public enum RecordStatus
    {
        Disable=0,
        Enable=1,
        Del=-1
    }

    public class Right
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Version { set; get; }
        public RecordStatus RecordStatus { set; get; }
    }
    public class User
    {
        public string Username { set; get; }
        public string Psw { set; get; }
        public string Role { set; get; }
        public int RoleId { set; get; }
        public int UserId { set; get; }
        public Dictionary<string,string>Rights { set; get; }
        public int CreaterId { set; get; }
        public string CreateDate { set; get; }
        public RecordStatus UserStatus { set; get; }
        public bool IsSuper { set; get; }
        public RecordStatus RoleStatus { set; get; }

     }

    public class Role
    {
        public int RoleId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string RightsId { set; get; }
        public Dictionary<string,string>Right{ set; get; }
        public int CreateId { set; get; }
        public string CreateDate { set; get; }
        public RecordStatus RecordStatus { set; get; }
    }
    public class UserDao
    {
        public static bool Add(User user,ref string msg)
        {
            bool ret = false;
            if (Find(user.Username).Count > 0)
            {
                msg = "same user name already exists";
                return false;
            }
            string insertSql = "INSERT INTO HPMS_User (" +
                               "UserName, Password, Salt, RoleID, CreateID, CreateDate, Status" +
                               ") VALUES (" +
                               "?, ?, ?, ?, ?, ?, ?)";
            OleDbParameter[] b = new OleDbParameter[7];
            b[0] = new OleDbParameter("0", user.Username);
            b[1] = new OleDbParameter("1", user.Psw);
            //salt is always figoba currently
            b[2] = new OleDbParameter("2", "figoba");
            b[3] = new OleDbParameter("3", user.RoleId);
            //all users created by this code are always normal users,so isSuper=0.
          
            b[4] = new OleDbParameter("4", user.CreaterId);
            b[5] = new OleDbParameter("5", user.CreateDate);
            b[6] = new OleDbParameter("6", (int)user.UserStatus);



            int insertCount = DbHelperOleDb.ExecuteSql(insertSql, b);
            if (insertCount == 1)
            {
                ret = true;
            }
            else
            {
                msg = "insert new user fail";
            }
            return ret;
        }

        public static bool Del(int userId)
        {
            bool ret = false;
            //just make a deleted flag,do not delete record really.
            string updateSql = "Update HPMS_User set Status = -1 where ID = " + userId;
            int delCount = DbHelperOleDb.ExecuteSql(updateSql);
            if (delCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static bool Update(User user)
        {
            bool ret = false;
            string updateSql = "UPDATE HPMS_User" +
                               " SET UserName = ?,Password = ?,Salt = ?,RoleID = ?," +
                               " CreateID = ?,CreateDate = ?,Status = ? where " +
                               "ID = ?";
            OleDbParameter[] b = new OleDbParameter[8];
            b[0] = new OleDbParameter("0", user.Username);
            b[1] = new OleDbParameter("1", user.Psw);
            //salt is always figoba currently
            b[2] = new OleDbParameter("2", "figoba");
            b[3] = new OleDbParameter("3", user.RoleId);
            //all users created by this code are always normal users,so isSuper=0.
           
            b[4] = new OleDbParameter("5", user.CreaterId);
            b[5] = new OleDbParameter("6", user.CreateDate);
            b[6] = new OleDbParameter("7", (int)user.UserStatus);
            b[7] = new OleDbParameter("8", user.UserId);



            int updateCount = DbHelperOleDb.ExecuteSql(updateSql, b);
            if (updateCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static List<User> Find(int userId)
        {
            List<User> ret=new List<User>();
            string querySql = "SELECT ID, UserName, Password, Salt, RoleID, " +
                              "isSuper, CreateID, CreateDate,RoleRights, UserStatus,RoleName,RoleStatus FROM v_HPMS_User" +
                              " where ID = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", userId);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];
            
            foreach (DataRow tempRow in table.Rows)
            {
                var user = new User();

                user.UserId = (int) tempRow["ID"];
                user.Username = (string) tempRow["UserName"];
                user.Psw = (string) tempRow["Password"];
                user.Role = (string) tempRow["RoleName"];
                user.RoleId = (int) tempRow["RoleID"];
                user.CreateDate = ((DateTime) tempRow["CreateDate"]).ToString();
                user.Rights = RightDao.GetRightsById((string) tempRow["RoleRights"]);
                user.CreaterId = (int) tempRow["CreateID"];
                user.UserStatus = (RecordStatus) (int) tempRow["UserStatus"];
                user.IsSuper = tempRow["isSuper"] != null;
                user.RoleStatus = (RecordStatus)(int)tempRow["RoleStatus"];

               
                ret.Add(user);
            }

            return ret;
        }

        public static List<User> Find(string userName)
        {
            List<User> ret = new List<User>();
            string querySql = "SELECT ID, UserName, Password, Salt, RoleID, " +
                              "isSuper, CreateID, CreateDate,RoleRights, UserStatus,RoleName,RoleStatus FROM v_HPMS_User" +
                              " where UserName = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", userName);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
                var user = new User();

                user.UserId = (int)tempRow["ID"];
                user.Username = (string)tempRow["UserName"];
                user.Psw = (string)tempRow["Password"];
                user.Role = (string)tempRow["RoleName"];
                user.RoleId = (int)tempRow["RoleID"];
                user.CreateDate = ((DateTime)tempRow["CreateDate"]).ToString();
                user.Rights = RightDao.GetRightsById((string)tempRow["RoleRights"]);
                user.CreaterId = (int)tempRow["CreateID"];
                user.UserStatus = (RecordStatus)(int)tempRow["UserStatus"];
                user.IsSuper = tempRow["isSuper"] != null;
                user.RoleStatus = (RecordStatus)(int)tempRow["RoleStatus"];

                ret.Add(user);
            }

            return ret;
        }

        public static DataTable Find()
        {

            string querySql = "SELECT ID, UserName, Password, Salt, RoleID, " +
                              " CreateID, CreateDate,RoleRights, UserStatus,RoleName,RoleStatus FROM v_HPMS_User" +
                              " where isSuper is Null and UserStatus >=0 ";
          
           
            DataSet dataSet = DbHelperOleDb.Query(querySql);
            return dataSet.Tables[0];

          
        }
    }

    public class RoleDao
    {
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Add(Role role,ref string msg)
        {
            bool ret = false;
            if (Find(role.Name).Count > 0)
            {
                msg = "same role name already exists";
                return false;
            }
            string insertSql = "INSERT INTO HPMS_Role (" +
                               "Name, Description, RightsID, Status, CreateID, CreateDate" +
                               ") VALUES (" +
                               "?, ?, ?, ?, ?, ?)";
            OleDbParameter[] b = new OleDbParameter[6];
            b[0] = new OleDbParameter("0", role.Name);
            b[1] = new OleDbParameter("1", role.Description);
            b[2] = new OleDbParameter("2", role.RightsId);
            b[3] = new OleDbParameter("3", (int)role.RecordStatus);
            b[4] = new OleDbParameter("4", role.CreateId);
            b[5] = new OleDbParameter("5", role.CreateDate);
        
            int insertCount = DbHelperOleDb.ExecuteSql(insertSql, b);
            if (insertCount == 1)
            {
                ret = true;
            }
            else
            {
                msg = "insert new role fail";
            }
            return ret;
        }

        public static bool Delete(int roleId)
        {
            bool ret = false;
            //just make a deleted flag,do not delete record really.
            string updateSql = "Update HPMS_Role set Status = -1 where ID = " + roleId;
            int delCount = DbHelperOleDb.ExecuteSql(updateSql);
            if (delCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static bool Update(Role role)
        {
            bool ret = false;
            string updateSql ="UPDATE HPMS_Role " +
                              "SET Name = ?,Description =?,RightsID = ?,Status = ?,CreateID = ?,CreateDate = ? " +
                              "where ID = ?";
            OleDbParameter[] b = new OleDbParameter[7];
            b[0] = new OleDbParameter("0", role.Name);
            b[1] = new OleDbParameter("1", role.Description);
            b[2] = new OleDbParameter("2", role.RightsId);
            b[3] = new OleDbParameter("3", (int)role.RecordStatus);
            b[4] = new OleDbParameter("4", role.CreateId);
            b[5] = new OleDbParameter("5", role.CreateDate);
            b[6] = new OleDbParameter("6", role.RoleId);
         
            int updateCount = DbHelperOleDb.ExecuteSql(updateSql, b);
            if (updateCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static DataTable Find()
        {
         
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role Where Status >=0 and isSuper is Null";
          
            DataSet dataSet = DbHelperOleDb.Query(querySql);
            DataTable ret  = dataSet.Tables[0];

            return ret;
        }
        public static List<Role> Find(int roleId)
        {
            List<Role> ret = new List<Role>();
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where ID=? and  Status >=0";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", roleId);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = (int)tempRow["ID"],
                    Name = (string)tempRow["Name"],
                    Description = (string)tempRow["Description"],
                    RightsId = (string)tempRow["RightsID"],
                    Right = RightDao.GetRightsById((string)tempRow["RightsID"]),
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    CreateId = (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["Status"]

                };
                ret.Add(role);
            }

            return ret;
        }

        public static List<Role> Find(string roleName)
        {
            List<Role> ret = new List<Role>();
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where Name=? and  Status >=0";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", roleName);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = (int)tempRow["ID"],
                    Name = (string)tempRow["Name"],
                    Description = (string)tempRow["Description"],
                    RightsId = (string)tempRow["RightsID"],
                    Right = RightDao.GetRightsById((string)tempRow["RightsID"]),
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    CreateId = (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["Status"]

                };
                ret.Add(role);
            }

            return ret;
        }

        public static List<Role> FindAll()
        {
            List<Role> ret = new List<Role>();
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where  Status =1 and isSuper is Null";
           
            DataSet dataSet = DbHelperOleDb.Query(querySql);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = (int)tempRow["ID"],
                    Name = (string)tempRow["Name"],
                    Description = (string)tempRow["Description"],
                    RightsId = (string)tempRow["RightsID"],
                    Right = RightDao.GetRightsById((string)tempRow["RightsID"]),
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    CreateId = (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["Status"]

                };
                ret.Add(role);
            }

            return ret;
        }
    }

    public class RightDao
    {
        #region 根据权限ID获取对应的权限

        public static Dictionary<string,string> GetRightsById(string rightsId)
        {
            var ret = new Dictionary<string,string>();
            string querySql = "select name,Description from HPMS_rights where ID in (" + rightsId + ")" + " and status=1";
            DataTable dataTable = DbHelperOleDb.Query(querySql).Tables[0];
            foreach (DataRow tempRow in dataTable.Rows)
            {
                ret.Add((string)tempRow[0], (string)tempRow[1]);
            }

            return ret;
        }

        #endregion

        #region 获取数据库中存在的所有权限

        /// <summary>
        /// 获取数据库中存在的所有权限
        /// </summary>
        /// <returns></returns>
        public static List<Right> GetAllRights()
        {
            List<Right> ret = new List<Right>();
            string querySql = "SELECT ID, Name, Description, Version, Status FROM HPMS_Rights";
            DataTable dataTable = DbHelperOleDb.Query(querySql).Tables[0];
            foreach (DataRow tempRow in dataTable.Rows)
            {
                var right = new Right();

                right.Id = (int)tempRow["ID"];
                right.Name = (string)tempRow["Name"];
                right.Description = (string)tempRow["Description"];
                right.Version = (string)tempRow["Version"];
                right.RecordStatus = (RecordStatus)(int)tempRow["Status"];
                ret.Add(right);
            }

            return ret;
        }

        #endregion
    }
    
    public class LoginHelper
    {
    }
}
