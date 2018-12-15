using System;
using System.IO;
using System.Windows.Forms;
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

            }

            return ret;
        }

        /// <summary>
        /// 授权检测
        /// </summary>
        /// <param name="machineCode"></param>
        /// <param name="softName"></param>
        /// <param name="softVersion"></param>
        /// <param name="regCode"></param>
        /// <returns></returns>
        public static bool IsAuthorize(string machineCode, string softName, ref string softVersion)
        {
            string regCode = ReadCode();
            bool ret = false;

            try
            {
                string regJson = SoftSecurity.MD5Decrypt(regCode, "bayuejun");
                JObject tempJObject = JObject.Parse(regJson.ToString());
                ret = (tempJObject.Property("machineCode").Value.ToString() == machineCode) ||
                      tempJObject.Property("softName").Value.ToString() == machineCode;
                softVersion = tempJObject.Property("softVersion").Value.ToString();
                
            }
            catch (Exception e)
            {

            }

            return ret;
        }
    }
}
