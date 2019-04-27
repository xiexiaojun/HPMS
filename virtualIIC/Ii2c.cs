namespace VirtualIIC
{
    /// <summary>
    /// I2C接口
    /// </summary>
    public  interface Ii2c
    {
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        bool openDevice();
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="slaveAdd">器件地址</param>
        /// <param name="startReadAdd">起始位置</param>
        /// <param name="channel">通道</param>
        /// <param name="readLen">读取长度</param>
        /// <returns></returns>
        operationResult readDevice(byte slaveAdd, byte startReadAdd, byte channel, byte readLen);
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="slaveAdd">器件地址</param>
        /// <param name="startWriteAdd"></param>
        /// <param name="channel"></param>
        /// <param name="writeLen"></param>
        /// <param name="writeBuffer"></param>
        /// <returns></returns>
        operationResult writeDevice(byte slaveAdd, byte startWriteAdd, byte channel, int writeLen, byte[] writeBuffer);
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        bool closeDevice();
    }
}
