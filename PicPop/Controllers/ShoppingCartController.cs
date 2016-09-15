using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PicPop.BusinessLogic;
using PicPop.BusinessLogic.Utils;
using PicPop.Models;

namespace PicPop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            string userId = GetCartId();

            var cart = ShoppingCartHelper.GetCart(userId);

            if(cart == null)
                return RedirectToAction("Index", "Home");

            var model = cart.ListItems.Select(x=> new SummaryCartViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount
            }).AsEnumerable();
            
            return View(model);
        }


        // GET: /ShoppingCart/Add/1
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Add(int id)
        {
            string userId = GetCartId();
            
            ShoppingCartHelper shopCartHelper = new ShoppingCartHelper();
            int numItems = shopCartHelper.Add(id, userId);

            // Display the confirmation message
            var result = new AddToCartViewModel()
            {
                NumItems = numItems.ToString()
            };

            return Json(result);
        }

        
        [Authorize]
        public ActionResult Buy()
        {
            string userId = string.Empty;

            if (User.Identity.IsAuthenticated)
                userId = User.Identity.GetUserId();
            else
                userId = GetCartId();

            var cart = ShoppingCartHelper.GetCart(userId);

            if (cart == null || !cart.ListItems.Any())
                return RedirectToAction("Index", "Home");
            
            var lst = cart.ListItems.Select(x => new TransactionItem()
            {
                ImageId = x.Id,
                Value = x.Amount
            }).ToList();
            
            var transactionHelper = new TransactionsHelper();
            // TypeId = true because is a Buy. We generate the sell with a trigger in the DB
            var transaction = new Transaction()
            {
                UserId = userId,
                DtAdded = DateTime.Now,
                CreatedBy = userId,
                Amount = cart.TotalAmount,
                TransactionItems = lst,
                TypeId = true
            };
            transactionHelper.Add(transaction);


            return RedirectToAction("Index","Transaction");
        }


        // GET: /ShoppingCart/Add/1
        [HttpGet]
        public ActionResult Remove(int id)
        {
            string userId = GetCartId();

            ShoppingCartHelper shoppingCartHelper = new ShoppingCartHelper();
            shoppingCartHelper.Delete(id, userId);
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult RemoveAll()
        {
            string userId = GetCartId();

            ShoppingCartHelper shoppingCartHelper = new ShoppingCartHelper();
            shoppingCartHelper.DeleteAll(userId);

            return RedirectToAction("Index");
        }


        private string GetCartId()
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["CartId"] = User.Identity.GetUserId();
                return User.Identity.GetUserId();
            }

            return PicPop.Helper.ShoppingCartHelper.GetCartId();
        }
    }
}
