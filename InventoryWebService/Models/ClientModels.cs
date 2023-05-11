using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class ClientModels
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientPassword { get; set; }
        public string ShopName { get; set; }
        public string Phone { get; set; }
        public int DivisionID { get; set; }
        public int TownshipID { get; set; }
        public string Address { get; set; }
        public bool IsSalePerson { get; set; }
        public string DivisionName { get; set; }
        public string TownshipName { get; set; }
        public string Token { get; set; }
    }
}