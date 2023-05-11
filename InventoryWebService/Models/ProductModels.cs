using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class ProductModels
    {
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int SalePrice { get; set; }
        public int SubMenuID { get; set; }
        public string PhotoUrl { get; set; }
    }
}