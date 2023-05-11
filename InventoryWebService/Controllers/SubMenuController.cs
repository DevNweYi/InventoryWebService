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
    [System.Web.Http.RoutePrefix("api/submenu")]
    public class SubMenuController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetSubMenu")]
        public IHttpActionResult GetSubMenu()
        {
            SubMenuModels model = new SubMenuModels();
            IList<SubMenuModels> list = new List<SubMenuModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getSubMenu, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new SubMenuModels();
                model.SubMenuID = Convert.ToInt32(reader["SubMenuID"]);
                model.MainMenuID = Convert.ToInt32(reader["MainMenuID"]);
                model.SubMenuName = Convert.ToString(reader["SubMenuName"]);
                model.PhotoUrl = Convert.ToString(reader["PhotoUrl"]);

                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }       
    }
}