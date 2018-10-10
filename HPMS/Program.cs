using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splasher.Show(typeof(frmSplash));
            Application.Run(new Form1());
        }
    }
}
