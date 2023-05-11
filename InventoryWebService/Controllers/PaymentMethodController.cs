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
    public class PaymentMethodController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetPaymentMethod()
        {
            PaymentMethodModels model = new PaymentMethodModels();
            IList<PaymentMethodModels> list = new List<PaymentMethodModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getPaymentMethod, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new PaymentMethodModels();
                model.PayMethodID = Convert.ToInt32(reader["PayMethodID"]);
                model.PayMethodName = Convert.ToString(reader["PayMethodName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}