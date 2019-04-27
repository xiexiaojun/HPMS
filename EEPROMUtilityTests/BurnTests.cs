using Microsoft.VisualStudio.TestTools.UnitTesting;
using EEPROMUtility;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualIIC;

namespace EEPROMUtility.Tests
{
    [TestClass()]
    public class BurnTests
    {
        [TestMethod()]
        public void WriteDataTest()
        {
            string pn = "aa";
            string folder = @"B:\";
            //Ii2c aa=new CP2112(1,20,8);


            Ii2c bb = new LuxshareIi2C("COM20",8,20);

            Burn burn=new Burn(pn,bb,folder);
            var data = new byte[1][];
           var readData= burn.WriteData(data);
            Assert.Fail();
        }
    }
}