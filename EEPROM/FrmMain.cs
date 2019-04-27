using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EEPROM.Code.Entity;
using EEPROM.Code.Utility;
using EEPROM.Code.View;
using EEPROM.Forms;
using EEPROMUtility;
using Tool;
using VirtualIIC;
using Action = System.Action;
using RomMapConvert;
using RomMapConvert.Impl;

namespace EEPROM
{
    
    public partial class FrmMain : Office2007Muti
    {
        private const string XmlFilePath = "config\\config.xml";//配置文件路径


        //private TestMode _testMode;//当前测试模式
        private Ii2c _i2cDevice;//I2C Adapter
        private Burn _burn;//烧录
        private RomConvert _romConvert = new RomConvert();
        private LabelConfig _labelConfig;//标签配置
        private byte[][] _snTemplate;//填充后的模板文件
        private byte[][] _readValue;//读出的写入值
        /// <summary>
        /// 软件配置
        /// </summary>
        private ProjectConfig _projectConfig=new ProjectConfig();//
        

        public FrmMain()
        {
            EnableGlass = false;
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AddDataGridRow(dgvRead);
            SetResult("");
            LoginCheck();
            ModeSet();
            ConfigIni();
            ReadConfig();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            ResizeDataGridView(dgvTemplate);
            ResizeDataGridView(dgvRead);
            ResizeListView(lsvData);
        }

        private void ResizeDataGridView(DataGridView dgv)
        {
            foreach (DataGridViewTextBoxColumn a in dgv.Columns)
            {
                a.FillWeight = 6;
                
            }

            dgv.RowTemplate.Height = (dgv.Height-20) / 16-3;
        }

        private void AddDataGridRow(DataGridView dgv)
        {
            dgv.Rows.Add(15);
           // dgv.RowTemplate.Height = 50;
        }

        private void ResizeListView(ListView lsv)
        {
            foreach (ColumnHeader columnHeader in lsv.Columns)
            {
                columnHeader.Width = (int)(lsv.Width * 0.14);
            }
        }
      
            private bool LoginCheck()
        {
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() != DialogResult.OK)
                {
                    //_currentUser = frmLogin.User;
                    //GetUserRights(Gloabal.SoftVersion);
                    return true;
                }

