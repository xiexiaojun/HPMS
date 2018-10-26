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
using HPMS.DB;
using HPMS.RightsControl;

namespace HPMS
{
    public partial class frmAdmin : Office2007Form
    {
        RightsWrapper rights=new RightsWrapper();
        private User _curretUser;
        public frmAdmin(User user)
        {
            _curretUser = user;
            EnableGlass = false;
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            
            btnRoleEdit.Enabled = false;
            btnUserEdit.Enabled = false;
            ButtonItem[] temp = new[] {btnRoleEdit, btnUserEdit};
            rights.SetProfileEdit(temp);

            txtUserName.Text = _curretUser.Username;
            txtUserRole.Text = _curretUser.Role;
            chk_MyProfile_rightsList.Items.Clear();
            foreach (var VARIABLE in _curretUser.Rights)
            {
                chk_MyProfile_rightsList.Items.Add(VARIABLE,true);
            }
            
        }

        private void btnRoleEdit_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage_RoleEdit");
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
           
            tabControl1.SelectTab("tabPage_MyProfile");
        }

        private void btnUserEdit_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage_UserEdit");
        }

       
    }
}
