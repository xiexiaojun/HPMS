﻿namespace EEPROM.Forms
{
    partial class FrmLogin
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
            this.txtUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtPsw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnLogin = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.chkDBMode = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.rbtMode_Write = new System.Windows.Forms.RadioButton();
            this.rbtMode_Check = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelX1.Location = new System.Drawing.Point(29, 93);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(90, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "用户名";
            // 
            // txtUser
            // 
            // 
            // 
            // 
            this.txtUser.Border.Class = "TextBoxBorder";
            this.txtUser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUser.Location = new System.Drawing.Point(125, 93);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(168, 21);
            this.txtUser.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelX2.Location = new System.Drawing.Point(29, 127);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(90, 21);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "密码";
            // 
            // txtPsw
            // 
            // 
            // 
            // 
            this.txtPsw.Border.Class = "TextBoxBorder";
            this.txtPsw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPsw.Location = new System.Drawing.Point(125, 129);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.PasswordChar = '*';
            this.txtPsw.Size = new System.Drawing.Size(168, 21);
            this.txtPsw.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLogin.Location = new System.Drawing.Point(125, 196);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(70, 28);
            this.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(225, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 28);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Calibri", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelX3.Location = new System.Drawing.Point(9, 34);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(382, 53);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "EEPROM SYSTEM";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // chkDBMode
            // 
            this.chkDBMode.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkDBMode.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkDBMode.Checked = true;
            this.chkDBMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDBMode.CheckValue = "Y";
            this.chkDBMode.Location = new System.Drawing.Point(309, 205);
            this.chkDBMode.Name = "chkDBMode";
            this.chkDBMode.Size = new System.Drawing.Size(74, 19);
            this.chkDBMode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkDBMode.TabIndex = 7;
            this.chkDBMode.Text = "本地登陆";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelX4.Location = new System.Drawing.Point(29, 161);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(90, 21);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "模式";
            // 
            // rbtMode_Write
            // 
            this.rbtMode_Write.AutoSize = true;
            this.rbtMode_Write.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtMode_Write.ForeColor = System.Drawing.Color.White;
            this.rbtMode_Write.Location = new System.Drawing.Point(125, 161);
            this.rbtMode_Write.Name = "rbtMode_Write";
            this.rbtMode_Write.Size = new System.Drawing.Size(73, 26);
            this.rbtMode_Write.TabIndex = 9;
            this.rbtMode_Write.TabStop = true;
            this.rbtMode_Write.Text = "Write";
            this.rbtMode_Write.UseVisualStyleBackColor = true;
            // 
            // rbtMode_Check
            // 
            this.rbtMode_Check.AutoSize = true;
            this.rbtMode_Check.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtMode_Check.ForeColor = System.Drawing.Color.White;
            this.rbtMode_Check.Location = new System.Drawing.Point(217, 161);
            this.rbtMode_Check.Name = "rbtMode_Check";
            this.rbtMode_Check.Size = new System.Drawing.Size(76, 26);
            this.rbtMode_Check.TabIndex = 10;
            this.rbtMode_Check.TabStop = true;
            this.rbtMode_Check.Text = "Check";
            this.rbtMode_Check.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EEPROM.Properties.Resources.LUXSHARE_ICT_LOGO_透明底_400x90;
            this.pictureBox1.Location = new System.Drawing.Point(5, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(387, 235);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rbtMode_Check);
            this.Controls.Add(this.rbtMode_Write);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.chkDBMode);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPsw);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUser;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPsw;
        private DevComponents.DotNetBar.ButtonX btnLogin;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDBMode;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.RadioButton rbtMode_Write;
        private System.Windows.Forms.RadioButton rbtMode_Check;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}