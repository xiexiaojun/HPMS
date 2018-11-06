using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Util;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;

namespace HPMS
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
            if (IsAuthorize(txtMachineCode.Text, "HPTS", ref softVersion, txtCode.Text))
            {
                MessageBoxEx.Show("注册成功"+Environment.NewLine+"您注册的是:"+softVersion+"版");
                File.WriteAllText(Application.StartupPath + @"\license.lic", txtCode.Text);

                _regFlag = true;
                Close();
            }
            else
            {
                MessageBoxEx.Show("注册码无效","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           }

        private bool IsAuthorize(string machineCode,string softName,ref string softVersion,string regCode)
        {
            bool ret = false;
            string regJson = SoftSecurity.MD5Decrypt(regCode, "bayuejun");
            try
            {
                JObject tempJObject = JObject.Parse(regJson.ToString());
                ret= (tempJObject.Property("machineCode").Value.ToString() == machineCode)||
                     tempJObject.Property("softName").Value.ToString() == machineCode;
                softVersion = tempJObject.Property("softVersion").Value.ToString();
            }
            catch (Exception e)
            {
              
            }

            return ret;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
