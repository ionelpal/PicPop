using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PicPop.Helper
{
    public class SharedDataActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ShopCartNumElements =
                PicPop.BusinessLogic.ShoppingCartHelper.NumElements(ShoppingCartHelper.GetCartId());

            if (HttpContext.Current.User.Identity.IsAuthenticated)
                filterContext.Controller.ViewBag.IsTransaction =
                    PicPop.BusinessLogic.TransactionsHelper.HasTransaction(HttpContext.Current.User.Identity.GetUserId());
            
        }
    }
}