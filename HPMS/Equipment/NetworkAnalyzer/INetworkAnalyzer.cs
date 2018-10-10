using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _32p_analyze;

namespace HPMS.Equipment.NetworkAnalyzer
{
    interface INetworkAnalyzer
    {
        /// <summary>
        /// 获取SNP文件方法
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="switchIndex"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool SaveSnp(string saveFilePath,int switchIndex, ref string msg);

       /// <summary>
        /// 直接获取测试数据的方法
       /// </summary>
       /// <param name="fre"></param>
       /// <param name="db"></param>
       /// <param name="switchIndex"></param>
       /// <param name="msg"></param>
       /// <returns></returns>
        bool GetTestData(ref double[]fre,double[]db,int switchIndex,ref string msg);
    }
}
