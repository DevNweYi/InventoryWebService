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
            
            MainMenuModels mainMenu;          
            IList<MainMenuModels> lstMainMenu = new List<MainMenuModels>();

            mainMenu = new MainMenuModels();
            mainMenu.MainMenuID = 1;
            mainMenu.MainMenuName = "MainMenu1";
            lstMainMenu.Add(mainMenu);

            mainMenu = new MainMenuModels();
            mainMenu.MainMenuID = 2;
            mainMenu.MainMenuName = "MainMenu2";
            lstMainMenu.Add(mainMenu);

            mainMenu = new MainMenuModels();
            mainMenu.MainMenuID = 3;
            mainMenu.MainMenuName = "MainMenu3";
            lstMainMenu.Add(mainMenu);

            SubMenuModels subMenu;
            IList<SubMenuModels> lstSubMenu = new List<SubMenuModels>();

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 1;
            subMenu.MainMenuID = 1;
            subMenu.SubMenuName = "SubMenu1";
            lstSubMenu.Add(subMenu);

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 2;
            subMenu.MainMenuID = 1;
            subMenu.SubMenuName = "SubMenu2";
            lstSubMenu.Add(subMenu);

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 3;
            subMenu.MainMenuID = 2;
            subMenu.SubMenuName = "SubMenu3";
            lstSubMenu.Add(subMenu);

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 4;
            subMenu.MainMenuID = 2;
            subMenu.SubMenuName = "SubMenu4";
            lstSubMenu.Add(subMenu);

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 5;
            subMenu.MainMenuID = 3;
            subMenu.SubMenuName = "SubMenu5";
            lstSubMenu.Add(subMenu);

            subMenu = new SubMenuModels();
            subMenu.SubMenuID = 6;
            subMenu.MainMenuID = 3;
            subMenu.SubMenuName = "SubMenu6";
            lstSubMenu.Add(subMenu);

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