using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class VoucherSettingModels
    {
        public int LocationID { get; set; }
        public string HeaderName { get; set; }
        public string HeaderDesp { get; set; }
        public string HeaderPhone { get; set; }
        public string HeaderAddress { get; set; }
        public string OtherHeader1 { get; set; }
        public string OtherHeader2 { get; set; }
        public string FooterMessage1 { get; set; }
        public string FooterMessage2 { get; set; }
        public string FooterMessage3 { get; set; }
        public byte[] VoucherLogo { get; set; }
        public string VoucherLogoUrl { get; set; }
    }
}