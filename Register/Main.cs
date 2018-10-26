using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;

namespace Register
{
    public partial class frmRegister : Office2007Form
    {
        SoftAuthorize softAuthorize = new HslCommunication.BasicFramework.SoftAuthorize();
        public frmRegister()
        {
            EnableGlass = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSoftName.SelectedIndex = 0;
            cmbSoftVersion.SelectedIndex = 0;
        }

        private void btnGetMachineCode_Click(object sender, EventArgs e)
        {
            txtMachineCode.Text = softAuthorize.GetMachineCodeString();
        }

        private void btnCalcuCode_Click(object sender, EventArgs e)
        {
            if (txtKey.Text.Trim().Length != 8)
            {
                MessageBoxEx.Show("秘钥必须为8位字母或数字");
                return;
            }
            var sourceJObjectobj = new JObject { 
                { "softName", cmbSoftName.SelectedItem.ToString() }, 
                { "softVersion", cmbSoftVersion.SelectedItem.ToString() },
                {"machineCode",txtMachineCode.Text } };
           
            txtCode.Text = SoftSecurity.MD5Encrypt(sourceJObjectobj.ToString(), txtKey.Text);
            
        }
    }
}
