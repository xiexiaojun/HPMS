namespace VirtualVNA.NetworkAnalyzer
{
    /// <summary>
    /// 网分抽象类，所有的网分型号对应的驱动类必须继承此类
    /// </summary>
    public abstract class NetworkAnalyzer
    {
        /// <summary>
        /// 获取SNP文件方法
        /// </summary>
        /// <param name="saveFilePath">s4p保存路径</param>
        /// <param name="switchIndex">对应的开关矩阵索引</param>
        /// <param name="mutiChannel">是否多通道</param>
        /// <param name="nextByTrace">是否串音测试</param>
        /// <param name="msg">出错信息</param>
        /// <returns></returns>
        public abstract bool SaveSnp(string saveFilePath, int switchIndex, bool mutiChannel, bool nextByTrace, ref string msg);

        /// <summary>
        /// 获取SNP文件方法
        /// </summary>
        /// <param name="saveFilePath">s4p保存路径</param>
        /// <param name="switchIndex">对应的开关矩阵命令</param>
        /// <param name="mutiChannel">是否多通道</param>
        /// <param name="nextByTrace">是否串音测试</param>
        /// <param name="index">对应的开关矩阵索引</param>
        /// <param name="msg">出错信息</param>
        /// <returns></returns>
        public abstract bool SaveSnp(string saveFilePath, byte[] switchIndex, int index, bool mutiChannel, bool nextByTrace, ref string msg);

        /// <summary>
        /// 直接获取测试数据的方法
        /// </summary>
        /// <param name="fre">频率</param>
        /// <param name="db">衰减</param>
        /// <param name="switchIndex">开关索引</param>
        /// <param name="msg">出错信息</param>
        /// <returns></returns>
        [System.Obsolete]
        public abstract bool GetTestData(ref double[] fre, double[] db, int switchIndex, ref string msg);

        /// <summary>
        /// 路径替换将\替换成/
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string ReplaceSlash(string source)
        {
            return source.Replace("\\", "/");
        }

        /// <summary>
        /// 载入档案配置文件
        /// </summary>
        /// <param name="calFilePath">档案文件路径</param>
        /// <param name="msg">出错信息</param>
        /// <returns></returns>
        public abstract bool LoadCalFile(string calFilePath, ref string msg);

    }
}
