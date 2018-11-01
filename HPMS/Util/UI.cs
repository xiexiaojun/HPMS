using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using HPMS.Languange;

namespace HPMS.Util
{
    public class UI
    {
       
       
        public static Point LocationOnClient(Control c, Point pointOffset)
        {
           
            Point p = c.PointToScreen(new Point(0, 0));
            //p.Y += c.Height;
            p.X += c.Width;
            p.Offset(pointOffset);
            return p;
          }

        public static void MessageBoxMuti(string msg)
        {
            MessageBoxEx.Show(Languange.LanguageHelper.GetMsgText(msg),"HPMS System");
        }

        public static DialogResult MessageBoxYesNoMuti(string msg)
        {
            return MessageBoxEx.Show(msg, "HPMS System", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        }

        
    }

    public class Office2007Muti : Office2007Form
    {
        public Office2007Muti()
        {
            base.TextChanged+=Office2007Muti_TextChanged;
            base.Shown+=Office2007Muti_Shown;
        }

        private void Office2007Muti_Shown(object sender, EventArgs e)
        {
            this.Text = LanguageHelper.GetLanguageText(this.Text);
        }

        private void Office2007Muti_TextChanged(object sender, EventArgs e)
        {
            LanguageHelper.SetControlLanguageText(this);
            base.ControlAdded += MyStyleFormBase_ControlAdded;
        }
        private void MyStyleFormBase_ControlAdded(object sender, ControlEventArgs e)
        {
            LanguageHelper.SetControlLanguageText(e.Control);
        }
        protected virtual void PerformChildrenChange(Control target)
        {
            LanguageHelper.SetControlLanguageText(target);
        }
    }
}