/////////////////////////////////////////////////////////////////////////////
// SLABCP2112.cs
// For SLABHIDtoSMBus.dll version 1.3
// and Silicon Labs CP2112 HID to SMBus
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// Namespaces
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

/////////////////////////////////////////////////////////////////////////////
// SLABHIDtoSMBus.dll Namespace
/////////////////////////////////////////////////////////////////////////////

namespace VirtualIIC
{
    /////////////////////////////////////////////////////////////////////////////
    // SLABHIDtoSMBus.dll Imports
    /////////////////////////////////////////////////////////////////////////////

     class CP2112_DLL
    {
        /////////////////////////////////////////////////////////////////////////////
        // Return Code Definitions
        /////////////////////////////////////////////////////////////////////////////
        public enum HID_SMBUS_STATUS
        {
            HID_SMBUS_SUCCESS = 0x00,
            HID_SMBUS_DEVICE_NOT_FOUND = 0x01,
            HID_SMBUS_INVALID_HANDLE = 0x02,
            HID_SMBUS_INVALID_DEVICE_OBJECT = 0x03,
            HID_SMBUS_INVALID_PARAMETER = 0x04,
            HID_SMBUS_INVALID_REQUEST_LENGTH = 0x05,
            HID_SMBUS_WRITE_COMAPARE_FAIL = 0x06,
            HID_SMBUS_ERROR = -1
        };
        #region Return Code Definitions

        // HID_SMBUS_STATUS Return Codes
        public const byte HID_SMBUS_SUCCESS = 0x00;
        public const byte HID_SMBUS_DEVICE_NOT_FOUND = 0x01;
        public const byte HID_SMBUS_INVALID_HANDLE = 0x02;
        public const byte HID_SMBUS_INVALID_DEVICE_OBJECT = 0x03;
        public const byte HID_SMBUS_INVALID_PARAMETER = 0x04;
        public const byte HID_SMBUS_INVALID_REQUEST_LENGTH = 0x05;

        public const byte HID_SMBUS_READ_ERROR = 0x10;
        public const byte HID_SMBUS_WRITE_ERROR = 0x11;
        public const byte HID_SMBUS_READ_TIMED_OUT = 0x12;
        public const byte HID_SMBUS_WRITE_TIMED_OUT = 0x13;
        public const byte HID_SMBUS_DEVICE_IO_FAILED = 0x14;
        public const byte HID_SMBUS_DEVICE_ACCESS_ERROR = 0x15;
        public const byte HID_SMBUS_DEVICE_NOT_SUPPORTED = 0x16;

        public const byte HID_SMBUS_UNKNOWN_ERROR = 0xFF;

        //public const byte HID_SMBUS_S0_IDLE = 0x00;
        //public const byte HID_SMBUS_S0_BUSY = 0x01;
        //public const byte HID_SMBUS_S0_COMPLETE = 0x02;
        //public const byte HID_SMBUS_S0_ERROR = 0x03;

        public enum HID_SMBUS_TRANSFER_S0 : byte
        {
            HID_SMBUS_S0_IDLE = 0x00,
            HID_SMBUS_S0_BUSY = 0x01,
            HID_SMBUS_S0_COMPLETE = 0x02,
            HID_SMBUS_S0_ERROR = 0x03
        }

        // HID_SMBUS_TRANSFER_S0 = HID_SMBUS_S0_BUSY
        public const byte HID_SMBUS_S1_BUSY_ADDRESS_ACKED = 0x00;
        public const byte HID_SMBUS_S1_BUSY_ADDRESS_NACKED = 0x01;
        public const byte HID_SMBUS_S1_BUSY_READING = 0x02;
        public const byte HID_SMBUS_S1_BUSY_WRITING = 0x03;

        // HID_SMBUS_TRANSFER_S0 = HID_SMBUS_S0_ERROR
        public const byte HID_SMBUS_S1_ERROR_TIMEOUT_NACK = 0x00;
        public const byte HID_SMBUS_S1_ERROR_TIMEOUT_BUS_NOT_FREE = 0x01;
        public const byte HID_SMBUS_S1_ERROR_ARB_LOST = 0x02;
        public const byte HID_SMBUS_S1_ERROR_READ_INCOMPLETE = 0x03;
        public const byte HID_SMBUS_S1_ERROR_WRITE_INCOMPLETE = 0x04;
        public const byte HID_SMBUS_S1_ERROR_SUCCESS_AFTER_RETRY = 0x05;

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // String Definitions
        /////////////////////////////////////////////////////////////////////////////

        #region String Definitions

