using System;
using DevComponents.DotNetBar;
using HPMS.AOP;
using HPMS.DB;

namespace HPMS.RightsControl
{
    /// <summary>
    /// 权限包装类，涉及到权限的操作全部通过此类进入
    /// </summary>
    [PermissonCheck(JoinType.Permission)]
    public class RightsWrapper : ContextBoundObject
    {
        [Permisson("Edit User")]
        public bool EditUser()
        {
            return true;
        }

        /// <summary>
        /// 用户管理模块按钮使能管理
        /// </summary>
        /// <param name="btiItems"></param>
        [Permisson("Edit User")]
        public void SetProfileEdit(ButtonItem[] btiItems)
        {
            foreach (var VARIABLE in btiItems)
            {
                VARIABLE.Enabled = true;
            }
        }

        [Permisson("Edit User")]
        public bool SaveRole(Role role, ref string msg)
        {
            return RoleDao.Add(role, ref msg);
        }

        [Permisson("Edit User")]
        public bool UpdateRole(Role role)
        {
            return RoleDao.Update(role);
        }
        [Permisson("Edit User")]
        public bool DeleteRole(int roleId)
        {
            return RoleDao.Delete(roleId);
        }

        [Permisson("Edit User")]
        public bool SaveUser(User user, ref string msg)
        {
            return UserDao.Add(user, ref msg);
        }
        [Permisson("Edit User")]
        public bool UpdateUser(User user)
        {
            return UserDao.Update(user);
        }
        [Permisson("Edit User")]
        public bool DeleteUser(int userId)
        {
            return UserDao.Del(userId);
        }

        [Permisson("YS-AMS-01-670")]
        public void test()
        {
            MessageBoxEx.Show("OK");
          
           
        }
      }
}
