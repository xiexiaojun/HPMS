using System;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HslCommunication.BasicFramework;
using Tool;
using Register = HPMS.Code.RightsControl.Register;

namespace HPMS.Forms
{
    public partial class frmRegist : Office2007Muti
    {
        SoftAuthorize softAuthorize = new HslCommunication.BasicFramework.SoftAuthorize();
        public bool _regFlag = false;
        public string _softVersion = "";
        public frmRegist()
        {
            
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmRegist_Load(object sender, EventArgs e)
        {
         
            txtMachineCode.Text = softAuthorize.GetMachineCodeString();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            string softVersion = "";
            string expireDate = "";
            string msg = "";
            if (Register.IsAuthorize(txtCode.Text,txtMachineCode.Text ,"HPTS", ref softVersion,ref expireDate,ref msg ))
            {
                MessageBoxEx.Show("注册成功"+Environment.NewLine+"您注册的是:"
                                  +softVersion+"版"+Environment.NewLine+"注册有效期:"+expireDate);
                File.WriteAllText(Application.StartupPath + @"\license.lic", txtCode.Text);

                _regFlag = true;
                Close();
            }
            else
            {
                MessageBoxEx.Show("注册码无效","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
