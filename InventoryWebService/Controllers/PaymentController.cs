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
    public class PaymentController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetPayment()
        {
            PaymentModels model = new PaymentModels();
            IList<PaymentModels> list = new List<PaymentModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getPayment, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new PaymentModels();
                model.PaymentID = Convert.ToInt32(reader["PaymentID"]);
                model.Keyword = Convert.ToString(reader["Keyword"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}