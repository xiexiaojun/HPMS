using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Config;
using HPMS.Languange;
using HPMS.Util;
using _32p_analyze;
using TabControl = System.Windows.Forms.TabControl;

namespace HPMS
{
    public partial class frmProfile : Office2007Muti
    {
        private bool _loaded = false;
        public frmProfile()
        {
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            if (_loaded )
            {
                return;
            }

            _loaded = true;
            TestItemIni();
            string[] tabStyles = { "General", "Project", "SpecFrequency", "SpecTime", "Wizard" };

            itemPanel_category.BeginUpdate();
            int i = 0;
            foreach (string style in tabStyles)
            {
                ButtonItem button = new ButtonItem(style, style);
                button.OptionGroup = "TabStyle"; // This provides automatic Checked property management.
                itemPanel_category.Items.Add(button);
                //if (style == tabControl1.Style.ToString())
                //    button.Checked = true;
                i++;
            }
            itemPanel_category.EndUpdate();
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

       

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void itemPanel_category_ItemClick(object sender, EventArgs e)
        {
            ButtonItem button = sender as ButtonItem;
            if (button == null)
                return;

            tabControl1.SelectTab("tabPage_" + button.Name);
        }

        private void textBoxX19_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar); 
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {

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
            
           
          return true;
        }

        private void btn_SpecFreFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();//首先根据打开文件对话框，选择excel表格
            ofd.Filter = "表格|*.xls;*.xlsx";//打开文件对话框筛选器
            string strPath;//文件完整的路径名
            if (ofd.ShowDialog() == DialogResult.OK)
            {
               
                    strPath = ofd.FileName;
                    dgv_SpecFre.Columns.Clear();
                    DataTable temp=EasyExcel.GetSpecifiedTable(strPath);
                    string aa = Serializer.SerializeDataTableXml(temp);
                    dgv_SpecFre.DataSource = Serializer.DeserializeDataTable(aa);

              
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {

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

        private void SaveProfile()
        {
            Project pnProject=new Project();
           
            pnProject.Awg = txt_AWG.Text;
            pnProject.Length = double.Parse(txt_Length.Text);
            pnProject.PnCustomer = txt_PnCustomer.Text;
            pnProject.Customer = txt_Customer.Text;
            pnProject.Pn = txt_Pn.Text;

            pnProject.Diff = GetlistboxValue(chkList_Diff);
            pnProject.Single = GetlistboxValue(chkList_Single);
            pnProject.Tdr = GetlistboxValue(chkList_TDR);
            pnProject.DiffPair = GetlistboxValue(chkList_SPair);
            pnProject.NextPair = GetlistboxValue(chkList_NextPair);
            pnProject.Diff = GetlistboxValue(chkList_FextPair);

            pnProject.ReportTempletePath = txt_ReportTempletePath.Text;
            pnProject.RomFileMode = GetRomFileMode();
            pnProject.RomWrite = chk_RomWrite.Checked;
            pnProject.RomFilePath = txt_RomFilePath.Text;
            pnProject.SwitchFilePath = txt_SwitchFilePath.Text;

            pnProject.FreSpec = (DataTable)dgv_SpecFre.DataSource;
            pnProject.FrePoints = (int)num_FrePoints.Value;
            pnProject.FreSpecFilePath = txt_FreSpecFilePath.Text;

            TdrParam[] tdrParamTemp = GetTdrParam();
            pnProject.Tdd11 = tdrParamTemp[0];
            pnProject.Tdd22 = tdrParamTemp[1];
            pnProject.Ild = (IldSpec)Enum.Parse(typeof(IldSpec), cmb_ILDSpec.SelectedItem.ToString(), true);
            pnProject.Skew = (double) num_Skew.Value;

            string savePath = "config\\PN.xml";
            LocalConfig.SaveObjToXmlFile(savePath, pnProject);
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
       

        private RomFileMode GetRomFileMode()
        {
            if (rb_DB.Checked)
            {
                return RomFileMode.DB;
            }
            else
            {
                return RomFileMode.Local;
            }
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
    }
}
