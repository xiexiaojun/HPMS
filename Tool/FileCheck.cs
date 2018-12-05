using System;
using System.IO;
using System.Threading;

namespace Tool
{
    public class FileCheckResult 
    {
       public int Code;
       public string Description;
    }

    public class FileCheck
    {
        public static bool FileIsUsing(string filePath, int timeOutCreate, int timeOutUse, ref string msg, ref int checkTime)
        {
            DateTime start = DateTime.Now;
            bool ret = false;
            bool fileExist = false;
            int createCycleTime = 50;
            int createCycleCount = timeOutCreate / createCycleTime;

            for (int i = 0; i < createCycleCount; i++)
            {
                if (File.Exists(filePath))
                {
                    fileExist = true;
                    break;
                }
                Thread.Sleep(createCycleTime);
            }

            if (!fileExist)
            {
                ret = false;
                msg = "File: " + filePath + " is not exist after " + timeOutCreate.ToString() + "ms";
                return ret;
            }

            int useCycleTime = 50;
            int useCycleCount = timeOutUse / useCycleTime;

            msg = "File: " + filePath + " is still be using after " + timeOutUse.ToString() + "ms";
            for (int i = 0; i < useCycleCount; i++)
            {
                if (FileIsUsing(filePath))
                {
                    ret = true;
                    DateTime stop = DateTime.Now;
                    TimeSpan span = stop - start;
                    checkTime = span.Milliseconds;
                    msg = "";
                    break;
                }
                Thread.Sleep(useCycleTime);
            }


            return ret;

        }

        private static bool FileIsUsing(string filePath)
        {
            bool ret = false;
            try
            {

                FileStream objFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                objFileStream.Close();
                ret = true;
            }
            catch (IOException)
            {
                ret = false;
            }

            return ret;
        }
    }
}
