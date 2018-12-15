using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Tool
{
   

    

    public class Ui
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
            MessageBoxEx.Show(LanguageHelper.GetMsgText(msg), "HPMS System");
        }

        public static void MessageBoxMuti(string msg,System.Windows.Forms.IWin32Window window)
        {
            MessageBoxEx.Show(window,LanguageHelper.GetMsgText(msg), "HPMS System");
        }
        public static DialogResult MessageBoxYesNoMuti(string msg)
        {
            return MessageBoxEx.Show(msg, "HPMS System", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

        }


    }
}