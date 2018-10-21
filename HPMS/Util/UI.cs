using System.Drawing;
using System.Windows.Forms;

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
        
       
    }
}