using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HPMS.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tool;


namespace Test
{
    [TestClass]
    public class UnitTest1
    {
       


        [TestMethod]
        public void TestMethod1()
        {
            string strSavepath = @"b:\sample.xml";
            Dictionary<string,string>result=new Dictionary<string, string>();
            result.Add("SDD21","OK");
            result.Add("SDD11", "OK");
            result.Add("NEXT", "NG");
            Dictionary<string, string> info = new Dictionary<string, string>();
            info.Add("application","WWW");
            info.Add("partNo", "figo11");
            TestUtil.SaveResult_Sample(strSavepath, result, info);
        }
    }
}
