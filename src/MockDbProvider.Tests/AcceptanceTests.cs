using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;

namespace MockDbProvider.Tests
{
    using Syntax;
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AcceptanceTests
    {
        private MockDbProviderFactory Factory
        {
            get
            {
                return MockDbProviderFactory.Instance;
            }
        }

        [TestMethod]
        public void CreatesProviderFromFactory()
        {
            var factory = DbProviderFactories.GetFactory("MockDbProvider") as MockDbProviderFactory;
            Assert.IsInstanceOfType(Factory, typeof(MockDbProviderFactory));
        }

        [TestInitialize]
        public void OnInit()
        {
            Factory.ClearBehaviors();
        }

        [TestMethod]
        public void CanSetupResultForSimpleForQuery()
        {
            var customers = new DataTableBuilder()
                .AddColumn("userid", typeof(Int32))
                .AddColumn("email", typeof(String))
                .AddRow(1, "a@a.com")
                .AddRow(10, "b@b.com").DataTable;
            
            var behavior = new MockCommandBehavior()
                .When(c=> c.CommandText.StartsWith("select *"))
                .ReturnsData(customers);
            Factory.AddBehavior(behavior);

            var table = SUT.DataAccess.GetAllUsers();
            Assert.AreEqual(2, table.Rows.Count);
            Assert.AreEqual(1, table.Rows[0][0]);
            Assert.AreEqual("a@a.com", table.Rows[0][1]);            
            Assert.AreEqual(10, table.Rows[1][0]);
            Assert.AreEqual("b@b.com", table.Rows[1][1]);            
        }

        [TestMethod]
        public void CanFillDataSet()
        {
            var users = new DataTableBuilder()
                .AddColumn("customerid", typeof(Int32))
                .AddColumn("firstname", typeof(String))
                .AddColumn("lastname", typeof(String))
                .AddRow(1, "joe", "black")
                .AddRow(1, "kurt", "vonnegut").DataTable;

            var orders = new DataTableBuilder()
                .AddColumn("orderid", typeof(Int32))
                .AddColumn("userid", typeof(Int32))
                .AddColumn("total", typeof(double))
                .AddRow(100, 1, 10.10)
                .AddRow(101, 1, 10.20)
                .AddRow(202, 2, 20.10)
                .AddRow(203, 2, 20.20).DataTable;

            Factory.AddBehavior(new MockCommandBehavior()
                .When(cmd=>cmd.CommandText.Contains("from customers"))
                .ReturnsData(users));
            Factory.AddBehavior(new MockCommandBehavior()
                .When(cmd=>cmd.CommandText.Contains("from orders"))
                .ReturnsData(orders));

            var result = SUT.DataAccess.GetAllOrders();

            Assert.AreEqual(2, result.Tables.Count);
            Assert.AreEqual(2, result.Tables["customers"].Rows.Count);
            Assert.AreEqual(4, result.Tables["orders"].Rows.Count);
                
        }

        [TestMethod]
        public void CanGetScalar()
        {
            Factory.AddBehavior(new MockCommandBehavior()
                .When(cmd => cmd.CommandText.StartsWith("select userid from users"))
                .ReturnsScalar(15559));

            var result = SUT.DataAccess.GetUserId("abc", "test");
            Assert.AreEqual(15559, result);
        }
    }
}
