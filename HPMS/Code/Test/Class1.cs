using System;
using System.Linq;
using HPMS.Code.AOP;

namespace HPMS.Code.Test
{
    /// <summary>
    /// 声明权限的标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissonsAttribute : Attribute
    {
        public string Role { get; set; }

        public PermissonsAttribute(string role)
        {
            this.Role = role;
        }

        //public PermissonAttribute()
        //{
        //}

    }
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileOperations
    {
        /// <summary>
        /// 任何人都可以调用Read
        /// </summary>
        [Permisson("Anyone")]
        public void Read()
        {
        }

        /// <summary>
        /// 只有文件所有者才能Write
        /// </summary>
        [Permisson("Owner")]
        public void Write()
        {
        }
    }

    /// <summary>
    /// 调用操作的工具类
    /// </summary>
    public static class OperationInvoker
    {
        public static void Invoke(object target, string role, string operationName, object[] parameters)
        {
            var targetType = target.GetType();
            var methodInfo = targetType.GetMethod(operationName);
            //Thread.CurrentPrincipal =
            //    new GenericPrincipal(new GenericIdentity("Administrator"),
            //        new[] { "ADMIN" });
            if (methodInfo.IsDefined(typeof(PermissonAttribute), false))
            {
                // 读取出所有权限相关的标记
                var permissons = methodInfo
                    .GetCustomAttributes(typeof(PermissonAttribute), false)
                    .OfType<PermissonAttribute>();
                // 如果其中有满足的权限
                if (permissons.Any(p => p.Role == role))
                {
                    methodInfo.Invoke(target, parameters);
                }
                else
                {
                    throw new Exception(string.Format("角色{0}没有访问操作{1}的权限！", role, operationName));
                }
            }
        }
    }
}
