using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PicPop.BusinessLogic;
using PicPop.Models;

namespace PicPop.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index(string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
                userId = User.Identity.GetUserId();

            var transactionHelper = new TransactionsHelper();
            var lstTransaction = transactionHelper.GetMyTransaction(userId);
            if (!lstTransaction.Any())
                return RedirectToAction("Index", "Home");
            var model = lstTransaction.Select(x => new TransactionsDetailsViewModels()
            {
                Id = x.Id,
                Amount = x.Amount,
                Date = x.DtAdded,
                TypeTransaction = x.TypeId ? "Buy" : "Sell"
            }).AsEnumerable();

            return View(model);
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int id, string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
                userId = User.Identity.GetUserId();

            var transactionHelper = new TransactionsHelper();
            var transaction = transactionHelper.GetTransaction(id);
            if (transaction == null || transaction.TransactionItems.Count.Equals(0) || !transaction.UserId.Equals(userId))
                return RedirectToAction("Index", "Transaction");

            var model = transaction.TransactionItems.Select(x => new TransactionsDetailsViewModels()
            {
                Id = x.ImageId,
                Name = x.PicPopImage.Name,
                Amount = x.Value,
                Date = x.Transaction.DtAdded
            }).AsEnumerable();
            
            return View(model);
        }



        //// GET: Transaction/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Transaction/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Transaction/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Transaction/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Transaction/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Transaction/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
