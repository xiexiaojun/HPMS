using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClassLibraryMisWebservice.MISGetSNAndPrintLabalWebReference;

namespace VirtualPrinter
{
    /// <summary>
    /// 条码类型
    /// </summary>
    public enum LabelType
    {
        /// <summary>
        /// 产品条码
        /// </summary>
        Product,
        /// <summary>
        /// 制程条码
        /// </summary>
        Process
    }


    /// <summary>
    /// Bartender打印方式类
    /// <example>
    /// <code>
    /// //初始化打印类
    /// BartenderPrinter bartenderPrinter=new BartenderPrinter();
    /// string printSn="L01HE019-SD-R190304000053";
    /// string printContent = "";
    /// string templatePath = "";
    /// //根据条码sn查出对应的打印内容
    /// if (bartenderPrinter.QuerySn(sn, ref printContent, ref templatePath, LabelType.Product, ref queryMsg))
    /// {
    ///         //设置打印模板路径
    ///          bartenderPrinter.TemplatePath = templatePath;
    ///         //打印返回的模板内容
    ///          bartenderPrinter.Print(printContent);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class BartenderPrinter:IPrinter
    {
        private string _templatePath;
        private BarTender.Application btApp=new BarTender.Application();
        MESDataServices mesDataServices = new MESDataServices();

        /// <summary>
        /// 设置打印模板路径
        /// </summary>
        public string TemplatePath
        {
            get { return _templatePath; }
            set { _templatePath = value; }
        }

        /// <summary>
        /// bartender打印功能实现
        /// </summary>
        /// <param name="content">打印内容，并非直接是SN</param>
        /// <returns>打印成功返回True</returns>
        public bool Print(string content)
        {
            BarTender.Format btFormat;
            BarTender.BtPrintResult btPrintRtn;
            BarTender.Messages btMsgs = null;

            string strFilePath = SaveContent2File(content);
            btFormat = btApp.Formats.Open(_templatePath, false, String.Empty);
            btPrintRtn = btFormat.Print("", false, -1, out btMsgs);

            if (btPrintRtn != BarTender.BtPrintResult.btSuccess)
            {
                foreach (BarTender.Message msg in btMsgs)
                {
                    throw new Exception(msg.Message);
                }
            }

            btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);
            btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
            return true;
        }

        private string SaveContent2File(string content)
        {
            string savFilePath = @"C:\Program Files\默认公司名称\Bartender.txt";
            File.WriteAllText(savFilePath, content,Encoding.UTF8);
            return savFilePath;
        }

        /// <summary>
        /// SN查询功能
        /// </summary>
        /// <param name="sn">要打印的sn</param>
        /// <param name="printContent">生成的打印内容</param>
        /// <param name="templatePath">对应的bartender模板路径</param>
        /// <param name="labelType">条码类型</param>
        /// <param name="queryMsg">查询结果信息</param>
        /// <returns>bool</returns>
        public bool QuerySn(string sn,ref string printContent,ref string templatePath,LabelType labelType,ref string queryMsg)
        {
            switch (labelType)
            {
                case    LabelType.Product:
                    queryMsg = mesDataServices.PrintSFCFSNLabel(sn, false, false, "0001", "HT001", true, true, false, false, "LXXT", "SEE-D",
                        ref templatePath, ref printContent);
                    break;
                case LabelType.Process:
                    string strFSNPE = "";
                    queryMsg = mesDataServices.PrintSFCFSNLabelN(sn, "HT001", "TestID", "LXXT", "SEE-D", 1, true, ref strFSNPE,
                        ref templatePath, ref printContent);
                    break;

            }

            return queryMsg == "OK";
        }

      }
}
