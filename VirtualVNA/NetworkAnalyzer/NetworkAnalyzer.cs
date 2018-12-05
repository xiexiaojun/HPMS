namespace VirtualVNA.NetworkAnalyzer
{
    public abstract class NetworkAnalyzer
    {
        /// <summary>
        /// 获取SNP文件方法
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="switchIndex"></param>
        /// <param name="mutiChannel"></param>
        /// <param name="nextByTrace"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg);

        /// <summary>
        /// 获取SNP文件方法
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="switchIndex"></param>
        /// <param name="mutiChannel"></param>
        /// <param name="nextByTrace"></param>
        /// <param name="index"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg);

        /// <summary>
        /// 直接获取测试数据的方法
        /// </summary>
        /// <param name="fre"></param>
        /// <param name="db"></param>
        /// <param name="switchIndex"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg);

        public string ReplaceSlash(string source)
        {
            return source.Replace("\\", "/");
        }
    }
}
