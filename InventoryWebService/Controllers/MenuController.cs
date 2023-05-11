using InventoryWebService.DBConnection;
using InventoryWebService.General;
using InventoryWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InventoryWebService.Controllers
{
    public class MenuController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetMenu()
        {
            MenuModels menu = new MenuModels();
            IList<MenuModels> lstMenu = new List<MenuModels>();

            MainMenuModels mainMenu = new MainMenuModels();
            IList<MainMenuModels> lstMainMenu = new List<MainMenuModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getMainMenu, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                mainMenu = new MainMenuModels();
                mainMenu.MainMenuID = Convert.ToInt32(reader["MainMenuID"]);
                mainMenu.MainMenuName = Convert.ToString(reader["MainMenuName"]);
                lstMainMenu.Add(mainMenu);
            }
            reader.Close();

            SubMenuModels subMenu = new SubMenuModels();
            IList<SubMenuModels> lstSubMenu = new List<SubMenuModels>();
            cmd = new SqlCommand(Procedure.getSubMenu, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                subMenu = new SubMenuModels();
                subMenu.SubMenuID = Convert.ToInt32(reader["SubMenuID"]);
                subMenu.MainMenuID = Convert.ToInt32(reader["MainMenuID"]);
                subMenu.SubMenuName = Convert.ToString(reader["SubMenuName"]);
                lstSubMenu.Add(subMenu);
            }
            reader.Close();

            for (int i = 0; i < lstMainMenu.Count(); i++)
            {
                menu = new MenuModels();
                menu.id = lstMainMenu[i].MainMenuID;
                menu.name = lstMainMenu[i].MainMenuName;
                List<SubMenuModels> list = lstSubMenu.Where(x => x.MainMenuID == lstMainMenu[i].MainMenuID).ToList();
                for (int k = 0; k < list.Count(); k++)
                {
                    MenuModels item = new MenuModels();
                    item.id = list[k].SubMenuID;
                    item.name = list[k].SubMenuName;
                    menu.subMenu.Add(item);
                }
                lstMenu.Add(menu);
            }

            conn.Close();

            return Json(lstMenu);
        }
    }
}