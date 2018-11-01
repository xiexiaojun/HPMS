namespace HPMS
{
    partial class frmPswModify
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
            this.txtOldPsw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtNewPsw = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtNewPswR = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnModify = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txtOldPsw
            // 
            // 
            // 
            // 
            this.txtOldPsw.Border.Class = "TextBoxBorder";
            this.txtOldPsw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOldPsw.Location = new System.Drawing.Point(30, 38);
            this.txtOldPsw.Name = "txtOldPsw";
            this.txtOldPsw.Size = new System.Drawing.Size(276, 21);
            this.txtOldPsw.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(30, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 29);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "请输入原密码";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(30, 74);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 29);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "请输入新密码";
            // 
            // txtNewPsw
            // 
            // 
            // 
            // 
            this.txtNewPsw.Border.Class = "TextBoxBorder";
            this.txtNewPsw.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewPsw.Location = new System.Drawing.Point(30, 112);
            this.txtNewPsw.Name = "txtNewPsw";
            this.txtNewPsw.Size = new System.Drawing.Size(276, 21);
            this.txtNewPsw.TabIndex = 3;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(30, 151);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(87, 29);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "请再输一次";
            // 
            // txtNewPswR
            // 
            // 
            // 
            // 
            this.txtNewPswR.Border.Class = "TextBoxBorder";
            this.txtNewPswR.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewPswR.Location = new System.Drawing.Point(30, 186);
            this.txtNewPswR.Name = "txtNewPswR";
            this.txtNewPswR.Size = new System.Drawing.Size(276, 21);
            this.txtNewPswR.TabIndex = 5;
            // 
            // btnModify
            // 
            this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnModify.Location = new System.Drawing.Point(145, 226);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(70, 30);
            this.btnModify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(236, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 30);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            // 
            // frmPswModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 264);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.txtNewPswR);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtNewPsw);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtOldPsw);
            this.DoubleBuffered = true;
            this.Name = "frmPswModify";
            this.Text = "frmPswModify";
            this.Load += new System.EventHandler(this.frmPswModify_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtOldPsw;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewPsw;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewPswR;
        private DevComponents.DotNetBar.ButtonX btnModify;
        private DevComponents.DotNetBar.ButtonX btnClose;
    }
}