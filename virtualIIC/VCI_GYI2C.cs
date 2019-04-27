using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace VirtualIIC
{

     struct GYI2C_DATA_INFO{
         public byte SlaveAddr;           //设备物理地址，bit7-1有效
         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 520)]
         public byte[] Databuffer;      //Data 报文的数据；
         public Int32 WriteNum;           //地址和数据的总个数
         public int ReadNum;           //需要读的数据的个数
         public byte IoSel;             //1 表示被选择，将被读/写
         public byte IoData;              //IO口状态，bit3－0分别表示4个IO口,只有与IoSel中为1的位相同的位值有效
         public int DlyMsRead;          //I2C读操作时，PC发出读命令后，延时多少ms请求读到的数据。
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
         public byte[] Reserved;    //Reserved 系统保留。
     }

     /// <summary>
     /// 吉阳光电adapter控制类
     /// </summary>
    public class VCI_GYI2C : Ii2c
    {
       

        //public const int DEV_GY7501A = 1; //1ch-I2C
        //public const int DEV_GY7512 = 2;//2ch-I2C
        //public const int DEV_GY7514 = 3;//4ch-I2C
        //public const int DEV_GY7518 = 4;//8ch-I2C
        //public const int DEV_GY7503 = 5;//1ch-I2C
        //public const int DEV_GY7506 = 6;//1ch-I2C,module/
        //public const int DEV_GY7601 = 7;//1ch-I2C
        //public const int DEV_GY7602 = 8;//2ch-I2C
        //public const int DEV_GY7604 = 9;//4ch-I2C
        //public const int DEV_GY7608 = 10;//8ch-I2C

        const byte IoData = 0; //1 表示被选择，将被读/写， bit3－0 分别表示 4 个 IO 口
        const byte IoSel = 0;//IO 口状态， bit3－0 分别表示 4 个 IO 口,只有与 IoSel 中为 1 的位相同的位值有效


        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Open(int DeviceType, int DeviceInd, int Reserved);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Close(int DeviceType, int DeviceInd);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_SetMode(int DeviceType, int DeviceInd, byte ModeValue);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_GetMode(int DeviceType, int DeviceInd);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_SetClk(int DeviceType, int DeviceInd, int ClkValue);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_GetClk(int DeviceType, int DeviceInd);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_SetChannel(int DeviceType, int DeviceInd, int ChannelValue);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_GetChannel(int DeviceType, int DeviceInd);

        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Read(int DeviceType, int DeviceInd, ref GYI2C_DATA_INFO pDataInfo);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Write(int DeviceType, int DeviceInd, ref GYI2C_DATA_INFO pDataInfo);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Write2(int DeviceType, int DeviceInd, ref byte[] buffer, int bufferLength);

        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_SetIO(int DeviceType, int DeviceInd, ref GYI2C_DATA_INFO pDataInfo);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_GetIO(int DeviceType, int DeviceInd, ref GYI2C_DATA_INFO pDataInfo);

        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Start(int DeviceType, int DeviceInd);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_WriteByte(int DeviceType, int DeviceInd, byte DataValue);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_ReadByte(int DeviceType, int DeviceInd, byte DataValue);
        [DllImport("dll\\VCI_GYI2C.dll")]
        private static extern int GYI2C_Stop(int DeviceType, int DeviceInd);

        private int deviceType;
        private int deviceInd;
        private int writeBlock;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceType">设备类型，通常为7604</param>
        /// <param name="deviceInd">设备索引，通常为0</param>
        /// <param name="writeBlock">每次写入数据字节数，通常为8个字节</param>
        public VCI_GYI2C(GYI2CType deviceType, int deviceInd, int writeBlock)
        {
            this.deviceType = (int)deviceType;
            this.deviceInd = deviceInd;
            this.writeBlock = writeBlock;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool openDevice() {
            int temp = VCI_GYI2C.GYI2C_Open(deviceType, deviceInd, 115200);
            if (temp == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool closeDevice() {
            int temp = VCI_GYI2C.GYI2C_Close(deviceType, deviceInd);
            if (temp == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>
        /// 写入数据
        /// </summary>
        /// <param name="slaveAdd">器件地址</param>
        /// <param name="startWriteAdd"></param>
        /// <param name="channel"></param>
        /// <param name="writeLen"></param>
        /// <param name="writeBuffer"></param>
        /// <returns></returns>
        public operationResult writeDevice(byte slaveAdd, byte startWriteAdd, byte channel, int writeLen, byte[] writeBuffer)
        {
            operationResult oR = new operationResult();
            int ClkValue = GYI2C_GetClk(deviceType, deviceInd);
            int ChValue = channel;
            int temp = VCI_GYI2C.GYI2C_SetChannel(deviceType, deviceInd, ChValue);
            if (temp == 0)
            {
                oR.result = false;
                oR.msg = "fail to set channel" + ChValue.ToString() ;
            }
            else if (temp == -1)
            {
                oR.result = false;
                oR.msg = "device is not open";

            }
            else {
                //temp = 1;
                int writedLen = writeBytes(slaveAdd, writeBlock, startWriteAdd, ClkValue, writeBuffer);
                if (writedLen == writeBuffer.Length)
                {
                    oR.result = true;
                    oR.msg = "write success";
                }
                else if (writeLen < 0)
                {
                    oR.result = false;
                    oR.msg = "device is not open!";
                }
                else {
                    oR.result = false;
                    oR.msg = "write fail";
                }
              
            }
            return oR;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="slaveAdd">器件地址</param>
        /// <param name="startReadAdd">起始位置</param>
        /// <param name="channel">通道</param>
        /// <param name="readLen">读取长度</param>
        /// <returns></returns>
        public operationResult readDevice(byte slaveAdd, byte startReadAdd, byte channel, byte readLen)
        {
            operationResult oR = new operationResult();
            int ClkValue = GYI2C_GetClk(deviceType, deviceInd);
            int ChValue = channel;
            int temp = VCI_GYI2C.GYI2C_SetChannel(deviceType, deviceInd, ChValue);
            if (temp == 0)
            {
                oR.result = false;
                oR.msg = "fail to set channel" + ChValue.ToString() + " !";
            }
            else if (temp == -1)
            {
                oR.result = false;
                oR.msg = "device is not open!";

            }
            else
            {
                //temp = 1;
                byte[] readBuffer = new byte[readLen];
                int readBufferLength=readBytes(slaveAdd, readLen, startReadAdd, ClkValue, ref readBuffer);
                if (readBufferLength == readLen)
                {
                    oR.result = true;
                    oR.msg = "read success";
                    oR.bufferData = readBuffer;
                }
                else {
                    oR.result = false;
                    oR.msg = "read fail";
                }
            }
            return oR;

        }

        private int readBytes(byte SlaveAddr, int readSize, int startAddress, int ClkValue, ref byte[] readBuffer)
        {
           
            GYI2C_DATA_INFO pDataInfo = new GYI2C_DATA_INFO();
            pDataInfo.IoData = IoData;
            pDataInfo.IoSel = IoSel;
            pDataInfo.SlaveAddr = SlaveAddr;
            pDataInfo.Databuffer = new byte[520];

            pDataInfo.Databuffer[0] = (byte)startAddress;
            pDataInfo.ReadNum = readSize;
            pDataInfo.DlyMsRead = 10 / ClkValue * (pDataInfo.ReadNum + 20);    //留一点余量
            pDataInfo.WriteNum = 1;

            int len = VCI_GYI2C.GYI2C_Read(deviceType, deviceInd, ref pDataInfo);
            if (len > 0) {
                 Array.Copy(pDataInfo.Databuffer, readBuffer, len);
            }
            return len;
        }


        private int writeBytes(byte SlaveAddr,int writeSize, int startAddress, int ClkValue,byte[] writeBuffer)
        {

          
            int writeBufferLength = writeBuffer.Length;
            int writeTimes = writeBufferLength / writeSize;

            int writeMod = writeBufferLength % writeSize;
            if (writeMod > 0)
            {
                writeTimes = writeTimes + 1;
            }

            int writeCount = 0;
            for (int i = 0; i < writeTimes; i++)
            {
                GYI2C_DATA_INFO pDataInfo = new GYI2C_DATA_INFO();
                pDataInfo.IoData = IoData;
                pDataInfo.IoSel = IoSel;
                pDataInfo.SlaveAddr = SlaveAddr;
                pDataInfo.Databuffer = new byte[520];
                pDataInfo.Reserved = new byte[4] { 0, 0, 0, 0 };
                pDataInfo.Databuffer[0] = (byte)(i * writeSize + startAddress);

                int writeLength = (writeSize < writeBufferLength - i * writeSize) ? writeSize : writeBufferLength - i * writeSize;
                for (int j = 0; j < writeLength; j++)
                {
                    pDataInfo.Databuffer[j + 1] = writeBuffer[i * writeSize + j];
                }
                pDataInfo.ReadNum = 0;
                pDataInfo.DlyMsRead = 10 * (writeSize + 20) / ClkValue;    //留一点余量
                pDataInfo.WriteNum = writeLength + 1;
                int len = VCI_GYI2C.GYI2C_Write(deviceType, deviceInd, ref pDataInfo);
                writeCount = writeCount + len;
                if (len != 1)
                 {
                 break;
                }

            }
         
            return writeCount;

        }

    }
}
