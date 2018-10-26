using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar;
using HPMS.AOP;

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
      }
}
