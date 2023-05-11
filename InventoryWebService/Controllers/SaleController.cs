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
                dt.Rows.Add(list[i].ProductID, list[i].Quantity, 0, list[i].SalePrice, 0, 0, 0, list[i].Amount, list[i].IsFOC);
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
    }
}