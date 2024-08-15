using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class TasteMenuModels
    {
        public int TasteID { get; set; }
        public int MainMenuID { get; set; }
        public string TasteName { get; set; }
        public int Price { get; set; }
    }
}