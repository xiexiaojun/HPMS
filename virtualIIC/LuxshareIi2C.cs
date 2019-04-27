using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;



namespace VirtualIIC
{
    /// <summary>
    /// CP2102(立讯自制)adapter控制类
    /// </summary>
   public class LuxshareIi2C:Ii2c
    {

        
      
        //b)	指令错误时，将返回数据0xEE 0x08 0x21 0x29 0xFF 0xFC 0xFF 0xFF
        //c)	读取数据，若读取成功将返回读取的数据，数据存放在param[256]中，
        //cmd_bus、cmd_type等信息会和上位机发送的一致；
        //若读取失败将返回0xEE 0x08 0x21 0x29 0xFF 0xFC 0xFF 0xFF
        //d)	写入数据时，若写入成功将返回数据0xEE 0x08 0x20 0x28 0xFF 0xFC 0xFF 0xFF
        //若写入失败将返回数据0xEE 0x08 0x21 0x29 0xFF 0xFC 0xFF 0xFF

        const byte read = 0;
        const byte write = 1;
        static readonly byte[] cmd_error = new byte[4] { 0xFF, 0xFC, 0xFF, 0xFF }; 	    //命令错误时返回信息
        static readonly byte[] cmd_read_fail = new byte[8] { 0xEE, 0x08, 0x21, 0x29, 0xFF, 0xFC, 0xFF, 0xFF }; 	    //读取失败时返回信息
        static readonly byte[] cmd_write_success = new byte[8] { 0xEE, 0x08, 0x20, 0x28, 0xFF, 0xFC, 0xFF, 0xFF }; 	    //写入成功时返回信息
        static readonly byte[] cmd_write_fail = new byte[8] { 0xEE, 0x08, 0x21, 0x29, 0xFF, 0xFC, 0xFF, 0xFF }; 	    //写入失败时返回信息


         struct commandFrame {
            const  byte cmd_head = 0xEE;    	//帧头 
	        byte    data_length_H;  //数据长度高位
	        byte    data_length_L;  //数据长度	低位
	        public byte    cmd_bus;    	//IIC路数选择
	        public byte    cmd_type;    	//IIC读写选择
	        public byte    cmd_slaves;     //从机地址
	        public byte    addr;   			//读写地址
	        public byte 	   length;			//读写长度	 若读写长度为256，该位值为0
	        public byte[]    param;  	//可变长度参数，最多10
	        byte    check;       	//检验码
            static readonly byte[] cmd_tail = new byte[4] { 0xFF, 0xFC, 0xFF, 0xFF }; 	    //帧尾
	      

            public byte[] convertToByteArray() {
                byte[] byteArray;

                if ((this.param == null) || (this.param.Length == 0))
                {
                    this.data_length_H = 0;
                    this.data_length_L = 0xD;
                }
                else {
                    this.data_length_H = 0;
                    this.data_length_L = (byte)(0xD + this.param.Length);
                }
                if (this.cmd_type == read)
                {
                    this.length = 0;
                   
                }
                else {
                    this.length = (byte)this.param.Length;
                 
                }

                byte[] btemp = new byte[this.data_length_L];
                btemp[0] = cmd_head;
                btemp[1] = this.data_length_H;
                btemp[2] = this.data_length_L;
                btemp[3] = this.cmd_bus;
                btemp[4] = this.cmd_type;
                btemp[5] = this.cmd_slaves;
                btemp[6] = this.addr;
                btemp[7] = this.length;

                for (int i = 0; i < this.length; i++) {
                    btemp[8 + i] = this.param[i];
                }


                byte bCheckSum = 0;
                for (int i = 1; i < 8+this.length; i++)
                {
                    bCheckSum = (byte)(bCheckSum ^ btemp[i]);
                }
                btemp[8+this.length] = bCheckSum;
                btemp[9 + this.length] =cmd_tail[0];
                btemp[10 + this.length] = cmd_tail[1];
                btemp[11 + this.length] = cmd_tail[2];
                btemp[12 + this.length] = cmd_tail[3];
                byteArray = btemp;

                return btemp;


            }
        }

        private SerialPort sp;
        private int writeBlock;
        private int timeOut;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sp">串口对象</param>
        /// <param name="writeBlock">每次写入数据字节数，通常为8个字节</param>
        /// <param name="timeOut">超时</param>
        public LuxshareIi2C(SerialPort sp, int writeBlock,int timeOut)
        {
            SetSerialPorts(sp, writeBlock, timeOut);
        }

        private void SetSerialPorts(SerialPort sp, int writeBlock, int timeOut)
        {
            this.sp = sp;
            this.writeBlock = writeBlock;
            this.timeOut = timeOut;
        }

