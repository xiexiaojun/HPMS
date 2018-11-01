using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using HPMS.DB;
using HPMS.RightsControl;
using HPMS.Util;
using MessageBox = System.Windows.Forms.MessageBox;
using Size = System.Drawing.Size;

namespace HPMS
{
    public partial class frmAdmin : Office2007Form
    {
       
        private User _curretUser;
        private Dictionary<int,Right> _allRights=new Dictionary<int, Right>();
        private Dictionary<int,Role>_allRoles=new Dictionary<int, Role>();
       private bool _autoStatus = true;//为真时listcheckbox选项可以改变
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
            Gloabal.GRightsWrapper.SetProfileEdit(temp);

            txtUserName.Text = _curretUser.Username;
            txtUserRole.Text = _curretUser.Role;
            chk_MyProfile_rightsList.Items.Clear();
            DisplayRights();
            DisplayRoles();
            DisplayUsers();
        }

        private void DisplayRights()
        {
            chk_MyProfile_rightsList.DisplayMember = "Text";
            chk_MyProfile_rightsList.ValueMember = "Value";
            var _allRightsList = RightDao.GetAllRights();
            foreach (var VARIABLE in _allRightsList)
            {
                _allRights.Add(VARIABLE.Id, VARIABLE);
                chk_MyProfile_rightsList.Items.Add(new { Text = VARIABLE.Name, Value = VARIABLE.Id,_Right=VARIABLE}, _curretUser.Rights.ContainsKey(VARIABLE.Name));
               
            }

            ;
            _autoStatus = false;
        }

        private void DisplayRoles()
        {
            DataTable roleDataTable = RoleDao.Find();
           
            dgvRoleTable.DataSource = roleDataTable;
            dgvRoleTable.AutoGenerateColumns = true;
            dgvRoleTable.DataSource = roleDataTable;
            //dgvRoleTable.Columns["RightsID"].Visible = false;
            dgvRoleTable.Columns["CreateID"].Visible = false;
            dgvRoleTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoleTable.Columns["ID"].FillWeight = 15;
            dgvRoleTable.Columns["Name"].FillWeight = 25;
            dgvRoleTable.Columns["Description"].FillWeight = 40;
            dgvRoleTable.Columns["Status"].FillWeight = 25;
            dgvRoleTable.Columns["CreateDate"].FillWeight = 50;
            BindingSource bs = new BindingSource();
            bs.DataSource = roleDataTable;
            bNRoleTable.BindingSource = bs;
            dgvRoleTable.DataSource = bs;

        }

        private void DisplayUsers()
        {
            DataTable userDataTable = UserDao.Find();

            dgvUserTable.DataSource = userDataTable;
            dgvUserTable.AutoGenerateColumns = true;
            dgvUserTable.DataSource = userDataTable;
            dgvUserTable.Columns["Salt"].Visible = false;
            dgvUserTable.Columns["RoleID"].Visible = false;
            dgvUserTable.Columns["CreateID"].Visible = false;
            dgvUserTable.Columns["RoleRights"].Visible = false;
            dgvUserTable.Columns["RoleStatus"].Visible = false;
            dgvUserTable.Columns["Password"].Visible = false;
            dgvUserTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvUserTable.Columns["ID"].FillWeight = 15;
            //dgvUserTable.Columns["Name"].FillWeight = 25;
            //dgvUserTable.Columns["Description"].FillWeight = 40;
            //dgvUserTable.Columns["Status"].FillWeight = 25;
            //dgvUserTable.Columns["CreateDate"].FillWeight = 50;
            BindingSource bs = new BindingSource();
            bs.DataSource = userDataTable;
            bNUserTable.BindingSource = bs;
            dgvUserTable.DataSource = bs;

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

        private void chk_MyProfile_rightsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var checkedListBox = (CheckedListBox) sender;
            dynamic item = checkedListBox.SelectedItem;
            txtRightDescription.Text = item._Right.Description;
        }

