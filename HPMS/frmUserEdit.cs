using System;
using System.Collections.Generic;
using HPMS.DB;
using HPMS.Util;
using Tool;


namespace HPMS
{
    public partial class frmUserEdit : Office2007Muti
    {
        private User _user;
        private Dictionary<int, Role> _allRoles;
        public frmUserEdit(User user, Dictionary<int, Role> allRoles)
        {
            EnableGlass = false;
            _user = user;
            _allRoles = allRoles;
            InitializeComponent();
        }
        
        private void frmUserEdit_Load(object sender, EventArgs e)
        {
            cmbRole.DisplayMember = "Text";
            cmbRole.ValueMember = "Value";
            foreach (var VARIABLE in _allRoles)
            {

                cmbRole.Items.Add(new { Text = VARIABLE.Value.Name, Value = VARIABLE.Value });

            }
            if (_user == null)
            {
                txtUserRole.ReadOnly = false;
                this.Text = "新增";
             }
            else
            {
                this.Text = "修改";
                txtUserRole.Text = _user.Username;
               
                chkUserEnable.Checked = _user.UserStatus == RecordStatus.Enable;
                cmbRole.SelectedIndex = this.cmbRole.FindString(_user.Role);


            }
        }

        private int GetRoleId()
        {
            dynamic temp = cmbRole.SelectedItem;
            var role = (Role) temp.Value;
            return role.RoleId;
        }

        private void SetRole(Role role)
        {
            cmbRole.SelectedText = role.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_user == null)
            {
                //MessageBox.Show(GetSelectedRightsId());
                //新增
                var user = new User();
                user.Username = txtUserRole.Text;
                user.RoleId = GetRoleId();
                user.UserStatus = chkUserEnable.Checked == true ? RecordStatus.Enable : RecordStatus.Disable;
                user.CreaterId = Gloabal.GUser.UserId;
                user.Psw = "123456";
                user.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               
                string msg = "";

                if (!Gloabal.GRightsWrapper.SaveUser(user, ref msg))
                {
                    Ui.MessageBoxMuti(msg);
                }
                else
                {
                    Ui.MessageBoxMuti("增加用户成功");
                }
            }
            else
            {
                //修改
                _user.UserStatus = chkUserEnable.Checked == true ? RecordStatus.Enable : RecordStatus.Disable;
                _user.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _user.RoleId = GetRoleId();
                
       
                if (!Gloabal.GRightsWrapper.UpdateUser(_user))
                {
                    Ui.MessageBoxMuti("修改用户失败");
                }
                else
                {

                    Ui.MessageBoxMuti("修改用户成功");
                }

            }
        }

     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
