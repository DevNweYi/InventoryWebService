using InventoryWebService.DBConnection;
using InventoryWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using InventoryWebService.General;
using System.Linq;

namespace InventoryWebService.Controllers
{
    [System.Web.Http.RoutePrefix("api/saleorder")]
    public class SaleOrderController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        AppSetting appSetting = new AppSetting();
        Procedure procedure = new Procedure();

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertSaleOrder([FromBody]MasterSaleOrderModels model)
        {
            string orderNumber = "";
            List<TranSaleOrderModels> list = model.lstSaleOrderTran;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ProductID", typeof(int)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dt.Columns.Add(new DataColumn("SalePrice", typeof(int)));
            dt.Columns.Add(new DataColumn("Amount", typeof(int)));
            for (int i = 0; i < list.Count; i++)
            {
                dt.Rows.Add(list[i].ProductID, list[i].Quantity, list[i].SalePrice, list[i].Amount);
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLInsertSaleOrder, conn);
            cmd.Parameters.AddWithValue("@DateTime", appSetting.getMMLocalDateTime());
            cmd.Parameters.AddWithValue("@ClientID", model.ClientID);
            cmd.Parameters.AddWithValue("@CustomerID", model.CustomerID);
            cmd.Parameters.AddWithValue("@Subtotal", model.Subtotal);
            cmd.Parameters.AddWithValue("@Tax", model.Tax);
            cmd.Parameters.AddWithValue("@TaxAmt", model.TaxAmt);
            cmd.Parameters.AddWithValue("@Charges", model.Charges);
            cmd.Parameters.AddWithValue("@ChargesAmt", model.ChargesAmt);
            cmd.Parameters.AddWithValue("@Total", model.Total);
            cmd.Parameters.AddWithValue("@Remark", model.Remark);
            cmd.Parameters.AddWithValue("@temptbl", dt);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) orderNumber = Convert.ToString(reader[0]);
            reader.Close();
            conn.Close();

