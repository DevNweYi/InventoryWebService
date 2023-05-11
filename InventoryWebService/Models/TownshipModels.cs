using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class TownshipModels
    {
        public int TownshipID { get; set; }
        public string TownshipName { get; set; }
        public int DivisionID { get; set; }
    }
}