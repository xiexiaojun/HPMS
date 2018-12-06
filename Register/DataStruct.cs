using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    class DataStruct
    {
    }

    public class SoftFunc
    {
        public string No { set; get; }
        public string Description { set; get; }
        public IList<SoftFunc>Children=new List<SoftFunc>();

        public virtual void AddChildren(SoftFunc softFunc)
        {
            this.Children.Add(softFunc);
        }

       
    }
}
