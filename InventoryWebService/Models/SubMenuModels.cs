using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class SubMenuModels
    {
        public int SubMenuID { get; set; }
        public int MainMenuID { get; set; }
        public string SubMenuName { get; set; }
        public string PhotoUrl { get; set; }
    }
}