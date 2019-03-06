using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Code.Config;
using HPMS.Code.Core;
using HPMS.Code.DB;
using HPMS.Code.Draw;
using HPMS.Code.Equipment;
using HPMS.Code.RightsControl;
using HPMS.Code.Splash;
using HPMS.Code.Utility;
using HPMS.Forms;
using VirtualSwitch;
using HslCommunication.BasicFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tool;

using Convert = HPMS.Code.Utility.Convert;
using NetworkAnalyzer = VirtualVNA.NetworkAnalyzer.NetworkAnalyzer;
using Register = HPMS.Code.RightsControl.Register;

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
            ResetUi();
            Task.Factory.StartNew(SiTest);
           //Task.Factory.StartNew(test);
        }

        private void test()
        {
            for (int i = 0; i < 1500; i++)
            {
              
               
                statusStrip1.BeginInvoke(new Action(() =>
                {
                    toolStripStatusMsg.Text = String.Format("test Num:{0}", i);
                }));
                ResetUi();
                SiTest();
                //Task.Factory.StartNew(SiTest);
            }
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
            ControlSafe.SetCheckbox(chkStop,false);
            //chkStop.Checked = false;
            ControlSafe.SetcontrolEnable(btnTest,false);
            //btnTest.Enabled = false;
            ControlSafe.ClearChecked(chkList_TestItem);
            ControlSafe.ClearChecked(chkList_LossPair);
            ControlSafe.ClearChecked(chkList_NextPair);
            ControlSafe.ClearChecked(chkList_FextPair);
            ControlSafe.ClearChecked(chkList_TestItem);
            ControlSafe.ClearChecked(chkList_LossPair);
            ControlSafe.ClearChecked(chkList_NextPair);
            ControlSafe.ClearChecked(chkList_FextPair);
            ControlSafe.ClearListview(lsvKeyPoint);
           rTextStatus.BeginInvoke(new Action(() => { rTextStatus.Clear();}));
          }

        private bool RegisterCheck()
        {
            string msg = "";
            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!Register.IsAuthorize(softAuthorize.GetMachineCodeString(), "HPTS", ref Gloabal.SoftVersion, ref Gloabal.ExpireDate, ref msg))
            {
                // 显示注册窗口
                Ui.MessageBoxMuti(msg, this);
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
            SITest siTest = new SITest(_switch, _iAnalyzer,_information);

            Savepath savepath=new Savepath();
            if (!SetTestFilePath(ref savepath))
            {
                return;
            }
            
            siTest.DoTest(_testConfigs, _chartDic, _aChart, _formUi, _spec,_keyPoint, savepath);
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
                string pn = Convert.SlashRepalce(txt_PN.Text.Trim());
                string sn=Convert.SlashRepalce(txtSN.Text.Trim());
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
          _keyPoint.Add("SDD21", sdd21.Select(float.Parse).ToArray());
       
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


       

        private DialogResult BlockedMsg()
        {
            var tmp = this.Invoke((Func<DialogResult>)(() =>
            {
                frmPrompt aa=new frmPrompt();

                return aa.ShowDialog();
            }
                   
                //MessageBox.Show("请手动接好开关", "", MessageBoxButtons.YesNoCancel)
                    ));
            return (DialogResult)tmp;
        }
      
        private void PnChangeHandle(string pn)
        {
            ResetUi();
            string msg = "";
            _curretnProject = ProjectHelper.Find(pn);
            _hardware = (Hardware)LocalConfig.GetObjFromXmlFile("config\\hardware.xml", typeof(Hardware));
            if (_curretnProject == null)
            {
                //AddStatus("未能找到对应的料号档案");
                AddStatus(LanguageHelper.GetMsgText("未能找到对应的料号档案"));
                return;
            }
            //AddStatus("成功找到对应的料号档案");
            AddStatus(LanguageHelper.GetMsgText("成功找到对应的料号档案"));
            if (_hardware == null)
            {
                AddStatus("未能找到对应的硬件设置档案");
                return;
            }
            AddStatus("成功找到对应硬件设置档案");
            if (!Util.SetHardware(_hardware, ref _switch, ref _iAnalyzer, ref msg, BlockedMsg))
            {
                AddStatus(msg);
                return;
            }

            _iAnalyzer.LoadCalFile(_curretnProject.CalFilePath,ref msg);
            AddStatus("连接开关成功");
            AddStatus("连接网分设备成功");
            
            bool setProjectFlag = Util.SetTestParams(_curretnProject, ref _testConfigs);
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


        
       

       

       


        private void SetUiAction()
        {
            _formUi.AddStatus = AddStatus;
            _formUi.ProgressDisplay = SetProgress;
            _formUi.SetCheckItem = SetCheckItem;
            _formUi.SetResult = SetResult;
            _formUi.SetKeyPointList = SetKeyPointList;
            _formUi.StopEnabbled = StopEnabbledRead;
        }

        /// <summary>
        /// 更新关键频点信息
        /// </summary>
        /// <param name="keyValue"></param>
        private void SetKeyPointList(Dictionary<string, List<PairData>> keyValue)
        {
            if (lsvKeyPoint.InvokeRequired)
            {
                Action<Dictionary<string, List<PairData>>> d = SetKeyPointList;
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
                            lvi.SubItems.Add((pairItem.XData[i] / 1000000).ToString("F2"));
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


        /// <summary>
        /// 增加测试图形
        /// </summary>
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

        /// <summary>
        /// 显示测试项目及对数
        /// </summary>
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

        /// <summary>
        /// 清除测试项目及对数
        /// </summary>
        private void ClearTestItems()
        {
            chkList_TestItem.Items.Clear();
            chkList_LossPair.Items.Clear();
            chkList_NextPair.Items.Clear();
            chkList_FextPair.Items.Clear();
        }

        /// <summary>
        /// 更新当前测试项目和对数
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="clbType"></param>
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
                Action<string,ClbType> d = SetCheckItem;
                this.Invoke(d, new object[] { itemName, clbType });
            }
            else
            {
                int index = targetCheckedListBox.FindString(itemName);
                targetCheckedListBox.SelectedIndex = index;
                targetCheckedListBox.SetItemChecked(index, true);
                Extende.SelectTab(targetCheckedListBox);
            }

        }

        /// <summary>
        /// 显示测试结果
        /// </summary>
        /// <param name="msg"></param>
        private void SetResult(string msg)
        {
            if (labelResult.InvokeRequired)
            {
                Action<string> d = SetResult;
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                labelResult.Text = msg;
                if (msg == "TEST")
                {
                    labelResult.ForeColor=Color.Blue;
                    labelResult.TextMove = true;
                    labelResult.Blink = true;
                    labelResult.BlinkColor = Color.DarkSeaGreen;
                    labelResult.Interval = 400;
                   
                    
                }
                if (msg == "PASS")
                {
                    labelResult.ForeColor = Color.Green;
                    labelResult.Blink=false;
                }
                if (msg == "FAIL")
                {
                    labelResult.ForeColor = Color.Red;
                    labelResult.Blink = false;
                }
               
            }
        }

        /// <summary>
        /// 测试信息输出
        /// </summary>
        /// <param name="msg"></param>
        private void AddStatus(string msg)
        {
            if (rTextStatus.InvokeRequired)
            {
                Action<string> d = AddStatus;
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                rTextStatus.AppendText(Convert.FormatMsg(msg) + "\n");
            }

        }

        /// <summary>
        /// 测试进度更新
        /// </summary>
        /// <param name="value"></param>
        /// <param name="step"></param>
        private void SetProgress(int value, bool step)
        {
            if (pgbTest.InvokeRequired)
            {
                Action<int, bool> d = SetProgress;
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
            Process.Start("config\\help.chm");
        }

        private void chkList_TestItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(chkList_TestItem.SelectedItem==null)
                return;
            Extende.SelectTab(tabControlChart,chkList_TestItem.SelectedItem.ToString());
          
        }
     
     
    }
}