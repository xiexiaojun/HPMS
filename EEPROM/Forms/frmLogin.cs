using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EEPROM.Code.Entity;
using Tool;

namespace EEPROM.Forms
{
    public partial class FrmLogin : Office2007Muti
    {
        //public User User;
       
        public FrmLogin()
        {
            EnableGlass = false;
           // this.Icon = base.Icon;
            InitializeComponent();
            
           
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        

        }

     

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataType.CurrentTestMode = GetTestMode();
            this.Close();
           // //this.Close();
           // Gloabal.SetDb(chkDBMode.Checked);
           // string userName = txtUser.Text;
            
           //// Gloabal.GDatabase = DBUtil.IDBFactory.CreateIDB("Data Source=.;Initial Catalog=JACKOA;User ID=sa;Password=sa;", "SQLSERVER");
           // List<User> userList = UserDao.Find(userName);
           // if (userList.Count == 1)
           // {
           //     User = userList[0];
           //     if (User.Psw == txtPsw.Text)
           //     {
           //         if (User.UserStatus != RecordStatus.Enable)
           //         {
           //             Ui.MessageBoxMuti("账户已停用或被删除",this);
           //         }
           //         else
           //         {
           //             if (User.IsSuper)
           //             {
           //                 Gloabal.GUser = User;
           //                 this.Close();
           //             }
           //         }

           //     }
           //     else
           //     {
           //         Ui.MessageBoxMuti("用户名或密码错误",this);
           //     }
           // }
           // else
           // {
           //     Ui.MessageBoxMuti("用户名或密码错误");
           // }
           
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private TestMode GetTestMode()
        {
            if (rbtMode_Check.Checked)
            {
                return TestMode.Check;
            }
            else
            {
                return TestMode.Write;
            }

          }

      
       
    }
}
