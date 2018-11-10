using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Log;
using HPMS.Splash;
using Application = System.Windows.Forms.Application;

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
            //绑定程序中的异常处理
            #if Release
                        BindExceptionHandler();     
            #endif
            MessageBoxEx.EnableGlass = false;
            Softgroup.NetResize.License.LicenseName = "figoba1";
            Softgroup.NetResize.License.LicenseUser = "figoba@gmail1.com";
            Softgroup.NetResize.License.LicenseKey = "FWAQB8CVZ9GDUBBUCRICXU9WE";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splasher.Show(typeof(frmSplash));
            Application.Run(new frmMain());
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
            LogHelper.WriteLog(null, e.Exception);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.WriteLog(null, e.ExceptionObject as Exception);
        }
    }
}
