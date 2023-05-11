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
    public class CustomerController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCustomer()
        {
            CustomerModels model = new CustomerModels();
            IList<CustomerModels> list = new List<CustomerModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getCustomer, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new CustomerModels();
                model.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                model.CustomerName = Convert.ToString(reader["CustomerName"]);
                model.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult InsertCustomer([FromBody]CustomerModels model)
        {
            int id = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLInsertCustomer, conn);
            cmd.Parameters.AddWithValue("@CustomerName", model.CustomerName);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@DivisionID", model.DivisionID);
            cmd.Parameters.AddWithValue("@TownshipID", model.TownshipID);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@Contact", model.Contact);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            conn.Close();

            return Json(id);
        }
    }
}