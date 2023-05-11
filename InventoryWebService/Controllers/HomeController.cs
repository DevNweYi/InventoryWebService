using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryWebService.General;

namespace InventoryWebService.Controllers
{
    public class HomeController : Controller
    {
        AppSetting appSetting = new AppSetting();
        public ActionResult Index()
        {
            //appSetting.getMMLocalDateTime();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
