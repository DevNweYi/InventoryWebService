using InventoryWebService.DBConnection;
using InventoryWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using InventoryWebService.General;

namespace InventoryWebService.Controllers
{
    [System.Web.Http.RoutePrefix("api/sale")]
    public class SaleController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        AppSetting appSetting = new AppSetting();

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertSale([FromBody]MasterSaleModels model)
        {
            int slipId = 0;
            List<TranSaleModels> list = model.LstSaleTran;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ProductID", typeof(int)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dt.Columns.Add(new DataColumn("UnitID", typeof(int)));
            dt.Columns.Add(new DataColumn("SalePrice", typeof(int)));
            dt.Columns.Add(new DataColumn("CurrencyID", typeof(int)));
            dt.Columns.Add(new DataColumn("DiscountPercent", typeof(int)));
            dt.Columns.Add(new DataColumn("Discount", typeof(int)));
            dt.Columns.Add(new DataColumn("Amount", typeof(int)));
            dt.Columns.Add(new DataColumn("IsFOC", typeof(bool)));
            for (int i = 0; i < list.Count; i++)
            {
                dt.Rows.Add(list[i].ProductID, list[i].Quantity, 0, list[i].SalePrice, 0, list[i].DiscountPercent, list[i].Discount, list[i].Amount, list[i].IsFOC);
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLInsertSale, conn);
            cmd.Parameters.AddWithValue("@SaleDateTime", appSetting.getMMLocalDateTime());
            cmd.Parameters.AddWithValue("@CustomerID", model.CustomerID);
            cmd.Parameters.AddWithValue("@LocationID", model.LocationID);
            cmd.Parameters.AddWithValue("@PaymentID", model.PaymentID);
            cmd.Parameters.AddWithValue("@VoucherDiscount", model.VoucherDiscount);
            cmd.Parameters.AddWithValue("@AdvancedPay", model.AdvancedPay);
            cmd.Parameters.AddWithValue("@Tax", model.Tax);
            cmd.Parameters.AddWithValue("@TaxAmt", model.TaxAmt);
            cmd.Parameters.AddWithValue("@Charges", model.Charges);
            cmd.Parameters.AddWithValue("@ChargesAmt", model.ChargesAmt);
            cmd.Parameters.AddWithValue("@Subtotal", model.Subtotal);
            cmd.Parameters.AddWithValue("@Total", model.Total);
            cmd.Parameters.AddWithValue("@Grandtotal", model.Grandtotal);
            cmd.Parameters.AddWithValue("@VouDisPercent", model.VouDisPercent);
            cmd.Parameters.AddWithValue("@VouDisAmount", model.VouDisAmount);
            cmd.Parameters.AddWithValue("@PayMethodID", model.PayMethodID);
            cmd.Parameters.AddWithValue("@BankPaymentID", model.BankPaymentID);
            cmd.Parameters.AddWithValue("@PaymentPercent", model.PaymentPercent);
            cmd.Parameters.AddWithValue("@IsClientSale", model.IsClientSale);
            cmd.Parameters.AddWithValue("@ClientID", model.ClientID);
            cmd.Parameters.AddWithValue("@LimitedDayID", model.LimitedDayID);
            cmd.Parameters.AddWithValue("@PayPercentAmt", model.PayPercentAmt);
            cmd.Parameters.AddWithValue("@Remark", model.Remark);
            cmd.Parameters.AddWithValue("@ModuleCode", 1);
            cmd.Parameters.AddWithValue("@temptbl", dt);
            cmd.Parameters.AddWithValue("@AccountCode", 210);
            cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) slipId = Convert.ToInt32(reader[0]);
            reader.Close();
            conn.Close();

