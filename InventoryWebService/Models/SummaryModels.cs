using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class SummaryModels
    {
        public int TotalProduct { get; set; }
        public int TodaySale { get; set; }
        public int CurrentOrder { get; set; }
        public int TotalSale { get; set; }
        public int TotalOrder { get; set; }
        public int NotiCount { get; set; }
    }
}