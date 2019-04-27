using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace VirtualIIC
{
    static class Util
    {
        public static byte getFromStr(string strHex) {
            byte tempdata1 = System.Text.Encoding.ASCII.GetBytes(strHex.Substring(0, 1))[0];
            byte tempdata2 = System.Text.Encoding.ASCII.GetBytes(strHex.Substring(1, 1))[0];
            if ((tempdata1 > 47) && (tempdata1 < 58))
            {
                tempdata1 = (byte)(tempdata1 - 48);
            }
            else
            {
                tempdata1 = (byte)(tempdata1 - 55);
            }

            if ((tempdata2 > 47) && (tempdata2 < 58))
            {
                tempdata2 = (byte)(tempdata2 - 48);
            }
            else
            {
                tempdata2 = (byte)(tempdata2 - 55);
            }

            byte nRomAddr1 = (byte)(tempdata1 * 16 + tempdata2);

            return nRomAddr1;
        }

       

    

        //累加和校验【每字节相加（16进）取后末两位】
        public static byte GetVerifyCodeByte(byte[] bytes)
        {

            int result = 0;
            foreach (byte b in bytes)
            {
                result += b;
            }
            return (byte)(result % 256);

        }

        public static byte[][] readTemplate(string templatePath) {
           string LastName = templatePath.Substring(templatePath.LastIndexOf(".") + 1, (templatePath.Length - templatePath.LastIndexOf(".") - 1)); //扩展名
           if (LastName.ToLower() == "bin") {
               return readBinFile(templatePath);
           }
           return File.ReadAllLines(templatePath).Select(x => x.Split('\t').Select(a => Convert.ToByte(a, 16)).ToArray()).ToArray();

       
        }

        public static byte[][] readBinFile(string binFilePath) {
            FileStream fs;
            fs = new FileStream(@binFilePath, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
             int fileLength = (int)fs.Length;
             byte[] data = br.ReadBytes((int)fs.Length);
            byte[][] result = new byte[fileLength][];
            for (int i = 0; i < data.Length; i++)
            {
                byte[] temp = new byte[1];
                temp[0] = data[i];
                result[i] = temp;
            }
            fs.Close();
            br.Close();
            return result;
        }

        public static void save_File(byte[] btemp, string filePath)
        {
            string LastName = filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1)); //扩展名
            if (LastName.ToLower() == "bin")
            {
                saveBinFile(btemp, filePath);
            }
            else {
                string[] stemp = Array.ConvertAll<byte, string>(btemp, s => s.ToString("x2").ToUpper());
                File.WriteAllLines(filePath, stemp);
            }
           
        }
        public static void saveBinFile(byte[] btemp, string filePath)
        {
            FileStream fs;
            fs = new FileStream(@filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter br = new BinaryWriter(fs);
            br.Write(btemp);
            fs.Close();
            br.Close();
        }
        public static byte[] twoD_ArrayIndex(this byte[][] source,int column_index,int rowLength) {
            int rows = source.GetLength(0);
            int col = source[0].GetLength(0);
            //int dimension = (int)source.Rank;

            if (column_index > col - 1)
            {
                column_index = col - 1;
            }
           
            byte[] btemp = new byte[rowLength];
            try
            {
                for (int i = 0; i < rowLength; i++)
                {
                    btemp[i] = source[i][column_index];
                }
            }
            catch(Exception e) {
                throw e;
            }
           
            return btemp;
        }

      

 

        public static bool arrayCompare(byte[] A, byte[] B)
        {
            if (A.Length != B.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < A.Length; i++)
                {
                    if (A[i] != B[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool IsHexadecimal(string str)
        {
            const string PATTERN = @"[A-Fa-f0-9]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
        }

        public static string HexValidate(string str)
        {
            string strResult = "00";
            string strTemp = str.Trim();
            if (strTemp.Length == 0) {
                return strResult;
            }
            if (strTemp.Length == 1)
            {
                strTemp = "0" + strTemp;
            }
            strTemp = strTemp.Trim().Substring(0, 2);
            if (IsHexadecimal(strTemp))
            {
                return strTemp;
            }
            else {
                return strResult;
            }
          
        }

        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
