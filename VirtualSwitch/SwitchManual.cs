using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VirtualSwitch
{
    /// <summary>
    /// 手动模式开关控制类
    /// </summary>
    public class SwitchManual:ISwitch
    {
        private Func<DialogResult> _blockedMsg;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blockedMsg">能够堵塞进程的对话框委托</param>
        public SwitchManual(Func<DialogResult> blockedMsg)
        {
            this._blockedMsg = blockedMsg;
        }

        /// <summary>
        /// 关闭所有开关
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool CloseAll(ref string errMsg)
        {
            return true;
        }

        /// <summary>
        /// 开启开关矩阵配置中的指定行或列的开关,由构造函数来决定打开一行或一列
        /// </summary>
        /// <param name="switchIndex">行/列索引</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Open(int switchIndex, ref string errMsg)
        {
           //DialogResult ret= MessageBox.Show("请手动接好开关", "", MessageBoxButtons.YesNoCancel);
            DialogResult ret = _blockedMsg();
            return ret == DialogResult.Yes;
        }

        /// <summary>
        /// 开启由对应开关组的串口指令指定的开关
        /// </summary>
        /// <param name="switchNum">要写入的byte[]数组</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        public bool Open(byte[] switchNum, ref string errMsg)
        {
            DialogResult ret = _blockedMsg();
            return ret == DialogResult.Yes;
        }

    }
}
