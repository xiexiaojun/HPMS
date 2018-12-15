using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HPMS.DB;
using HPMS.Util;
using Tool;

namespace HPMS
{
    public partial class frmLogin : Office2007Muti
    {
        private string _softVersion;
        public User User;
       
        public frmLogin(string softVersion)
        {
            _softVersion = softVersion;
            EnableGlass = false;
            this.Icon = base.Icon;
            InitializeComponent();
            
           
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        

        }

     

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //this.Close();
            string userName = txtUser.Text;
            UserDao.DbMode = chkDBMode.Checked;
            List<User> userList = UserDao.Find(userName);
            if (userList.Count == 1)
            {
                User = userList[0];
                if (User.Psw == txtPsw.Text)
                {
                    if (User.UserStatus != RecordStatus.Enable)
                    {
                        Ui.MessageBoxMuti("账户已停用或被删除",this);
                    }
                    else
                    {
                        if (User.IsSuper)
                        {
                            Gloabal.GUser = User;
                            this.Close();
                        }
                    }

                }
                else
                {
                    Ui.MessageBoxMuti("用户名或密码错误",this);
                }
            }
            else
            {
                Ui.MessageBoxMuti("用户名或密码错误");
            }
           
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

      
       
    }
}
