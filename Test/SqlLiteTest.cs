using HPMS.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;

namespace Test
{
    
    
    /// <summary>
    ///这是 SqlLiteTest 的测试类，旨在
    ///包含所有 SqlLiteTest 单元测试
    ///</summary>
    [TestClass()]
    public class SqlLiteTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///ExecuteDataTable 的测试
        ///</summary>
        [TestMethod()]
        public void ExecuteDataTableTest()
        {
            //string sql = string.Empty; // TODO: 初始化为适当的值
            //OleDbParameter[] pms = null; // TODO: 初始化为适当的值
            //DataTable expected = null; // TODO: 初始化为适当的值
            //DataTable actual;
            //string querySql = "SELECT ID, Name, Description, RightsID, Status,CreateID,CreateDate FROM HPMS_Role where ID=? and  Status >=0";
            //SQLiteParameter[] b = new SQLiteParameter[1];
            //b[0] = new SQLiteParameter("0", 1);
            //actual = SqlLite.ExecuteDataTable(querySql);
            //int kk = actual.Rows.Count;
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
