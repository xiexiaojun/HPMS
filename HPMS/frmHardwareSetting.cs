using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using HPMS.Config;
using HPMS.Util;
using VirtualVNA.Enum;
using ToolTip = System.Windows.Forms.ToolTip;

namespace HPMS
{
    public partial class frmHardwareSetting : Office2007Muti
    {
        public frmHardwareSetting()
        {
           
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
          
            SetTooltip();
            SetAdapterPorts();
            HardwareLoad();
        }

       

     

        private void GetVisaAddress(object sender, MouseEventArgs e)
        {
            TextBoxX sourceBoxX = (TextBoxX) sender;
            frmVisaLists _frmVisaLists = new frmVisaLists(sourceBoxX);
            _frmVisaLists.StartPosition = FormStartPosition.Manual;

            _frmVisaLists.Location = UI.LocationOnClient(sourceBoxX, new System.Drawing.Point(0, 0));
            _frmVisaLists.ShowDialog(this);
        }

     
        private void SetAdapterPorts()
        {
            foreach (string s in Equipment.Util.GetSerialPortsList())
            {
                cmbAdpaterPort.Items.Add(s);
            }
        }

        private void SetTooltip()
        {
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.

            toolTip1.SetToolTip(this.txtNwaVisaAdd, "双击进行编辑");
            toolTip1.SetToolTip(this.txtSbVisaAdd, "双击进行编辑");
        }


      

        private void HardwareSave()
        {
            Hardware hardware=new Hardware();
           
            hardware.Analyzer = (NetworkAnalyzer)Enum.Parse(typeof(NetworkAnalyzer), cmbNwaType.SelectedItem.ToString()); 
            hardware.SwitchBox = (SwitchBox)Enum.Parse(typeof(SwitchBox), cmbSwitchBox.SelectedItem.ToString());
            hardware.VisaNetWorkAnalyzer = txtNwaVisaAdd.Text;
            hardware.VisaSwitchBox = txtSbVisaAdd.Text;
            hardware.Adapter = (Adapter)Enum.Parse(typeof(Adapter), cmbAdapterType.SelectedItem.ToString());
            hardware.AdapterPort = cmbAdpaterPort.Text;
            hardware.SnpFolder = txtSnpSaveFolder.Text;
            hardware.TxtFolder = txtTxtSaveFolder.Text;

            LocalConfig.SaveObjToXmlFile("config\\hardware.xml", hardware);
            


        }

        private void HardwareLoad()
        {
            Hardware hardware = (Hardware) LocalConfig.GetObjFromXmlFile("config\\hardware.xml", typeof(Hardware));
            cmbNwaType.SelectedIndex = cmbNwaType.FindString(hardware.Analyzer.ToString());
            cmbSwitchBox.SelectedIndex = cmbSwitchBox.FindString(hardware.SwitchBox.ToString());
           
            txtNwaVisaAdd.Text=hardware.VisaNetWorkAnalyzer ;
            txtSbVisaAdd.Text=hardware.VisaSwitchBox ;
            cmbAdapterType.SelectedIndex = cmbAdapterType.FindString(hardware.Adapter.ToString());
            
            cmbAdpaterPort.Text=hardware.AdapterPort ;
            txtSnpSaveFolder.Text=hardware.SnpFolder ;
            txtTxtSaveFolder.Text=hardware.TxtFolder ;

        


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            HardwareSave();
            UI.MessageBoxMuti("保存成功");
        }

        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void FolderBrowseCallback( Action<string> action)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                action.Invoke(fbd.SelectedPath);
            }
        }

        private void btnBrowseSnp_Click(object sender, EventArgs e)
        {
            FolderBrowseCallback(delegate(string folderName) { txtSnpSaveFolder.Text = folderName; });
        }

        private void btnBrowseTxt_Click(object sender, EventArgs e)
        {
            FolderBrowseCallback(delegate(string folderName) { txtTxtSaveFolder.Text = folderName; });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
       

       
    }
}
