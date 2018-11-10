using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar.Controls;

namespace HPMS.Util
{
    public class Convert
    {
        public static string List2Str(List<string> source)
        {
            return String.Join(",", source.ToArray());
        }

        public static List<string> Str2List(string source)
        {
            string[] separater = new[] { "," };
            return source.Split(separater, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string formatMsg(string msgIn)
        {
            return DateTime.Now.ToString() + "    " + msgIn;
        }
    }
}
