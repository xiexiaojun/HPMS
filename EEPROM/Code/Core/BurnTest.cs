using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EEPROMUtility;
using VirtualIIC;

namespace EEPROM.Code.Core
{
    public struct TestConfig
    {
        public Ii2c I2CDevice;
        public Burn Burn;
        public Action<string> DisplayStatus;
    }
    public class BurnTest
    {
        public static TestConfig TestConfig;
        //public bool Write(byte[][] template)
        //{
        //    return TestConfig.Burn.WriteData(template);
        //}

        //public byte[][] Read()
        //{
        //   return TestConfig.Burn.ReadData();
        //}
    }
}
