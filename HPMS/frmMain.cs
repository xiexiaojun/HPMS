using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Config;
using HPMS.Core;
using HPMS.DB;
using HPMS.Draw;
using HPMS.Languange;
using HPMS.Log;
using HPMS.RightsControl;
using HPMS.Splash;
using HPMS.Test;
using HPMS.Util;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;
using Convert = HPMS.Util.Convert;

namespace HPMS
{
    public partial class frmMain : Office2007Form
    {
        private readonly Dictionary<string, ToolStripMenuItem> styleItems = new Dictionary<string, ToolStripMenuItem>();
        frmHardwareSetting _frmHardwareSetting;
        frmProfile _frmProfile;
        private Theme _theme=new Theme();
        SoftAuthorize softAuthorize = new HslCommunication.BasicFramework.SoftAuthorize();
        private bool _regFlag = false;
        private User currentUser;
        private AChart _aChart = null;
        private Dictionary<string, object> chartDic = new Dictionary<string, object>();
        private bool _pnChange = false;
       

        public frmMain()
        {
            Splasher.Status = "正在展示相关的内容";
            System.Threading.Thread.Sleep(3000);
            System.Threading.Thread.Sleep(50);

            Splasher.Close();
           
            EnableGlass = false;
            InitializeComponent();
            //Splasher.Status = "正在展示相关的内容";
            //System.Threading.Thread.Sleep(3000);
            //System.Threading.Thread.Sleep(50);

            //Splasher.Close();
            StyleMenuAdd();
            LoadTheme();
            Gloabal.GRightsWrapper=new RightsWrapper();
        }


        #region Languange & Style

