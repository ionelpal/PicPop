using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPop.BusinessLogic.Models
{
    [Serializable]
    public class ShoppingCartModel
    {
        public decimal TotalAmount { get; set; }
        public List<ShoppingCartItemModel> ListItems { get; set; }

        public ShoppingCartModel()
        {
            ListItems = new List<ShoppingCartItemModel>();
        }
    }

    [Serializable]
    public class ShoppingCartItemModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
    }
}
