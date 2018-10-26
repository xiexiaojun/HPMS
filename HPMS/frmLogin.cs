using System;
using System.Collections.Generic;
using DevComponents.DotNetBar;
using HPMS.DB;

namespace HPMS
{
    public partial class frmLogin : Office2007Form
    {
        private string _softVersion;
        public User User;
       
        public frmLogin(string softVersion)
        {
            _softVersion = softVersion;
            EnableGlass = false;
            InitializeComponent();
            
           
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        

        }

     

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            List<User> userList=UserDao.FindUser(userName);
            if (userList.Count == 1)
            {
                User = userList[0];
                if (User.Psw == txtPsw.Text)
                {
                    this.Close();
                }
            }
            else
            {
                
            }
           
            
        }

      
       
    }
}
