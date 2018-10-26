namespace Register
{
    partial class frmRegister
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtMachineCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnGetMachineCode = new DevComponents.DotNetBar.ButtonX();
            this.btnCalcuCode = new DevComponents.DotNetBar.ButtonX();
            this.cmbSoftName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cmbSoftVersion = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtKey = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(14, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 30);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "机器码";
            // 
            // txtMachineCode
            // 
            // 
            // 
            // 
            this.txtMachineCode.Border.Class = "TextBoxBorder";
            this.txtMachineCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMachineCode.Location = new System.Drawing.Point(62, 33);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(238, 21);
            this.txtMachineCode.TabIndex = 2;
            // 
            // btnGetMachineCode
            // 
            this.btnGetMachineCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetMachineCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetMachineCode.Location = new System.Drawing.Point(336, 33);
            this.btnGetMachineCode.Name = "btnGetMachineCode";
            this.btnGetMachineCode.Size = new System.Drawing.Size(110, 24);
            this.btnGetMachineCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetMachineCode.TabIndex = 3;
            this.btnGetMachineCode.Text = "获取本机注册码";
            this.btnGetMachineCode.Click += new System.EventHandler(this.btnGetMachineCode_Click);
            // 
            // btnCalcuCode
            // 
            this.btnCalcuCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCalcuCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCalcuCode.Location = new System.Drawing.Point(478, 33);
            this.btnCalcuCode.Name = "btnCalcuCode";
            this.btnCalcuCode.Size = new System.Drawing.Size(110, 24);
            this.btnCalcuCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCalcuCode.TabIndex = 4;
            this.btnCalcuCode.Text = "计算注册码";
            this.btnCalcuCode.Click += new System.EventHandler(this.btnCalcuCode_Click);
            // 
            // cmbSoftName
            // 
            this.cmbSoftName.DisplayMember = "Text";
            this.cmbSoftName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSoftName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoftName.FormattingEnabled = true;
            this.cmbSoftName.ItemHeight = 15;
            this.cmbSoftName.Items.AddRange(new object[] {
            this.comboItem2});
            this.cmbSoftName.Location = new System.Drawing.Point(63, 84);
            this.cmbSoftName.Name = "cmbSoftName";
            this.cmbSoftName.Size = new System.Drawing.Size(113, 21);
            this.cmbSoftName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSoftName.TabIndex = 6;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(210, 84);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(44, 21);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "版本";
            // 
            // cmbSoftVersion
            // 
            this.cmbSoftVersion.DisplayMember = "Text";
            this.cmbSoftVersion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSoftVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoftVersion.FormattingEnabled = true;
            this.cmbSoftVersion.ItemHeight = 15;
            this.cmbSoftVersion.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem3,
            this.comboItem4});
            this.cmbSoftVersion.Location = new System.Drawing.Point(260, 84);
            this.cmbSoftVersion.Name = "cmbSoftVersion";
            this.cmbSoftVersion.Size = new System.Drawing.Size(113, 21);
            this.cmbSoftVersion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSoftVersion.TabIndex = 8;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(14, 86);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(44, 21);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "软件";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "HPMS";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "express";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "professional";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "enterprise";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(402, 86);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(44, 21);
            this.labelX4.TabIndex = 11;
            this.labelX4.Text = "秘钥";
            // 
            // txtKey
            // 
            // 
            // 
            // 
            this.txtKey.Border.Class = "TextBoxBorder";
            this.txtKey.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtKey.Location = new System.Drawing.Point(452, 86);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(136, 21);
            this.txtKey.TabIndex = 12;
            // 
            // txtCode
            // 
            // 
            // 
            // 
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(16, 142);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(572, 168);
            this.txtCode.TabIndex = 13;
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 333);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cmbSoftVersion);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cmbSoftName);
            this.Controls.Add(this.btnCalcuCode);
            this.Controls.Add(this.btnGetMachineCode);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "frmRegister";
            this.Text = "注册机";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMachineCode;
        private DevComponents.DotNetBar.ButtonX btnGetMachineCode;
        private DevComponents.DotNetBar.ButtonX btnCalcuCode;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSoftName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSoftVersion;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtKey;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCode;

    }
}

