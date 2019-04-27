using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSwitch
{
    /// <summary>
    /// 演示模式开关控制类,无实际作用
    /// </summary>
    public class SwitchDemo:ISwitch
    {
       
        /// <summary>
        /// 关闭所有开关
        /// </summary>
        /// <param name="errMsg">异常信息</param>
        /// <returns>true/false</returns>
        public bool CloseAll(ref string errMsg)
        {
            errMsg = "";
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
            errMsg = "";
            return true;
        }

        /// <summary>
        /// 开启由对应开关组的串口指令指定的开关
        /// </summary>
        /// <param name="switchNum">要写入的byte[]数组</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        public bool Open(byte[] switchNum, ref string errMsg)
        {
            errMsg = "";
            return true;
        }

    }
}
