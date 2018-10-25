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
using TabControl = System.Windows.Forms.TabControl;

namespace HPMS
{
    public partial class frmProfile : Office2007Form
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

            foreach (Control VARIABLE in Controls) LanguageHelper.SetControlLanguageText(VARIABLE);
            // this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabPage1.Width, this.tabPage1.Height));
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

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
    }
}
