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
    public class TasteController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTaste()
        {
            TasteModels model = new TasteModels();
            IList<TasteModels> list = new List<TasteModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getTaste, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TasteModels();
                model.TasteID = Convert.ToInt32(reader["TasteID"]);
                model.TasteName = Convert.ToString(reader["TasteName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}