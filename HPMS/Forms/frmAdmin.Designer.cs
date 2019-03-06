namespace HPMS.Forms
{
    partial class frmAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.itemPanel_category = new DevComponents.DotNetBar.ItemPanel();
            this.btnMyProfile = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoleEdit = new DevComponents.DotNetBar.ButtonItem();
            this.btnUserEdit = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_MyProfile = new System.Windows.Forms.TabPage();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.txtRightDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtUserRole = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnModifyPsw = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.chk_MyProfile_rightsList = new System.Windows.Forms.CheckedListBox();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.tabPage_RoleEdit = new System.Windows.Forms.TabPage();
            this.bNRoleTable = new DevComponents.DotNetBar.Controls.BindingNavigatorEx(this.components);
            this.bindingNavigatorCountItem = new DevComponents.DotNetBar.LabelItem();
            this.bindingNavigatorMoveFirstItem = new DevComponents.DotNetBar.ButtonItem();
            this.bindingNavigatorMovePreviousItem = new DevComponents.DotNetBar.ButtonItem();
            this.bindingNavigatorPositionItem = new DevComponents.DotNetBar.TextBoxItem();
            this.bindingNavigatorMoveNextItem = new DevComponents.DotNetBar.ButtonItem();
            this.bindingNavigatorMoveLastItem = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoleAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoleDel = new DevComponents.DotNetBar.ButtonItem();
            this.btnRoleModify = new DevComponents.DotNetBar.ButtonItem();
            this.dgvRoleTable = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tabPage_UserEdit = new System.Windows.Forms.TabPage();
            this.bNUserTable = new DevComponents.DotNetBar.Controls.BindingNavigatorEx(this.components);
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.textBoxItem1 = new DevComponents.DotNetBar.TextBoxItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.btnAddUser = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelUser = new DevComponents.DotNetBar.ButtonItem();
            this.btnModifyUser = new DevComponents.DotNetBar.ButtonItem();
            this.dgvUserTable = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panelEx1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_MyProfile.SuspendLayout();
            this.tabPage_RoleEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bNRoleTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleTable)).BeginInit();
            this.tabPage_UserEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bNUserTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // itemPanel_category
            // 
            // 
            // 
            // 
            this.itemPanel_category.BackgroundStyle.Class = "ItemPanel";
            this.itemPanel_category.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemPanel_category.ContainerControlProcessDialogKey = true;
            this.itemPanel_category.Dock = System.Windows.Forms.DockStyle.Left;
            this.itemPanel_category.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnMyProfile,
            this.btnRoleEdit,
            this.btnUserEdit});
            this.itemPanel_category.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemPanel_category.Location = new System.Drawing.Point(0, 0);
            this.itemPanel_category.Name = "itemPanel_category";
            this.itemPanel_category.Size = new System.Drawing.Size(128, 413);
            this.itemPanel_category.TabIndex = 2;
            this.itemPanel_category.Text = "itemPanel1";
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Text = "我的信息";
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // btnRoleEdit
            // 
            this.btnRoleEdit.Name = "btnRoleEdit";
            this.btnRoleEdit.Text = "角色编辑";
            this.btnRoleEdit.Click += new System.EventHandler(this.btnRoleEdit_Click);
            // 
            // btnUserEdit
            // 
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Text = "用户编辑";
            this.btnUserEdit.Click += new System.EventHandler(this.btnUserEdit_Click);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandableControl = this.itemPanel_category;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(128, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 413);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 3;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabControl1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(134, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(735, 413);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_MyProfile);
            this.tabControl1.Controls.Add(this.tabPage_RoleEdit);
            this.tabControl1.Controls.Add(this.tabPage_UserEdit);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(735, 413);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_MyProfile
            // 
            this.tabPage_MyProfile.Controls.Add(this.labelX10);
            this.tabPage_MyProfile.Controls.Add(this.txtRightDescription);
            this.tabPage_MyProfile.Controls.Add(this.txtUserRole);
            this.tabPage_MyProfile.Controls.Add(this.btnModifyPsw);
            this.tabPage_MyProfile.Controls.Add(this.labelX7);
            this.tabPage_MyProfile.Controls.Add(this.chk_MyProfile_rightsList);
            this.tabPage_MyProfile.Controls.Add(this.txtUserName);
            this.tabPage_MyProfile.Controls.Add(this.labelX8);
            this.tabPage_MyProfile.Controls.Add(this.labelX9);
            this.tabPage_MyProfile.Location = new System.Drawing.Point(4, 22);
            this.tabPage_MyProfile.Name = "tabPage_MyProfile";
            this.tabPage_MyProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_MyProfile.Size = new System.Drawing.Size(727, 387);
            this.tabPage_MyProfile.TabIndex = 0;
            this.tabPage_MyProfile.Text = "我的信息";
            this.tabPage_MyProfile.UseVisualStyleBackColor = true;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(345, 71);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(84, 22);
            this.labelX10.TabIndex = 37;
            this.labelX10.Text = "权限描述";
            // 
            // txtRightDescription
            // 
            // 
            // 
            // 
            this.txtRightDescription.Border.Class = "TextBoxBorder";
            this.txtRightDescription.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRightDescription.Location = new System.Drawing.Point(345, 99);
            this.txtRightDescription.Multiline = true;
            this.txtRightDescription.Name = "txtRightDescription";
            this.txtRightDescription.Size = new System.Drawing.Size(297, 180);
            this.txtRightDescription.TabIndex = 36;
            // 
            // txtUserRole
            // 
            // 
            // 
            // 
            this.txtUserRole.Border.Class = "TextBoxBorder";
            this.txtUserRole.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserRole.Location = new System.Drawing.Point(79, 38);
            this.txtUserRole.Name = "txtUserRole";
            this.txtUserRole.ReadOnly = true;
            this.txtUserRole.Size = new System.Drawing.Size(172, 21);
            this.txtUserRole.TabIndex = 35;
            // 
            // btnModifyPsw
            // 
            this.btnModifyPsw.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModifyPsw.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModifyPsw.Location = new System.Drawing.Point(181, 295);
            this.btnModifyPsw.Name = "btnModifyPsw";
            this.btnModifyPsw.Size = new System.Drawing.Size(70, 25);
            this.btnModifyPsw.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnModifyPsw.TabIndex = 34;
            this.btnModifyPsw.Text = "修改密码";
            this.btnModifyPsw.Click += new System.EventHandler(this.btnModifyPsw_Click);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(13, 71);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(84, 22);
            this.labelX7.TabIndex = 32;
            this.labelX7.Text = "权限列表";
            // 
            // chk_MyProfile_rightsList
            // 
            this.chk_MyProfile_rightsList.FormattingEnabled = true;
            this.chk_MyProfile_rightsList.Items.AddRange(new object[] {
            "AA",
            "BB",
            "CC"});
            this.chk_MyProfile_rightsList.Location = new System.Drawing.Point(13, 99);
            this.chk_MyProfile_rightsList.Name = "chk_MyProfile_rightsList";
            this.chk_MyProfile_rightsList.Size = new System.Drawing.Size(238, 180);
            this.chk_MyProfile_rightsList.TabIndex = 31;
            this.chk_MyProfile_rightsList.ThreeDCheckBoxes = true;
            this.chk_MyProfile_rightsList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chk_MyProfile_rightsList_ItemCheck);
            this.chk_MyProfile_rightsList.SelectedIndexChanged += new System.EventHandler(this.chk_MyProfile_rightsList_SelectedIndexChanged);
            // 
            // txtUserName
            // 
            // 
            // 
            // 
            this.txtUserName.Border.Class = "TextBoxBorder";
            this.txtUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserName.Location = new System.Drawing.Point(79, 11);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(172, 21);
            this.txtUserName.TabIndex = 30;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(13, 34);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(50, 22);
            this.labelX8.TabIndex = 29;
            this.labelX8.Text = "角色";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(13, 10);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(50, 22);
            this.labelX9.TabIndex = 28;
            this.labelX9.Text = "用户名";
            // 
            // tabPage_RoleEdit
            // 
            this.tabPage_RoleEdit.Controls.Add(this.bNRoleTable);
            this.tabPage_RoleEdit.Controls.Add(this.dgvRoleTable);
            this.tabPage_RoleEdit.Location = new System.Drawing.Point(4, 22);
            this.tabPage_RoleEdit.Name = "tabPage_RoleEdit";
            this.tabPage_RoleEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_RoleEdit.Size = new System.Drawing.Size(727, 387);
            this.tabPage_RoleEdit.TabIndex = 2;
            this.tabPage_RoleEdit.Text = "角色编辑";
            this.tabPage_RoleEdit.UseVisualStyleBackColor = true;
            // 
            // bNRoleTable
            // 
            this.bNRoleTable.AntiAlias = true;
            this.bNRoleTable.CountLabel = this.bindingNavigatorCountItem;
            this.bNRoleTable.CountLabelFormat = "of {0}";
            this.bNRoleTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bNRoleTable.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bNRoleTable.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.btnRoleAdd,
            this.btnRoleDel,
            this.btnRoleModify});
            this.bNRoleTable.Location = new System.Drawing.Point(3, 359);
            this.bNRoleTable.MoveFirstButton = this.bindingNavigatorMoveFirstItem;
            this.bNRoleTable.MoveLastButton = this.bindingNavigatorMoveLastItem;
            this.bNRoleTable.MoveNextButton = this.bindingNavigatorMoveNextItem;
            this.bNRoleTable.MovePreviousButton = this.bindingNavigatorMovePreviousItem;
            this.bNRoleTable.Name = "bNRoleTable";
            this.bNRoleTable.PositionTextBox = this.bindingNavigatorPositionItem;
            this.bNRoleTable.Size = new System.Drawing.Size(721, 25);
            this.bNRoleTable.Stretch = true;
            this.bNRoleTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bNRoleTable.TabIndex = 17;
            this.bNRoleTable.TabStop = false;
            this.bNRoleTable.Text = "bindingNavigatorEx1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Text = "of {0}";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.BeginGroup = true;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.TextBoxWidth = 54;
            this.bindingNavigatorPositionItem.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.BeginGroup = true;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // btnRoleAdd
            // 
            this.btnRoleAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnRoleAdd.Image")));
            this.btnRoleAdd.Name = "btnRoleAdd";
            this.btnRoleAdd.Text = "Add new";
            this.btnRoleAdd.Click += new System.EventHandler(this.btnRoleAdd_Click);
            // 
            // btnRoleDel
            // 
            this.btnRoleDel.Image = ((System.Drawing.Image)(resources.GetObject("btnRoleDel.Image")));
            this.btnRoleDel.Name = "btnRoleDel";
            this.btnRoleDel.Text = "Delete";
            this.btnRoleDel.Click += new System.EventHandler(this.btnRoleDel_Click);
            // 
            // btnRoleModify
            // 
            this.btnRoleModify.Icon = ((System.Drawing.Icon)(resources.GetObject("btnRoleModify.Icon")));
            this.btnRoleModify.Name = "btnRoleModify";
            this.btnRoleModify.Text = "buttonItem1";
            this.btnRoleModify.Click += new System.EventHandler(this.btnRoleModify_Click);
            // 
            // dgvRoleTable
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoleTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRoleTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoleTable.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvRoleTable.EnableHeadersVisualStyles = false;
            this.dgvRoleTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvRoleTable.Location = new System.Drawing.Point(6, 6);
            this.dgvRoleTable.Name = "dgvRoleTable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoleTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRoleTable.RowTemplate.Height = 23;
            this.dgvRoleTable.Size = new System.Drawing.Size(713, 347);
            this.dgvRoleTable.TabIndex = 0;
            this.dgvRoleTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRoleTable_CellFormatting);
            // 
            // tabPage_UserEdit
            // 
            this.tabPage_UserEdit.Controls.Add(this.bNUserTable);
            this.tabPage_UserEdit.Controls.Add(this.dgvUserTable);
            this.tabPage_UserEdit.Location = new System.Drawing.Point(4, 22);
            this.tabPage_UserEdit.Name = "tabPage_UserEdit";
            this.tabPage_UserEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_UserEdit.Size = new System.Drawing.Size(727, 387);
            this.tabPage_UserEdit.TabIndex = 3;
            this.tabPage_UserEdit.Text = "用户编辑";
            this.tabPage_UserEdit.UseVisualStyleBackColor = true;
            // 
            // bNUserTable
            // 
            this.bNUserTable.AntiAlias = true;
            this.bNUserTable.CountLabel = this.labelItem1;
            this.bNUserTable.CountLabelFormat = "of {0}";
            this.bNUserTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bNUserTable.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bNUserTable.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem7,
            this.buttonItem8,
            this.textBoxItem1,
            this.labelItem1,
            this.buttonItem9,
            this.buttonItem10,
            this.btnAddUser,
            this.btnDelUser,
            this.btnModifyUser});
            this.bNUserTable.Location = new System.Drawing.Point(3, 359);
            this.bNUserTable.MoveFirstButton = this.buttonItem7;
            this.bNUserTable.MoveLastButton = this.buttonItem10;
            this.bNUserTable.MoveNextButton = this.buttonItem9;
            this.bNUserTable.MovePreviousButton = this.buttonItem8;
            this.bNUserTable.Name = "bNUserTable";
            this.bNUserTable.PositionTextBox = this.textBoxItem1;
            this.bNUserTable.Size = new System.Drawing.Size(721, 25);
            this.bNUserTable.Stretch = true;
            this.bNUserTable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bNUserTable.TabIndex = 18;
            this.bNUserTable.TabStop = false;
            this.bNUserTable.Text = "bindingNavigatorEx2";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "of {0}";
            // 
            // buttonItem7
            // 
            this.buttonItem7.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem7.Image")));
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "Move first";
            // 
            // buttonItem8
            // 
            this.buttonItem8.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem8.Image")));
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Text = "Move previous";
            // 
            // textBoxItem1
            // 
            this.textBoxItem1.AccessibleName = "Position";
            this.textBoxItem1.BeginGroup = true;
            this.textBoxItem1.Name = "textBoxItem1";
            this.textBoxItem1.Text = "0";
            this.textBoxItem1.TextBoxWidth = 54;
            this.textBoxItem1.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // buttonItem9
            // 
            this.buttonItem9.BeginGroup = true;
            this.buttonItem9.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem9.Image")));
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "Move next";
            // 
            // buttonItem10
            // 
            this.buttonItem10.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem10.Image")));
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "Move last";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Text = "Add new";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnDelUser
            // 
            this.btnDelUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDelUser.Image")));
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Text = "Delete";
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // btnModifyUser
            // 
            this.btnModifyUser.Icon = ((System.Drawing.Icon)(resources.GetObject("btnModifyUser.Icon")));
            this.btnModifyUser.Name = "btnModifyUser";
            this.btnModifyUser.Text = "buttonItem1";
            this.btnModifyUser.Click += new System.EventHandler(this.btnModifyUser_Click);
            // 
            // dgvUserTable
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUserTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvUserTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUserTable.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvUserTable.EnableHeadersVisualStyles = false;
            this.dgvUserTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvUserTable.Location = new System.Drawing.Point(6, 4);
            this.dgvUserTable.Name = "dgvUserTable";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUserTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvUserTable.RowTemplate.Height = 23;
            this.dgvUserTable.Size = new System.Drawing.Size(713, 347);
            this.dgvUserTable.TabIndex = 17;
            this.dgvUserTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUserTable_CellFormatting);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 413);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.itemPanel_category);
            this.DoubleBuffered = true;
            this.Name = "frmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.frmAdmin_Load);
            this.panelEx1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_MyProfile.ResumeLayout(false);
            this.tabPage_RoleEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bNRoleTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleTable)).EndInit();
            this.tabPage_UserEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bNUserTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ItemPanel itemPanel_category;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonItem btnMyProfile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_MyProfile;
        private DevComponents.DotNetBar.ButtonItem btnRoleEdit;
        private DevComponents.DotNetBar.ButtonItem btnUserEdit;
        private System.Windows.Forms.TabPage tabPage_RoleEdit;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvRoleTable;
        private DevComponents.DotNetBar.ButtonX btnModifyPsw;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.CheckedListBox chk_MyProfile_rightsList;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.TabPage tabPage_UserEdit;
        private DevComponents.DotNetBar.Controls.BindingNavigatorEx bNUserTable;
        private DevComponents.DotNetBar.ButtonItem btnAddUser;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem btnDelUser;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.TextBoxItem textBoxItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvUserTable;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserRole;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRightDescription;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevComponents.DotNetBar.Controls.BindingNavigatorEx bNRoleTable;
        private DevComponents.DotNetBar.ButtonItem btnRoleAdd;
        private DevComponents.DotNetBar.LabelItem bindingNavigatorCountItem;
        private DevComponents.DotNetBar.ButtonItem btnRoleDel;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorMoveFirstItem;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorMovePreviousItem;
        private DevComponents.DotNetBar.TextBoxItem bindingNavigatorPositionItem;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorMoveNextItem;
        private DevComponents.DotNetBar.ButtonItem bindingNavigatorMoveLastItem;
        private DevComponents.DotNetBar.ButtonItem btnRoleModify;
        private DevComponents.DotNetBar.ButtonItem btnModifyUser;

    }
}