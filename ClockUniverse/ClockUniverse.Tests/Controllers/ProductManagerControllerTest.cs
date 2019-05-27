using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using ClockUniverse.Controllers;
using ClockUniverse;
using System.Text;
using System;
namespace ClockUniverse.Tests.Controllers
{
    [TestClass]
    public class ProductManagerControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CsK23T3bEntities();
            var controller = new ProductManagerController();
            var result = controller.Index();

            var view = result as ViewResult;
            Assert.IsNotNull(view);

            var model = view.Model as List<ProductTable>;
            Assert.AreEqual(db.ProductTables.Count(), model.Count);
            
        }

        [TestMethod]
        public void TestCreateG() {
            var controller = new ProductManagerController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateP() {
        }

        [TestMethod]
        public void TestDetail()
        {
            {
                var db = new CsK23T3bEntities();
                var item = db.ProductTables.First();
                var controller = new ProductManagerController();

                var result = controller.Details(item.Watch_ID);
                var view = result as ViewResult;
                Assert.IsNotNull(view);

                var model = view.Model as ProductTable;
                Assert.IsNotNull(model);
                Assert.AreEqual(item.Watch_ID, model.Watch_ID);

                result = controller.Details(0);
                Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            }
        }

        [TestMethod]
        public void TestEditG()
        {
            var controller = new ProductManagerController();
            var result0 = controller.Edit(0);
            Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));

            var db = new CsK23T3bEntities();
            var item = db.ProductTables.First();
            var result1 = controller.Edit(item.Watch_ID) as ViewResult;
            Assert.IsNotNull(result1);
            var model = result1.Model as ProductTable;
            Assert.AreEqual(item.Watch_ID, model.Watch_ID);
        }

      
    }
}
