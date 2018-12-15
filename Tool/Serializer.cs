using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tool
{
   
    public class Serializer
    {
        /// <summary>
        /// 序列化类到xml文档
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">类的对象</param>
        /// <param name="filePath">xml文档路径（包含文件名）</param>
        /// <returns>成功：true，失败：false</returns>
        public static bool CreateXML<T>(T obj, string filePath)
        {
            XmlWriter writer = null;    //声明一个xml编写器
            XmlWriterSettings writerSetting = new XmlWriterSettings //声明编写器设置
            {
                Indent = true,//定义xml格式，自动创建新的行
                Encoding = UTF8Encoding.UTF8,//编码格式
            };

            try
            {
                string xmlDirectory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(xmlDirectory))
                {
                    Directory.CreateDirectory(xmlDirectory);
                }

                //创建一个保存数据到xml文档的流
                writer = XmlWriter.Create(filePath, writerSetting);
            }
            catch (Exception ex)
            {
                //_logServ.Error(string.Format("创建xml文档失败：{0}", ex.Message));
                return false;
            }

            XmlSerializer xser = new XmlSerializer(typeof(T));  //实例化序列化对象

            try
            {
                xser.Serialize(writer, obj);  //序列化对象到xml文档
            }
            catch (Exception ex)
            {
                //_logServ.Error(string.Format("创建xml文档失败：{0}", ex.Message));
                return false;
            }
            finally
            {
                writer.Close();
            }
            return true;
        }


        public static bool CreateXML<T>(T obj, ref Stream xmlStream)
        {
            
            XmlWriterSettings writerSetting = new XmlWriterSettings //声明编写器设置
            {
                Indent = true,//定义xml格式，自动创建新的行
                Encoding = UTF8Encoding.UTF8,//编码格式
            };

            

            XmlSerializer xser = new XmlSerializer(typeof(T));  //实例化序列化对象

            try
            {
                xser.Serialize(xmlStream, obj);  //序列化对象到xml文档
                using (MemoryStream ms = new MemoryStream())
                {
                    xmlStream.CopyTo(ms);
                   // return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                //_logServ.Error(string.Format("创建xml文档失败：{0}", ex.Message));
                return false;
            }
            finally
            {
                xmlStream.Close();
            }
            return true;
        }


        public static object FromXmlString(Stream xmlStream, Type type)
        {
          
            using (XmlReader reader=XmlReader.Create(xmlStream))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                try
                {
                    return serializer.Deserialize(reader);
                  
                }
                catch
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 从 XML 文档中反序列化为对象
        /// </summary>
        /// <param name="filePath">文档路径（包含文档名）</param>
        /// <param name="type">对象的类型</param>
        /// <returns>返回object类型</returns>
        public static object FromXmlString(string filePath, Type type)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            string xmlString = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(xmlString))
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                try
                {
                    return serializer.Deserialize(stream);
                }
                catch
                {
                    return null;
                }
            }

        }

        public static string Datatable2Json(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt);
        }

        public static DataTable Json2DataTable(string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);

        }

        public static string Object2Json(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Json2Object<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
     

        public static string SerializeDataTableXml(DataTable pDt)
        {
            // 序列化DataTable
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            pDt.TableName = "Test";
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();

            return sb.ToString();
        }

        public static DataTable DeserializeDataTable(string pXml)
        {

            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));

            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;

            return dt;
        }
    }
}