            return Json(slipId);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleByDate")]
        public IHttpActionResult GetMasterSaleByDate(string date,int clientId)
        {
            DateTime dateOnly = appSetting.getDateOnly(date);

            MasterSaleModels model = new MasterSaleModels();
            IList<MasterSaleModels> list = new List<MasterSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleByDate, conn);
            cmd.Parameters.AddWithValue("@Date", dateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleModels();
                model.SaleDateTime = date;
                model.UserVoucherNo = Convert.ToString(reader["UserVoucherNo"]);
                model.Grandtotal = Convert.ToInt32(reader["Grandtotal"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.SaleID= Convert.ToInt32(reader["SaleID"]);
                model.SlipID = Convert.ToInt32(reader["SlipID"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleByFromToDate")]
        public IHttpActionResult GetMasterSaleByFromToDate(string fromDate,string toDate, int clientId)
        {
            DateTime fromDateOnly = appSetting.getDateOnly(fromDate);
            DateTime toDateOnly = appSetting.getDateOnly(toDate);

            MasterSaleModels model = new MasterSaleModels();
            IList<MasterSaleModels> list = new List<MasterSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleByFromToDate, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDateOnly);
            cmd.Parameters.AddWithValue("@ToDate", toDateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleModels();
                model.SaleDateTime = Convert.ToString(reader["Date"]);
                model.UserVoucherNo = Convert.ToString(reader["UserVoucherNo"]);
                model.Grandtotal = Convert.ToInt32(reader["Grandtotal"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.SaleID = Convert.ToInt32(reader["SaleID"]);
                model.SlipID = Convert.ToInt32(reader["SlipID"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleByValue")]
        public IHttpActionResult GetMasterSaleByValue(string value, int clientId)
        {
            MasterSaleModels model = new MasterSaleModels();
            IList<MasterSaleModels> list = new List<MasterSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleByValue, conn);
            cmd.Parameters.AddWithValue("@Value", value);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleModels();
                model.SaleDateTime = Convert.ToString(reader["Date"]);
                model.UserVoucherNo = Convert.ToString(reader["UserVoucherNo"]);
                model.Grandtotal = Convert.ToInt32(reader["Grandtotal"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.SaleID = Convert.ToInt32(reader["SaleID"]);
                model.SlipID = Convert.ToInt32(reader["SlipID"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleItemByDate")]
        public IHttpActionResult GetSaleItemByDate(string date, int clientId,int mainMenuId,int subMenuId)
        {
            DateTime dateOnly = appSetting.getDateOnly(date);

            TranSaleModels model = new TranSaleModels();
            IList<TranSaleModels> list = new List<TranSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleItemByDate, conn);
            cmd.Parameters.AddWithValue("@Date", dateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@MainMenuID", mainMenuId);
            cmd.Parameters.AddWithValue("@SubMenuID", subMenuId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                model.Amount = Convert.ToInt32(reader["Amount"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleItemByFromToDate")]
        public IHttpActionResult GetSaleItemByFromToDate(string fromDate, string toDate, int clientId, int mainMenuId, int subMenuId)
        {
            DateTime fromDateOnly = appSetting.getDateOnly(fromDate);
            DateTime toDateOnly = appSetting.getDateOnly(toDate);

            TranSaleModels model = new TranSaleModels();
            IList<TranSaleModels> list = new List<TranSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleItemByFromToDate, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDateOnly);
            cmd.Parameters.AddWithValue("@ToDate", toDateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@MainMenuID", mainMenuId);
            cmd.Parameters.AddWithValue("@SubMenuID", subMenuId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                model.Amount = Convert.ToInt32(reader["Amount"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleItemByValue")]
        public IHttpActionResult GetSaleItemByValue(string value, int clientId)
        {
            TranSaleModels model = new TranSaleModels();
            IList<TranSaleModels> list = new List<TranSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleItemByValue, conn);
            cmd.Parameters.AddWithValue("@Value", value);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                model.Amount = Convert.ToInt32(reader["Amount"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetTranSaleBySaleID")]
        public IHttpActionResult GetTranSaleBySaleID(int saleId)
        {
            TranSaleModels model = new TranSaleModels();
            IList<TranSaleModels> list = new List<TranSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetTranSaleBySaleID, conn);
            cmd.Parameters.AddWithValue("@SaleID", saleId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleModels();
                model.ProductID = Convert.ToInt32(reader["ProductID"]);
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                model.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                model.Amount = Convert.ToInt32(reader["Amount"]);
                model.Discount = Convert.ToInt32(reader["Discount"]);
                model.DiscountPercent = Convert.ToInt32(reader["DiscountPercent"]);
                model.IsFOC = Convert.ToBoolean(reader["IsFOC"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleBySaleID")]
        public IHttpActionResult GetMasterSaleBySaleID(int saleId)
        {
            MasterSaleModels model = new MasterSaleModels();
            IList<MasterSaleModels> list = new List<MasterSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleBySaleID, conn);
            cmd.Parameters.AddWithValue("@SaleID", saleId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                model.LocationID = Convert.ToInt32(reader["LocationID"]);
                model.VouDisPercent = Convert.ToInt32(reader["VouDisPercent"]);
                model.VouDisAmount = Convert.ToInt32(reader["VouDisAmount"]);
                model.PaymentID = Convert.ToInt32(reader["PaymentID"]);
                model.PayMethodID = Convert.ToInt32(reader["PayMethodID"]);
                model.Remark = Convert.ToString(reader["Remark"]);
                model.LimitedDayID = Convert.ToInt32(reader["LimitedDayID"]);
                model.AdvancedPay = Convert.ToInt32(reader["AdvancedPay"]);
                model.BankPaymentID = Convert.ToInt32(reader["BankPaymentID"]);
                model.PaymentPercent = Convert.ToInt32(reader["PaymentPercent"]);
                model.SlipID = Convert.ToInt32(reader["SlipID"]);
                model.SaleDateTime = Convert.ToString(reader["Date"]);
                model.VoucherDiscount = Convert.ToInt32(reader["VoucherDiscount"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateSale([FromBody] MasterSaleModels model)
        {
            List<TranSaleModels> list = model.LstSaleTran;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ProductID", typeof(int)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dt.Columns.Add(new DataColumn("UnitID", typeof(int)));
            dt.Columns.Add(new DataColumn("SalePrice", typeof(int)));
            dt.Columns.Add(new DataColumn("CurrencyID", typeof(int)));
            dt.Columns.Add(new DataColumn("DiscountPercent", typeof(int)));
            dt.Columns.Add(new DataColumn("Discount", typeof(int)));
            dt.Columns.Add(new DataColumn("Amount", typeof(int)));
            dt.Columns.Add(new DataColumn("IsFOC", typeof(bool)));
            for (int i = 0; i < list.Count; i++)
            {
                dt.Rows.Add(list[i].ProductID, list[i].Quantity, 0, list[i].SalePrice, 0, list[i].DiscountPercent, list[i].Discount, list[i].Amount, list[i].IsFOC);
            }

            List<TranSaleModels> lstLog = model.LstSaleTranLog;
            DataTable dtLog = new DataTable();
            dtLog.Columns.Add(new DataColumn("ProductID", typeof(int)));
            dtLog.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dtLog.Columns.Add(new DataColumn("UnitID", typeof(int)));
            dtLog.Columns.Add(new DataColumn("SalePrice", typeof(int)));
            dtLog.Columns.Add(new DataColumn("CurrencyID", typeof(int)));
            dtLog.Columns.Add(new DataColumn("DiscountPercent", typeof(int)));
            dtLog.Columns.Add(new DataColumn("Discount", typeof(int)));
            dtLog.Columns.Add(new DataColumn("Amount", typeof(int)));
            dtLog.Columns.Add(new DataColumn("IsFOC", typeof(bool)));
            dtLog.Columns.Add(new DataColumn("ActionCode", typeof(string)));
            dtLog.Columns.Add(new DataColumn("ActionName", typeof(string)));
            dtLog.Columns.Add(new DataColumn("OrginalQuantity", typeof(int)));
            for (int i = 0; i < lstLog.Count; i++)
            {
                dtLog.Rows.Add(lstLog[i].ProductID, lstLog[i].Quantity, 0, lstLog[i].SalePrice, 0, lstLog[i].DiscountPercent, lstLog[i].Discount, lstLog[i].Amount, lstLog[i].IsFOC, lstLog[i].ActionCode, lstLog[i].ActionName, lstLog[i].OrginalQuantity);
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLUpdateSale, conn);
            cmd.Parameters.AddWithValue("@SaleID", model.SaleID);
            cmd.Parameters.AddWithValue("@CustomerID", model.CustomerID);
            cmd.Parameters.AddWithValue("@LocationID", model.LocationID);
            cmd.Parameters.AddWithValue("@PaymentID", model.PaymentID);
            cmd.Parameters.AddWithValue("@VoucherDiscount", model.VoucherDiscount);
            cmd.Parameters.AddWithValue("@AdvancedPay", model.AdvancedPay);
            cmd.Parameters.AddWithValue("@Tax", model.Tax);
            cmd.Parameters.AddWithValue("@TaxAmt", model.TaxAmt);
            cmd.Parameters.AddWithValue("@Charges", model.Charges);
            cmd.Parameters.AddWithValue("@ChargesAmt", model.ChargesAmt);
            cmd.Parameters.AddWithValue("@Subtotal", model.Subtotal);
            cmd.Parameters.AddWithValue("@Total", model.Total);
            cmd.Parameters.AddWithValue("@Grandtotal", model.Grandtotal);
            cmd.Parameters.AddWithValue("@VouDisPercent", model.VouDisPercent);
            cmd.Parameters.AddWithValue("@VouDisAmount", model.VouDisAmount);
            cmd.Parameters.AddWithValue("@PayMethodID", model.PayMethodID);
            cmd.Parameters.AddWithValue("@BankPaymentID", model.BankPaymentID);
            cmd.Parameters.AddWithValue("@PaymentPercent", model.PaymentPercent);
            cmd.Parameters.AddWithValue("@IsClientSale", model.IsClientSale);
            cmd.Parameters.AddWithValue("@ClientID", model.ClientID);
            cmd.Parameters.AddWithValue("@LimitedDayID", model.LimitedDayID);
            cmd.Parameters.AddWithValue("@PayPercentAmt", model.PayPercentAmt);
            cmd.Parameters.AddWithValue("@Remark", model.Remark);
            cmd.Parameters.AddWithValue("@temptbl", dt);
            cmd.Parameters.AddWithValue("@AccountCode", 210);
            cmd.Parameters.AddWithValue("@StaffID", model.StaffID);
            cmd.Parameters.AddWithValue("@temptbllog", dtLog);
            cmd.Parameters.AddWithValue("@UpdatedDateTime", appSetting.getMMLocalDateTime());
            cmd.Parameters.AddWithValue("@ActionCode", AppConstant.EditActionCode);
            cmd.Parameters.AddWithValue("@ActionName", AppConstant.EditActionName);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();         
            conn.Close();

            return Json("");
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteSale(int saleId)
        {
            bool isSuccess = false;
          
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLDeleteSale, conn);
            cmd.Parameters.AddWithValue("@SaleID", saleId);
            cmd.Parameters.AddWithValue("@SaleAccountCode", 210);
            cmd.Parameters.AddWithValue("@ARAccountCode", 310);
            cmd.Parameters.AddWithValue("@UpdatedDateTime", appSetting.getMMLocalDateTime());            
            cmd.Parameters.AddWithValue("@ActionCode", AppConstant.DeleteActionCode);
            cmd.Parameters.AddWithValue("@ActionName", AppConstant.DeleteActionName);            
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) isSuccess = Convert.ToBoolean(reader[0]);
            reader.Close();
            conn.Close();

            return Json(isSuccess);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleBySaleID")]
        public IHttpActionResult GetSaleBySaleID(int saleId)
        {
            MasterSaleModels model = new MasterSaleModels();
            IList<MasterSaleModels> list = new List<MasterSaleModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleBySaleID, conn);
            cmd.Parameters.AddWithValue("@SaleID", saleId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.LocationID = Convert.ToInt32(reader["LocationID"]);
                model.SlipID = Convert.ToInt32(reader["SlipID"]);
                model.SaleDateTime = Convert.ToString(reader["Date"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.ClientName = Convert.ToString(reader["ClientName"]);
                model.Tax = Convert.ToInt32(reader["Tax"]);
                model.TaxAmt = Convert.ToInt32(reader["TaxAmt"]);
                model.Charges = Convert.ToInt32(reader["Charges"]);
                model.ChargesAmt = Convert.ToInt32(reader["ChargesAmt"]);
                model.Subtotal = Convert.ToInt32(reader["Subtotal"]);
                model.VouDisPercent = Convert.ToInt32(reader["VouDisPercent"]);
                model.VoucherDiscount = Convert.ToInt32(reader["VoucherDiscount"]);
                model.AdvancedPay = Convert.ToInt32(reader["AdvancedPay"]);
                model.PaymentPercent = Convert.ToInt32(reader["PaymentPercent"]);
                model.PayPercentAmt = Convert.ToInt32(reader["PayPercentAmt"]);
                model.Grandtotal = Convert.ToInt32(reader["Grandtotal"]);
                model.BankPaymentID = Convert.ToInt32(reader["BankPaymentID"]);
            }
            reader.Close();

            int number = 1;
            TranSaleModels tranSaleModel = new TranSaleModels();
            cmd = new SqlCommand(Procedure.PrcCLGetTranSaleBySaleID, conn);
            cmd.Parameters.AddWithValue("@SaleID", saleId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tranSaleModel = new TranSaleModels();
                tranSaleModel.Number = number++;
                tranSaleModel.ProductName = Convert.ToString(reader["ProductName"]);
                tranSaleModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                tranSaleModel.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                tranSaleModel.Amount = Convert.ToInt32(reader["Amount"]);
                tranSaleModel.Discount = Convert.ToInt32(reader["Discount"]);
                model.LstSaleTran.Add(tranSaleModel);
            }
            reader.Close();

            list.Add(model);

            conn.Close();

            return Json(list);
        }
    }
}