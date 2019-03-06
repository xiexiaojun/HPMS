using System;
using System.Collections.Generic;
using System.Data;
using HPMS.Code.Utility;

namespace HPMS.Code.DB
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

            string insertSql = "INSERT INTO HPMS_User (UserName, Password, Salt, RoleID,  CreateID, CreateDate, Status) VALUES" +
                               " (@username, @password, @salt, @roleid,  @createid, @createdate, @status)";


            IDataParameter[] b = new IDataParameter[7];
            b[0] = Gloabal.GDatabase.CreatePara("username", user.Username);
            b[1] = Gloabal.GDatabase.CreatePara("password", user.Psw);
            b[2] = Gloabal.GDatabase.CreatePara("salt", "figoba");
            b[3] = Gloabal.GDatabase.CreatePara("roleid", user.RoleId);
            b[4] = Gloabal.GDatabase.CreatePara("createid", user.CreaterId);
            b[5] = Gloabal.GDatabase.CreatePara("createdate", user.CreateDate);
            b[6] = Gloabal.GDatabase.CreatePara("status", (int)user.UserStatus);
              

                int insertCount = Gloabal.GDatabase.ExecuteSql(insertSql, b);
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
            int delCount = Gloabal.GDatabase.ExecuteSql(updateSql);
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
                               " SET UserName = @username,Password =@password,Salt =@salt,RoleID = @roleid," +
                               " CreateID = @createid,CreateDate = @createdate,Status = @status where " +
                               "ID = @id";


            IDataParameter[] b = new IDataParameter[8];
            b[0] = Gloabal.GDatabase.CreatePara("username", user.Username);
            b[1] = Gloabal.GDatabase.CreatePara("password", user.Psw);
                //salt is always figoba currently
            b[2] = Gloabal.GDatabase.CreatePara("salt", "figoba");
            b[3] = Gloabal.GDatabase.CreatePara("roleid", user.RoleId);
                //all users created by this code are always normal users,so isSuper=0.

            b[4] = Gloabal.GDatabase.CreatePara("createid", user.CreaterId);
            b[5] = Gloabal.GDatabase.CreatePara("createdate", user.CreateDate);
            b[6] = Gloabal.GDatabase.CreatePara("status", (int)user.UserStatus);
            b[7] = Gloabal.GDatabase.CreatePara("id", user.UserId);

                int updateCount = Gloabal.GDatabase.ExecuteSql(updateSql, b);
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
                              " where ID = @id";


                IDbDataParameter[] b = new IDbDataParameter[1];
                b[0] = Gloabal.GDatabase.CreatePara("id", userId);
                DataTable table = Gloabal.GDatabase.GetDataTable(querySql, b);
         
            foreach (DataRow tempRow in table.Rows)
            {
                var user = new User();

                user.UserId = (int) tempRow["ID"];
                user.Username = (string) tempRow["UserName"];
                user.Psw = (string) tempRow["Password"];
                user.Role = (string) tempRow["RoleName"];
                user.RoleId = int.Parse(tempRow["RoleID"].ToString());
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
                              " where UserName = @username";

                IDbDataParameter[] b = new IDbDataParameter[1];
                b[0] = Gloabal.GDatabase.CreatePara("username", userName);
                DataTable table = Gloabal.GDatabase.GetDataTable(querySql, b);
          

            foreach (DataRow tempRow in table.Rows)
            {
                var user = new User();

                user.UserId = int.Parse(tempRow["ID"].ToString());
                user.Username = (string)tempRow["UserName"];
                user.Psw = (string)tempRow["Password"];
                user.Role = (string)tempRow["RoleName"];
                user.RoleId = int.Parse(tempRow["RoleID"].ToString());
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


            DataTable dt = Gloabal.GDatabase.GetDataTable(querySql);
            //SqlLite.ExecuteDataTable(querySql);
            return dt;

          
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
                               "@name, @description, @rightsid, @status, @createid, @createdate)";

                IDataParameter[] b = new IDataParameter[6];
                b[0] = Gloabal.GDatabase.CreatePara("name", role.Name);
                b[1] = Gloabal.GDatabase.CreatePara("description", role.Description);
                b[2] = Gloabal.GDatabase.CreatePara("rightsid", role.RightsId);
                b[3] = Gloabal.GDatabase.CreatePara("status", (int)role.RecordStatus);
                b[4] = Gloabal.GDatabase.CreatePara("createid", role.CreateId);
                b[5] = Gloabal.GDatabase.CreatePara("createdate", role.CreateDate);

                int insertCount = Gloabal.GDatabase.ExecuteSql(insertSql, b);
            
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
            int delCount = Gloabal.GDatabase.ExecuteSql(updateSql);
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
                              "SET Name = @name,Description =@description,RightsID = @rights,Status = @status,CreateID = createid,CreateDate = @createdate " +
                              "where ID = @id";

                IDataParameter[] b = new IDataParameter[7];
                b[0] = Gloabal.GDatabase.CreatePara("name", role.Name);
                b[1] = Gloabal.GDatabase.CreatePara("description", role.Description);
                b[2] = Gloabal.GDatabase.CreatePara("rights", role.RightsId);
                b[3] = Gloabal.GDatabase.CreatePara("status", (int)role.RecordStatus);
                b[4] = Gloabal.GDatabase.CreatePara("createid", role.CreateId);
                b[5] = Gloabal.GDatabase.CreatePara("createdate", role.CreateDate);
                b[6] = Gloabal.GDatabase.CreatePara("id", role.RoleId);

                int updateCount = Gloabal.GDatabase.ExecuteSql(updateSql, b);
                if (updateCount == 1)
                {
                    ret = true;
                }
           
            return ret;
        }

        public static DataTable Find()
        {
         
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role Where Status >=0 and isSuper is Null";
            DataTable ret = Gloabal.GDatabase.GetDataTable(querySql);

            return ret;
        }
        public static List<Role> Find(int roleId)
        {
            List<Role> ret = new List<Role>();
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where ID=@id and  Status >=0";

                IDbDataParameter[] b = new IDbDataParameter[1];
                b[0] = Gloabal.GDatabase.CreatePara("id", roleId);
                DataTable table = Gloabal.GDatabase.GetDataTable(querySql,b);
           
            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = int.Parse(tempRow["ID"].ToString()),
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
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where Name=@name and  Status >=0";

            IDbDataParameter[] b = new IDbDataParameter[1];
            b[0] = Gloabal.GDatabase.CreatePara("name", roleName);
            DataTable table = Gloabal.GDatabase.GetDataTable(querySql, b);

            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = int.Parse(tempRow["ID"].ToString()),
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

            DataTable table = Gloabal.GDatabase.GetDataTable(querySql);

            foreach (DataRow tempRow in table.Rows)
            {
                Role role = new Role
                {
                    RoleId = int.Parse(tempRow["ID"].ToString()),
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
            DataTable dataTable =Gloabal.GDatabase.GetDataTable(querySql);
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
            DataTable dataTable = Gloabal.GDatabase.GetDataTable(querySql);
            foreach (DataRow tempRow in dataTable.Rows)
            {
                var right = new Right();

                right.Id = int.Parse(tempRow["ID"].ToString());
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
