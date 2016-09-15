using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PicPop.Controllers;
using PicPop.Models;

namespace PicPopUnitTest
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public void TestMethodIndex()
        {
            var controller = new TransactionController();
            var result = controller.Index(Properties.Settings.Default.UserTestId) as ViewResult;
            Assert.IsNotNull(result);
            if (result == null)
            {
                Assert.Fail("Object null");
                return;
            }
            var model = (IEnumerable<TransactionsDetailsViewModels>) result.ViewData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.Any());
            var firstElement = model.FirstOrDefault();

            Assert.AreEqual(9, firstElement.Id);

            Assert.AreEqual(9.00, firstElement.Id);
            Assert.AreEqual("11/29/2015", firstElement.Date.ToString("MM/dd/yyyy"));

        }

        [TestMethod]
        public void TestMethodDetails()
        {
            var controller = new TransactionController();
            var result = controller.Details(9, Properties.Settings.Default.UserTestId) as ViewResult;
            Assert.IsNotNull(result);
            
            var model = (IEnumerable<TransactionsDetailsViewModels>) result.ViewData.Model;
            Assert.IsNotNull(model);

            Assert.IsTrue(model.Any());
            var firstElement = model.FirstOrDefault();

            Assert.AreEqual("9", firstElement.Amount.ToString("#.##"));
            Assert.AreEqual("11/29/2015", firstElement.Date.ToString("MM/dd/yyyy"));
        }
    }
}