        // Product String Types
        public const uint HID_SMBUS_GET_VID_STR = 0x01;
        public const uint HID_SMBUS_GET_PID_STR = 0x02;
        public const uint HID_SMBUS_GET_PATH_STR = 0x03;
        public const uint HID_SMBUS_GET_SERIAL_STR = 0x04;
        public const uint HID_SMBUS_GET_MANUFACTURER_STR = 0x05;
        public const uint HID_SMBUS_GET_PRODUCT_STR = 0x06;

        // String Lengths
        public const uint HID_SMBUS_DEVICE_STRLEN = 260;

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // SMBUS Definitions
        /////////////////////////////////////////////////////////////////////////////

        #region SMBUS Definitions

        // SMbus Configuration Limits
        public const uint HID_SMBUS_MIN_BIT_RATE = 1;
        public const ushort HID_SMBUS_MIN_TIMEOUT = 0;
        public const ushort HID_SMBUS_MAX_TIMEOUT = 1000;
        public const ushort HID_SMBUS_MAX_RETRIES = 1000;
        public const byte HID_SMBUS_MIN_ADDRESS = 0x02;
        public const byte HID_SMBUS_MAX_ADDRESS = 0xFE;

        // Read/Write Limits
        public const ushort HID_SMBUS_MIN_READ_REQUEST_SIZE = 1;
        public const ushort HID_SMBUS_MAX_READ_REQUEST_SIZE = 512;
        public const byte HID_SMBUS_MIN_TARGET_ADDRESS_SIZE = 1;
        public const byte HID_SMBUS_MAX_TARGET_ADDRESS_SIZE = 16;
        public const byte HID_SMBUS_MAX_READ_RESPONSE_SIZE = 61;
        public const byte HID_SMBUS_MIN_WRITE_REQUEST_SIZE = 1;
        public const byte HID_SMBUS_MAX_WRITE_REQUEST_SIZE = 61;

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // GPIO Definitions
        /////////////////////////////////////////////////////////////////////////////

        #region GPIO Definitions

        // GPIO Pin Direction Bit Value
        public const byte HID_SMBUS_DIRECTION_INPUT = 0;
        public const byte HID_SMBUS_DIRECTION_OUTPUT = 1;

        // GPIO Pin Mode Bit Value
        public const byte HID_SMBUS_MODE_OPEN_DRAIN = 0;
        public const byte HID_SMBUS_MODE_PUSH_PULL = 1;

        // GPIO Function Bitmask
        public const byte HID_SMBUS_MASK_FUNCTION_GPIO_7_CLK = 0x01;
        public const byte HID_SMBUS_MASK_FUNCTION_GPIO_0_TXT = 0x02;
        public const byte HID_SMBUS_MASK_FUNCTION_GPIO_1_RXT = 0x04;

        // GPIO Function Bit Value
        public const byte HID_SMBUS_GPIO_FUNCTION = 0;
        public const byte HID_SMBUS_SPECIAL_FUNCTION = 1;

        // GPIO Pin Bitmask
        public const byte HID_SMBUS_MASK_GPIO_0 = 0x01;
        public const byte HID_SMBUS_MASK_GPIO_1 = 0x02;
        public const byte HID_SMBUS_MASK_GPIO_2 = 0x04;
        public const byte HID_SMBUS_MASK_GPIO_3 = 0x08;
        public const byte HID_SMBUS_MASK_GPIO_4 = 0x10;
        public const byte HID_SMBUS_MASK_GPIO_5 = 0x20;
        public const byte HID_SMBUS_MASK_GPIO_6 = 0x40;
        public const byte HID_SMBUS_MASK_GPIO_7 = 0x80;

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // Part Number Definitions
        /////////////////////////////////////////////////////////////////////////////

        #region Part Number Definitions

        // Part Numbers
        public const byte HID_SMBUS_PART_CP2112 = 0x0C;

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // User Customization Definitions
        /////////////////////////////////////////////////////////////////////////////

        #region User Customization Definitions

        // User-Customizable Field Lock Bitmasks
        public const byte HID_SMBUS_LOCK_VID = 0x01;
        public const byte HID_SMBUS_LOCK_PID = 0x02;
        public const byte HID_SMBUS_LOCK_POWER = 0x04;
        public const byte HID_SMBUS_LOCK_POWER_MODE = 0x08;
        public const byte HID_SMBUS_LOCK_RELEASE_VERSION = 0x10;
        public const byte HID_SMBUS_LOCK_MFG_STR = 0x20;
        public const byte HID_SMBUS_LOCK_PRODUCT_STR = 0x40;
        public const byte HID_SMBUS_LOCK_SERIAL_STR = 0x80;

