using InventoryWebService.DBConnection;
using InventoryWebService.General;
using InventoryWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace InventoryWebService.Controllers
{
    public class SummaryController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        Procedure procedure = new Procedure();
        AppSetting appSetting = new AppSetting();

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSummaryData(int clientId, string date)
        {
            DateTime dateOnly = appSetting.getDateOnly(date);

            SummaryModels model = new SummaryModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetSummaryData, conn);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@TodayDate", dateOnly);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.TotalProduct = Convert.ToInt32(reader["TotalProduct"]);
                model.TodaySale = Convert.ToInt32(reader["TodaySale"]);
                model.CurrentOrder = Convert.ToInt32(reader["CurrentOrder"]);
                model.TotalSale = Convert.ToInt32(reader["TotalSale"]);
                model.TotalOrder = Convert.ToInt32(reader["TotalOrder"]);
                model.NotiCount = Convert.ToInt32(reader["NotiCount"]);
            }

            reader.Close();
            conn.Close();

            return Json(model);
        }
    }
}