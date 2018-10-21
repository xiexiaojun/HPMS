using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using NationalInstruments.VisaNS;
using System.IO.Ports;

namespace HPMS.Equipment
{
    public class Util
    {
        /// <summary>
        /// 返回visa资源列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetVisaList()
        {
            List<string>ret=new List<string>();
            string[] resources = ResourceManager.GetLocalManager().FindResources("?*");
            foreach (string s in resources)
            {
                ret.Add(s);
            }
            if (ret.Count == 0)
            {
                ret.Add("无Visa设备");
            }
            return ret;
        }

        /// <summary>
        /// 返回串口列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSerialPortsList()
        {
            List<string>ret=new List<string>();
            
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                ret.Add(vPortName);
            }

            if (ret.Count == 0)
            {
                ret.Add("无串口");
            }

            return ret;
        }
    }
}
