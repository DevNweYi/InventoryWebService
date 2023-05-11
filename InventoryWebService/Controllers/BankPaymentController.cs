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
    public class BankPaymentController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetBankPayment()
        {
            BankPaymentModels model = new BankPaymentModels();
            IList<BankPaymentModels> list = new List<BankPaymentModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getBankPayment, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new BankPaymentModels();
                model.BankPaymentID = Convert.ToInt32(reader["BankPaymentID"]);
                model.BankPaymentName = Convert.ToString(reader["BankPaymentName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}