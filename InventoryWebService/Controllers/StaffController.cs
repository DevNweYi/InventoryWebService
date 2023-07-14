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
    public class StaffController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetStaff()
        {
            StaffModels model = new StaffModels();
            IList<StaffModels> list = new List<StaffModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getStaff, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new StaffModels();
                model.StaffID = Convert.ToInt32(reader["StaffID"]);
                model.StaffName = Convert.ToString(reader["StaffName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}