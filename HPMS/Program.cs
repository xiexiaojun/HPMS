using System;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Code.Log;
using HPMS.Code.Splash;
using Tool;
using Application = System.Windows.Forms.Application;
using frmSplash = HPMS.Code.Splash.frmSplash;

namespace HPMS
{
    static class Program
    {
       

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
           //AppDomain.CurrentDomain.AssemblyResolve+=CurrentDomain_AssemblyResolve;
            //绑定程序中的异常处理
            #if Publish
                        BindExceptionHandler();     
            #endif
            MessageBoxEx.EnableGlass = false;
           Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splasher.Show(typeof(frmSplash));
            try
            {
                Application.Run(new frmMain());
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("用户取消了注册");
            }
           
            
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            return Assembly.LoadFrom("dll");
        }

        private static void BindExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Ui.MessageBoxMuti(e.Exception.Message);
            LogHelper.WriteLog(null, e.Exception);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Ui.MessageBoxMuti(e.ExceptionObject.ToString());
            LogHelper.WriteLog(null, e.ExceptionObject as Exception);
        }
    }
}
