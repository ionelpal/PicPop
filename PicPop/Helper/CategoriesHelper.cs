using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicPop.Helper
{
    public class CategoriesHelper
    {
        public static List<SelectListItem> GetCategoriesSelectedItems()
        {
            var category = new BusinessLogic.CategoriesHelper();
            var lst = category.GetListItem();

            return lst.Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text
            }).ToList();
        }
    }
}