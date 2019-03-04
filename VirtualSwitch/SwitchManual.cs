using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VirtualSwitch
{
    public class SwitchManual:ISwitch
    {
        private Func<DialogResult> _blockedMsg;
        public SwitchManual(Func<DialogResult> blockedMsg)
        {
            this._blockedMsg = blockedMsg;
        }

        public bool CloseAll(ref string errMsg)
        {
            throw new NotImplementedException();
        }

        public bool Open(int switchIndex, ref string errMsg)
        {
           //DialogResult ret= MessageBox.Show("请手动接好开关", "", MessageBoxButtons.YesNoCancel);
            DialogResult ret = _blockedMsg();
            return ret == DialogResult.Yes;
        }

        public bool Open(byte[] switchNum, ref string errMsg)
        {
            DialogResult ret = _blockedMsg();
            return ret == DialogResult.Yes;
        }

        public bool OpenS(int switchIndex, ref string errMsg)
        {
            throw new NotImplementedException();
        }

        public bool OpenS(byte[] switchNum, ref string errMsg)
        {
            throw new NotImplementedException();
        }
    }
}
