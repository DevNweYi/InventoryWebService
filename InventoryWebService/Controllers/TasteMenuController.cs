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
    public class TasteMenuController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTasteMenu()
        {
            TasteMenuModels model = new TasteMenuModels();
            IList<TasteMenuModels> list = new List<TasteMenuModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getTasteMenu, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TasteMenuModels();
                model.TasteID = Convert.ToInt32(reader["ID"]);
                model.TasteName = Convert.ToString(reader["TasteName"]);
                model.MainMenuID = Convert.ToInt32(reader["MainMenuID"]);
                model.Price = Convert.ToInt32(reader["Price"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}