            return Json(orderNumber);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleOrderByDate")]
        public IHttpActionResult GetMasterSaleOrderByDate(string date, int clientId, bool isOrderFinished)
        {
            DateTime dateOnly = appSetting.getDateOnly(date);

            MasterSaleOrderModels model = new MasterSaleOrderModels();
            IList<MasterSaleOrderModels> list = new List<MasterSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleOrderByDate, conn);
            cmd.Parameters.AddWithValue("@Date", dateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsOrderFinished", isOrderFinished);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleOrderModels();
                model.Year = Convert.ToString(reader["Year"]);
                model.Month = Convert.ToString(reader["Month"]);
                model.Day = Convert.ToString(reader["Day"]);
                model.SaleOrderID = Convert.ToInt32(reader["SaleOrderID"]);
                model.OrderNumber = Convert.ToString(reader["OrderNumber"]);
                model.Total = Convert.ToInt32(reader["Total"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.OrderDateTime = Convert.ToString(reader["OrderDateTime"]);
                model.Subtotal = Convert.ToInt32(reader["Subtotal"]);
                model.TaxAmt = Convert.ToInt32(reader["TaxAmt"]);
                model.ChargesAmt = Convert.ToInt32(reader["ChargesAmt"]);
                model.Remark = Convert.ToString(reader["Remark"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleOrderByFromToDate")]
        public IHttpActionResult GetMasterSaleOrderByFromToDate(string fromDate, string toDate, int clientId, bool isOrderFinished)
        {
            DateTime fromDateOnly = appSetting.getDateOnly(fromDate);
            DateTime toDateOnly = appSetting.getDateOnly(toDate);

            MasterSaleOrderModels model = new MasterSaleOrderModels();
            IList<MasterSaleOrderModels> list = new List<MasterSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleOrderByFromToDate, conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDateOnly);
            cmd.Parameters.AddWithValue("@ToDate", toDateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsOrderFinished", isOrderFinished);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleOrderModels();
                model.Year = Convert.ToString(reader["Year"]);
                model.Month = Convert.ToString(reader["Month"]);
                model.Day = Convert.ToString(reader["Day"]);
                model.SaleOrderID = Convert.ToInt32(reader["SaleOrderID"]);
                model.OrderNumber = Convert.ToString(reader["OrderNumber"]);
                model.Total = Convert.ToInt32(reader["Total"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.OrderDateTime = Convert.ToString(reader["OrderDateTime"]);
                model.Subtotal = Convert.ToInt32(reader["Subtotal"]);
                model.TaxAmt = Convert.ToInt32(reader["TaxAmt"]);
                model.ChargesAmt = Convert.ToInt32(reader["ChargesAmt"]);
                model.Remark = Convert.ToString(reader["Remark"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleOrderByValue")]
        public IHttpActionResult GetMasterSaleOrderByValue(string value, int clientId, bool isOrderFinished)
        {
            MasterSaleOrderModels model = new MasterSaleOrderModels();
            IList<MasterSaleOrderModels> list = new List<MasterSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleOrderByValue, conn);
            cmd.Parameters.AddWithValue("@Value", value);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsOrderFinished", isOrderFinished);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MasterSaleOrderModels();
                model.Year = Convert.ToString(reader["Year"]);
                model.Month = Convert.ToString(reader["Month"]);
                model.Day = Convert.ToString(reader["Day"]);
                model.SaleOrderID = Convert.ToInt32(reader["SaleOrderID"]);
                model.OrderNumber = Convert.ToString(reader["OrderNumber"]);
                model.Total = Convert.ToInt32(reader["Total"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.OrderDateTime = Convert.ToString(reader["OrderDateTime"]);
                model.Subtotal = Convert.ToInt32(reader["Subtotal"]);
                model.TaxAmt = Convert.ToInt32(reader["TaxAmt"]);
                model.ChargesAmt = Convert.ToInt32(reader["ChargesAmt"]);
                model.Remark = Convert.ToString(reader["Remark"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMasterSaleOrderBySaleOrderID")]
        public IHttpActionResult GetMasterSaleOrderBySaleOrderID(int saleOrderId)
        {
            MasterSaleOrderModels model = new MasterSaleOrderModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.getMasterSaleOrder(saleOrderId), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {                            
                model.OrderNumber = Convert.ToString(reader["OrderNumber"]);
                model.Total = Convert.ToInt32(reader["Total"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.OrderDateTime = Convert.ToString(reader["OrderDateTime"]);
                model.Subtotal = Convert.ToInt32(reader["Subtotal"]);
                model.TaxAmt = Convert.ToInt32(reader["TaxAmt"]);
                model.ChargesAmt = Convert.ToInt32(reader["ChargesAmt"]);
                model.Remark = Convert.ToString(reader["Remark"]);             
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetTranSaleOrderBySaleOrderID")]
        public IHttpActionResult GetTranSaleOrderBySaleOrderID(int saleOrderId)
        {
            int number = 0;
            TranSaleOrderModels model = new TranSaleOrderModels();
            IList<TranSaleOrderModels> list = new List<TranSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetTranSaleOrderBySaleOrderID, conn);
            cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleOrderModels();
                number += 1;
                model.Number = number;
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                model.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                model.Amount = Convert.ToInt32(reader["Amount"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleOrderItemByDate")]
        public IHttpActionResult GetSaleOrderItemByDate(string date, int clientId, int mainMenuId, int subMenuId)
        {
            DateTime dateOnly = appSetting.getDateOnly(date);

            TranSaleOrderModels model = new TranSaleOrderModels();
            IList<TranSaleOrderModels> list = new List<TranSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleOrderItemByDate, conn);
            cmd.Parameters.AddWithValue("@Date", dateOnly);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@MainMenuID", mainMenuId);
            cmd.Parameters.AddWithValue("@SubMenuID", subMenuId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleOrderModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleOrderItemByFromToDate")]
        public IHttpActionResult GetSaleOrderItemByFromToDate(string fromDate, string toDate, int clientId, int mainMenuId, int subMenuId)
        {
            DateTime fromDateOnly = appSetting.getDateOnly(fromDate);
            DateTime toDateOnly = appSetting.getDateOnly(toDate);

            TranSaleOrderModels model = new TranSaleOrderModels();
            IList<TranSaleOrderModels> list = new List<TranSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleOrderItemByFromToDate, conn);
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
                model = new TranSaleOrderModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSaleOrderItemByValue")]
        public IHttpActionResult GetSaleOrderItemByValue(string value, int clientId)
        {
            TranSaleOrderModels model = new TranSaleOrderModels();
            IList<TranSaleOrderModels> list = new List<TranSaleOrderModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSaleOrderItemByValue, conn);
            cmd.Parameters.AddWithValue("@Value", value);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TranSaleOrderModels();
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Quantity = Convert.ToInt32(reader["Quantity"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOrder")]
        public IHttpActionResult GetOrder(int clientId, bool isOrderFinished)
        {
            MasterSaleOrderModels masterModel = new MasterSaleOrderModels();
            TranSaleOrderModels tranModel = new TranSaleOrderModels();
            IList<MasterSaleOrderModels> list = new List<MasterSaleOrderModels>();
            List<MasterSaleOrderModels> lstMasterOrder = new List<MasterSaleOrderModels>();
            List<TranSaleOrderModels> lstTranOrder = new List<TranSaleOrderModels>();

            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetMasterSaleOrder, conn);        
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsOrderFinished", isOrderFinished);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                masterModel = new MasterSaleOrderModels();
                masterModel.SaleOrderID = Convert.ToInt32(reader["SaleOrderID"]);
                masterModel.Year = Convert.ToString(reader["Year"]);
                masterModel.Month = Convert.ToString(reader["Month"]);
                masterModel.Day = Convert.ToString(reader["Day"]);
                masterModel.OrderNumber = Convert.ToString(reader["OrderNumber"]);
                masterModel.Total = Convert.ToInt32(reader["Total"]);
                lstMasterOrder.Add(masterModel);
            }
            reader.Close();

            cmd = new SqlCommand(Procedure.PrcCLGetTranSaleOrder, conn);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsOrderFinished", isOrderFinished);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tranModel = new TranSaleOrderModels();
                tranModel.SaleOrderID = Convert.ToInt32(reader["SaleOrderID"]);
                tranModel.ProductID = Convert.ToInt32(reader["ProductID"]);
                tranModel.ProductName = Convert.ToString(reader["ProductName"]);
                tranModel.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                tranModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                tranModel.Amount = Convert.ToInt32(reader["Amount"]);
                lstTranOrder.Add(tranModel);
            }
            reader.Close();

            conn.Close();

            for(int i = 0; i < lstMasterOrder.Count; i++)
            {
                List<TranSaleOrderModels> lst= lstTranOrder.Where(x => x.SaleOrderID == lstMasterOrder[i].SaleOrderID).ToList();
                MasterSaleOrderModels mod = lstMasterOrder[i];
                mod.lstSaleOrderTran = lst;
                list.Add(mod);
            }

            return Json(list);
        }
    }
}