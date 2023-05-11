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
    [System.Web.Http.RoutePrefix("api/mainmenu")]
    public class MainMenuController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetMainMenu")]
        public IHttpActionResult GetMainMenu()
        {
            MainMenuModels model = new MainMenuModels();
            IList<MainMenuModels> list = new List<MainMenuModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getMainMenu, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new MainMenuModels();
                model.MainMenuID = Convert.ToInt32(reader["MainMenuID"]);
                model.MainMenuName = Convert.ToString(reader["MainMenuName"]);
                model.PhotoUrl = Convert.ToString(reader["PhotoUrl"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }      
    }
}