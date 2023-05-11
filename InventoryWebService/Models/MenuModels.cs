using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class MenuModels
    {
        public MenuModels()
        {
            this.subMenu = new List<MenuModels>();
        }
        public int id { get; set; }
        public string name { get; set; }
        public List<MenuModels> subMenu { get; set; }
    }
}