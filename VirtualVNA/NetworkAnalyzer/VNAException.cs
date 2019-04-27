using System;

namespace VirtualVNA.NetworkAnalyzer
{
    /// <summary>
    /// 网分异常类
    /// </summary>
    [Serializable]
    public class VnaException:ApplicationException
    {
        private string _error;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">异常信息</param>
        public VnaException(string msg) : base(msg)
        {
            this._error = msg;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="innerException">异常</param>
        public VnaException(string msg, Exception innerException) : base(msg)
        {
            this._error = msg;
        }

        /// <summary>
        /// 返回对应的异常信息
        /// </summary>
        /// <returns></returns>
        public string GetError()
        {
            return _error;
        }
    }
}
