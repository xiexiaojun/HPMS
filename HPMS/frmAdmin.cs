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
        private Dictionary<string,string> _allRights=new Dictionary<string, string>();
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
            rights.SetProfileEdit(temp);

            txtUserName.Text = _curretUser.Username;
            txtUserRole.Text = _curretUser.Role;
            chk_MyProfile_rightsList.Items.Clear();
            DisplayRights();
            DisplayRoles();
        }

        private void DisplayRights()
        {
            var allRights = RightDao.GetAllRights();
            foreach (var VARIABLE in allRights)
            {
                _allRights.Add(VARIABLE.Name, VARIABLE.Description);
                chk_MyProfile_rightsList.Items.Add(VARIABLE.Name, _curretUser.Rights.ContainsKey(VARIABLE.Name));
            }

            _autoStatus = false;
        }

        private void DisplayRoles()
        {
            DataTable roleDataTable = RoleDao.Find();
            dgvRoleTable.AutoGenerateColumns = true;
            dgvRoleTable.DataSource = roleDataTable;
            //dgvRoleTable.Columns["column1"].DataPropertyName = roleDataTable.Columns["ID"].ToString();//column1是DatagridView的第一列的name值
            //dgvRoleTable.Columns["column2"].DataPropertyName = roleDataTable.Columns["Name"].ToString();
            //dgvRoleTable.Columns["column3"].DataPropertyName = roleDataTable.Columns["Description"].ToString()+"Figo";
            //dgvRoleTable.Update();
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
           txtRightDescription.Text = _allRights[checkedListBox.SelectedItem.ToString()];
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
            var columnName = dgvRoleTable.Columns[e.ColumnIndex].Name;
            var a = e.Value;
            if (columnName.Equals("Name"))
            {
                e.Value = "Figo";
            }
        }

       
    }
}
