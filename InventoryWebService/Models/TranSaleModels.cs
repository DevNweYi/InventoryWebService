using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class TranSaleModels
    {       
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int SalePrice { get; set; }
        public int Amount { get; set; }
        public bool IsFOC { get; set; }
        public string ProductName { get; set; }
        public int DiscountPercent { get; set; }
        public int Discount { get; set; }
    }
}