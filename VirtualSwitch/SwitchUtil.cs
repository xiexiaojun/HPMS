using System.Collections.Generic;
using NationalInstruments.Restricted;
using NationalInstruments.VisaNS;


namespace VirtualSwitch
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
            bool[] currentArrays = IndexArray(switchArrays, switchIndex,IndexType.Row);
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
        public static byte[] GetMcuFormatBytes(byte[]switchArrays)
        {
           
            CmdFrame cmdFrame = new CmdFrame();
            cmdFrame.param = switchArrays;
            return cmdFrame.convertToByteArray();
        }

        /// <summary>
        /// 索引二维数组中的一行或一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="index"></param>
        /// <param name="indexType"></param>
        /// <returns></returns>
        private static T[] IndexArray<T>(T[,] sourceArray, int index, IndexType indexType)
        {
            int rowCount = sourceArray.GetLength(0);
            int colCount = sourceArray.GetLength(1);
            //int size = index.Length;
            if (indexType == IndexType.Row)
            {
                T[] ret = new T[colCount];

                for (int i = 0; i < colCount; i++)
                {
                    ret[i] = sourceArray[index, i];
                }

                return ret;
            }
            else
            {
                T[] ret = new T[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    ret[i] = sourceArray[i, index];

                }
                return ret;
            }
        }

        public static string[] GetResource()
        {
            return ResourceManager.GetLocalManager().FindResources("?*");
        }

        public static int FindIndex(string[] source, string findstr)
        {
            return source.IndexOf(findstr);
        }
    }
}