        public LuxshareIi2C(string serialPortName, int writeBlock, int timeOut)
        {
            SerialPort sp=new SerialPort();
            sp.PortName = serialPortName;
            sp.Parity = Parity.None;
            sp.BaudRate = 115200;
            sp.StopBits = StopBits.One;
            sp.WriteBufferSize = 1024;
            sp.ReadBufferSize = 1024;
            sp.DataBits = 8;
            SetSerialPorts(sp, writeBlock, timeOut);
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool openDevice(){
            if (this.sp.IsOpen)
            {
                return true;
            }
            else {
                sp.Open();
                return sp.IsOpen;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool closeDevice() {
            if (this.sp.IsOpen) {
                sp.Close();
            }
            return !this.sp.IsOpen;
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
        public operationResult writeDevice(byte slaveAdd, byte startWriteAdd, byte channel, int writeLen,  byte[] writeBuffer)
        {
            operationResult oR = new operationResult();
            int writeTimes = writeLen / writeBlock;
            int writeMod = writeLen % writeBlock;
            if (writeMod > 0)
            {
                writeTimes = writeTimes + 1;
            }
            bool writeResult = true;
            for (int i = 0; i < writeTimes; i++) {
                int writeLength = (writeBlock < (writeLen - i * writeBlock)) ? writeBlock : (writeLen - i * writeBlock);
                byte[] writeBufferBlock=new byte[writeLength];
                for (int j = 0; j < writeLength; j++) { 
                    writeBufferBlock[j]=writeBuffer[i*writeBlock+j];
                }


                operationResult oRtemp = sendData(slaveAdd, (byte)(startWriteAdd + i * writeBlock), channel, (byte)writeLength, sp, writeBufferBlock);
                if (!oRtemp.result)
                {
                    writeResult = false;
                    oR.msg = oRtemp.msg;
                    break;
                }
            }
            oR.result = writeResult;
            if (writeResult)
            {
                oR.msg = EEPROM.msgWriteSuccess;
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
            return sendData(slaveAdd, startReadAdd, channel, readLen, sp);
        }

        private  operationResult sendData(byte slaveAdd, byte startReadAdd, byte channel, byte readLen, SerialPort sp, byte[] writeBuffer = null)
        {

            operationResult oR = new operationResult();
            commandFrame cFRead = new commandFrame();
            if (writeBuffer == null)
            {
                cFRead.cmd_type = read;
            }
            else {
                cFRead.cmd_type = write;
            }
           
            cFRead.cmd_slaves = slaveAdd;
            cFRead.addr = startReadAdd;
            cFRead.cmd_bus = channel;
            cFRead.length = readLen;
            cFRead.param = writeBuffer;

            if (!sp.IsOpen) {
                oR.result = false;
                oR.msg = EEPROM.msgDeviceNotOpen;
                return oR;
            }
            byte[] writeData=cFRead.convertToByteArray();
            sp.Write(writeData, 0, writeData.Length);

            int countTime = 0;
            while (sp.BytesToRead == 0) {
                Thread.Sleep(10);
                countTime++;
                if (countTime > 10) {
                    oR.result = false;
                    oR.msg = EEPROM.msgDeviceTimeOut;
                    return oR;
                }
            }
            if (cFRead.cmd_type == read)
            {
                Thread.Sleep(1000);
            }
            else {
                Thread.Sleep(timeOut);
            }
        
            byte[] recData = new byte[sp.BytesToRead];
            sp.Read(recData, 0, recData.Length);

            byte[] operationHead=new byte[8];
            Array.Copy(recData,operationHead,8);

            if (Util.arrayCompare(operationHead, cmd_error))
            {
                //命令格式错误
                oR.result = false;
                oR.msg = EEPROM.msgCMDError;
                return oR;
            }

            if (Util.arrayCompare(operationHead, cmd_read_fail))
            {
                //读取失败
                oR.result = false;
                oR.msg = EEPROM.msgReadFail;
                return oR;
            }

            if (Util.arrayCompare(operationHead, cmd_write_fail))
            {
                //写入失败
                oR.result = false;
                oR.msg = EEPROM.msgWriteFail;
                return oR;
            }

            if (Util.arrayCompare(operationHead, cmd_write_success))
            {
                //写入成功
                oR.result = true;
                oR.msg = EEPROM.msgWriteSuccess;
                return oR;
            }

            if (cFRead.cmd_type == read) {
                oR.result = true;
                oR.msg = EEPROM.msgReadSuccess;
                int dataLength = readLen == 0 ? 256 : readLen;
                byte[] readData = new byte[dataLength];
                Array.Copy(recData, 8, readData, 0, dataLength);
                oR.bufferData = readData;
               
            }

            return oR;
       
 
     }

       

    }
}
