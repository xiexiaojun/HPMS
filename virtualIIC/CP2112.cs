using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace VirtualIIC
{
    /// <summary>
    /// CP2112型号adapter控制类
    /// </summary>
    public class CP2112 : Ii2c
    {
        private IntPtr m_hidSmbus;
        private uint deviceNum;
        private uint timeOut;
        private uint writeBlock;
        private int delayTime;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceNum">设备序号，通常固定为1</param>
        /// <param name="timeOut"></param>
        /// <param name="writeBlock"></param>
        /// <param name="delayTime"></param>
        public CP2112(uint deviceNum, uint timeOut, uint writeBlock, int delayTime=25)
        {
            this.deviceNum = deviceNum;
            this.timeOut=timeOut;
            this.writeBlock = writeBlock;
            if (delayTime == 0)
            {
                this.delayTime = 25;
            }
            else
            {
                this.delayTime = delayTime;
            }

           
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool openDevice()
        {
            uint numDevices = 1;
            CP2112_DLL.HID_SMBUS_STATUS a = CP2112_DLL.GetNumDevices(ref numDevices);
           CP2112_DLL.HID_SMBUS_STATUS temp = CP2112_DLL.HidSmbus_Open
               (ref m_hidSmbus, deviceNum - 1, CP2112_DLL.HidSmbus_VID, CP2112_DLL.HidSmbus_PID);
           if (temp == CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS)
            {
                //return true;
                if (setAutoReadRespon())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
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
        public bool closeDevice()
        {
            if (isOpened())
            {
                cancel();
                if (CP2112_DLL.HidSmbus_Close(m_hidSmbus) == CP2112_DLL.HID_SMBUS_SUCCESS)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
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
            if(!setChannel(channel)){
            oR.result=false;
            oR.msg=EEPROM.msgDeviceChannelSetFail;
             return oR;
            }
            int readLength = 0;
            if (readLen == 0)
            {
                readLength = 256;
            }
            else
            {
                readLength = readLen;
            }

            byte[] readBuffer = new byte[readLength];

            if(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS==
               Read3(slaveAdd, (ushort)readLength, startReadAdd, ref readBuffer))
            {
                oR.result=true;
                oR.msg=EEPROM.msgReadSuccess;
                oR.bufferData=readBuffer;
            }else{
                 oR.result=false;
                 oR.msg=EEPROM.msgReadFail;
            }
            return oR;

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
            if (!setChannel(channel))
            {
                oR.result = false;
                oR.msg = EEPROM.msgDeviceChannelSetFail;
                return oR;
            }
          
            CP2112_DLL.HID_SMBUS_STATUS ret=Write3(slaveAdd, (ushort)writeLen, startWriteAdd, writeBuffer);

            if (CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS ==
              ret)
            {
                oR.result = true;
                oR.msg = EEPROM.msgWriteSuccess;

            }
            else if (CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_WRITE_COMAPARE_FAIL==ret)
            {
                oR.result = false;
                oR.msg = EEPROM.msgWriteCheckFail;
            }
            else
            {
                oR.result = false;
                oR.msg = EEPROM.msgWriteFail;
            }
            return oR;
        }

        private bool isOpened()
        {
            int OpenFlag = 0;
            if (CP2112_DLL.HidSmbus_IsOpened(m_hidSmbus, ref OpenFlag) == CP2112_DLL.HID_SMBUS_SUCCESS)
            {
                return OpenFlag == 1;
            }
            else
            {
                return false;
            }
        }

        private bool setChannel(byte channel)
        {
            byte slaveAddress=0;
            byte numBytestoWrite=1;
            byte[] buffer=new byte[1];
            if(!offAllChannel()){
                return false;
            }
            if(channel<8){
                slaveAddress=0XE2;
                buffer[0]=(byte)Math.Pow(2,channel);
            }else{
                slaveAddress=0XE0;
                  buffer[0]=(byte)Math.Pow(2,channel-8);
            }
           if(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS==
               CP2112_DLL.HidSmbus_WriteRequest(m_hidSmbus,slaveAddress,  buffer,numBytestoWrite)) {
               return true;
           }
            else{
               return false;
           }
        }

        private bool offAllChannel(){
           const byte slaveAddressU9=0XE2;
           const byte slaveAddressU8=0XE0;
            byte[] buffer={0};
            byte numBytestoWrite=1;
             if(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS!=
               CP2112_DLL.HidSmbus_WriteRequest(m_hidSmbus,slaveAddressU9, buffer,numBytestoWrite)){
                return false;
             }
             if(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS!=
               CP2112_DLL.HidSmbus_WriteRequest(m_hidSmbus,slaveAddressU8, buffer,numBytestoWrite)){
                return false;
             }
            return true;
        }

        private bool setTimeOut()
        {
           if(CP2112_DLL.HidSmbus_SetTimeouts(m_hidSmbus,timeOut)==CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) {
               return true;
           }else{
               return false;
           }
        }

        private CP2112_DLL.HID_SMBUS_STATUS Read2(byte slaveAddress, ushort numBytesToRead,
                                           byte OffsetAddr, ref byte[] readBytes)
        {
           
            CP2112_DLL.HID_SMBUS_STATUS ret = CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            byte[] BufTmp = new byte[64];
            byte[] targetAddress = new byte[16];
            byte ReadStatus = 0;
            byte numBytesRead = 0;
            Array.Clear(readBytes, 0, numBytesToRead);

            if (!isOpened()) return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            //if (!cancel()) return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_INVALID_DEVICE_OBJECT;

            targetAddress[0] = OffsetAddr;
            ret = CP2112_DLL.HidSmbus_AddressReadRequest(m_hidSmbus, slaveAddress, numBytesToRead, 1, targetAddress);
            if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            ushort readCont = 0;
            do
            {
               
                ret = CP2112_DLL.HidSmbus_ForceReadResponse(m_hidSmbus, numBytesToRead);
                if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;

              
                ret = CP2112_DLL.HidSmbus_GetReadResponse(m_hidSmbus, ref ReadStatus,
                                               BufTmp, 64, ref numBytesRead);

               // CP2112_DLL.HidSmbus_CancelTransfer(m_hidSmbus);
                if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) break;
                if (ReadStatus != (byte)CP2112_DLL.HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR)
                {
                    Array.Copy(BufTmp, 0, readBytes, readCont, numBytesRead);
                    readCont += numBytesRead;

                }

                else return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }
            while ((ReadStatus == (byte)CP2112_DLL.HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_BUSY)
                );
            

            return ret;
        }

        private CP2112_DLL.HID_SMBUS_STATUS Read3(byte slaveAddress, ushort numBytesToRead,
                                         byte OffsetAddr, ref byte[] readBytes)
        {
            byte[] targetAddress = new byte[1];
            targetAddress[0] = OffsetAddr;
            byte[] BufTmp = new byte[255];

            byte ReadStatus = 0;
            byte numBytesRead = 0;
            ushort readCont = 0;
            CP2112_DLL.HID_SMBUS_STATUS ret = CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            if (!isOpened()) return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            ret = CP2112_DLL.HidSmbus_SetTimeouts(m_hidSmbus, 100);
            if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;

            ret = CP2112_DLL.HidSmbus_AddressReadRequest(m_hidSmbus, slaveAddress, numBytesToRead, 1, targetAddress);
            Util.Delay(50);
            
            ret = CP2112_DLL.HidSmbus_ForceReadResponse(m_hidSmbus, numBytesToRead);
            if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            Util.Delay(20);

            do
            {
             ret = CP2112_DLL.HidSmbus_GetReadResponse(m_hidSmbus, ref ReadStatus,
                                               BufTmp, 64, ref numBytesRead);

                // CP2112_DLL.HidSmbus_CancelTransfer(m_hidSmbus);
                if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) break;
                if (ReadStatus != (byte)CP2112_DLL.HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR)
                {
                    Array.Copy(BufTmp, 0, readBytes, readCont, numBytesRead);
                    readCont += numBytesRead;

                }

                else return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }
            while (readCont < numBytesToRead);
          

            //ret = CP2112_DLL.HidSmbus_GetReadResponse(m_hidSmbus, ref ReadStatus,
            //                                  BufTmp, 255, ref numBytesRead);

            //Array.Copy(BufTmp, 0, readBytes, 0, numBytesRead);

             CP2112_DLL.HidSmbus_CancelTransfer(m_hidSmbus);
            return ret;

         
        }

        private CP2112_DLL.HID_SMBUS_STATUS Write2(byte slaveAddress, ushort numBytesToWrite,
                                            byte OffsetAddr, byte[] WriteBytes)
        {
            CP2112_DLL.HID_SMBUS_STATUS ret = CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS;

            if (!isOpened())
            {
                return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }

            byte[] wrbuf = new byte[numBytesToWrite+1];
            byte wradr = OffsetAddr;
            ushort destlength = numBytesToWrite;
            ushort destindex = 0;
            byte status = 0;
            byte detailedstatus = 0;
            ushort numretries = 0;
            ushort bytesread = 0;

            while (numBytesToWrite > 0)
            {
                destlength = numBytesToWrite;
                if (numBytesToWrite > writeBlock)
                    destlength = (ushort)writeBlock;
                Array.Copy(WriteBytes, destindex, wrbuf, 1, destlength);
                wrbuf[0] = wradr;
                do
                {
                    ret = CP2112_DLL.HidSmbus_TransferStatusRequest(m_hidSmbus);
                    if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    ret = CP2112_DLL.HidSmbus_GetTransferStatusResponse(m_hidSmbus, ref status, ref detailedstatus,
                        ref numretries, ref bytesread);
                    if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    if (status == (byte)CP2112_DLL.HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR) return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
                }
                while (status == 0x01);
                ret = CP2112_DLL.HidSmbus_WriteRequest(m_hidSmbus, slaveAddress,  wrbuf, (byte)(destlength + 1));
                if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;

                numBytesToWrite -= destlength;
                destindex += destlength;
                wradr += (byte)destlength;
            }
            return ret;
        }

        private CP2112_DLL.HID_SMBUS_STATUS Write3(byte slaveAddress, ushort numBytesToWrite,
                                          byte OffsetAddr, byte[] WriteBytes)
        {
            CP2112_DLL.HID_SMBUS_STATUS ret = CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS;

            if (!isOpened())
            {
                return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }

           
            byte wradr = OffsetAddr;
            ushort destlength = numBytesToWrite;
            ushort numBytesToRead=numBytesToWrite;
            byte[] readBytes=new byte[numBytesToRead];
            ushort destindex = 0;
            byte status = 0;
            byte detailedstatus = 0;
            ushort numretries = 0;
            ushort bytesread = 0;

            while (numBytesToWrite >0)
            {
                status = 0;
                destlength = numBytesToWrite;
                if (numBytesToWrite > writeBlock)
                    destlength = (ushort)writeBlock;
                byte[] wrbuf = new byte[destlength + 1];
                Array.Copy(WriteBytes, destindex, wrbuf, 1, destlength);
                wrbuf[0] = wradr;

                ret = CP2112_DLL.HidSmbus_WriteRequest(m_hidSmbus, slaveAddress, wrbuf, (byte)(destlength + 1));
                Util.Delay(delayTime);
                //Console.WriteLine("destlength:" + destlength);
                if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                do
                {
                    ret = CP2112_DLL.HidSmbus_TransferStatusRequest(m_hidSmbus);
                    if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    ret = CP2112_DLL.HidSmbus_GetTransferStatusResponse(m_hidSmbus, ref status, ref detailedstatus,
                        ref numretries, ref bytesread);
                    if (ret != CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    if (status == (byte)CP2112_DLL.HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR) return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_ERROR;
                    //Console.WriteLine("detailedstatus :" + detailedstatus + "status :" + status + "numretries:" + numretries);

                    if (numretries >999)
                    {
                        
                        break;
                    }
                }
                while (status != 0x02);

               
                

              

                numBytesToWrite -= destlength;
                destindex += destlength;
                wradr += (byte)destlength;
            }

            ret = CP2112_DLL.HidSmbus_CancelTransfer(m_hidSmbus);

            //Util.Delay(50);

            //ret=Read3(slaveAddress, numBytesToRead, OffsetAddr, ref readBytes);

            //if (!Util.arrayCompare(readBytes, WriteBytes))
            //{
            //    ret = CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_WRITE_COMAPARE_FAIL;
            //}
             
          
            return ret;
        }

        private bool setAutoReadRespon()
        {
            uint bitRate=0;
             byte address=0;
            int autoReadRespond=0;
            ushort writeTimeout=0;
            ushort readTimeout=0;
            int sclLowtimeout=0;
            ushort transferRetries=0;

            if (!(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS ==
                CP2112_DLL.HidSmbus_GetSmbusConfig(m_hidSmbus, ref bitRate, ref address,
                 ref autoReadRespond, ref  writeTimeout, ref  readTimeout, ref  sclLowtimeout, ref  transferRetries)))
            {
                return false;
            }
              if (!(CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS ==
              CP2112_DLL.HidSmbus_SetSmbusConfig(m_hidSmbus,bitRate,address,1,writeTimeout,readTimeout,sclLowtimeout,transferRetries)))
            {
                return false;
            }
              return true;
        }

        private bool cancel()
        {
            return (CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS == CP2112_DLL.HidSmbus_TransferStatusRequest(m_hidSmbus));
        }

        private static bool GetHidGuid(ref Guid guid)
        {
            return CP2112_DLL.HID_SMBUS_STATUS.HID_SMBUS_SUCCESS==CP2112_DLL.HidSmbus_GetHidGuid(ref guid);
        }

      
    }
}
