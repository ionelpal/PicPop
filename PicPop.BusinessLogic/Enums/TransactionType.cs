using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic
{
  
    public enum TransactionType : int
    {
        [Display(Name = "Sell")]
        Sell = 0,
        [Display(Name = "Buy")]
        Buy = 1
    }
}
