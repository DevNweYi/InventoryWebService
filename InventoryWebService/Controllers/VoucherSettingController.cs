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
    public class VoucherSettingController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetVoucherSetting()
        {
            VoucherSettingModels model = new VoucherSettingModels();
            List<VoucherSettingModels> list = new List<VoucherSettingModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetVoucherSetting, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new VoucherSettingModels();
                model.LocationID = Convert.ToInt32(reader["LocationID"]);
                model.HeaderName = Convert.ToString(reader["HeaderName"]);
                model.HeaderDesp = Convert.ToString(reader["HeaderDesp"]);
                model.HeaderPhone = Convert.ToString(reader["HeaderPhone"]);
                model.HeaderAddress = Convert.ToString(reader["HeaderAddress"]);
                model.OtherHeader1 = Convert.ToString(reader["OtherHeader1"]);
                model.OtherHeader2 = Convert.ToString(reader["OtherHeader2"]);
                model.FooterMessage1 = Convert.ToString(reader["FooterMessage1"]);
                model.FooterMessage2 = Convert.ToString(reader["FooterMessage2"]);
                model.FooterMessage3 = Convert.ToString(reader["FooterMessage3"]);
                model.VoucherLogoUrl = Convert.ToString(reader["VoucherLogoUrl"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}