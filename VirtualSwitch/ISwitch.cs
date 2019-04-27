namespace VirtualSwitch
{
    /// <summary>
    /// 矩阵开关控制接口
    /// </summary>
    public interface ISwitch
    {
        /// <summary>
        /// 关闭所有通道
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        bool CloseAll(ref string errMsg);
        /// <summary>
        /// 开启开关矩阵配置中的指定行或列的开关,由构造函数来决定打开一行或一列
        /// </summary>
        /// <param name="switchIndex">行/列索引</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        bool Open(int switchIndex, ref string errMsg);
        /// <summary>
        /// 开启由对应开关组的串口指令指定的开关
        /// </summary>
        /// <param name="switchNum">要写入的byte[]数组</param>
        /// <param name="errMsg">异常信息</param>
        /// <returns></returns>
        bool Open(byte[] switchNum, ref string errMsg);

    }
    enum IndexType
    {
        Row,
        Column
    }
}
