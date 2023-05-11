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
    public class TownshipController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        Procedure procedure = new Procedure();

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTownship(int divisionId)
        {
            TownshipModels model = new TownshipModels();
            IList<TownshipModels> list = new List<TownshipModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.getTownshipByDivision(divisionId), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TownshipModels();
                model.TownshipID = Convert.ToInt32(reader["TownshipID"]);
                model.TownshipName = Convert.ToString(reader["TownshipName"]);
                
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}