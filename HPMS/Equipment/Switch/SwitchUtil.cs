using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using HPMS.Util;

namespace HPMS.Equipment.Switch
{
    struct CmdFrame
    {
        const byte head = 0xEE;    	//帧头 
        private byte data_length;  //数据长度
        const byte cmd_type=0x1;    	//命令类型,控制开关使用时，固定为1，写入数据。
        public byte[] param;  	//可变长度参数，最多10,待写入的数据区
        private byte check;       	//检验码
        private static readonly byte[] cmd_tail = new byte[4] { 0xFF, 0xFC, 0xFF, 0xFF }; 	    //帧尾

        

        public byte[] convertToByteArray()
        {
            byte[] byteArray;
            int _dataLength = 0;
           
            if ((this.param == null) || (this.param.Length == 0))
            {
                data_length = 8;
                _dataLength = 0;
            }
            else
            {

                data_length = (byte)(8 + param.Length);
                _dataLength = param.Length;
            }


            byte[] btemp = new byte[data_length];
            btemp[0] = head;
            btemp[1] = data_length;
            btemp[2] = cmd_type;

            for (int i = 0; i < _dataLength; i++)
            {
                btemp[3 + i] = param[i];
            }


            byte bCheckSum = 0;
            for (int i = 1; i < 3 + _dataLength; i++)
            {
                bCheckSum = (byte)(bCheckSum ^ btemp[i]);
            }
            btemp[3 + _dataLength] = bCheckSum;
            btemp[4 + _dataLength] = cmd_tail[0];
            btemp[5 + _dataLength] = cmd_tail[1];
            btemp[6 + _dataLength] = cmd_tail[2];
            btemp[7 + _dataLength] = cmd_tail[3];
            
            return btemp;


        }
    }
    /// <summary>
    /// 开关序列号转换成对应的串口数据格式的工具类
    /// </summary>
    public class SwitchUtil
    {
        private static byte[] cmdUART0 = {0xEE,0xA,0x1,0x0,0xFF,0xFC,0xFF,0xFF};//开关完整帧的数据格式
        public static byte[] GetMcuFormatBytes(bool[,] switchArrays, int switchIndex)
        {
            bool[] currentArrays = ArrayEnhance.IndexArray(switchArrays, switchIndex,IndexType.Row);
            int length = currentArrays.Length;
            List<byte>switchEnableIndexs=new List<byte>();
            for (int i = 0; i < length; i++)
            {
                if (currentArrays[i])
                {
                    switchEnableIndexs.Add((byte)(i+1));
                }
            }
            CmdFrame cmdFrame=new CmdFrame();
            cmdFrame.param = switchEnableIndexs.ToArray();
            return cmdFrame.convertToByteArray();
        }
    }
}
