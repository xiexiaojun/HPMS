using System;
using System.Windows.Forms;
using HPMS.Util;
using Tool;

namespace HPMS
{
    public partial class frmPswModify : Office2007Muti
    {
        public frmPswModify()
        {
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmPswModify_Load(object sender, EventArgs e)
        {

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (PswValidate())
            {
                PswModify();
            }

           
        }

        private void PswModify()
        {
            var key = Ui.MessageBoxYesNoMuti("确定修改密码吗");
            if (key == DialogResult.Yes)
            {
                Gloabal.GUser.Psw = txtNewPsw.Text;
                if (Gloabal.GRightsWrapper.UpdateUser(Gloabal.GUser))
                {
                    Ui.MessageBoxMuti("修改密码成功");
                }
                else
                {
                    Ui.MessageBoxMuti("修改密码失败");
                }

            }
        }

        private bool PswValidate()
        {
            if (txtOldPsw.Text.Trim().Equals(""))
            {
                Ui.MessageBoxMuti("原密码不能为空");
                return false;
            }
            if (txtNewPsw.Text.Trim().Equals(""))
            {
                Ui.MessageBoxMuti("新密码不能为空");
                return false;
            }
            if (txtNewPsw.Text.Trim().Length >= 18)
            {
                Ui.MessageBoxMuti("密码长度不能超过18位");
                return false;
            }
            if (txtNewPswR.Text.Trim() != txtNewPsw.Text.Trim())
            {
                Ui.MessageBoxMuti("输入的两次密码不一致");
                return false;
            }

            return true;
        }
    }
}
