using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace PicPop.Models
{
    public class AddToCartViewModel
    {
        public string NumItems { get; set; }
    }

    public class SummaryCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount  { get; set; }
    }
}