using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.Models
{
    public class MasterSaleOrderModels
    {
        public int SaleOrderID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDateTime { get; set; }
        public int CustomerID { get; set; }
        public int TaxAmt { get; set; }
        public int ChargesAmt { get; set; }
        public int Tax { get; set; }
        public int Charges { get; set; }
        public int Total { get; set; }
        public int Subtotal { get; set; }
        public int ClientID { get; set; }
        public string Remark { get; set; }
        public List<TranSaleOrderModels> lstSaleOrderTran { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string CustomerName { get; set; }
    }
}