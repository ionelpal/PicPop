using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicPop.BusinessLogic.Utils;

namespace PicPop.Helper
{
    public class ShoppingCartHelper
    {


        public static string GetCartId()
        {
            if (HttpContext.Current.Session["CartId"] != null)
                return HttpContext.Current.Session["CartId"].ToString();

            var userId = Common.GetGuid();
            HttpContext.Current.Session["CartId"] = userId;
            return userId;
        }
    }
}