        private void LanguangeMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem) sender;
            中文ToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
            toolStripMenuItem.Checked = true;
            var languangeSourceFile = toolStripMenuItem.Text == "中文" ? "Resources/lang/中文.json" : "Resources/lang/english.json";
            var msgSourceFile = toolStripMenuItem.Text == "中文" ? null : "Resources/lang/englishMsg.json";
            LanguageHelper.SetResources(languangeSourceFile, msgSourceFile);
            foreach (Control VARIABLE in Controls) LanguageHelper.SetControlLanguageText(VARIABLE);
        }

        private void StyleMenuAdd()
        {
            foreach (eStyle suit in Enum.GetValues(typeof(eStyle)))
            {
                //添加菜单一 
                ToolStripMenuItem subItem;
                subItem = AddContextMenu(suit.ToString(), m_Set_Style.DropDownItems, StyleMenuClicked);
                styleItems.Add(suit.ToString(), subItem);
               }

            ToolStripMenuItem subItemCustomer;
            AddContextMenu("-", m_Set_Style.DropDownItems, null);
            AddContextMenu("自定义", m_Set_Style.DropDownItems, StyleMenuClicked);
        }

        private ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                var tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }

            if (!string.IsNullOrEmpty(text))
            {
                var tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);
                return tsmi;
            }

            return null;
        }

        private void StyleMenuClicked(object sender, EventArgs e)
        {
            var styleClickedItem = (ToolStripMenuItem) sender;
            var currentStyle = styleManager1.ManagerStyle;
            if (styleClickedItem.Tag.ToString() == "自定义TAG")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    var color = colorDialog1.Color;
                    StyleManager.ChangeStyle(currentStyle, color);
                    _theme.EStyle = currentStyle;
                    _theme.Color = color.Name;
                   
                    _theme.Customer = true;
                    LocalConfig.SaveTheme(_theme);
                }
            }
            else
            {
                styleItems[currentStyle.ToString()].Checked = false;
                styleClickedItem.Checked = true;
                styleManager1.ManagerStyle = (eStyle) Enum.Parse(typeof(eStyle), styleClickedItem.Text);
                _theme.EStyle = styleManager1.ManagerStyle;
                styleManager1.ManagerColorTint = Color.FromName("0");
                _theme.Customer = false;
                LocalConfig.SaveTheme(_theme);
            }
        }

        private void LoadTheme()
        {
            _theme = LocalConfig.LoadTheme();
            if (_theme.Customer)
            {
                StyleManager.ChangeStyle(_theme.EStyle, Color.FromName(_theme.Color));
            }
            else
            {
                styleManager1.ManagerStyle = _theme.EStyle;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity("Administrator","Figo"),
                    new[] { "ADMIN","Add"});

            //var role = "Anyone";
            //var operation = new FileOperations();
            //// 可以正常调用Read
            //OperationInvoker.Invoke(operation, role, "Read", null);
            //// 但是不能调用Write
            //OperationInvoker.Invoke(operation, role, "Write", null);

            CalculatorHandler bb=new CalculatorHandler();
           double kk= bb.Add(3, 5);
            MessageBox.Show(kk.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            int a = 8;
            int b = 0;
            int c = a / b;
            for (int i = 0; i < 10000; i++)
            {
                LogHelper.WriteLog("AAA");
                LogHelper.WriteLog("BBB", new Exception("Test")); 
            }
           
        }

        private void m_Set_hardware_Click(object sender, EventArgs e)
        {
          

            if (_frmHardwareSetting == null || _frmHardwareSetting.IsDisposed)
            {
                _frmHardwareSetting = new frmHardwareSetting {StartPosition = FormStartPosition.CenterParent};
                _frmHardwareSetting.ShowDialog();
            }
            else
            {
                _frmHardwareSetting.WindowState = FormWindowState.Normal;
                _frmHardwareSetting.ShowDialog();
            }
           
        }

       

      

       

        private void m_Set_profile_Click(object sender, EventArgs e)
        {
            if (_frmProfile == null || _frmProfile.IsDisposed)
            {
                _frmProfile = new frmProfile { StartPosition = FormStartPosition.CenterParent };
                _frmProfile.ShowDialog();
            }
            else
            {
                _frmProfile.WindowState = FormWindowState.Normal;
                _frmProfile.ShowDialog();
            }
           
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = true; //托盘图标隐藏
            }
            if (this.WindowState == FormWindowState.Minimized)//最小化事件
            {
                this.Hide();//最小化时窗体隐藏
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal; //还原窗体 
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

           // MessageBox.Show(Gloabal.GRightsWrapper.EditUser().ToString());
            Thread t = new Thread(new ThreadStart(test));
            t.Start();

            //for (int i = 0; i < 100; i++)
            //{
            //    pgbTest.Value = i+1;
            //    Thread.Sleep(50);
            //}
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string code = ReadCode();
            string softVersion = "";


            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!IsAuthorize(softAuthorize.GetMachineCodeString(), "HPTS", ref softVersion, code))
            {
                // 显示注册窗口

                using (frmRegist form =
                    new frmRegist())
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        _regFlag = form._regFlag;
                    }
                }

                if (_regFlag)
                {
                    Application.ExitThread();
                    Application.Exit();
                    Application.Restart();
                    Process.GetCurrentProcess().Kill();  
                  
                }
                else
                {
                    Close();
                }
               
            }
            else
            {
                using (frmLogin _frmLogin = new frmLogin(softVersion))
                {
                    if (_frmLogin.ShowDialog() != DialogResult.OK)
                    {
                        currentUser = _frmLogin.User;
                        GetUserRights();
                    }
                    else
                    {
                        Close();
                    }
                    
                }

                _aChart = new VsAChart(this.tabControlChart);
                string[] testItems = { "SDD21", "SDD11", "SCD11" };
                foreach (var VARIABLE in testItems)
                {
                    object chart = _aChart.ChartAdd(VARIABLE);
                    chartDic.Add(VARIABLE, chart);
                }
              
            }
          
        }

        private void GetUserRights()
        {
            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity(currentUser.Username, currentUser.Role),
                    currentUser.Rights.Select(t=>t.Key).ToArray());
        }

        private string ReadCode()
        {
            string ret = "";
            try
            {
                ret = File.ReadAllText(Application.StartupPath + @"\license.lic");
            }
            catch (Exception e)
            {
                
            }

            return ret;
        }

        private bool IsAuthorize(string machineCode, string softName, ref string softVersion, string regCode)
        {
            bool ret = false;
           
            try
            {
                string regJson = SoftSecurity.MD5Decrypt(regCode, "bayuejun");
                JObject tempJObject = JObject.Parse(regJson.ToString());
                ret = (tempJObject.Property("machineCode").Value.ToString() == machineCode) ||
                      tempJObject.Property("softName").Value.ToString() == machineCode;
                softVersion = tempJObject.Property("softVersion").Value.ToString();
            }
            catch (Exception e)
            {
               
            }

            return ret;
        }

        private void m_Set_user_Click(object sender, EventArgs e)
        {
            using (frmAdmin form =
                new frmAdmin(currentUser))
            {
                form.ShowDialog();
                //if (form.ShowDialog() != DialogResult.OK)
                //{
                //    _regFlag = form._regFlag;
                //}
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(test));
            t.Start();
          
        }

        private void test()
        {
            Action<string> addAction = AddStatus;
            Action<int, bool> progressDisplay = SetProgress;
            SITest a = new SITest();
            for (int i = 0; i < 1000; i++)
            {
                AddStatus("第"+i+"次测试开始");
                a.Start(chartDic, _aChart, addAction, progressDisplay); 
                Thread.Sleep(10000);
            }
            
        }


        private void GetTestItems(Project pnProject)
        {
          

           }


        #region Add charts
        private void SetCharts(Project pnProject)
        {
            foreach (var VARIABLE in pnProject.Diff)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.Tdr)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.NextPair)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.FextPair)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                chartDic.Add(VARIABLE, chart);
            }
        }
        
        #endregion

        #region different test Items and pairs display
        private void DiffIni(Project pnProject)
        {
            lsbTestItems.Items.Clear();
            foreach (var VARIABLE in pnProject.Diff)
            {
                lsbTestItems.Items.Add(VARIABLE);
            }
            foreach (var VARIABLE in pnProject.Tdr)
            {
                lsbTestItems.Items.Add(VARIABLE);
            }
            lsbTestPairs.Items.Clear();
            foreach (var VARIABLE in pnProject.DiffPair)
            {
                lsbTestPairs.Items.Add(VARIABLE);
            }
        }
        
        #endregion

        #region Next test Items and pairs display
        private void NextIni(Project pnProject)
        {
            lsbTestItems.Items.Clear();
            foreach (var VARIABLE in pnProject.NextPair)
            {
                lsbTestItems.Items.Add(VARIABLE);
            }
            foreach (var VARIABLE in pnProject.NextPair)
            {
                lsbTestPairs.Items.Add(VARIABLE);
            }
        }

        #endregion

        #region Fext test Items and pairs display
        private void FextIni(Project pnProject)
        {
            lsbTestItems.Items.Clear();
            foreach (var VARIABLE in pnProject.FextPair)
            {
                lsbTestItems.Items.Add(VARIABLE);
            }
           
            foreach (var VARIABLE in pnProject.FextPair)
            {
                lsbTestPairs.Items.Add(VARIABLE);
            }
        }

        #endregion

        private void txt_PN_TextChanged(object sender, EventArgs e)
        {
            _pnChange = true;
        }

        private void txt_PN_Leave(object sender, EventArgs e)
        {
            if (_pnChange)
            {
                MessageBox.Show("aaa");
                _pnChange = false;
            }
        }

        private delegate void SetaddStatusCallback(string msg);
        private void AddStatus(string msg)
        {
            if (rTextStatus.InvokeRequired)
            {
                SetaddStatusCallback d = new SetaddStatusCallback(AddStatus);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                rTextStatus.AppendText(Convert.formatMsg(msg) + "\n");
            }

        }

        delegate void SetProgressCallback(int value, bool step);
        private void SetProgress(int value, bool step)
        {
            if (pgbTest.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { value, step });
            }
            else
            {
                if (step)
                {
                    pgbTest.Value = pgbTest.Value + value;
                }
                else
                {
                    pgbTest.Value = value;
                }

            }
        }

        private void rTextStatus_TextChanged(object sender, EventArgs e)
        {
            rTextStatus.SelectionStart = rTextStatus.Text.Length;
            rTextStatus.ScrollToCaret();
        }
       
     
    }
}