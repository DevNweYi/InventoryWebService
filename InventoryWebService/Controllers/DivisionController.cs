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
    public class DivisionController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetDivision()
        {
            DivisionModels model = new DivisionModels();
            IList<DivisionModels> list = new List<DivisionModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getDivision, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new DivisionModels();
                model.DivisionID = Convert.ToInt32(reader["DivisionID"]);
                model.DivisionName = Convert.ToString(reader["DivisionName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();           

            return Json(list);
        }
    }
}