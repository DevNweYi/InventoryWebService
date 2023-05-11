using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class NotificationModels
    {
        public short NotiType { get; set; }
        public int NotiID { get; set; }
        public string NotiMessage { get; set; }
        public string NotiDateTime { get; set; }
    }
}