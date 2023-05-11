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
    public class LimitedDayController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetLimitedDay()
        {
            LimitedDayModels model = new LimitedDayModels();
            IList<LimitedDayModels> list = new List<LimitedDayModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getLimitedDay, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new LimitedDayModels();
                model.LimitedDayID = Convert.ToInt32(reader["LimitedDayID"]);
                model.LimitedDayName = Convert.ToString(reader["LimitedDayName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}