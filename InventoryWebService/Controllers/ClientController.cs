using InventoryWebService.DBConnection;
using InventoryWebService.General;
using InventoryWebService.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace InventoryWebService.Controllers
{
    [System.Web.Http.RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        Procedure procedure = new Procedure();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("InsertClient")]
        public IHttpActionResult InsertClient([FromBody]ClientModels model)
        {
            int id = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLInsertClient, conn);
            cmd.Parameters.AddWithValue("@ClientName", model.ClientName);
            cmd.Parameters.AddWithValue("@ShopName", model.ShopName);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@DivisionID", model.DivisionID);
            cmd.Parameters.AddWithValue("@TownshipID", model.TownshipID);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Parameters.AddWithValue("@IsSalePerson", model.IsSalePerson);
            cmd.Parameters.AddWithValue("@Token", model.Token);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) id = Convert.ToInt32(reader["ID"]);
            reader.Close();
            conn.Close();

            return Json(id);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("UpdateClientPassword")]
        public IHttpActionResult UpdateClientPassword(int clientId, string password)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLUpdateClientPassword, conn);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@ClientPassword", password);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("UpdateClientToken")]
        public IHttpActionResult UpdateClientToken(string phone, string token)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLUpdateClientToken, conn);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("UpdateClient")]
        public IHttpActionResult UpdateClient(int clientId, [FromBody]ClientModels model)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLUpdateClient, conn);
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@ClientName", model.ClientName);
            cmd.Parameters.AddWithValue("@ShopName", model.ShopName);
            cmd.Parameters.AddWithValue("@DivisionID", model.DivisionID);
            cmd.Parameters.AddWithValue("@TownshipID", model.TownshipID);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }

        [System.Web.Http.HttpGet]        
        public IHttpActionResult CheckClient(string phone)
        {
            ClientModels model = new ClientModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLCheckClientByPhone, conn);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.ClientID = Convert.ToInt32(reader["ClientID"]);
                model.ClientName = Convert.ToString(reader["ClientName"]);
                model.ClientPassword = Convert.ToString(reader["ClientPassword"]);
                model.ShopName = Convert.ToString(reader["ShopName"]);
                model.DivisionID = Convert.ToInt32(reader["DivisionID"]);
                model.TownshipID = Convert.ToInt32(reader["TownshipID"]);
                model.Address = Convert.ToString(reader["Address"]);
                model.DivisionName = Convert.ToString(reader["DivisionName"]);
                model.TownshipName = Convert.ToString(reader["TownshipName"]);
                model.Phone = phone;
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }

        [System.Web.Http.HttpGet]        
        public IHttpActionResult CheckClient(string phone, bool isSalePerson)
        {
            ClientModels model = new ClientModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLCheckClientByPhone, conn);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@IsSalePerson", isSalePerson);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.ClientID = Convert.ToInt32(reader["ClientID"]);
                model.ClientName = Convert.ToString(reader["ClientName"]);
                model.ClientPassword = Convert.ToString(reader["ClientPassword"]);
                model.ShopName = Convert.ToString(reader["ShopName"]);
                model.DivisionID = Convert.ToInt32(reader["DivisionID"]);
                model.TownshipID = Convert.ToInt32(reader["TownshipID"]);
                model.Address = Convert.ToString(reader["Address"]);
                model.DivisionName = Convert.ToString(reader["DivisionName"]);
                model.TownshipName = Convert.ToString(reader["TownshipName"]);
                model.Phone = phone;
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAccessClientApp")]
        public IHttpActionResult GetAccessClientApp(int clientId)
        {
            ClientAccessSettingModels model = new ClientAccessSettingModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.getAccessClientApp(clientId), conn);          
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.IsEditAccessClientApp = Convert.ToInt16(reader["IsEditAccessClientApp"]);
                model.IsDeleteAccessClientApp = Convert.ToInt16(reader["IsDeleteAccessClientApp"]);
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }
    }
}