        private void chk_MyProfile_rightsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!_autoStatus)
            {
                e.NewValue = e.CurrentValue;
            }
          
        }

        private void dgvRoleTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ( e == null || e.Value == null||e.Value.ToString() == string.Empty || !(sender is DataGridView) )
                return;

            var columnName = dgvRoleTable.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("Status"))
            {
                if ((int)e.Value == 1)
                {
                    e.Value = "启用";
                }
                else
                {
                    e.Value = "禁用";  
                }
                
            }
            if (columnName.Equals("RightsID"))
            {
                e.Value = GetRightsFromId((string) e.Value);

            }
        }

        private string GetRightsFromId(string rightsId)
        {
            var temp=rightsId.Split(',').Select(t => _allRights[int.Parse(t)].Description).ToArray();
            return string.Join(",", temp);
        }

       

     
        private void btnRoleModify_Click(object sender, EventArgs e)
        {
            var id = dgvRoleTable.CurrentRow.Cells["id"].Value.ToString();
            if (id.Equals(""))
            {
                UI.MessageBoxMuti("没有角色被选中,无法修改");
            }
            else
            {
                Role role = RoleDao.Find(int.Parse(id))[0];
                using (frmRoleEdit f=new frmRoleEdit(role,_allRights))
                {
                    f.ShowDialog();
                }
                DisplayRoles();
            }
           
        }
        private void btnRoleAdd_Click(object sender, EventArgs e)
        {
            using (frmRoleEdit f = new frmRoleEdit(null, _allRights))
            {
                f.ShowDialog();
            }
            DisplayRoles();
        }
        private void btnRoleDel_Click(object sender, EventArgs e)
        {
            var id = dgvRoleTable.CurrentRow.Cells["id"].Value.ToString();
            if (id.Equals(""))
            {
                UI.MessageBoxMuti("没有角色被选中,无法删除");
            }
            else
            {
                var key = UI.MessageBoxYesNoMuti("确定删除当前角色吗?");
                if (key == DialogResult.Yes)
                {
                    int t = int.Parse(id);
                    if(Gloabal.GRightsWrapper.DeleteRole(t))
                    {
                        UI.MessageBoxMuti("删除角色成功");
                        DisplayRoles();
                    }
                    else
                    {
                        UI.MessageBoxMuti("删除角色失败");
                    }

                }
            
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            _allRoles.Clear();
            var roleLists = RoleDao.FindAll();
            foreach (var VARIABLE in roleLists)
            {
                _allRoles.Add(VARIABLE.RoleId,VARIABLE);
            }

            using (frmUserEdit f = new frmUserEdit(null, _allRoles))
            {
                f.ShowDialog();
            }
            DisplayUsers();
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            var id = dgvUserTable.CurrentRow.Cells["id"].Value.ToString();
            if (id.Equals(""))
            {
                UI.MessageBoxMuti("没有用户被选中,无法修改");
            }
            else
            {
                _allRoles.Clear();
                var roleLists = RoleDao.FindAll();
                foreach (var VARIABLE in roleLists)
                {
                    _allRoles.Add(VARIABLE.RoleId, VARIABLE);
                }
                User user = UserDao.Find(int.Parse(id))[0];
                using (frmUserEdit f = new frmUserEdit(user, _allRoles))
                {
                    f.ShowDialog();
                }
                DisplayUsers();
            }

        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            var id = dgvUserTable.CurrentRow.Cells["id"].Value.ToString();
            if (id.Equals(""))
            {
                UI.MessageBoxMuti("没有用户被选中,无法删除");
            }
            else
            {
                var key = UI.MessageBoxYesNoMuti("确定删除当前用户吗?");
                if (key == DialogResult.Yes)
                {
                    int t = int.Parse(id);
                    if (Gloabal.GRightsWrapper.DeleteUser(t))
                    {
                        UI.MessageBoxMuti("删除用户成功");
                        DisplayUsers();
                    }
                    else
                    {
                        UI.MessageBoxMuti("删除用户失败");
                    }

                }

            }
        }

        private void dgvUserTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || e.Value.ToString() == string.Empty || !(sender is DataGridView))
                return;

            var columnName = dgvUserTable.Columns[e.ColumnIndex].Name;
            if (columnName.Equals("UserStatus"))
            {
                if ((int)e.Value == 1)
                {
                    e.Value = "启用";
                }
                else
                {
                    e.Value = "禁用";
                }

            }
           

        }

        private void btnModifyPsw_Click(object sender, EventArgs e)
        {
            using (frmPswModify frm=new frmPswModify())
            {
                frm.ShowDialog();
            }
        }

       

       

       
    }
}