        // Field Lock Bit Values
        public const byte HID_SMBUS_LOCK_UNLOCKED = 1;
        public const byte HID_SMBUS_LOCK_LOCKED = 0;

        // Power Max Value (500 mA)
        public const byte HID_SMBUS_BUS_POWER_MAX = 0xFA;

        // Power Modes
        public const byte HID_SMBUS_BUS_POWER = 0x00;
        public const byte HID_SMBUS_SELF_POWER_VREG_DIS = 0x01;
        public const byte HID_SMBUS_SELF_POWER_VREG_EN = 0x02;

        // USB Config Bitmasks
        public const byte HID_SMBUS_SET_VID = 0x01;
        public const byte HID_SMBUS_SET_PID = 0x02;
        public const byte HID_SMBUS_SET_POWER = 0x04;
        public const byte HID_SMBUS_SET_POWER_MODE = 0x08;
        public const byte HID_SMBUS_SET_RELEASE_VERSION = 0x10;

        // USB Config Bit Values
        public const byte HID_SMBUS_SET_IGNORE = 0;
        public const byte HID_SMBUS_SET_PROGRAM = 1;

        // String Lengths
        public const byte HID_SMBUS_CP2112_MFG_STRLEN = 30;
        public const byte HID_SMBUS_CP2112_PRODUCT_STRLEN = 30;
        public const byte HID_SMBUS_CP2112_SERIAL_STRLEN = 30;

        #endregion

        public const ushort HidSmbus_VID = 0x10C4;
        public const ushort HidSmbus_PID = 0xEA90;
        public static IntPtr m_hidSmbus;
        //public static byte SlaveAddre = 0x30;
        public static uint BandRate = 0x061A80;
        public static HID_SMBUS_STATUS GetNumDevices(ref uint numDevices)
        {
            return HidSmbus_GetNumDevices(ref numDevices, HidSmbus_VID, HidSmbus_PID);
        }
        public static HID_SMBUS_STATUS Open()
        {
            HID_SMBUS_STATUS ret = 0;
            uint numDevices = 0;

            if (IsOpen() == HID_SMBUS_SUCCESS) return HID_SMBUS_SUCCESS;

            ret = HidSmbus_GetNumDevices(ref numDevices, HidSmbus_VID, HidSmbus_PID);
            if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            if (numDevices == 0) return HID_SMBUS_STATUS.HID_SMBUS_DEVICE_NOT_FOUND;

            ret = HidSmbus_Open(ref m_hidSmbus, 0, HidSmbus_VID, HidSmbus_PID);
            if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            return HidSmbus_SetSmbusConfig(m_hidSmbus, BandRate, 0x30, 1, 1000, 1000, 0, 0);
        }


        public static HID_SMBUS_STATUS Open(uint nDevices, ref IntPtr m_hidSmbus)
        {
            HID_SMBUS_STATUS ret = 0;
            uint numDevices = 0;
            m_hidSmbus = IntPtr.Zero;
            //if (IsOpen() == HID_SMBUS_SUCCESS) return HID_SMBUS_SUCCESS;

            ret = HidSmbus_GetNumDevices(ref numDevices, HidSmbus_VID, HidSmbus_PID);
            if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            if (numDevices <= nDevices) return HID_SMBUS_STATUS.HID_SMBUS_DEVICE_NOT_FOUND;

            ret = HidSmbus_Open(ref m_hidSmbus, 0, HidSmbus_VID, HidSmbus_PID);
            if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            return HidSmbus_SetSmbusConfig(m_hidSmbus, BandRate, 0x30, 1, 1000, 1000, 0, 0);
        }

        public static HID_SMBUS_STATUS ConfigSet()
        {
            return HidSmbus_SetSmbusConfig(m_hidSmbus, BandRate, 0x30, 1, 1000, 1000, 0, 0);
        }
        public static HID_SMBUS_STATUS IsOpen()
        {
            int OpenFlag = 0;
            if (m_hidSmbus != IntPtr.Zero)
            {
                if (HidSmbus_IsOpened(m_hidSmbus, ref OpenFlag) == HID_SMBUS_SUCCESS)
                {
                    if (OpenFlag == 1) return HID_SMBUS_SUCCESS;
                }
            }
            return HID_SMBUS_STATUS.HID_SMBUS_INVALID_HANDLE;
        }


