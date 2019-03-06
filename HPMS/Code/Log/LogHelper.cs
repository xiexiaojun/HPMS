using System;
using System.Diagnostics;
using System.Reflection;

namespace HPMS.Code.Log
{
    /// <summary>  
    /// LogHelper的摘要说明。   
    /// </summary>   
    public class LogHelper
    {
        /// <summary>
        /// 静态只读实体对象info信息
        /// </summary>
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");
        /// <summary>
        ///  静态只读实体对象error信息
        /// </summary>
        public static readonly log4net.ILog Logerror = log4net.LogManager.GetLogger("logerror");

        /// <summary>
        /// 加载配置
        /// </summary>
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog(string info)
        {
            try
            {
                if (Loginfo.IsInfoEnabled)
                {
                    string codeInfo = getFileName();
                    Loginfo.Info(codeInfo+"\nmsg:"+info);
                    
                }
            }
            catch { }
        }


        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLog(string info, Exception ex)
        {
            try
            {
                if (Logerror.IsErrorEnabled)
                {
                    string codeInfo = getFileName();
                    Logerror.Error(codeInfo + "\nmsg:" + info, ex);
                }
            }
            catch { }
        }

        public static string getFileName()
        {
            StackTrace trace = new StackTrace(true);
           
            StackFrame frame = trace.GetFrame(2);//1代表上级，2代表上上级，以此类推
            MethodBase method = frame.GetMethod();
            int lineNumber = frame.GetFileLineNumber();
            String className = method.ReflectedType.Name;
            string fileName = frame.GetFileName();
            return ("fileName:" + fileName + "\nClassName:" + className + "\nMethodName:" + method.Name + "\nlineNumber:" + lineNumber);
        }

        /// <summary>
        /// @Author:      HTL
        /// @Email:       Huangyuan413026@163.com
        /// @DateTime:    2015-06-03 19:54:49
        /// @Description: 获取当前堆栈的上级调用方法列表,直到最终调用者,只会返回调用的各方法,而不会返回具体的出错行数，可参考：微软真是个十足的混蛋啊！让我们跟踪Exception到行把！（不明真相群众请入） 
        /// </summary>
        /// <returns></returns>
        static string GetStackTraceModelName()
        {
            //当前堆栈信息
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            //过虑的方法名称,以下方法将不会出现在返回的方法调用列表中
            string _filterdName = "ResponseWrite,ResponseWriteError,";
            string _fullName = string.Empty, _methodName = string.Empty;
            for (int i = 1; i < sfs.Length; ++i)
            {
                //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                _methodName = sfs[i].GetMethod().Name;//方法名称
                //sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
                if (_filterdName.Contains(_methodName)) continue;
                _fullName = _methodName + "()->" + _fullName;
            }
            st = null;
            sfs = null;
            _filterdName = _methodName = null;
            return _fullName.TrimEnd('-', '>');
        }
    }
}
