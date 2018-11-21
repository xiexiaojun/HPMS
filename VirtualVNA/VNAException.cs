using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualVNA
{
    [Serializable]
    public class VnaException:ApplicationException
    {
        private string error;
        private Exception innerException;

        public VnaException()
        {

        }

        public VnaException(string msg) : base(msg)
        {
            this.error = msg;
        }

        public VnaException(string msg, Exception innerException) : base(msg)
        {
            this.error = msg;
            this.innerException = innerException;
        }

        public string GetError()
        {
            return error;
        }
    }
}
