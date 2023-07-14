using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class CompanySettingModels
    {
        public int Tax { get; set; }
        public int ServiceCharges { get; set; }
        public string HomeCurrency { get; set; }
        public int IsClientPhoneVerify { get; set; }
        public string ShopTypeCode { get; set; }
    }
}