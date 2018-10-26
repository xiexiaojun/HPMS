using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPMS.DB
{
    public enum RecordStatus
    {
        Enable,
        Disable,
        Del
    }

    public class Right
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Version { set; get; }
        public int Status { set; get; }
    }
    public class User
    {
        public string Username { set; get; }
        public string Psw { set; get; }
        public string Role { set; get; }
        public int RoleId { set; get; }
        public int UserId { set; get; }
        public string[]Rights { set; get; }
        public int CreaterId { set; get; }
        public string CreateDate { set; get; }
        public RecordStatus RecordStatus { set; get; }

     }

    public class Role
    {
        public int RoleId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string RightsId { set; get; }
        public string[]Rights { set; get; }
        public int CreateId { set; get; }
        public string CreateDate { set; get; }
        public RecordStatus RecordStatus { set; get; }
    }
    public class UserDao
    {
        public static bool AddUser(User user,ref string msg)
        {
            bool ret = false;
            if (FindUser(user.Username).Count > 0)
            {
                msg = "same user name already exists";
                return false;
            }
            string insertSql = "INSERT INTO HPMS_User (" +
                               "UserName, Password, Salt, RoleID, isSuper, CreateID, CreateDate, Status" +
                               ") VALUES (" +
                               "?, ?, ?, ?, ?, ?, ?, ?)";
            OleDbParameter[] b = new OleDbParameter[8];
            b[0] = new OleDbParameter("0", user.Username);
            b[1] = new OleDbParameter("1", user.Psw);
            //salt is always figoba currently
            b[2] = new OleDbParameter("2", "figoba");
            b[3] = new OleDbParameter("3", user.RoleId);
            //all users created by this code are always normal users,so isSuper=0.
            b[4] = new OleDbParameter("4", 0);
            b[5] = new OleDbParameter("5", user.CreaterId);
            b[6] = new OleDbParameter("6", user.CreateDate);
            b[7] = new OleDbParameter("7", (int)user.RecordStatus);



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

        public static bool DelUser(int userId)
        {
            bool ret = false;
            //just make a deleted flag,do not delete record really.
            string updateSql = "Update HPMS set Status 0 where ID = "+userId;
            int delCount = DbHelperOleDb.ExecuteSql(updateSql);
            if (delCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static bool UpdateUser(User user)
        {
            bool ret = false;
            string updateSql = "UPDATE HPMS_User" +
                               " SET UserName = ?,Password = ?,Salt = ?,RoleID = ?," +
                               "isSuper = ?,CreateID = ?,CreateDate = ?,Status = ? where" +
                               "ID = ?";
            OleDbParameter[] b = new OleDbParameter[8];
            b[0] = new OleDbParameter("0", user.Username);
            b[1] = new OleDbParameter("1", user.Psw);
            //salt is always figoba currently
            b[2] = new OleDbParameter("2", "figoba");
            b[3] = new OleDbParameter("3", user.RoleId);
            //all users created by this code are always normal users,so isSuper=0.
            b[4] = new OleDbParameter("4", 0);
            b[5] = new OleDbParameter("5", user.CreaterId);
            b[6] = new OleDbParameter("6", user.CreateDate);
            b[7] = new OleDbParameter("7", (int)user.RecordStatus);



            int updateCount = DbHelperOleDb.ExecuteSql(updateSql, b);
            if (updateCount == 1)
            {
                ret = true;
            }
            return ret;
        }

        public static List<User> FindUser(int userId)
        {
            List<User> ret=new List<User>();
            string querySql = "SELECT ID, UserName, Password, Salt, RoleID, " +
                              "isSuper, CreateID, CreateDate,RoleRights, UserStatus,RoleName FROM v_HPMS_User" +
                              " where ID = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", userId);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];
            
            foreach (DataRow tempRow in table.Rows)
            {
                User user = new User
                {
                    UserId = (int) tempRow["ID"],
                    Username = (string)tempRow["UserName"],
                    Psw = (string)tempRow["Password"],
                     Role= (string)tempRow["RoleName"],
                     RoleId= (int)tempRow["RoleID"],
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    Rights = GetRightsById((string)tempRow["RoleRights"]),
                    CreaterId =  (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["UserStatus"]

                };
                ret.Add(user);
            }

            return ret;
        }

        public static List<User> FindUser(string userName)
        {
            List<User> ret = new List<User>();
            string querySql = "SELECT ID, UserName, Password, Salt, RoleID, " +
                              "isSuper, CreateID, CreateDate,RoleRights, UserStatus,RoleName FROM v_HPMS_User" +
                              " where UserName = ?";
            OleDbParameter[] b = new OleDbParameter[1];
            b[0] = new OleDbParameter("0", userName);
            DataSet dataSet = DbHelperOleDb.Query(querySql, b);
            DataTable table = dataSet.Tables[0];

            foreach (DataRow tempRow in table.Rows)
            {
                User user = new User
                {
                    UserId = (int)tempRow["ID"],
                    Username = (string)tempRow["UserName"],
                    Psw = (string)tempRow["Password"],
                    Role = (string)tempRow["RoleName"],
                    RoleId = (int)tempRow["RoleID"],
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    Rights = GetRightsById((string)tempRow["RoleRights"]),
                    CreaterId = (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["UserStatus"]

                };
                ret.Add(user);
            }

            return ret;
        }

        public static string[] GetRightsById(string rightsId)
        {
            List<string>ret=new List<string>();
            string querySql = "select name from HPMS_rights where ID in (" + rightsId + ")" + " and status=1";
            DataTable dataTable = DbHelperOleDb.Query(querySql).Tables[0];
            foreach (DataRow tempRow in dataTable.Rows)
            {
                ret.Add((string)tempRow[0]);
            }

            return ret.ToArray();
        }

        public static List<Right> GetAllRights()
        {
            List<Right> ret = new List<Right>();
            string querySql = "SELECT ID, Name, Description, Version, Status FROM HPMS_Rights";
            DataTable dataTable = DbHelperOleDb.Query(querySql).Tables[0];
            foreach (DataRow tempRow in dataTable.Rows)
            {
                Right right=new Right();
                right.Id = (int)tempRow["ID"];
                right.Name = (string) tempRow["Name"];
                right.Description = (string)tempRow["Description"];
                ret.Add(right);
            }

            return ret;
        }
    }

    public class RoleDao
    {
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
            string updateSql = "Update HPMS_Role set Status 0 where ID = " + roleId;
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

        public static List<Role> Find(int roleId)
        {
            List<Role> ret = new List<Role>();
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where ID=?";
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
                    Rights = UserDao.GetRightsById((string)tempRow["RightsID"]),
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
            string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where Name=?";
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
                    Rights = UserDao.GetRightsById((string)tempRow["RightsID"]),
                    CreateDate = ((DateTime)tempRow["CreateDate"]).ToString(),
                    CreateId = (int)tempRow["CreateID"],
                    RecordStatus = (RecordStatus)(int)tempRow["Status"]

                };
                ret.Add(role);
            }

            return ret;
        }
    }

    
    public class LoginHelper
    {
    }
}
