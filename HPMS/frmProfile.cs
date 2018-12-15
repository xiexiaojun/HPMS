using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Config;
using HPMS.DB;
using HPMS.Log;
using HPMS.Util;
using NationalInstruments.Restricted;
using Newtonsoft.Json;
using Tool;
using _32p_analyze;

namespace HPMS
{
    public partial class frmProfile : Office2007Muti
    {
        private bool _loaded = false;
        private List<Project> _projects;
        public frmProfile()
        {
            EnableGlass = false;
            InitializeComponent();
        
        }

        private void FastProfileLoad()
        {
            _projects = ProjectDao.Findfast();
            foreach (Project project in _projects)
            {
                ButtonX button = new ButtonX();
                button.Text = Tool.Encode.Decrypt(project.Pn);
                button.Style = eDotNetBarStyle.StyleManagerControlled;
                button.ColorTable = eButtonColor.OrangeWithBackground;
                button.Font = new Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                button.AutoSize = true;
                button.Click+=buttonFastProfile_Click;
                flowLayoutPanel_FastprofileBtn.Controls.Add(button);
            }
        }

       

        private void buttonFastProfile_Click(object sender, EventArgs e)
        {
            ButtonX btn = (ButtonX) sender;
            Project findedProject = _projects.First(t => t.Pn == Encode.Encrypt(btn.Text));
            SetCheckListUnchecked();
            SetProject(findedProject);
            txt_Pn.Text = "";
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            if (_loaded )
            {
                return;
            }

            _loaded = true;
            TestItemIni();
            FastProfileLoad();
           
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Text = "";
            }
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel2, true, null);
            tableLayoutPanel4.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel4, true, null);

            cmb_ILDSpec.SelectedIndex = 0;
        }

       

      

      

        private void itemPanel_category_ItemClick(object sender, EventArgs e)
        {
            ButtonItem button = sender as ButtonItem;
            if (button == null)
                return;

            tabControl1.SelectTab("tabPage_" + button.Text);
        }

       
       

        private bool TestItemIni()
        {
            TestItem testItem = (TestItem)LocalConfig.GetObjFromXmlFile("config\\testitem.xml", typeof(TestItem));
            chkList_Diff.Items.Clear();
            chkList_Single.Items.Clear();
            chkList_TDR.Items.Clear();
            chkList_SPair.Items.Clear();
            chkList_NextPair.Items.Clear();
            chkList_FextPair.Items.Clear();
            foreach (string temp in testItem.Diff)
            {
                chkList_Diff.Items.Add(temp, false);
            }
            foreach (string temp in testItem.Single)
            {
                chkList_Single.Items.Add(temp, false);
            }
            foreach (string temp in testItem.Tdr)
            {
                chkList_TDR.Items.Add(temp, false);
            }
            foreach (string temp in testItem.DiffPair)
            {
                chkList_SPair.Items.Add(temp, false);
            }
            foreach (string temp in testItem.NextPair)
            {
                chkList_NextPair.Items.Add(temp, false);
            }
            foreach (string temp in testItem.FextPair)
            {
                chkList_FextPair.Items.Add(temp, false);
            }

            foreach (string temp in testItem.Speed)
            {
                cmbSpeed.Items.Add(temp);
            }
            foreach (string temp in testItem.ProductType)
            {
                cmbTypeL.Items.Add(temp);
                cmbTypeR.Items.Add(temp);
            }
            foreach (string temp in testItem.Power)
            {
                cmbPower.Items.Add(temp);
            }
           
          return true;
        }

        private void btn_SpecFreFileBrowse_Click(object sender, EventArgs e)
        {
           string filter = "表格|*.xls;*.xlsx";//打开文件对话框筛选器
            FileBrowseCallback(filter, delegate(string fileName)
            {
                string strPath = fileName;
                txt_FreSpecFilePath.Text = strPath;
                dgv_SpecFre.Columns.Clear();
                DataTable temp = EasyExcel.GetSpecifiedTable(strPath);
                dgv_SpecFre.DataSource = temp;
                dgv_SpecFre.Columns[0].Name = "";
            });
        }

      

        private void frmProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProfile();
        }

        

        private static bool CheckValueInRange(NumericUpDown numBox,int min,int max)
        {
            return (numBox.Value < max)&&(numBox.Value>min);
        }

        readonly Func<NumericUpDown,int,int,bool> _dFunc=CheckValueInRange;


        private string GetDescription()
        {
            string speed = cmbSpeed.GetValue();
            string productTypeL = cmbTypeL.GetValue();
            string productTypeR = cmbTypeR.GetValue();
            string power = cmbPower.GetValue();
            int awg = (int)num_AWG.Value;
            int length = (int)num_Length.Value;

            return String.Format("{0} {1} to {2} {3} Cable Assembly {4}AWG {5}mm", speed, productTypeL, productTypeR,power,awg,length);
        }

        private void SaveProfile()
        {
            try
            {
                //MessageBox.Show(cmbPower.GetValue());
                Project pnProject = new Project();
                pnProject.Awg = num_AWG.GetNumVali<int>(1,40,_dFunc);
                pnProject.Length = num_Length.GetNumVali<int>(1, 10000, _dFunc);
                pnProject.PnCustomer = txt_PnCustomer.GetTextVali();
                pnProject.Customer = txt_Customer.GetTextVali();
                pnProject.Pn = txt_Pn.GetTextVali();

                
                pnProject.Diff = GetlistboxValue(chkList_Diff);
                pnProject.Single = GetlistboxValue(chkList_Single);
                pnProject.Tdr = GetlistboxValue(chkList_TDR);
                pnProject.DiffPair = GetlistboxValue(chkList_SPair);
                pnProject.NextPair = GetlistboxValue(chkList_NextPair);
                pnProject.FextPair = GetlistboxValue(chkList_FextPair);

                pnProject.ReportTempletePath = txt_ReportTempletePath.GetTextVali();
                pnProject.RomFileMode = GetRomFileMode();
                pnProject.RomWrite = chk_RomWrite.Checked;
                pnProject.RomFilePath = txt_RomFilePath.GetTextVali();
                pnProject.SwitchFilePath = txt_SwitchFilePath.GetTextVali();

                pnProject.FreSpec = Serializer.Datatable2Json((DataTable)dgv_SpecFre.DataSource);
                pnProject.FrePoints = num_FrePoints.GetNumVali<int>(1, 20000, _dFunc);
                pnProject.FreSpecFilePath = txt_FreSpecFilePath.GetTextVali();

                TdrParam[] tdrParamTemp = GetTdrParam();
                pnProject.Tdd11 = tdrParamTemp[0];
                pnProject.Tdd22 = tdrParamTemp[1];
                pnProject.Ild = (IldSpec)Enum.Parse(typeof(IldSpec), cmb_ILDSpec.SelectedItem.ToString(), true);
                pnProject.Skew = (double)num_Skew.Value;

                pnProject.Speed = cmbSpeed.GetValue();
                pnProject.ProductTypeL = cmbTypeL.GetValue();
                pnProject.ProductTypeR = cmbTypeR.GetValue();
                pnProject.Power = cmbPower.GetValue();
                pnProject.Description = GetDescription();
                pnProject.CalFilePath = txt_CalFilePath.Text;
                pnProject.KeyPoint = GetKeypoint();

                string msg = "";
                if (ProjectHelper.Find(pnProject.Pn) != null)
                {
                    var key = Ui.MessageBoxYesNoMuti("确定覆盖当前料号吗?");
                    if (key == DialogResult.Yes)
                    {
                        SaveProfile(pnProject,  true, ref msg);
                     
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    SaveProfile(pnProject,false, ref msg);
                }

                Ui.MessageBoxMuti(msg);
               
               
            }
            catch (Exception e)
            {
               LogHelper.WriteLog("保存料号时参数异常",e);

            }
          

        }

        private bool SaveProfile(Project pnProject,  bool replace,ref string msg)
        {
            bool ret = false;
          
                msg = "保存料号档案到数据库成功";
                if (replace)
                {
                    ret = ProjectDao.Update(pnProject, ref msg);
                }
                else
                {
                    ret = ProjectDao.Add(pnProject, ref msg);
                }
               
          

            return ret;
        }

        private void LoadProfile()
        {
           
                string pn = txt_Pn.GetTextVali();
                Project pnProject = ProjectHelper.Find(pn);
          
                if (pnProject == null)
                {
                    Ui.MessageBoxMuti("未能找到对应的料号档案");
                    return;
                }

                Ui.MessageBoxMuti("成功找到对应的料号档案");
                SetProject(pnProject);
        }

        private void SetProject(Project pnProject)
        {
            num_AWG.Value = pnProject.Awg;
            num_Length.Value = pnProject.Length;
            txt_PnCustomer.Text = pnProject.PnCustomer;
            txt_Customer.Text = pnProject.Customer;
            txt_Pn.Text = pnProject.Pn;

            SetlistboxValue(chkList_Diff, pnProject.Diff);
            SetlistboxValue(chkList_Single, pnProject.Single);
            SetlistboxValue(chkList_TDR, pnProject.Tdr);
            SetlistboxValue(chkList_SPair, pnProject.DiffPair);
            SetlistboxValue(chkList_NextPair, pnProject.NextPair);
            SetlistboxValue(chkList_FextPair, pnProject.FextPair);

            txt_ReportTempletePath.Text = pnProject.ReportTempletePath;
            SetRomFileMode(pnProject.RomFileMode);

            chk_RomWrite.Checked = pnProject.RomWrite;
            txt_RomFilePath.Text = pnProject.RomFilePath;
            txt_SwitchFilePath.Text = pnProject.SwitchFilePath;
            txt_CalFilePath.Text = pnProject.CalFilePath;
            dgv_SpecFre.Columns.Clear();
            dgv_SpecFre.DataSource = Serializer.Json2DataTable(pnProject.FreSpec);
            dgv_SpecFre.Columns[0].Name = "";
            num_FrePoints.Value = pnProject.FrePoints;
            txt_FreSpecFilePath.Text = pnProject.FreSpecFilePath;

            SetTdrParam(new[] { pnProject.Tdd11, pnProject.Tdd22 });
            cmb_ILDSpec.SelectedIndex = cmb_ILDSpec.FindString(pnProject.Ild.ToString());
            num_Skew.Value = (decimal)pnProject.Skew;

            cmbSpeed.SelectedIndex = cmbSpeed.FindString(pnProject.Speed);
            cmbTypeL.SelectedIndex = cmbTypeL.FindString(pnProject.ProductTypeL);
            cmbTypeR.SelectedIndex = cmbTypeR.FindString(pnProject.ProductTypeR);
            cmbPower.SelectedIndex = cmbPower.FindString(pnProject.Power);
            SetKeypoint(pnProject.KeyPoint);



        }


       

        private TdrParam[] GetTdrParam()
        {
            TdrParam[]ret=new TdrParam[2];
            TdrParam tdd11=new TdrParam();
            tdd11.StartTime = (double)num_StartTime1.Value;
            tdd11.EndTime = (double)num_StopTime1.Value;
            tdd11.Points = (int)num_TdrPoint1.Value;
            tdd11.RiseTime = (double)num_RiseTime1.Value;
            tdd11.Offset = (double)num_TdrOffset1.Value;

            tdd11.UperTimePoints = new[] { (double)num_UpperMatingStartTime1.Value, (double)num_UpperCableStartTime1.Value };
            tdd11.UperResi = new[] { (double)num_UpperMatingSpec1.Value, (double)num_UpperCableSpec1.Value };
            tdd11.LowerTimePoints = new[] { (double)num_LowerMatingStartTime1.Value, (double)num_LowerCableStartTime1.Value };
            tdd11.LowerResi = new[] { (double)num_LowerMatingSpec1.Value, (double)num_LowerCableSpec1.Value };


            TdrParam tdd22 = new TdrParam();
            tdd22.StartTime = (double)num_StartTime2.Value;
            tdd22.EndTime = (double)num_StopTime2.Value;
            tdd22.Points = (int)num_TdrPoint2.Value;
            tdd22.RiseTime = (double)num_RiseTime2.Value;
            tdd22.Offset = (double)num_TdrOffset2.Value;

            tdd22.UperTimePoints = new[] { (double)num_UpperMatingStartTime2.Value, (double)num_UpperCableStartTime2.Value };
            tdd22.UperResi = new[] { (double)num_UpperMatingSpec2.Value, (double)num_UpperCableSpec2.Value };
            tdd22.LowerTimePoints = new[] { (double)num_LowerMatingStartTime2.Value, (double)num_LowerCableStartTime2.Value };
            tdd22.LowerResi = new[] { (double)num_LowerMatingSpec2.Value, (double)num_LowerCableSpec2.Value };

            ret[0] = tdd11;
            ret[1] = tdd22;

            return ret;
        }

        private void SetTdrParam(TdrParam[] tdrParams)
        {
          
            num_StartTime1.Value = (decimal)tdrParams[0].StartTime;
            num_StopTime1.Value = (decimal)tdrParams[0].EndTime;
            num_TdrPoint1.Value = tdrParams[0].Points;
            num_RiseTime1.Value = (decimal)tdrParams[0].RiseTime;
            num_TdrOffset1.Value = (decimal)tdrParams[0].Offset;

            num_UpperMatingStartTime1.Value = (decimal)tdrParams[0].UperTimePoints[0];
            num_UpperCableStartTime1.Value = (decimal)tdrParams[0].UperTimePoints[1];
            num_UpperMatingSpec1.Value = (decimal)tdrParams[0].UperResi[0];
            num_UpperCableSpec1.Value = (decimal)tdrParams[0].UperResi[1];
            num_LowerMatingStartTime1.Value = (decimal)tdrParams[0].LowerTimePoints[0];
            num_LowerCableStartTime1.Value = (decimal)tdrParams[0].LowerTimePoints[1];
            num_LowerMatingSpec1.Value = (decimal)tdrParams[0].LowerResi[0];
            num_LowerCableSpec1.Value = (decimal)tdrParams[0].LowerResi[1];


            num_StartTime2.Value = (decimal)tdrParams[1].StartTime;
            num_StopTime2.Value = (decimal)tdrParams[1].EndTime;
            num_TdrPoint2.Value = tdrParams[1].Points;
            num_RiseTime2.Value = (decimal)tdrParams[1].RiseTime;
            num_TdrOffset2.Value = (decimal)tdrParams[1].Offset;

            num_UpperMatingStartTime2.Value = (decimal)tdrParams[1].UperTimePoints[0];
            num_UpperCableStartTime2.Value = (decimal)tdrParams[1].UperTimePoints[1];
            num_UpperMatingSpec2.Value = (decimal)tdrParams[1].UperResi[0];
            num_UpperCableSpec2.Value = (decimal)tdrParams[1].UperResi[1];
            num_LowerMatingStartTime2.Value = (decimal)tdrParams[1].LowerTimePoints[0];
            num_LowerCableStartTime2.Value = (decimal)tdrParams[1].LowerTimePoints[1];
            num_LowerMatingSpec2.Value = (decimal)tdrParams[1].LowerResi[0];
            num_LowerCableSpec2.Value = (decimal)tdrParams[1].LowerResi[1];
           
        }
       

        private RomFileMode GetRomFileMode()
        {
            if (rb_DB.Checked)
            {
                return RomFileMode.DB;
            }

            return RomFileMode.Local;
        }

        private void SetRomFileMode(RomFileMode romFileMode)
        {
            if (romFileMode == RomFileMode.DB)
            {
                rb_DB.Checked = true;
            }
            else
            {
                rb_Local.Checked = true;
            }
        }

        private List<string> GetlistboxValue(CheckedListBox clbBox)
        {
            List<string>ret=new List<string>();
            foreach (var clbBoxSelectedItem in clbBox.CheckedItems)
            {
                ret.Add(clbBoxSelectedItem.ToString());
            }

            return ret;
        }

        private void SetlistboxValue(CheckedListBox clbBox,List<string>selectedValues)
        {
            foreach (var VARIABLE in selectedValues)
            {
                int indexFind = clbBox.FindString(VARIABLE);
                if (indexFind != -1)
                {
                    clbBox.SetItemChecked(indexFind,true);
                }
                
            }
           
        }

        private void chkTdr2Same_CheckValueChanged(object sender, EventArgs e)
        {
            if (chkTdr2Same.Checked)
            {
                num_StartTime2.Value = num_StartTime1.Value;
                num_StopTime2.Value = num_StopTime1.Value;
                num_TdrPoint2.Value = num_TdrPoint1.Value;
                num_RiseTime2.Value = num_RiseTime1.Value;
                num_TdrOffset2.Value = num_TdrOffset1.Value;

                num_UpperMatingStartTime2.Value = num_UpperMatingStartTime1.Value;
                num_UpperCableStartTime2.Value = num_UpperCableStartTime1.Value;
                num_UpperMatingSpec2.Value = num_UpperMatingSpec1.Value;
                num_UpperCableSpec2.Value = num_UpperCableSpec1.Value;
                num_LowerMatingStartTime2.Value = num_LowerMatingStartTime1.Value;
                num_LowerCableStartTime2.Value = num_LowerCableStartTime1.Value;
                num_LowerMatingSpec2.Value = num_LowerMatingSpec1.Value;
                num_LowerCableSpec2.Value = num_LowerCableSpec1.Value;

            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            SetCheckListUnchecked();
            lsvSummary.Items.Clear();
            LoadProfile();
        }

        private void SetCheckListUnchecked()
        {
            for (int i = 0; i < chkList_Diff.Items.Count; i++)
            {
                chkList_Diff.SetItemChecked(i,false);
            }
            for (int i = 0; i < chkList_Single.Items.Count; i++)
            {
                chkList_Single.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkList_TDR.Items.Count; i++)
            {
                chkList_TDR.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkList_SPair.Items.Count; i++)
            {
                chkList_SPair.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkList_NextPair.Items.Count; i++)
            {
                chkList_NextPair.SetItemChecked(i, false);
            }
            for (int i = 0; i < chkList_FextPair.Items.Count; i++)
            {
                chkList_FextPair.SetItemChecked(i, false);
            }
          
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                string pn = txt_Pn.GetTextVali();
                var key = Ui.MessageBoxYesNoMuti("确定删除当前料号吗?");
                if (key == DialogResult.Yes)
                {
                    if (ProjectHelper.Find(pn) != null)
                    {
                        Ui.MessageBoxMuti(DeleteProfile(pn) ? "删除料号成功" : "删除料号失败");
                    }
                    else
                    {
                        Ui.MessageBoxMuti("未能找到对应的料号档案");
                    }

                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
             }
          
          }

        private bool DeleteProfile(string pn)
        {


            return ProjectDao.Delete(pn);





        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowseReport_Click(object sender, EventArgs e)
        {
            string filter = "表格|*.xls;*.xlsx";//打开文件对话框筛选器
            FileBrowseCallback(filter, delegate(string fileName)
            {
                txt_ReportTempletePath.Text = fileName;
            });
        }

        private void btnBrowseRomTemplete_Click(object sender, EventArgs e)
        {
          
            string filter = @"txt|*.txt|bin|*.bin";
            FileBrowseCallback(filter, delegate(string fileName)
            {
                txt_RomFilePath.Text = fileName;
            });
        }

        private void btnBrowseSwitch_Click(object sender, EventArgs e)
        {
            string filter = @"txt|*.txt";
            FileBrowseCallback(filter, delegate(string fileName)
            {
                txt_SwitchFilePath.Text = fileName;
            });
        }

        private void FileBrowseCallback(string filter,Action<string> action)
        {
            OpenFileDialog ofd = new OpenFileDialog {Filter = filter}; 
             if (ofd.ShowDialog() == DialogResult.OK)
            {
               action.Invoke(ofd.FileName);
            }
        }

        private void chkDBMode_CheckedChanged(object sender, EventArgs e)
        {
            ProjectDao.DbMode = chkDBMode.Checked;
        }

        private void btnBrowseCalFile_Click(object sender, EventArgs e)
        {
            string filter = @"CSA|*.csa";
            FileBrowseCallback(filter, delegate(string fileName)
            {
                txt_CalFilePath.Text = fileName;
            });
        }

        private void txt_Pn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery.PerformClick();
            }
        }

        private void btnSummaryAdd_Click(object sender, EventArgs e)
        {
            if (lsvSummary.Items.Count > 20)
            {
                Ui.MessageBoxMuti("最多只能添加20个频点");
                return;
            }

            ListViewItem lvi = new ListViewItem();
            lvi.Text = numSummaryKeypoint.Value.ToString();
            lsvSummary.Items.Add(lvi);
          }

        private void btnSummaryClear_Click(object sender, EventArgs e)
        {
            lsvSummary.Items.Clear();
        }

        private void btnSummaryDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lsvSummary.SelectedItems)  //选中项遍历  
            {
                lsvSummary.Items.RemoveAt(lvi.Index); // 按索引移除  
                //listView1.Items.Remove(lvi);   //按项移除  
            }   
           
        }

        private string GetKeypoint()
        {
            List<string>keyList=new List<string>();
            foreach (ListViewItem lvi in lsvSummary.Items)
            {
                keyList.Add(lvi.Text);
            }

            return JsonConvert.SerializeObject(keyList);
        }

        private void SetKeypoint(string keyList)
        {
            List<string> key =  JsonConvert.DeserializeObject<List<string>>(keyList);
            lsvSummary.Items.Clear();
            if(key==null)
                return;
            foreach (var variable in key)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = variable;
                lsvSummary.Items.Add(lvi);
            }
        }
    }
}
