using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EEPROM.Code.Utility
{
    public static class Extension
    {
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute=Attribute.GetCustomAttribute(field,typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }

        public static string GetChinese(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            ToChinese attribute = Attribute.GetCustomAttribute(field, typeof(ToChinese)) as ToChinese;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Chinese;
        }

        public static T[][] Rotate<T>(this T[][]tArray)
        {
            int x = tArray.Length; // 一维
            int y = tArray[0].Length; // 二维
            T[][]ret=new T[y][];
            for (int i = 0; i < y; i++)
            {
                T[]temp=new T[x];
                for (int j = 0; j < x; j++)
                {
                    temp[j] = tArray[j][i];
                }
                ret[i] = temp;
            }
            return ret;
        }

        public static void Write2DArray<T>(T[][]tArray,string fileName)
        {
            var fileFolder = System.IO.Directory.GetParent(fileName).FullName;
            if (!System.IO.Directory.Exists(fileFolder))
            {
                System.IO.Directory.CreateDirectory(fileFolder);
            }
            var writeStrings = tArray
                .Select(t => { return string.Join("\t", t.Select(t1 => t1.ToString())); }).ToArray();
            File.WriteAllLines(fileName,writeStrings);
        }

        /// <summary>
        /// 返回串口列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSerialPortsList()
        {
            List<string> ret = new List<string>();

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
