using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VirtualIIC;

namespace EEPROMUtility
{

    public enum CmdMode
    {
        Write,
        Read
    }
    public struct DeviceData
    {
        public byte Chip;//芯片地址
        public byte Offset;//起始地址
        public byte[] Buffer;//待写入数据    
        public int Length;//读写长度
        public int WriteStart;//写入起始
        public CmdMode CmdMode;
    }

    public struct KeyInformation
    {
        public string Name;
        public string Value;
        public byte[] Data;
        public bool Convert;
        public int Start;
        public int End;
    }

    /// <summary>
    /// 比较结果
    /// </summary>
    public struct CheckResult
    {
        /// <summary>
        /// 整体判定结果
        /// </summary>
        public bool Total;
        /// <summary>
        /// 各端口判定结果
        /// </summary>
        public List<bool> Port;
        /// <summary>
        /// 各端口fail的地址
        /// </summary>
        public List<bool[]> Detail;
        /// <summary>
        /// 忽略的地址位
        /// </summary>
        public List<byte[]> IgnoreAddr;
    }

    public class Burn
    {
        private string _pn;
        private string _portListFolder;//存放xml配置文件的文件夹
        private List<ParameterList> _portList;
        private int _portCount;
        private Ii2c _i2cDevice;

        private List<DeviceData>[] _writeActionList;//解析后的写操作列表
        private List<DeviceData>[] _readActionList; //解析后的读操作列表
        private Dictionary<string,KeyInformation>[] _keyInformationsList;//解析后的关键字列表
        private byte[][] _ignoreBytes;//忽略的byte位

        /// <summary>
        /// xml文件下载成功时发生
        /// </summary>
        public event Action<List<ParameterList>,string> OnXmlGet;

        /// <summary>
        /// 读取每个port时发生
        /// </summary>
        public event Action<string,bool,int> OnPortWrite;
        /// <summary>
        /// 写入每个port成功时发生
        /// </summary>
        public event Action<string,bool,int> OnPortRead;
        /// <summary>
        /// I2C设备打开时发生
        /// </summary>
        public event Action<string> OnI2CDeviceOpen;
        /// <summary>
        /// I2C设备关闭时发生
        /// </summary>
        public event Action<string> OnI2CDeviceClose;
        /// <summary>
        /// 读写进度变化时发生
        /// </summary>
        public event Action<int> OnProgressChange;

       public Burn(string pn,Ii2c i2cDevice, string configFolder="pnConfig")
        {
            this._pn = pn;
            this._portListFolder = configFolder + "\\" + pn;
            
            this._i2cDevice = i2cDevice;
          
        }

        /// <summary>
        /// 改变烧录器型号时使用
        /// </summary>
        /// <param name="i2cDevice"></param>
        public void ChangeAdapter(Ii2c i2cDevice)
        {
            this._i2cDevice = i2cDevice;
        }

        /// <summary>
        /// 下载并解析配置文件
        /// </summary>
        public void DownloadConfig()
        {
            this._portList = GetPortLists(_pn);
            Action<List<ParameterList>, string> temp = OnXmlGet;
            temp?.Invoke(_portList, _portListFolder);
            ParseData();
        }

        private List<ParameterList> GetPortLists(string pn)
        {
            DownloadConfigFile(pn);
            _portCount = System.IO.Directory.GetFiles(_portListFolder).Length;
            List<ParameterList>ret=new List<ParameterList>(_portCount);
            for (int i = 0; i < _portCount; i++)
            {
                string xmlFilePath = _portListFolder + "\\" + i + ".xml";
                if (File.Exists(xmlFilePath))
                {
                    ret.Add(GetPortFromFile(xmlFilePath));
                }
                else
                {
                    throw new Exception("xmlFilePath "+"not exist");
                }
            }

            return ret;
        }

        /// <summary>
        /// 下载xml文件到_portListFolder文件夹，每个头一个，以0.xml开始，老王提供
        /// </summary>
        /// <param name="pn"></param>
        private void DownloadConfigFile(string pn)
        {
            //Todo 待老王提供
            if (!System.IO.Directory.Exists(_portListFolder))
            {
                System.IO.Directory.CreateDirectory(_portListFolder);
            }
        }