        public static HID_SMBUS_STATUS Read(byte slaveAddress, ushort numBytesToRead,
                                            byte OffsetAddr, ref byte[] readBytes)
        {
            HID_SMBUS_STATUS ret = HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            byte[] BufTmp = new byte[64];
            byte[] targetAddress = new byte[16];
            byte ReadStatus = 0;
            byte numBytesRead = 0;
            Array.Clear(readBytes, 0, numBytesToRead);

           // if (IsOpen() != HID_SMBUS_SUCCESS) return HID_SMBUS_STATUS.HID_SMBUS_ERROR;

            targetAddress[0] = OffsetAddr;
            ret = HidSmbus_AddressReadRequest(m_hidSmbus, slaveAddress, numBytesToRead, 1, targetAddress);
            if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
            ushort readCont = 0;
            do
            {
                ret = HidSmbus_GetReadResponse(m_hidSmbus, ref ReadStatus,
                                               BufTmp, 64, ref numBytesRead);
                if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) break;
                if (ReadStatus != (byte)HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR)
                {
                    Array.Copy(BufTmp, 0, readBytes, readCont, numBytesRead);
                    readCont += numBytesRead;
                }
                else return HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }
            while (ReadStatus == (byte)HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_BUSY);

            return ret;
        }

        public static HID_SMBUS_STATUS Write(byte slaveAddress, ushort numBytesToWrite,
                                             byte OffsetAddr, byte[] WriteBytes)
        {
            HID_SMBUS_STATUS ret = HID_SMBUS_STATUS.HID_SMBUS_SUCCESS;

            if (IsOpen() != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS)
            {
                return HID_SMBUS_STATUS.HID_SMBUS_ERROR;
            }

            byte[] wrbuf = new byte[80];
            byte wradr = OffsetAddr;
            ushort destlength = numBytesToWrite;
            ushort destindex = 0;
            byte status = 0;
            byte detailedstatus = 0;
            ushort numretries = 0;
            ushort bytesread = 0;

            while(numBytesToWrite > 0)
            {
                destlength = numBytesToWrite;
                if (numBytesToWrite > 60)
                    destlength = 60;
                Array.Copy(WriteBytes, destindex, wrbuf, 1, destlength);
                wrbuf[0] = wradr;
                do
                {
                    ret = HidSmbus_TransferStatusRequest(m_hidSmbus);
                    if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    ret = HidSmbus_GetTransferStatusResponse(m_hidSmbus, ref status, ref detailedstatus,
                        ref numretries, ref bytesread);
                    if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;
                    if (status == (byte)HID_SMBUS_TRANSFER_S0.HID_SMBUS_S0_ERROR) return HID_SMBUS_STATUS.HID_SMBUS_ERROR;
                }
                while (status == 0x01);
                ret = HidSmbus_WriteRequest(m_hidSmbus, slaveAddress,  wrbuf, (byte)(destlength + 1));
                if (ret != HID_SMBUS_STATUS.HID_SMBUS_SUCCESS) return ret;

                numBytesToWrite -= destlength;
                destindex += destlength;
                wradr += (byte)destlength;
            }
            return ret;
        }
        /////////////////////////////////////////////////////////////////////////////
        // Exported Library Functions
        /////////////////////////////////////////////////////////////////////////////

        #region Exported Library Functions

        // HidSmbus_GetNumDevices
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_GetNumDevices(ref uint numDevices, ushort vid, ushort pid);

        // HidSmbus_GetString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetString(uint deviceNum, ushort vid, ushort pid, StringBuilder deviceString, uint options);

        // HidSmbus_GetOpenedString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetOpenedString(IntPtr device, StringBuilder deviceString, uint options);

        // HidSmbus_GetIndexedString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetIndexedString(uint deviceNum, ushort vid, ushort pid, uint stringIndex, StringBuilder deviceString);

        // HidSmbus_GetOpenedIndexedString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetOpenedIndexedString(IntPtr device, uint stringIndex, StringBuilder deviceString);

        // HidSmbus_GetAttributes
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetAttributes(uint deviceNum, ushort vid, ushort pid, ref ushort deviceVid, ref ushort devicePid, ref ushort deviceReleaseNumber);

        // HidSmbus_GetOpenedAttributes
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetOpenedAttributes(IntPtr device, ref ushort deviceVid, ref ushort devicePid, ref ushort deviceReleaseNumber);

        // HidSmbus_Open
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_Open(ref IntPtr device, uint deviceNum, ushort vid, ushort pid);

        // HidSmbus_Close
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_Close(IntPtr device);

        // HidSmbus_IsOpened
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_IsOpened(IntPtr device, ref int opened);

        // HidSmbus_ReadRequest
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_ReadRequest(IntPtr device, byte slaveAddress, ushort numBytesToRead);

