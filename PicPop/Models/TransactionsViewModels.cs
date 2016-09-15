using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicPop.Models
{
    public class TransactionsViewModels
    {
    }

    public class TransactionsDetailsViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Amount")]
        public string TypeTransaction { get; set; }
    }
}