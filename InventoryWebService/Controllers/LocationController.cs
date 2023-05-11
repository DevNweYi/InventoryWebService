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
    public class LocationController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetLocation()
        {
            LocationModels model = new LocationModels();
            IList<LocationModels> list = new List<LocationModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getLocation, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new LocationModels();
                model.LocationID = Convert.ToInt32(reader["LocationID"]);
                model.ShortName = Convert.ToString(reader["ShortName"]);             
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}