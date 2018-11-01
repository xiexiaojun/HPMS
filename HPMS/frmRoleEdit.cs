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
using HPMS.Util;


namespace HPMS
{
    public partial class frmRoleEdit : Office2007Form
    {
        private Role _role;
        private Dictionary<int, Right> _allRights;
        public frmRoleEdit(Role role,Dictionary<int,Right>allRights)
        {
            EnableGlass = false;
            _role = role;
            _allRights = allRights;
            InitializeComponent();
        }
        
        private void frmRoleEdit_Load(object sender, EventArgs e)
        {
            if (_role == null)
            {
                txtUserRole.ReadOnly = false;
                this.Text = "新增";
                chk_MyProfile_rightsList.DisplayMember = "Text";
                chk_MyProfile_rightsList.ValueMember = "Value";
                foreach (var VARIABLE in _allRights)
                {
                    // _allRights.Add(VARIABLE.Id, VARIABLE);
                    chk_MyProfile_rightsList.Items.Add(new { Text = VARIABLE.Value.Name, Value = VARIABLE.Value.Id, _Right = VARIABLE }, false);

                }
            }
            else
            {
                this.Text = "修改";
                txtUserRole.Text = _role.Name;
                txtRoleDescription.Text = _role.Description;
                chkUserEnable.Checked = (RecordStatus)_role.RecordStatus == RecordStatus.Enable;
                chk_MyProfile_rightsList.DisplayMember = "Text";
                chk_MyProfile_rightsList.ValueMember = "Value";
                foreach (var VARIABLE in _allRights)
                {
                   // _allRights.Add(VARIABLE.Id, VARIABLE);
                    chk_MyProfile_rightsList.Items.Add(new { Text = VARIABLE.Value.Name, Value = VARIABLE.Value.Id, _Right = VARIABLE }, _role.Right.ContainsKey(VARIABLE.Value.Name));

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_role == null)
            {
                //MessageBox.Show(GetSelectedRightsId());
                //新增
                Role role=new Role();
                role.Description = txtRoleDescription.Text;
                role.Name = txtUserRole.Text;
                role.CreateId = Gloabal.GUser.UserId;
                role.RightsId = GetSelectedRightsId();
                role.RecordStatus = chkUserEnable.Checked==true?RecordStatus.Enable:RecordStatus.Disable;
                role.CreateDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                string msg = "";
                
                if (!Gloabal.GRightsWrapper.SaveRole(role, ref msg))
                {
                    UI.MessageBoxMuti(msg);
                }
                else
                {
                    UI.MessageBoxMuti("增加角色成功");
                }
            }
            else
            {
                //修改
                _role.Description = txtRoleDescription.Text;
                _role.Name = txtUserRole.Text;
                _role.CreateId = Gloabal.GUser.UserId;
                _role.RightsId = GetSelectedRightsId();
                _role.RecordStatus = chkUserEnable.Checked == true ? RecordStatus.Enable : RecordStatus.Disable;
                _role.CreateDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                if (!Gloabal.GRightsWrapper.UpdateRole(_role))
                {
                    UI.MessageBoxMuti("修改角色失败");
                }
                else
                {

                    UI.MessageBoxMuti("修改角色成功");
                }
                
            }
        }

        private string GetSelectedRightsId()
        {
            List<string>retList=new List<string>();
            foreach (var VARIABLE in chk_MyProfile_rightsList.CheckedItems)
            {
            
                dynamic item = VARIABLE;
                retList.Add(item.Value.ToString());
         
            }

            return string.Join(",",retList.ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
