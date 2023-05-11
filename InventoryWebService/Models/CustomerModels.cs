using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class CustomerModels
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public bool IsDefault { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int DivisionID { get; set; }
        public int TownshipID { get; set; }
    }
}