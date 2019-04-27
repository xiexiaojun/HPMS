using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEPROM.Code.Utility
{
    public class ToChinese:System.Attribute
    {
        private string chinese;

        public ToChinese(string chinese)
        {
            this.chinese = chinese;
        }
        public string Chinese
        {
            get => chinese;
            //set => chinese = value;
        }
    }
}
