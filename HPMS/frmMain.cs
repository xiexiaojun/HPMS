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
using VirtualSwitch;

using HPMS.Config;
using HPMS.Core;
using HPMS.DB;
using HPMS.Draw;
using HPMS.RightsControl;
using HPMS.Splash;
using HPMS.Util;
using HslCommunication.BasicFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tool;

using Convert = HPMS.Util.Convert;
using NetworkAnalyzer = VirtualVNA.NetworkAnalyzer.NetworkAnalyzer;

namespace HPMS
{
    public partial class frmMain : Office2007Form
    {
       
       // frmHardwareSetting _frmHardwareSetting;
        frmProfile _frmProfile;
        private Dictionary<string,ToolStripMenuItem>styleItems=new Dictionary<string, ToolStripMenuItem>();
        private Theme _theme=new Theme();
        SoftAuthorize softAuthorize = new SoftAuthorize();
        private bool _regFlag = false;
        private User _currentUser;
        private AChart _aChart = null;
        private readonly Dictionary<string, object> _chartDic = new Dictionary<string, object>();
        private Project _curretnProject;
        private Hardware _hardware;
        private TestConfig[] _testConfigs=new TestConfig[4];//3类测试项目参数,直通，近串，远串
        private ISwitch _switch;
        private NetworkAnalyzer _iAnalyzer;
        private Dictionary<string, plotData> _spec = new Dictionary<string, plotData>();//规格线
        private FormUi _formUi;
        
        private readonly Dictionary<string,float[]>_keyPoint=new Dictionary<string, float[]>();//关键频点
        private readonly Dictionary<string,string>_information=new Dictionary<string, string>();//报告信息
        
       
        
       


