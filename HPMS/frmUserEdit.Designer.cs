namespace HPMS
{
    partial class frmUserEdit
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtUserRole = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.chkUserEnable = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmbRole = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(99, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "用户名";
            // 
            // txtUserRole
            // 
            // 
            // 
            // 
            this.txtUserRole.Border.Class = "TextBoxBorder";
            this.txtUserRole.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserRole.Location = new System.Drawing.Point(12, 41);
            this.txtUserRole.Name = "txtUserRole";
            this.txtUserRole.ReadOnly = true;
            this.txtUserRole.Size = new System.Drawing.Size(218, 21);
            this.txtUserRole.TabIndex = 36;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 79);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(78, 23);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "角色";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(78, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(176, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkUserEnable
            // 
            // 
            // 
            // 
            this.chkUserEnable.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUserEnable.Location = new System.Drawing.Point(176, 119);
            this.chkUserEnable.Name = "chkUserEnable";
            this.chkUserEnable.Size = new System.Drawing.Size(100, 23);
            this.chkUserEnable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUserEnable.TabIndex = 45;
            this.chkUserEnable.Text = "启用状态";
            // 
            // cmbRole
            // 
            this.cmbRole.DisplayMember = "Text";
            this.cmbRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.ItemHeight = 15;
            this.cmbRole.Location = new System.Drawing.Point(12, 119);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(136, 21);
            this.cmbRole.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbRole.TabIndex = 46;
            // 
            // frmUserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 218);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.chkUserEnable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtUserRole);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "frmUserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmUserEdit";
            this.Load += new System.EventHandler(this.frmUserEdit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserRole;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUserEnable;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbRole;
    }
}