                //Close();
                return false;

            }
        }

        private void ModeSet()
        {
            labelMain.Text = DataType.CurrentTestMode.GetDescription().ToUpper();
            if (DataType.CurrentTestMode == TestMode.Write)
            {
                labelMain.ForeColor = Color.Red;
            }
            
            
            statusMode.Text = DataType.CurrentTestMode.GetChinese();
            this.Text="当前模式:"+ DataType.CurrentTestMode.GetChinese();

        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            statusTime.Text = DateTime.Now.ToString("G");
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (GetSnTemplate())
            {
                SetProgress(0, false);
                SetResult("TEST");
                EEPROMTest(DataType.CurrentTestMode);
            }
           
        }

        private void EEPROMTest(TestMode testMode)
        {
           // FormUI.SetControlState(btnTest, false);
            Task<TestResult> task=null;
                switch (DataType.CurrentTestMode)
                {
                    case TestMode.Check:
                        task = Task<TestResult>.Factory.StartNew(Check);
                        break;
                    case TestMode.Write:
                        task = Task<TestResult>.Factory.StartNew(Write);
                        break;
                    default:
                        break;
                }

                task.ContinueWith(SaveRecord);
          
           
        }


        private void txtPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PnChangeHandle(txtPN.Text.Trim());
            }
        }

        private void PnChangeHandle(string pn)
        {
            
            ResetUi();
            _burn = new Burn(pn, _i2cDevice);
            _burn.OnXmlGet += _burn_OnXmlGet;
            _burn.OnPortRead += _burn_OnPortRead;
            _burn.OnPortWrite += _burn_OnPortWrite;
            _burn.OnI2CDeviceClose += _burn_OnI2CDeviceClose;
            _burn.OnI2CDeviceOpen += _burn_OnI2CDeviceOpen;
            _burn.OnProgressChange += _burn_OnProgressChange;
            _burn.DownloadConfig();
            
            AddStatus("料号档案下载成功");
            GetPnConfig(pn);


        }

        private void _burn_OnPortWrite(string arg1, bool arg2, int arg3)
        {
            AddStatus(arg1 + " port:" + arg3);
        }

        private void _burn_OnProgressChange(int obj)
        {
            SetProgress(obj,false);
        }

        private void _burn_OnI2CDeviceOpen(string obj)
        {
            AddStatus(obj);
        }

        private void _burn_OnI2CDeviceClose(string obj)
        {
            AddStatus(obj);
        }

        private void _burn_OnPortRead(string arg1, bool arg2, int arg3)
        {
            AddStatus(arg1+" port:"+arg3);
        }

        private void _burn_OnXmlGet(System.Collections.Generic.List<ParameterList> arg1, string arg2)
        {
            AddStatus(arg2);
        }

        private bool GetPnConfig(string pn)
        {
            bool result = false;
            string labelMsg = "";
            _labelConfig = _romConvert.GetLabelConfig(pn, ref result, ref labelMsg);
            if (result)
            {
                AddStatus("标签配置下载成功");
                if (_labelConfig.TwoSn)
                {
                    txtSNExtend.Visible = true;
                    AddStatus("该料号有两个sn,请注意");
                }
                var template = _labelConfig.TempeleteBytes.Rotate();
                
                string templateFile = "template\\" + pn + ".txt";
                Extension.Write2DArray(template, templateFile);
                AddStatus("下载料号模板文件成功,路径:"+templateFile);
                btnTest.Enabled = true;
                txtSN.Focus();
            }
            else
            {
                AddStatus("标签配置下载失败");
            }

            return result;
        }

        private void ResetUi()
        {
            //清空测试结果
            FormUI.SetResult(lblResult,"");
            txtSNExtend.Visible = false;
            btnTest.Enabled = false;
            rtStatus.Clear();
            //rtStatus.BeginInvoke(new Action(() => { rtStatus.Clear(); }));
        }

        private void AddStatus(string msg)
        {
            FormUI.AddStatus(rtStatus,msg);
            FormUI.AddStatus(statusMsg, msg);
        }

        private void SetResult(string result)
        {
            FormUI.SetResult(lblResult,result);
        }

        private void SetProgress(int value,bool step)
        {
            FormUI.SetProgress(pgbTest, value,step);
        }

       

        private bool GetSnTemplate()
        {
            string sn = "";
            if (txtSN.Text.Trim() == "")
            {
                AddStatus("sn不能为空");
                return false;
            }

            sn = txtSN.Text.Trim();
            if (_labelConfig.TwoSn)
            {
                if (txtSNExtend.Text.Trim() == "")
                {
                    AddStatus("sn第二段不能为空");
                    return false;
                }

                sn = sn + txtSNExtend.Text.Trim();
            }

            if (sn.Length != _labelConfig.LabelLength)
            {
                AddStatus("条码长度不对,应为:"+ _labelConfig.LabelLength);
                return false;
            }

            try
            {
                _snTemplate = SnChangeHandle(sn);
                return true;
            }
            catch (Exception e)
            {
             AddStatus(e.Message);
             return false;
            }
            
        }

        private byte[][] SnChangeHandle(string sn)
        {
            var data= _romConvert.GetRomMapCross(_labelConfig, sn);
            AddStatus(String.Format("sn:{0}对应的模板生成成功",sn));
            return data;
        }

        private TestResult Write()
        {
            TestResult ret = new TestResult();
            try
            {
                var writeResult= _burn.WriteData(_snTemplate);
                ret = Check();
                ret.WriteResult = writeResult;
                ret.TempalteValue = _snTemplate;
                return ret;

            }
            catch (Exception e)
            {
                //MessageBoxEx.Show(e.Message);
                ret.HasException = true;
                return ret;
            }
            
        }

        private TestResult Check()
        {
            TestResult ret=new TestResult();
            try
            {
                byte[][] readData = null;
                ret.ReadResult = _burn.ReadData(ref readData);
               
                var checkResult = _burn.CheckData(_snTemplate, readData);
                ret.CheckResult = checkResult.Port.ToArray();
                ret.ReadValue = readData;
                ret.TempalteValue = _snTemplate;
                
                return ret;
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(e.Message);
                ret.HasException = true;
                return ret;
            }
          
        }

        private void SaveRecord(Task<TestResult> t)
        {
            var a = t.Result;
            
            SetResult(!a.HasException&&a.CheckResult.Aggregate((t1,t2)=>t1&&t2) ? "PASS" : "FAIL");
            _readValue = a.ReadValue;
            //FormUI.SetGrid(dgvTemplate, _readValue[0]);
            //FormUI.SetGrid(dgvRead,_readValue[0]);
            var mm=_burn.GetKeyInformation(_readValue);
            FormUI.SetListView(lsvData, Combine(mm,a.CheckResult));
            FormUI.SetControlState(btnTest,true);
        }

        private string[][] Combine(string[][]array1,bool[]result)
        {
            int length = result.Length;
            string[][]ret=new string[length][];
            for (int i = 0; i < length; i++)
            {
                List<string> temp = new List<string>();
                temp.AddRange(array1[i]);
                temp.Add(result[i]?"PASS":"FAIL");
                ret[i] = temp.ToArray();
            }

            return ret;
        }

        private void ReadConfig()
        {
            _projectConfig = ProjectHelper.GetProjectConfig(XmlFilePath);
            cmbAdpter.SelectedIndex = (int)_projectConfig.Adapter;
            cmbCom.SelectedIndex = cmbCom.FindString(_projectConfig.Com);
            cmbWriteBlock.SelectedIndex = cmbWriteBlock.FindString(_projectConfig.WriteBlock.ToString());
            cmbWriteDelay.SelectedIndex= cmbWriteDelay.FindString(_projectConfig.WriteDelay.ToString());
            chkLabelPrint.Checked = _projectConfig.Print;
            numLabelNum.Value = _projectConfig.LabelNum;
            SetAdapter();
        }

        private void SetAdapter()
        {
            switch (_projectConfig.Adapter)
            {
                case BurnAdapter.GYI:
                    _i2cDevice=new VCI_GYI2C(GYI2CType.DEV_GY7604,0,_projectConfig.WriteBlock);
                    break;
                case BurnAdapter.CP2102:
                    _i2cDevice=new LuxshareIi2C(_projectConfig.Com,_projectConfig.WriteBlock,20);
                    break;
                case BurnAdapter.CP2112:
                    _i2cDevice=new CP2112(1,20,(uint)_projectConfig.WriteBlock,_projectConfig.WriteDelay);
                    break;
                default:
                    break;
            }
        }

        private void SaveConfig()
        {
            _projectConfig.Adapter = (BurnAdapter) cmbAdpter.SelectedIndex;
            _projectConfig.Com = cmbCom.SelectedItem.ToString();
            _projectConfig.WriteBlock=int.Parse(cmbWriteBlock.SelectedItem.ToString());
            _projectConfig.WriteDelay= int.Parse(cmbWriteDelay.SelectedItem.ToString());
            _projectConfig.Print = chkLabelPrint.Checked;
            _projectConfig.LabelNum = (int)(numLabelNum.Value);
            if (_projectConfig.Adapter != BurnAdapter.CP2112&&_projectConfig.Com== "无串口")
            {
                MessageBoxEx.Show("串口端口选择错误");
                return;
            }
            if (ProjectHelper.SaveProjectConfig(XmlFilePath, _projectConfig))
            {
                FormUI.AddStatus(statusMsg,"保存配置文件成功");
                SetAdapter();
                _burn?.ChangeAdapter(_i2cDevice);
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void ConfigIni()
        {
            foreach (string s in Extension.GetSerialPortsList())
            {
                cmbCom.Items.Add(s);
            }

            cmbCom.SelectedIndex = 0;
            cmbAdpter.SelectedIndex = 0;
            cmbWriteBlock.SelectedIndex = 0;
            cmbWriteDelay.SelectedIndex = 0;
        }
    }
}
