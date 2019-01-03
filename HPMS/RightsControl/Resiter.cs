using System;
using System.IO;
using System.Windows.Forms;
using HPMS.Log;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;

namespace HPMS.RightsControl
{
    class Resiter
    {
        private static string ReadCode()
        {
            string ret = "";
            try
            {
                ret = File.ReadAllText(Application.StartupPath + @"\license.lic");
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("读取"+Application.StartupPath + @"\license.lic 文件出错",e);
            }

            return ret;
        }

        private static void WriteCode(JObject licJObject)
        {
            try
            {
                string txt = SoftSecurity.MD5Encrypt(licJObject.ToString(),"bayuejun");
                File.WriteAllText(Application.StartupPath + @"\license.lic",txt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public static bool IsAuthorize(string machineCode, string softName, ref string softVersion,
            ref string expireDate,ref string msg)
        {
            string regCode = ReadCode();
            return IsAuthorize(regCode, machineCode, softName, ref softVersion, ref expireDate,ref msg);
        }

        /// <summary>
        /// 授权检测
        /// </summary>
        /// <param name="machineCode"></param>
        /// <param name="softName"></param>
        /// <param name="softVersion"></param>
        /// <param name="regCode"></param>
        /// <returns></returns>
        public static bool IsAuthorize(string regCode,string machineCode, string softName, ref string softVersion,ref string expireDate,ref string msg)
        {
            //string regCode = ReadCode();
            bool ret = false;

            try
            {
                string regJson = SoftSecurity.MD5Decrypt(regCode, "bayuejun");
                JObject regJObject = JObject.Parse(regJson);
                softVersion = regJObject.Property("softVersion").Value.ToString();
                if (regJObject.ContainsKey("lic"))
                {
                    JObject dateJObject = (JObject)regJObject.Property("lic").Value;
                    bool dateType = (bool)dateJObject.Property("dateType").Value;
                    if (dateType)
                    {
                        //长期许可
                        ret = true;
                        expireDate = "长期";
                    }
                    else
                    {
                        DateTime now=DateTime.Now;
                        DateTime expireDateTime = (DateTime)dateJObject.Property("expireDate").Value;
                        DateTime lastDateTime = (DateTime)dateJObject.Property("lastDate").Value;
                        DateTime licDateTime = (DateTime)dateJObject.Property("licDate").Value;

                        if(CheckLicDate(now,expireDateTime,lastDateTime,licDateTime,ref msg))
                        {
                            //当前时间小于expiretime并且大于上次时间和lic生成时间
                            //regJObject
                            dateJObject["lastDate"] = DateTime.Now;
                            regJObject["lic"] = dateJObject;
                            expireDate = expireDateTime.ToString("yyyy-MM-dd");
                            ret = true;
                        }
                        else
                        {
                            //许可到期
                            ret = false;
                           }
                    }
                }
                else
                {
                    //兼容未加入时间限制时的许可证处理
                    ret = (regJObject.Property("machineCode").Value.ToString() == machineCode) ||
                          regJObject.Property("softName").Value.ToString() == machineCode;
                    
                    expireDate = "长期";
                }
              
                
            }
            catch (Exception e)
            {
                msg = "软件未注册";
                LogHelper.WriteLog("注册文件读取处理错误",e);
            }

            return ret;
        }

        private static bool CheckLicDate(DateTime now, DateTime expireDateTime, DateTime lastDateTime, DateTime licDateTime,ref string msg)
        {
            if (DateTime.Compare(now, expireDateTime) > 0)
            {
                msg = "许可已到期";
                return false;
            }
            if (DateTime.Compare(now, lastDateTime) < 0)
            {
                msg = "系统日期错误";
                return false;
            }
            if (DateTime.Compare(now, licDateTime) < 0)
            {
                msg = "系统日期错误";
                return false;
            }

            return true;
        }
    }
}