        public frmMain()
        {
            Splasher.Status = "正在载入";
            Thread.Sleep(1000);
            Splasher.Close();
            EnableGlass = false;
            InitializeComponent();
            GetThemeDic();
            
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


      

       
        private void menuStyle_Click(object sender, EventArgs e)
        {
            var styleClickedItem = (ToolStripMenuItem)sender;
            var currentStyle = styleManager1.ManagerStyle;
            
            if (styleClickedItem.Tag.ToString() == "customerColor")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    var color = colorDialog1.Color;
                    if (StyleManager.IsMetro(currentStyle))
                        StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, color);
                    else
                        StyleManager.ColorTint = color;
                    //StyleManager.ChangeStyle(currentStyle, color);
                    _theme.EStyle = currentStyle;
                    _theme.Color = color.Name;
                    _theme.Tag = styleClickedItem.Tag.ToString();
                    _theme.Customer = true;
                    LocalConfig.SaveTheme(_theme);
                }
            }
            else
            {
                styleItems[currentStyle.ToString()].Checked = false;
                styleClickedItem.Checked = true;
                styleManager1.ManagerStyle = (eStyle)Enum.Parse(typeof(eStyle), styleClickedItem.Tag.ToString());
                _theme.EStyle = styleManager1.ManagerStyle;
                styleManager1.ManagerColorTint = Color.FromName("0");
                _theme.Customer = false;
                _theme.Tag = styleClickedItem.Tag.ToString();
                LocalConfig.SaveTheme(_theme);
            }
        }

        private void GetThemeDic()
        {
            foreach (var VARIABLE in Skin_ToolStripMenuItem.DropDownItems)
            {
                if (!(VARIABLE is ToolStripSeparator))
                {
                    ToolStripMenuItem temp = (ToolStripMenuItem)VARIABLE;
                    styleItems.Add(temp.Tag.ToString(), temp);
                }
              
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
                styleItems[_theme.Tag].Checked = true;
                styleManager1.ManagerStyle = _theme.EStyle;
                if (StyleManager.IsMetro(_theme.EStyle))
                    StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, Color.FromName(_theme.Color));
                else
                    StyleManager.ColorTint = Color.FromName(_theme.Color);
                //StyleManager.ChangeStyle(_theme.EStyle, Color.FromName(_theme.Color));
                //StyleManager.ColorTint = Color.FromName(_theme.Color);
            }
            else
            {
                styleItems[_theme.Tag].Checked = true;
                styleManager1.ManagerStyle = _theme.EStyle;
            }
        }

        #endregion

       

       

        private void m_Set_hardware_Click(object sender, EventArgs e)
        {
          
            frmHardwareSetting frmHardwareSetting = new frmHardwareSetting();
            frmHardwareSetting.ShowDialog();
           
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
            chkStop.Checked = false;

            //MessageBox.Show(Gloabal.GRightsWrapper.EditUser().ToString());
            Thread t = new Thread(new ThreadStart(SiTest));
            t.Start();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           Ini();
         }

        private void Ini()
        {
           
            if(!RegisterCheck())
                return;
            if(!LoginCheck())
                return;
            ResetUi();
            LoadTheme();
            SetStatusBar();
            SetUiAction();
        }

        private void ResetUi()
        {
            //清空测试结果
            SetResult("");
            btnTest.Enabled = false;
        }

        private bool RegisterCheck()
        {
            string msg = "";
            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!Resiter.IsAuthorize(softAuthorize.GetMachineCodeString(), "HPTS", ref Gloabal.SoftVersion, ref Gloabal.ExpireDate, ref msg))
            {
                // 显示注册窗口
                Ui.MessageBoxMuti(msg);
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
                    return false;

                }

                Close();
                return false;

            }

            return true;


        }

        private bool LoginCheck()
        {
                using (frmLogin _frmLogin = new frmLogin())
                {
                    if (_frmLogin.ShowDialog() != DialogResult.OK)
                    {
                        _currentUser = _frmLogin.User;
                        GetUserRights(Gloabal.SoftVersion);
                        return true;
                    }

                    Close();
                    return false;

                }
        }
       
        private void SetStatusBar()
        {
            toolStripStatusLabelUser.Text = "user:" + _currentUser.Username + "  role:" + _currentUser.Role;
            toolStripStatusLabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void GetUserRights(string softVersion)
        {
            JObject tempJObject = JObject.Parse(softVersion);

            List<string> funcList = tempJObject.Properties().Select(t => t.Name).ToList();
            funcList.AddRange(_currentUser.Rights.Select(t => t.Key).ToList());
            Thread.CurrentPrincipal =
                new GenericPrincipal(new GenericIdentity(_currentUser.Username, _currentUser.Role),
                    funcList.ToArray());
        }

        /// <summary>
        /// 注册文件读取
        /// </summary>
        /// <returns></returns>
       

        private void m_Set_user_Click(object sender, EventArgs e)
        {
            using (frmAdmin form =
                new frmAdmin(_currentUser))
            {
                form.ShowDialog();
            
            }
        }


        private void SiTest()
        {
            SITest _siTest = new SITest(_switch, _iAnalyzer,_information);
            string msg = "";
          
            Savepath savepath=new Savepath();
            if (!SetTestFilePath(ref savepath))
            {
                return;
            }
            ControlSafe.SetcontrolEnable(btnTest,false);
            ControlSafe.ClearListview(lsvKeyPoint);
            _siTest.DoTest(_testConfigs, _chartDic, _aChart, _formUi, _spec,_keyPoint, savepath);
            ControlSafe.SetcontrolEnable(btnTest, true);
        }

        private bool SetTestFilePath(ref Savepath savepath)
        {
            if (txtSN.Text.Trim() == "")
            {
                AddStatus("SN不能为空");
                Ui.MessageBoxMuti("SN为空,请输入SN");
                return false;
            }
            else
            {
                string pn = Util.Convert.SlashRepalce(txt_PN.Text.Trim());
                string sn=Util.Convert.SlashRepalce(txtSN.Text.Trim());
                savepath.SnpFilePath = _hardware.SnpFolder + "\\" + pn + "\\" + sn;
                savepath.TxtFilePath = _hardware.TxtFolder + "\\" + pn + "\\" + sn;
                savepath.Sn = sn;
                savepath.XmlPath = _hardware.TxtFolder + "\\" + pn + "\\" + sn + "\\txt\\1\\Result & Sample info.xml";
                savepath.ReportTempletePath = _curretnProject.ReportTempletePath;

                Directory.CreateDirectory(savepath.SnpFilePath);
                Directory.CreateDirectory(savepath.TxtFilePath);
                return true;
            }
        }

        private void SetKeyPoint()
        {
            _keyPoint.Clear();
            List<string> sdd21 = JsonConvert.DeserializeObject<List<string>>(_curretnProject.KeyPoint);
            if (sdd21 == null)
            {
                return;
            }
            float[] SDD21 = new[] { 120000000f, 130000000f, 140000000f, 150000000f };
            float[] SDD11 = new[] { 120000000f, 130000000f, 140000000f, 150000000f };
            _keyPoint.Add("SDD21", sdd21.Select(t=>float.Parse(t)).ToArray());
            //_keyPoint.Add("SDD11", SDD11);
        }

        private delegate void SetKeyPointListCallback(Dictionary<string, List<PairData>> keyValue);
        private void SetKeyPointList(Dictionary<string, List<PairData>> keyValue)
        {
            if (lsvKeyPoint.InvokeRequired)
            {
                SetKeyPointListCallback d=new SetKeyPointListCallback(SetKeyPointList);
                this.BeginInvoke(d, new object[] { keyValue });
            }
            else
            {
                
                lsvKeyPoint.BeginUpdate();
                foreach (var variable in keyValue)
                {
                    ListViewGroup tempGroup = new ListViewGroup();
                    tempGroup.Header = variable.Key;
                    this.lsvKeyPoint.Groups.Add(tempGroup);
                    foreach (var pairItem in variable.Value)
                    {
                        int length = pairItem.XData.Length;
                        for (int i = 0; i < length; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.SubItems.Add(pairItem.PairName);
                            lvi.SubItems.Add((pairItem.XData[i]/1000000).ToString("F2"));
                            lvi.SubItems.Add(pairItem.YData[i].ToString("F2"));
                            tempGroup.Items.Add(lvi);
                            this.lsvKeyPoint.Items.Add(lvi);
                        }
                    }
                }
                lsvKeyPoint.EndUpdate();
            }
        }

        private bool StopEnabbledRead()
        {
            return chkStop.Checked;
        }

       
      


        #region Add charts
        private void SetCharts(Project pnProject)
        {
            foreach (var VARIABLE in pnProject.Diff)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                _chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.Tdr)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                _chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.NextPair)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                _chartDic.Add(VARIABLE, chart);
            }
            foreach (var VARIABLE in pnProject.FextPair)
            {
                object chart = _aChart.ChartAdd(VARIABLE);
                _chartDic.Add(VARIABLE, chart);
            }
        }
        
        #endregion

   

      
        private void PnChangeHandle(string pn)
        {
            ResetUi();
            string msg = "";
            _curretnProject = ProjectHelper.Find(pn);
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

            _iAnalyzer.LoadCalFile(_curretnProject.CalFilePath,ref msg);
            AddStatus("连接开关成功");
            AddStatus("连接网分设备成功");
            
            bool setProjectFlag = Equipment.Util.SetTestParams(_curretnProject, ref _testConfigs);
            if (!setProjectFlag)
            {
                AddStatus("开关对数与测试对数不一致");
                return;
            }

            SetInformation();
            _spec = TestUtil.GetPnSpec(_curretnProject);
            ClearTestItems();
            DisplayTestItems();
            
            AddCharts();
            SetKeyPoint();
            btnTest.Enabled = true;
            txtSN.Focus();
        }


        private void SetInformation()
        {
            _information.Clear();
            _information.Add("application",_currentUser.Username);
            _information.Add("partDescription",_curretnProject.Description);
            _information.Add("partNo",_curretnProject.Pn);
            _information.Add("fixtureSerialNo","No1");
            _information.Add("preparedBy", _currentUser.Username);
            _information.Add("approvedBy", _currentUser.Username);
            _information.Add("temprature","23");
            _information.Add("rHumidity","60");
            _information.Add("tdrModel","None");
            _information.Add("vnaModel","N5224A");
        }


        private void AddCharts()
        {
            _chartDic.Clear();

            _aChart = new VsAChart(this.tabControlChart);
            _aChart.ChartClear();
            foreach (var VARIABLE in _testConfigs)
            {
                foreach (var test in VARIABLE.AnalyzeItems)
                {
                    object chart = _aChart.ChartAdd(test);
                    _chartDic.Add(test, chart); 
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

                if (VARIABLE.Pairs != null)
                {
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
        }


        private void SetUiAction()
        {
            _formUi.AddStatus = AddStatus;
            _formUi.ProgressDisplay = SetProgress;
            _formUi.SetCheckItem = SetCheckItem;
            _formUi.SetResult = SetResult;
            _formUi.SetKeyPointList = SetKeyPointList;
            _formUi.StopEnabbled = StopEnabbledRead;
        }

        private delegate void SetCheckItemCallback(string itemName, ClbType clbType);
        private void SetCheckItem(string itemName,ClbType clbType)
        {
            CheckedListBox targetCheckedListBox=new CheckedListBox();
            switch (clbType)
            {
                case ClbType.TestItem:
                    targetCheckedListBox = chkList_TestItem;
                    break;
                case ClbType.DiffPair:
                    targetCheckedListBox = chkList_LossPair;
                    break;
                case ClbType.NextPair:
                    targetCheckedListBox = chkList_NextPair;
                    break;
                case ClbType.FextPair:
                    targetCheckedListBox = chkList_FextPair;
                    break;
            }
            if (targetCheckedListBox.InvokeRequired)
            {
                SetCheckItemCallback d = new SetCheckItemCallback(SetCheckItem);
                this.Invoke(d, new object[] { itemName, clbType });
            }
            else
            {
                int index = targetCheckedListBox.FindString(itemName);
                targetCheckedListBox.SelectedIndex = index;
                Extende.SelectTab(targetCheckedListBox);
            }

        }

        private delegate void SetResultCallback(string msg);
        private void SetResult(string msg)
        {
            if (labelResult.InvokeRequired)
            {
                SetResultCallback d = new SetResultCallback(SetResult);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                labelResult.Text = msg;
                if (msg == "TEST")
                {
                    labelResult.ForeColor=Color.Blue;
                }
                if (msg == "PASS")
                {
                    labelResult.ForeColor = Color.Green;
                }
                if (msg == "FAIL")
                {
                    labelResult.ForeColor = Color.Red;
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
                rTextStatus.AppendText(Convert.FormatMsg(msg) + "\n");
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
                PnChangeHandle(txt_PN.Text.Trim());
            }
        }

      


        /// <summary>
        /// 设置菜单的兄弟节点check状态，也包括自身
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="status"></param>
        private void SetBrotherMenuStatus(object sender, bool status)
        {
            var styleClickedItem = (ToolStripMenuItem)sender;
            ToolStripMenuItem parentItem = (ToolStripMenuItem)styleClickedItem.OwnerItem;
            foreach (var variable in parentItem.DropDownItems)
            {
                ToolStripMenuItem childItem = (ToolStripMenuItem)variable;
                childItem.Checked = status;
            }
        }

        /// <summary>
        /// 设置菜单的子节点启用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="status"></param>
        private void SetChildMenuEnable(object sender, bool status)
        {
            ToolStripMenuItem parentItem = (ToolStripMenuItem)sender;
            foreach (var variable in parentItem.DropDownItems)
            {
                ToolStripMenuItem childItem = (ToolStripMenuItem)variable;
                childItem.Enabled = status;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTest.PerformClick();
            }
        }

        private void switchMatrixBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string toolPath = @"tools\switchBox\SwitchBoxDebug.exe";
            ExcAttachedTool(toolPath);
        }

        private void eEPROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string toolPath = @"tools\EEPROM\EEPROM Tool.exe";
            ExcAttachedTool(toolPath);
        }

        private void ExcAttachedTool(string toolPath)
        {
            if (!File.Exists(toolPath))
            {
                Ui.MessageBoxMuti("此工具不存在");
                return;
            }
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = toolPath;
            psi.UseShellExecute = false;
            psi.WorkingDirectory = Path.GetDirectoryName(toolPath);
            psi.CreateNoWindow = true;
            Process.Start(psi);
        }

        private void 帮助文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("config\\help.chm");
        }
     
     
    }
}