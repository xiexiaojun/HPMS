using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataTable table = new DataTable();
            string value = table.Compute("1+sin(2)*(4-3)", "").ToString();
            Console.WriteLine(value);
        }
    }
}
