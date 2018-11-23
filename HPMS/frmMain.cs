using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

using VirtualSwitch;
using DevComponents.DotNetBar;
using HPMS.Config;
using HPMS.Core;
using HPMS.DB;
using HPMS.Draw;
using HPMS.Languange;
using HPMS.RightsControl;
using HPMS.Splash;
using HPMS.Util;
using HslCommunication.BasicFramework;
using VirtualSwitch;
//using VirtualSwitch;
using VirtualVNA.Enum;
using VirtualVNA.NetworkAnalyzer;
using Convert = HPMS.Util.Convert;

namespace HPMS
{
    public partial class frmMain : Office2007Form
    {
        private readonly Dictionary<string, ToolStripMenuItem> styleItems = new Dictionary<string, ToolStripMenuItem>();
        frmHardwareSetting _frmHardwareSetting;
        frmProfile _frmProfile;
        private Theme _theme=new Theme();
        SoftAuthorize softAuthorize = new SoftAuthorize();
        private bool _regFlag = false;
        private User currentUser;
        private AChart _aChart = null;
        private Dictionary<string, object> chartDic = new Dictionary<string, object>();
        private bool _pnChange = false;
        private Project _curretnProject;
        private Hardware _hardware;
        private TestConfig[] _testConfigs=new TestConfig[3];//3类测试项目参数,直通，近串，远串
        private ISwitch _switch;
        private INetworkAnalyzer _iAnalyzer;
        private Dictionary<string, plotData> _spec = new Dictionary<string, plotData>();//规格线
       

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
            if (_theme == null)
            {
                return;
            }
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

      

        private void btnTest_Click(object sender, EventArgs e)
        {

           // MessageBox.Show(Gloabal.GRightsWrapper.EditUser().ToString());
            Thread t = new Thread(new ThreadStart(SiTest));
            t.Start();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RegisterLoginCheck();
            SetStatusBar();

        }

        /// <summary>
        /// registration & login check
        /// </summary>
        private void RegisterLoginCheck()
        {
            #region register check


            string softVersion = "";
            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!Resiter.IsAuthorize(softAuthorize.GetMachineCodeString(), "HPTS", ref softVersion))
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



            #endregion

            #region Login check

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
                        return;
                    }

                }

            }

            #endregion
        }

        private void SetStatusBar()
        {
            toolStripStatusLabelUser.Text = "当前用户:" + currentUser.Username + "  角色:" + currentUser.Role;
            //toolStripStatusLabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void GetUserRights()
        {
            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity(currentUser.Username, currentUser.Role),
                    currentUser.Rights.Select(t=>t.Key).ToArray());
        }

        /// <summary>
        /// 注册文件读取
        /// </summary>
        /// <returns></returns>
       

        private void m_Set_user_Click(object sender, EventArgs e)
        {
            using (frmAdmin form =
                new frmAdmin(currentUser))
            {
                form.ShowDialog();
            
            }
        }


        private void SiTest()
        {
            Action<string> addAction = AddStatus;
            Action<int, bool> progressDisplay = SetProgress;
            SITest siTest = new SITest(_switch, _iAnalyzer);
            AddStatus("测试开始");
            siTest.DoTest(_testConfigs, chartDic, _aChart, addAction, progressDisplay, "B:", _spec);

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

   

      
        private void PnChangeHandle(string pn,bool dbMode)
        {
            string msg = "";
            btnTest.Enabled = false;
            _curretnProject = ProjectHelper.Find(pn, dbMode);
            _hardware = (Hardware)LocalConfig.GetObjFromXmlFile("config\\hardware.xml", typeof(Hardware));
            if (_curretnProject == null)
            {
                AddStatus("未能找到对应的料号档案");
                
                return;
            }
            AddStatus("成功找到对应的料号档案");
            if (_hardware == null)
            {
                AddStatus("未能找到对应的硬件设置档案");
                return;
            }
            AddStatus("成功找到对应硬件设置档案");
            if (!Equipment.Util.SetHardware(_hardware,ref _switch,ref _iAnalyzer,ref msg))
            {
                AddStatus(msg);
                return;
            }
            AddStatus("连接开关成功");
            AddStatus("连接网分设备成功");
            bool setProjectFlag = Equipment.Util.SetTestParams(_curretnProject, ref _testConfigs);
            if (!setProjectFlag)
            {
                AddStatus("开关对数与测试对数不一致");
                return;
            }

            _spec = TestUtil.GetPnSpec(_curretnProject);
            ClearTestItems();
            DisplayTestItems();
            AddCharts();
            btnTest.Enabled = true;
        }

       


        private void AddCharts()
        {
            chartDic.Clear();

            _aChart = new ZedAChart(this.tabControlChart);
            _aChart.ChartClear();
            foreach (var VARIABLE in _testConfigs)
            {
                foreach (var test in VARIABLE.AnalyzeItems)
                {
                    object chart = _aChart.ChartAdd(test);
                    chartDic.Add(test, chart); 
                }
            }
          
        }
       

        private void ClearTestItems()
        {
            chkList_TestItem.Items.Clear();
            chkList_LossPair.Items.Clear();
            chkList_NextPair.Items.Clear();
            chkList_FextPair.Items.Clear();
        }

        private void DisplayTestItems()
        {
            foreach (var VARIABLE in _testConfigs)
            {
                foreach (string s in VARIABLE.AnalyzeItems)
                {
                    chkList_TestItem.Items.Add(s);
                }

                foreach (Pair pair in VARIABLE.Pairs)
                {
                    switch (VARIABLE.ItemType)
                    {
                        case ItemType.Loss:
                            chkList_LossPair.Items.Add(pair.PairName);
                            break;
                        case ItemType.Next:
                            chkList_NextPair.Items.Add(pair.PairName);
                            break;
                        case ItemType.Fext:
                            chkList_FextPair.Items.Add(pair.PairName);
                            break;
                    }
                }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void txt_PN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PnChangeHandle(txt_PN.Text.Trim(), false);
            }
        }

       

       
       
     
    }
}