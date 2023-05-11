using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class TranSaleOrderModels
    {
        public int ID { get; set; }
        public int SaleOrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int SalePrice { get; set; }
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public int Number { get; set; }
    }
}