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
    [System.Web.Http.RoutePrefix("api/companysetting")]
    public class CompanySettingController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetCompanySetting")]
        public IHttpActionResult GetCompanySetting()
        {
            CompanySettingModels model = new CompanySettingModels();
            conn.Open();    
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetCompanySetting, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model = new CompanySettingModels();
                model.Tax = Convert.ToInt32(reader["Tax"]);
                model.ServiceCharges = Convert.ToInt32(reader["ServiceCharges"]);
                model.HomeCurrency = Convert.ToString(reader["HomeCurrency"]);
                model.IsClientPhoneVerify = Convert.ToInt32(reader["IsClientPhoneVerify"]);
                model.ShopTypeCode = Convert.ToString(reader["ShopTypeCode"]);
                model.AccessPasswordClientApp = Convert.ToString(reader["AccessPasswordClientApp"]);
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }
    }
}