        // HidSmbus_AddressReadRequest
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_AddressReadRequest(IntPtr device, byte slaveAddress, ushort numBytesToRead, byte targetAddressSize, byte[] targetAddress);

        // HidSmbus_ForceReadResponse
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_ForceReadResponse(IntPtr device, ushort numBytesToRead);

        // HidSmbus_ForceReadResponse
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_GetReadResponse(IntPtr device, ref byte status, byte[] buffer, byte bufferSize, ref byte numBytesRead);

        // HidSmbus_WriteRequest
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_WriteRequest(IntPtr device, byte slaveAddress,  byte[] buffer, byte numBytesToWrite);

        // HidSmbus_TransferStatusRequest
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_TransferStatusRequest(IntPtr device);

        // HidSmbus_GetTransferStatusResponse
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_GetTransferStatusResponse(IntPtr device, ref byte status, ref byte detailedStatus, ref ushort numRetries, ref ushort bytesRead);

        // HidSmbus_CancelTransfer
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_CancelTransfer(IntPtr device);

        // HidSmbus_CancelIo
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_CancelIo(IntPtr device);

        // HidSmbus_SetTimeouts
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_SetTimeouts(IntPtr device, uint responseTimeout);

        // HidSmbus_GetTimeouts
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetTimeouts(IntPtr device, ref uint responseTimeout);

        // HidSmbus_SetSmbusConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_SetSmbusConfig(IntPtr device, uint bitRate, byte address, int autoReadRespond, ushort writeTimeout, ushort readTimeout, int sclLowTimeout, ushort transferRetries);

        // HidSmbus_GetSmbusConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_GetSmbusConfig(IntPtr device, ref uint bitRate, ref byte address, ref int autoReadRespond, ref ushort writeTimeout, ref ushort readTimeout, ref int sclLowtimeout, ref ushort transferRetries);

        // HidSmbus_Reset
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_Reset(IntPtr device);

        // HidSmbus_SetGpioConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_SetGpioConfig(IntPtr device, byte direction, byte mode, byte function, byte clkDiv);

        // HidSmbus_GetGpioConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetGpioConfig(IntPtr device, ref byte direction, ref byte mode, ref byte function, ref byte clkDiv);

        // HidSmbus_ReadLatch
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_ReadLatch(IntPtr device, ref byte latchValue);

        // HidSmbus_WriteLatch
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_WriteLatch(IntPtr device, byte latchValue, byte latchMask);

        // HidSmbus_GetPartNumber
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetPartNumber(IntPtr device, ref byte partNumber, ref byte version);

        // HidSmbus_GetLibraryVersion
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetLibraryVersion(ref byte major, ref byte minor, ref int release);

        // HidSmbus_GetHidLibraryVersion
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetHidLibraryVersion(ref byte major, ref byte minor, ref int release);

        // HidSmbus_GetHidGuid
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern HID_SMBUS_STATUS HidSmbus_GetHidGuid(ref Guid guid);

        #endregion

        /////////////////////////////////////////////////////////////////////////////
        // Exported Library Functions - Device Customization
        /////////////////////////////////////////////////////////////////////////////

        #region Exported Library Functions - Device Customization

        // HidSmbus_SetLock
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_SetLock(IntPtr device, byte lockValue);

        // HidSmbus_GetLock
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetLock(IntPtr device, ref byte lockValue);

        // HidSmbus_SetUsbConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_SetUsbConfig(IntPtr device, ushort vid, ushort pid, byte power, byte powerMode, ushort releaseVersion, byte mask);

        // HidSmbus_GetUsbConfig
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetUsbConfig(IntPtr device, ref ushort vid, ref ushort pid, ref byte power, ref byte powerMode, ref ushort releaseVersion);

        // HidSmbus_SetManufacturingString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_SetManufacturingString(IntPtr device, byte[] manufacturingString, byte strlen);

        // HidSmbus_GetManufacturingString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetManufacturingString(IntPtr device, StringBuilder manufacturingString, ref byte strlen);

        // HidSmbus_SetProductString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_SetProductString(IntPtr device, byte[] productString, byte strlen);

        // HidSmbus_GetProductString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetProductString(IntPtr device, StringBuilder productString, ref byte strlen);

        // HidSmbus_SetSerialString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_SetSerialString(IntPtr device, byte[] serialString, byte strlen);

        // HidSmbus_GetSerialString
        [DllImport("dll\\SLABHIDtoSMBus.dll")]
        public static extern int HidSmbus_GetSerialString(IntPtr device, StringBuilder serialString, ref byte strlen);

        #endregion
    }
}
