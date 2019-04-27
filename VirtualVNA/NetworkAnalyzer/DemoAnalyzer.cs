using System;
using System.Threading;
using VirtualSwitch;

namespace VirtualVNA.NetworkAnalyzer
{
    /// <summary>
    /// 演示模式网分设备控制，无实际作用
    /// </summary>
    public class DemoAnalyzer:NetworkAnalyzer
    {
        private ISwitch _iswitch;
        private string _visaAddress;
        private int _responseTime;

        /// <summary>
        /// 演示模网分构造函数
        /// </summary>
        /// <param name="iSwitch">开关对象</param>
        /// <param name="visaAddress">网分visa地址</param>
        /// <param name="responseTime">响应时间</param>
        public DemoAnalyzer(ISwitch iSwitch,string visaAddress,int responseTime=2000)
        {
            if (responseTime == 0)
                responseTime = 2000;
            this._iswitch = iSwitch;
            this._visaAddress = visaAddress;
            this._responseTime = responseTime;
            //Connect();

        }

        /// <summary>
        /// 保存s4p
        /// </summary>
        /// <param name="saveFilePath">s4p文件路径</param>
        /// <param name="switchIndex">开关矩阵索引</param>
        /// <param name="mutiChannel">是否使用多个channel</param>
        /// <param name="nextByTrace">是否串音</param>
        /// <param name="msg">异常信息</param>
        /// <returns>true/false</returns>
        public override bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
            //Thread.Sleep(3000);
           // throw new Exception("Test error");
            return true;
        }

        /// <summary>
        /// 保存s4p
        /// </summary>
        /// <param name="saveFilePath">s4p文件路径</param>
        /// <param name="switchIndex">开关矩阵字节数组</param>
        /// <param name="index">开关矩阵索引</param>
        /// <param name="mutiChannel">是否使用多个channel</param>
        /// <param name="nextByTrace">是否串音</param>
        /// <param name="msg">异常信息</param>
        /// <returns>true/false</returns>
        public override bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg)
        {
            bool ret = false;
            ret = _iswitch.Open(switchIndex, ref msg);
            if (!ret)
            {
                return false;
            }
           // Thread.Sleep(3000);
           // throw new Exception("Test error");
            return true;
        }

        /// <summary>
        /// 直接获取测试数据，不保存s4p文件
        /// </summary>
        /// <param name="fre">返回的频率</param>
        /// <param name="db">返回的db值</param>
        /// <param name="switchIndex">开关矩阵索引</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        [Obsolete]
        public override bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg)
        {
            return true;
        }

        /// <summary>
        /// 载入校准档案文件
        /// </summary>
        /// <param name="calFilePath">档案文件路径,相对网分设备</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool LoadCalFile(string calFilePath, ref string msg)
        {
            return true;
        }
    }
}
