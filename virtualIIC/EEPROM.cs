using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualIIC
{
    /// <summary>
    /// I2C操作返回数据
    /// </summary>
    public struct operationResult
    {
        /// <summary>
        /// 读取的数据
        /// </summary>
        public byte[] bufferData;
        /// <summary>
        /// 操作结果
        /// </summary>
        public bool result;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string msg;
       
    }
    class EEPROM
    {
        public const string msgCMDError = "command error";
        public const string msgReadSuccess = "read success";
        public const string msgReadFail = "read fail";
        public const string msgWriteSuccess = "write success";
        public const string msgWriteFail = "write fail";
        public const string msgDeviceNotOpen = "device is not open";
        public const string msgDeviceTimeOut = "device time out";
        public const string msgDeviceChannelSetFail = "set device channel fail";
        public const string msgWriteCheckFail = "write operation success,compare write result fail";

    }

    /// <summary>
    /// 吉阳光电adapter的型号
    /// </summary>
    public enum GYI2CType
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        DEV_GY7501A = 1, //1ch-I2C
        DEV_GY7512 = 2,//2ch-I2C
        DEV_GY7514 = 3,//4ch-I2C
        DEV_GY7518 = 4,//8ch-I2C
        DEV_GY7503 = 5,//1ch-I2C
        DEV_GY7506 = 6,//1ch-I2C,mo
        DEV_GY7601 = 7,//1ch-I2C
        DEV_GY7602 = 8,//2ch-I2C
        DEV_GY7604 = 9,//4ch-I2C
        DEV_GY7608 = 10,//8ch-I2C
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
