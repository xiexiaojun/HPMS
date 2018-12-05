namespace HPMS
{
    partial class frmHardwareSetting
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
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbAdpaterPort = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbNwaType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.txtSbVisaAdd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmbAdapterType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cmbSwitchBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.txtNwaVisaAdd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnBrowseTxt = new DevComponents.DotNetBar.ButtonX();
            this.txtTxtSaveFolder = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX41 = new DevComponents.DotNetBar.LabelX();
            this.btnBrowseSnp = new DevComponents.DotNetBar.ButtonX();
            this.txtSnpSaveFolder = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(473, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 26);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.cmbAdpaterPort);
            this.groupBox1.Controls.Add(this.cmbNwaType);
            this.groupBox1.Controls.Add(this.txtSbVisaAdd);
            this.groupBox1.Controls.Add(this.cmbAdapterType);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.cmbSwitchBox);
            this.groupBox1.Controls.Add(this.txtNwaVisaAdd);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 211);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "硬件设置";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(25, 20);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(180, 21);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "网分型号";
            // 
            // cmbAdpaterPort
            // 
            this.cmbAdpaterPort.DisplayMember = "Text";
            this.cmbAdpaterPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAdpaterPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdpaterPort.FormattingEnabled = true;
            this.cmbAdpaterPort.ItemHeight = 15;
            this.cmbAdpaterPort.Location = new System.Drawing.Point(250, 101);
            this.cmbAdpaterPort.Name = "cmbAdpaterPort";
            this.cmbAdpaterPort.Size = new System.Drawing.Size(297, 21);
            this.cmbAdpaterPort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbAdpaterPort.TabIndex = 14;
            // 
            // cmbNwaType
            // 
            this.cmbNwaType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNwaType.DisplayMember = "Text";
            this.cmbNwaType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNwaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNwaType.FormattingEnabled = true;
            this.cmbNwaType.ItemHeight = 15;
            this.cmbNwaType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem5});
            this.cmbNwaType.Location = new System.Drawing.Point(25, 47);
            this.cmbNwaType.Name = "cmbNwaType";
            this.cmbNwaType.Size = new System.Drawing.Size(186, 21);
            this.cmbNwaType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbNwaType.TabIndex = 5;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "N5224A";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "E5071C";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "Demo";
            // 
            // txtSbVisaAdd
            // 
            // 
            // 
            // 
            this.txtSbVisaAdd.Border.Class = "TextBoxBorder";
            this.txtSbVisaAdd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSbVisaAdd.Location = new System.Drawing.Point(250, 167);
            this.txtSbVisaAdd.Name = "txtSbVisaAdd";
            this.txtSbVisaAdd.ReadOnly = true;
            this.txtSbVisaAdd.Size = new System.Drawing.Size(297, 21);
            this.txtSbVisaAdd.TabIndex = 6;
            this.txtSbVisaAdd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GetVisaAddress);
            // 
            // cmbAdapterType
            // 
            this.cmbAdapterType.DisplayMember = "Text";
            this.cmbAdapterType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAdapterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdapterType.FormattingEnabled = true;
            this.cmbAdapterType.ItemHeight = 15;
            this.cmbAdapterType.Items.AddRange(new object[] {
            this.comboItem4});
            this.cmbAdapterType.Location = new System.Drawing.Point(25, 101);
            this.cmbAdapterType.Name = "cmbAdapterType";
            this.cmbAdapterType.Size = new System.Drawing.Size(186, 21);
            this.cmbAdapterType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbAdapterType.TabIndex = 13;
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "CP2112";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(250, 74);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(297, 21);
            this.labelX6.TabIndex = 11;
            this.labelX6.Text = "烧录器端口";
            // 
            // cmbSwitchBox
            // 
            this.cmbSwitchBox.DisplayMember = "Text";
            this.cmbSwitchBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSwitchBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSwitchBox.FormattingEnabled = true;
            this.cmbSwitchBox.ItemHeight = 15;
            this.cmbSwitchBox.Items.AddRange(new object[] {
            this.comboItem3,
            this.comboItem6});
            this.cmbSwitchBox.Location = new System.Drawing.Point(25, 167);
            this.cmbSwitchBox.Name = "cmbSwitchBox";
            this.cmbSwitchBox.Size = new System.Drawing.Size(186, 21);
            this.cmbSwitchBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSwitchBox.TabIndex = 9;
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "MCU";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "Demo";
            // 
            // txtNwaVisaAdd
            // 
            // 
            // 
            // 
            this.txtNwaVisaAdd.Border.Class = "TextBoxBorder";
            this.txtNwaVisaAdd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNwaVisaAdd.Location = new System.Drawing.Point(250, 47);
            this.txtNwaVisaAdd.Name = "txtNwaVisaAdd";
            this.txtNwaVisaAdd.ReadOnly = true;
            this.txtNwaVisaAdd.Size = new System.Drawing.Size(297, 21);
            this.txtNwaVisaAdd.TabIndex = 2;
            this.txtNwaVisaAdd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GetVisaAddress);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(250, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(297, 26);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "网分地址";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(25, 140);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(186, 21);
            this.labelX3.TabIndex = 8;
            this.labelX3.Text = "开关型号";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(250, 140);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(297, 21);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "开关地址";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(25, 74);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(297, 21);
            this.labelX5.TabIndex = 12;
            this.labelX5.Text = "烧录器型号";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(345, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 26);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelX7);
            this.groupBox2.Controls.Add(this.btnBrowseTxt);
            this.groupBox2.Controls.Add(this.txtTxtSaveFolder);
            this.groupBox2.Controls.Add(this.labelX41);
            this.groupBox2.Controls.Add(this.btnBrowseSnp);
            this.groupBox2.Controls.Add(this.txtSnpSaveFolder);
            this.groupBox2.Location = new System.Drawing.Point(16, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 151);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "路径设置";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(21, 76);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(209, 23);
            this.labelX7.TabIndex = 35;
            this.labelX7.Text = "TXT文件保存路径";
            // 
            // btnBrowseTxt
            // 
            this.btnBrowseTxt.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBrowseTxt.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBrowseTxt.Location = new System.Drawing.Point(457, 105);
            this.btnBrowseTxt.Name = "btnBrowseTxt";
            this.btnBrowseTxt.Size = new System.Drawing.Size(74, 21);
            this.btnBrowseTxt.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBrowseTxt.TabIndex = 34;
            this.btnBrowseTxt.Text = "浏览";
            this.btnBrowseTxt.Click += new System.EventHandler(this.btnBrowseTxt_Click);
            // 
            // txtTxtSaveFolder
            // 
            // 
            // 
            // 
            this.txtTxtSaveFolder.Border.Class = "TextBoxBorder";
            this.txtTxtSaveFolder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTxtSaveFolder.Location = new System.Drawing.Point(21, 105);
            this.txtTxtSaveFolder.Name = "txtTxtSaveFolder";
            this.txtTxtSaveFolder.ReadOnly = true;
            this.txtTxtSaveFolder.Size = new System.Drawing.Size(412, 21);
            this.txtTxtSaveFolder.TabIndex = 33;
            // 
            // labelX41
            // 
            // 
            // 
            // 
            this.labelX41.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX41.Location = new System.Drawing.Point(21, 20);
            this.labelX41.Name = "labelX41";
            this.labelX41.Size = new System.Drawing.Size(209, 23);
            this.labelX41.TabIndex = 32;
            this.labelX41.Text = "SNP文件保存路径";
            // 
            // btnBrowseSnp
            // 
            this.btnBrowseSnp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBrowseSnp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBrowseSnp.Location = new System.Drawing.Point(457, 49);
            this.btnBrowseSnp.Name = "btnBrowseSnp";
            this.btnBrowseSnp.Size = new System.Drawing.Size(74, 21);
            this.btnBrowseSnp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBrowseSnp.TabIndex = 31;
            this.btnBrowseSnp.Text = "浏览";
            this.btnBrowseSnp.Click += new System.EventHandler(this.btnBrowseSnp_Click);
            // 
            // txtSnpSaveFolder
            // 
            // 
            // 
            // 
            this.txtSnpSaveFolder.Border.Class = "TextBoxBorder";
            this.txtSnpSaveFolder.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSnpSaveFolder.Location = new System.Drawing.Point(21, 49);
            this.txtSnpSaveFolder.Name = "txtSnpSaveFolder";
            this.txtSnpSaveFolder.ReadOnly = true;
            this.txtSnpSaveFolder.Size = new System.Drawing.Size(412, 21);
            this.txtSnpSaveFolder.TabIndex = 3;
            // 
            // frmHardwareSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 438);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "frmHardwareSetting";
            this.Text = "本地设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetting_FormClosing);
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtNwaVisaAdd;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbNwaType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSwitchBox;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSbVisaAdd;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbAdapterType;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbAdpaterPort;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSnpSaveFolder;
        private DevComponents.DotNetBar.ButtonX btnBrowseSnp;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnBrowseTxt;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTxtSaveFolder;
        private DevComponents.DotNetBar.LabelX labelX41;


    }
}