        private ParameterList GetPortFromFile(string xmlFilePath)
        {
            return (ParameterList)Serializer.FromXmlString(xmlFilePath, typeof(ParameterList));
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool[] WriteData(byte[][]data)
        {
            int dataColumn = data.Length;
            bool[]ret=new bool[dataColumn];
            //if (dataColumn != _portCount)
            //{
            //    throw new Exception(String.Format("data columns is:{0},port number is :{1},not equal",dataColumn,_portCount));
            //}
            var tempProgressChange = OnProgressChange;
            OpenDevice();
            try
            {
                for (int i = 0; i < _portCount; i++)
                {
                    ret[i]=WriteSinglePort(_writeActionList[i], i, data[i]);
                    tempProgressChange?.Invoke((i + 1) * 100 / _portCount);
                }
            }
            finally
            {
                tempProgressChange?.Invoke(100);
                CloseDevice();
            }

            return ret;
        }

        public bool[] ReadData(ref byte[][]readData)
        {
            List<byte[]>readByte=new List<byte[]>();
            List<bool>ret=new List<bool>();
            var tempProgressChange = OnProgressChange;
            OpenDevice();
            try
            {
                for (int i = 0; i < _portCount; i++)
                {
                   byte[] temp=null;
                   ret.Add(ReadSinglePort(_readActionList[i], i,ref temp));
                   readByte.Add(temp);
                   tempProgressChange?.Invoke((i+1)*100/_portCount);
                   readData = readByte.ToArray();
                }

                
            }
            finally
            {
                CloseDevice();
                readData = readByte.ToArray();
            }

            return ret.ToArray();
        }

        private void OpenDevice()
        {
            var tempI2CDeviceOpen = OnI2CDeviceOpen;
            //var tempProgressChange = OnProgressChange;
            if (!_i2cDevice.openDevice())
            {
                tempI2CDeviceOpen?.Invoke("打开设备失败");
                throw new Exception("打开设备失败");
            }

            tempI2CDeviceOpen?.Invoke("打开设备成功");
        }

        private void CloseDevice()
        {
            var tempI2CDeviceClose = OnI2CDeviceClose;
            try
            {
                _i2cDevice.closeDevice();
                tempI2CDeviceClose?.Invoke("关闭设备成功");
            }
            catch (Exception e)
            {
                tempI2CDeviceClose?.Invoke("关闭设备失败:" + e.Message);
            }
        }

        public bool WriteSinglePort(List<DeviceData> deviceDataList,int portNo,byte[]template)
        {
            var tempOnPortWrite = OnPortWrite;
            try
            {
                foreach (var deviceData in deviceDataList)
                {
                    switch (deviceData.CmdMode)
                    {
                        case CmdMode.Read:
                            Read(deviceData, portNo);
                            break;
                        case CmdMode.Write:
                            var temp = deviceData;
                            if (deviceData.Buffer == null)
                            {
                                temp.Buffer = SliceArray(template, deviceData.WriteStart, deviceData.Length);
                            }
                            Write(temp, portNo);
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                tempOnPortWrite?.Invoke("写入port数据失败:"+e.Message, false, portNo);
                return true;
            }
           
            tempOnPortWrite?.Invoke("写入port数据成功", true, portNo);
            return true;
        }

        public bool ReadSinglePort(List<DeviceData> deviceDataList, int portNo,ref byte[]readData)
        {
            List<byte>ret=new List<byte>();
            var tempOnPortRead = OnPortRead;
            try
            {
                foreach (var deviceData in deviceDataList)
                {
                    switch (deviceData.CmdMode)
                    {
                        case CmdMode.Read:
                            ret.AddRange(Read(deviceData, portNo));
                            break;
                        case CmdMode.Write:
                            Write(deviceData, portNo);
                            break;
                        default:
                            break;
                    }

                    readData = ret.ToArray();

                }
                tempOnPortRead?.Invoke("读取port数据成功",true,portNo);
                return true;
            }
            catch (Exception e)
            {
                tempOnPortRead?.Invoke("读取port数据失败:"+e.Message, false, portNo);
                return false;
            }
           

           
        }

        /// <summary>
        /// 获取所有头的关键信息
        /// </summary>
        /// <param name="data">所有头的读取数据</param>
        /// <returns></returns>
        public string[][] GetKeyInformation(byte[][]data)
        {
            int dataCount = data.Length;
            string[][]ret=new string[dataCount][];
            for (int i = 0; i < dataCount; i++)
            {
                ret[i] = GetKeyValue(this._keyInformationsList[i], data[i]);
            }

            return ret;
        }

        /// <summary>
        /// 解析xml里的关键字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private Dictionary<string, KeyInformation> GetKeyField(List<Field>fields)
        {
            Dictionary<string,KeyInformation>ret=new Dictionary<string, KeyInformation>();
            foreach (var keyField in fields)
            {
                KeyInformation temp=new KeyInformation();
                temp.Name = keyField.Name;
                temp.Convert = Convert.ToBoolean(keyField.Convert);
                temp.Start = ParseStr2Int(keyField.Start);
                temp.End = ParseStr2Int(keyField.End);
                ret.Add(keyField.Name, temp);
            }

            return ret;
        }

        /// <summary>
        /// 读取data里的关键字段
        /// </summary>
        /// <param name="dKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string[] GetKeyValue(Dictionary<string, KeyInformation>dKey,byte[]data)
        {
            if (data == null||data.Length==0)
            {
                return new []{"NA","NA","NA","NA","NA","NA"};
            }
            List<string> ret = new List<string>();
            foreach (var keyField in dKey)
            {
                KeyInformation temp = keyField.Value;
                temp.Data = SliceArray(data, temp.Start, temp.End - temp.Start + 1);
                if (temp.Convert)
                {
                    temp.Value = System.Text.Encoding.Default.GetString(temp.Data);
                }
                else
                {
                    temp.Value = string.Join("",temp.Data.Select(t => t.ToString().PadLeft(2, '0')).ToArray());
                }
                
                ret.Add(temp.Value);
            }

            return ret.ToArray();
        }

        private byte[] GetIgnore(Ignore ignore)
        {
            if (!ignore.Enable.Equals("true"))
            {
                return new byte[]{};
            }
            else
            {
                return ignore.Bits.Split(' ').Select(t => (byte) ParseStr2Int(t)).ToArray();
            }
        }

        private void Write(DeviceData deviceData,int portNo)
        {
            var result= _i2cDevice.writeDevice(deviceData.Chip, deviceData.Offset,
               (byte)portNo,  deviceData.Buffer.Length, deviceData.Buffer);
           if(!result.result)
                throw new Exception("write device error,error msg:"+result.msg);
        }

        private byte[] Read(DeviceData deviceData, int portNo)
        {
            var result = _i2cDevice.readDevice(deviceData.Chip, deviceData.Offset,
                (byte)portNo, (byte)deviceData.Length);
            if (!result.result)
                throw new Exception("read device error,error msg:" + result.msg);
            return result.bufferData;
        }

        /// <summary>
        /// 比较数据
        /// </summary>
        /// <param name="templateBytes"></param>
        /// <param name="readBytes"></param>
        /// <returns></returns>
        public CheckResult CheckData(byte[][] templateBytes, byte[][] readBytes)
        {
            CheckResult ret = new CheckResult();
           
            //List<byte[]> ignore = _pnParam.IgnoreAddr;
            List<bool[]> detail = new List<bool[]>();
            List<bool> port = new List<bool>();
            int portCount = _portCount;
            for (int i = 0; i < portCount; i++)
            {
                byte[] readBytePort = i < readBytes.Length ? readBytes[i] : new byte[0];
                byte[] ignoreBytePort = i < _ignoreBytes.Length ? _ignoreBytes[i] : new byte[0];
                bool[] portDetail = BytesCheck(templateBytes[i], readBytePort, ignoreBytePort);
                detail.Add(portDetail);
                port.Add(portDetail.Aggregate((addr1, addr2) => (addr1 && addr2)));
            }

            ret.Detail = detail;
            ret.Port = port;
            ret.Total = port.Aggregate((addr1, addr2) => (addr1 && addr2));
            ret.IgnoreAddr = _ignoreBytes.ToList();
            return ret;
        }

        private T[] IndexArray<T>(T[][]source,int index)
        {
            if (source == null)
            {
                return new T[]{};
            }
            else
            {
                return source[index];
            }
        }

        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="template">byte数组1</param>
        /// <param name="readData">byte数组2</param>
        /// <param name="ignore">忽略位</param>
        /// <returns>是否相等</returns>
        public bool[] BytesCheck(byte[] template, byte[] readData, byte[] ignore)
        {
            if (readData == null)
            {
                readData=new byte[]{};
            }
            int lengthMax = template.Length > readData.Length ? template.Length : readData.Length;
            int lengthMin = template.Length < readData.Length ? template.Length : readData.Length;

            bool[] ret = new bool[lengthMax];
            for (int i = 0; i < lengthMin; i++)
            {
                ret[i] = template[i] == readData[i];
            }

            foreach (var variable in ignore)
            {
                if (variable < lengthMax)
                {
                    ret[variable] = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// 解析所有头对应的文件
        /// </summary>
        private void ParseData()
        {
            List<DeviceData>[]write=new List<DeviceData>[_portCount];
            List<DeviceData>[]read = new List<DeviceData>[_portCount];
            Dictionary<string,KeyInformation>[] keys= new Dictionary<string, KeyInformation>[_portCount];
            byte[][]ignoreBytes=new byte[_portCount][];
            for (int i = 0; i < _portCount; i++)
            {
                write[i] = GetDeviceData(_portList[i].Action.Write.Command, _portList[i].Data);
                read[i] = GetDeviceData(_portList[i].Action.Read.Command, _portList[i].Data);
                keys[i] = GetKeyField(_portList[i].Display.KeyField.Field);
                ignoreBytes[i] = GetIgnore(_portList[i].Check.Ignore);
            }

            this._keyInformationsList = keys;
            this._writeActionList = write;
            this._readActionList = read;
            this._ignoreBytes = ignoreBytes;
        }

        private List<DeviceData> GetDeviceData(List<Command>commands, Data indirectData)
        {
            List<DeviceData>ret=new List<DeviceData>();
            foreach (var command in commands)
            {
                DeviceData deviceData = ParseCommand(command, indirectData);
                ret.Add(deviceData);
            }

            return ret;
        }

        private DeviceData ParseCommand(Command command, Data indirectData)
        {
            DeviceData ret = new DeviceData();
            ret.Buffer = ParseContext(command, indirectData);
            ret.Chip = Convert.ToByte(command.Chip,16);
            ret.Offset = (byte)ParseStr2Int(command.ChipOffset);
            ret.CmdMode = ParseStr2CmdMode(command.Mode);
            ret.WriteStart= ParseStr2Int(command.Start);
            ret.Length= ParseStr2Int(command.Length);
            return ret;
        }

        /// <summary>
        /// 解析命令模式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private CmdMode ParseStr2CmdMode(string str)
        {
            if (str == "write")
            {
                return CmdMode.Write;
            }else if (str == "read")
            {
                return CmdMode.Read;
            }
            else
            {
                throw new Exception("unsupported Command mode:"+str);
            }
        }

        /// <summary>
        /// 解析Command里面的context属性字段
        /// </summary>
        /// <param name="command">读写命令</param>
        /// <param name="indirectData">Data间接数据</param>
        /// <param name="templateData">模板数据</param>
        /// <returns></returns>
        private byte[] ParseContext(Command command,Data indirectData)
        {
            var context = command.Context;
            if (context.Trim().Length == 0)
            {
                return null;
            }
            int start = ParseStr2Int(command.Start);
            int length = ParseStr2Int(command.Length);
           
            foreach (WriteContext writeContext in indirectData.WriteContext)
            {
                if (writeContext.Name.Equals(context))
                {
                    if (writeContext.Type == "template")
                    {
                        return null;
                    }
                    else if(writeContext.Type== "normal")
                    {
                        return writeContext.Value.Split(' ').Select(t => (byte)ParseStr2Int(t)).ToArray();
                    }
                    else if(writeContext.Type== "repeat")
                    {
                        return RepeatArray(writeContext.Value.Split(' ').Select(t => (byte)ParseStr2Int(t)).ToArray(), ParseStr2Int(writeContext.Repeat));
                    }
                    else
                    {
                        throw new Exception("unsupported WriteContext type:"+ writeContext.Type);
                    }
                }
            }
            return context.Split(' ').Select(t => (byte)ParseStr2Int(t)).ToArray();
        }

        /// <summary>
        /// 将字符串转为int 0x开头为十六进制，否则为十进制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int ParseStr2Int(string str)
        {
            if (str.StartsWith("0x"))
            {
                return Convert.ToInt32(str.Substring(2, str.Length - 2), 16);
            }
            else
            {
                return Convert.ToInt32(str);
            }
        }

        /// <summary>
        /// 截取数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private T[] SliceArray<T>(T[]data,int start, int length)
        {
            if (length == 0)
            {
                return data.Skip(start).ToArray();
            }
            else
            {
                return data.Skip(start).Take(length).ToArray();
            }
        }

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="repeatCount">复制次数</param>
        /// <returns></returns>
        private T[] RepeatArray<T>(T[]source,int repeatCount)
        {
            List<T>ret=new List<T>();
            for (int i = 0; i < repeatCount; i++)
            {
                ret.AddRange(source.ToList());
            }

            return ret.ToArray();
        }
    }
}
