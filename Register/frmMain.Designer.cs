namespace Register
{
    sealed partial class frmRegister
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.btnCalcuCode = new DevComponents.DotNetBar.ButtonX();
            this.btnGetMachineCode = new DevComponents.DotNetBar.ButtonX();
            this.txtKey = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cmbSoftVersion = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbSoftName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtMachineCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnAddfoot = new DevComponents.DotNetBar.ButtonX();
            this.btnLoadtree = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSavetree = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnMovedown = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnMoveup = new DevComponents.DotNetBar.ButtonX();
            this.btnAddchild = new DevComponents.DotNetBar.ButtonX();
            this.btnDelnode = new DevComponents.DotNetBar.ButtonX();
            this.tabItem4 = new DevComponents.DotNetBar.TabItem(this.components);
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabItem2
            // 
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "配置";
            // 
            // tabItem1
            // 
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "注册";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtCode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(686, 544);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(3, 301);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(324, 240);
            this.txtCode.TabIndex = 40;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabControl1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(324, 292);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.Transparent;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(324, 292);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.Tabs.Add(this.tabItem4);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.btnCalcuCode);
            this.tabControlPanel1.Controls.Add(this.btnGetMachineCode);
            this.tabControlPanel1.Controls.Add(this.txtKey);
            this.tabControlPanel1.Controls.Add(this.labelX4);
            this.tabControlPanel1.Controls.Add(this.cmbSoftVersion);
            this.tabControlPanel1.Controls.Add(this.labelX3);
            this.tabControlPanel1.Controls.Add(this.cmbSoftName);
            this.tabControlPanel1.Controls.Add(this.labelX2);
            this.tabControlPanel1.Controls.Add(this.txtMachineCode);
            this.tabControlPanel1.Controls.Add(this.labelX1);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(324, 266);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem3;
            // 
            // btnCalcuCode
            // 
            this.btnCalcuCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCalcuCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCalcuCode.Location = new System.Drawing.Point(194, 189);
            this.btnCalcuCode.Name = "btnCalcuCode";
            this.btnCalcuCode.Size = new System.Drawing.Size(110, 24);
            this.btnCalcuCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCalcuCode.TabIndex = 67;
            this.btnCalcuCode.Text = "计算注册码";
            this.btnCalcuCode.Click += new System.EventHandler(this.btnCalcuCode_Click);
            // 
            // btnGetMachineCode
            // 
            this.btnGetMachineCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetMachineCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetMachineCode.Location = new System.Drawing.Point(20, 189);
            this.btnGetMachineCode.Name = "btnGetMachineCode";
            this.btnGetMachineCode.Size = new System.Drawing.Size(110, 24);
            this.btnGetMachineCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetMachineCode.TabIndex = 66;
            this.btnGetMachineCode.Text = "获取本机机器码";
            this.btnGetMachineCode.Click += new System.EventHandler(this.btnGetMachineCode_Click);
            // 
            // txtKey
            // 
            // 
            // 
            // 
            this.txtKey.Border.Class = "TextBoxBorder";
            this.txtKey.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtKey.Location = new System.Drawing.Point(80, 145);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(224, 21);
            this.txtKey.TabIndex = 64;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(20, 145);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(44, 21);
            this.labelX4.TabIndex = 65;
            this.labelX4.Text = "秘钥";
            // 
            // cmbSoftVersion
            // 
            this.cmbSoftVersion.DisplayMember = "Text";
            this.cmbSoftVersion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSoftVersion.FormattingEnabled = true;
            this.cmbSoftVersion.ItemHeight = 15;
            this.cmbSoftVersion.Location = new System.Drawing.Point(80, 99);
            this.cmbSoftVersion.Name = "cmbSoftVersion";
            this.cmbSoftVersion.Size = new System.Drawing.Size(107, 21);
            this.cmbSoftVersion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSoftVersion.TabIndex = 63;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(20, 99);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(44, 21);
            this.labelX3.TabIndex = 62;
            this.labelX3.Text = "版本";
            // 
            // cmbSoftName
            // 
            this.cmbSoftName.DisplayMember = "Text";
            this.cmbSoftName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSoftName.FormattingEnabled = true;
            this.cmbSoftName.ItemHeight = 15;
            this.cmbSoftName.Items.AddRange(new object[] {
            this.comboItem1});
            this.cmbSoftName.Location = new System.Drawing.Point(80, 53);
            this.cmbSoftName.Name = "cmbSoftName";
            this.cmbSoftName.Size = new System.Drawing.Size(107, 21);
            this.cmbSoftName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSoftName.TabIndex = 61;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "HPMS";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(20, 53);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(44, 21);
            this.labelX2.TabIndex = 60;
            this.labelX2.Text = "软件";
            // 
            // txtMachineCode
            // 
            // 
            // 
            // 
            this.txtMachineCode.Border.Class = "TextBoxBorder";
            this.txtMachineCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMachineCode.Location = new System.Drawing.Point(80, 7);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(224, 21);
            this.txtMachineCode.TabIndex = 57;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(20, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 21);
            this.labelX1.TabIndex = 58;
            this.labelX1.Text = "机器码";
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel1;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "注册";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.labelX5);
            this.tabControlPanel2.Controls.Add(this.btnAddfoot);
            this.tabControlPanel2.Controls.Add(this.btnLoadtree);
            this.tabControlPanel2.Controls.Add(this.textBoxX1);
            this.tabControlPanel2.Controls.Add(this.btnSavetree);
            this.tabControlPanel2.Controls.Add(this.textBoxX2);
            this.tabControlPanel2.Controls.Add(this.btnMovedown);
            this.tabControlPanel2.Controls.Add(this.labelX6);
            this.tabControlPanel2.Controls.Add(this.btnMoveup);
            this.tabControlPanel2.Controls.Add(this.btnAddchild);
            this.tabControlPanel2.Controls.Add(this.btnDelnode);
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(324, 266);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabItem4;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(9, 14);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(44, 21);
            this.labelX5.TabIndex = 55;
            this.labelX5.Text = "描述";
            // 
            // btnAddfoot
            // 
            this.btnAddfoot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddfoot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddfoot.Location = new System.Drawing.Point(9, 83);
            this.btnAddfoot.Name = "btnAddfoot";
            this.btnAddfoot.Size = new System.Drawing.Size(136, 24);
            this.btnAddfoot.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddfoot.TabIndex = 52;
            this.btnAddfoot.Text = "添加根节点";
            this.btnAddfoot.Click += new System.EventHandler(this.btnAddfoot_Click);
            // 
            // btnLoadtree
            // 
            this.btnLoadtree.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoadtree.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLoadtree.Location = new System.Drawing.Point(9, 218);
            this.btnLoadtree.Name = "btnLoadtree";
            this.btnLoadtree.Size = new System.Drawing.Size(136, 24);
            this.btnLoadtree.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLoadtree.TabIndex = 62;
            this.btnLoadtree.Text = "载入";
            this.btnLoadtree.Click += new System.EventHandler(this.btnLoadtree_Click);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(9, 41);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(136, 21);
            this.textBoxX1.TabIndex = 53;
            // 
            // btnSavetree
            // 
            this.btnSavetree.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSavetree.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSavetree.Location = new System.Drawing.Point(169, 219);
            this.btnSavetree.Name = "btnSavetree";
            this.btnSavetree.Size = new System.Drawing.Size(136, 24);
            this.btnSavetree.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSavetree.TabIndex = 61;
            this.btnSavetree.Text = "保存";
            this.btnSavetree.Click += new System.EventHandler(this.btnSavetree_Click);
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Location = new System.Drawing.Point(169, 41);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(136, 21);
            this.textBoxX2.TabIndex = 54;
            // 
            // btnMovedown
            // 
            this.btnMovedown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMovedown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMovedown.Location = new System.Drawing.Point(169, 173);
            this.btnMovedown.Name = "btnMovedown";
            this.btnMovedown.Size = new System.Drawing.Size(136, 24);
            this.btnMovedown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMovedown.TabIndex = 60;
            this.btnMovedown.Text = "下移节点";
            this.btnMovedown.Click += new System.EventHandler(this.btnMovedown_Click);
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(169, 14);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(44, 21);
            this.labelX6.TabIndex = 56;
            this.labelX6.Text = "编号";
            // 
            // btnMoveup
            // 
            this.btnMoveup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMoveup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMoveup.Location = new System.Drawing.Point(169, 128);
            this.btnMoveup.Name = "btnMoveup";
            this.btnMoveup.Size = new System.Drawing.Size(136, 24);
            this.btnMoveup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMoveup.TabIndex = 59;
            this.btnMoveup.Text = "上移节点";
            this.btnMoveup.Click += new System.EventHandler(this.btnMoveup_Click);
            // 
            // btnAddchild
            // 
            this.btnAddchild.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddchild.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddchild.Location = new System.Drawing.Point(9, 128);
            this.btnAddchild.Name = "btnAddchild";
            this.btnAddchild.Size = new System.Drawing.Size(136, 24);
            this.btnAddchild.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddchild.TabIndex = 57;
            this.btnAddchild.Text = "添加子节点";
            this.btnAddchild.Click += new System.EventHandler(this.btnAddchild_Click);
            // 
            // btnDelnode
            // 
            this.btnDelnode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelnode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelnode.Location = new System.Drawing.Point(9, 173);
            this.btnDelnode.Name = "btnDelnode";
            this.btnDelnode.Size = new System.Drawing.Size(136, 24);
            this.btnDelnode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelnode.TabIndex = 58;
            this.btnDelnode.Text = "删除节点";
            this.btnDelnode.Click += new System.EventHandler(this.btnDelnode_Click);
            // 
            // tabItem4
            // 
            this.tabItem4.AttachedControl = this.tabControlPanel2;
            this.tabItem4.Name = "tabItem4";
            this.tabItem4.Text = "配置";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.treeView1);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(333, 3);
            this.panelEx2.Name = "panelEx2";
            this.tableLayoutPanel1.SetRowSpan(this.panelEx2, 2);
            this.panelEx2.Size = new System.Drawing.Size(350, 538);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "panelEx2";
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ForeColor = System.Drawing.Color.Blue;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(350, 538);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 544);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "frmRegister";
            this.Text = "注册机";
            this.Load += new System.EventHandler(this.frmRegister_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem4;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
        private DevComponents.DotNetBar.ButtonX btnCalcuCode;
        private DevComponents.DotNetBar.ButtonX btnGetMachineCode;
        private DevComponents.DotNetBar.Controls.TextBoxX txtKey;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSoftVersion;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSoftName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMachineCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnAddfoot;
        private DevComponents.DotNetBar.ButtonX btnLoadtree;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.ButtonX btnSavetree;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.ButtonX btnMovedown;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX btnMoveup;
        private DevComponents.DotNetBar.ButtonX btnAddchild;
        private DevComponents.DotNetBar.ButtonX btnDelnode;
        private DevComponents.Editors.ComboItem comboItem1;
        private System.Windows.Forms.TreeView treeView1;


    }
}

