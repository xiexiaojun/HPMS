namespace HPMS
{
    partial class frmRoleEdit
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
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtRoleDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chk_MyProfile_rightsList = new System.Windows.Forms.CheckedListBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.chkUserEnable = new DevComponents.DotNetBar.Controls.CheckBoxX();
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
            this.labelX1.Text = "角色名";
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
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 81);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(78, 23);
            this.labelX2.TabIndex = 37;
            this.labelX2.Text = "角色描述";
            // 
            // txtRoleDescription
            // 
            // 
            // 
            // 
            this.txtRoleDescription.Border.Class = "TextBoxBorder";
            this.txtRoleDescription.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRoleDescription.Location = new System.Drawing.Point(12, 119);
            this.txtRoleDescription.Multiline = true;
            this.txtRoleDescription.Name = "txtRoleDescription";
            this.txtRoleDescription.Size = new System.Drawing.Size(218, 180);
            this.txtRoleDescription.TabIndex = 38;
            // 
            // chk_MyProfile_rightsList
            // 
            this.chk_MyProfile_rightsList.FormattingEnabled = true;
            this.chk_MyProfile_rightsList.Location = new System.Drawing.Point(265, 119);
            this.chk_MyProfile_rightsList.Name = "chk_MyProfile_rightsList";
            this.chk_MyProfile_rightsList.Size = new System.Drawing.Size(196, 180);
            this.chk_MyProfile_rightsList.TabIndex = 39;
            this.chk_MyProfile_rightsList.ThreeDCheckBoxes = true;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(265, 81);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(78, 23);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "权限列表";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(298, 311);
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
            this.btnCancel.Location = new System.Drawing.Point(391, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkUserEnable
            // 
            // 
            // 
            // 
            this.chkUserEnable.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUserEnable.Location = new System.Drawing.Point(275, 38);
            this.chkUserEnable.Name = "chkUserEnable";
            this.chkUserEnable.Size = new System.Drawing.Size(100, 23);
            this.chkUserEnable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUserEnable.TabIndex = 44;
            this.chkUserEnable.Text = "启用状态";
            // 
            // frmRoleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 339);
            this.Controls.Add(this.chkUserEnable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.chk_MyProfile_rightsList);
            this.Controls.Add(this.txtRoleDescription);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtUserRole);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "frmRoleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmRoleEdit";
            this.Load += new System.EventHandler(this.frmRoleEdit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserRole;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRoleDescription;
        private System.Windows.Forms.CheckedListBox chk_MyProfile_rightsList;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRoleEnable;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUserEnable;
    }
}