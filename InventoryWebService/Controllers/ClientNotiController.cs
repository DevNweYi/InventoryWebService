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
    [System.Web.Http.RoutePrefix("api/clientnoti")]
    public class ClientNotiController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        Procedure procedure = new Procedure();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetClientNotiCount")]
        public IHttpActionResult GetClientNotiCount(int clientId)
        {
            int totalCount = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.getNotiCountByClient(clientId), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) totalCount = Convert.ToInt32(reader["NotiCount"]);

            reader.Close();
            conn.Close();

            return Json(totalCount);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetClientNotification")]
        public IHttpActionResult GetClientNotification(int clientId, bool isForStatusBar)
        {
            List<NotificationModels> list = new List<NotificationModels>();
            NotificationModels model = new NotificationModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.PrcCLGetClientNotification, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientID", clientId);
            cmd.Parameters.AddWithValue("@IsForStatusBar", isForStatusBar);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new NotificationModels();
                model.NotiID = Convert.ToInt32(reader["NotiID"]);
                model.NotiType = Convert.ToInt16(reader["NotiType"]);
                model.NotiMessage = Convert.ToString(reader["NotiMessage"]);
                model.NotiDateTime = Convert.ToString(reader["NotiDateTime"]);
                list.Add(model);
            }

            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("DeleteClientNotification")]
        public IHttpActionResult DeleteClientNotification(int clientId, int notiId, int notiType)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.deleteClientNotification(clientId, notiId, notiType), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("DeleteAllClientNotification")]
        public IHttpActionResult DeleteAllClientNotification(int clientId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.deleteClientNotification(clientId), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("UpdateClientNotification")]
        public IHttpActionResult UpdateClientNotification(int clientId, short notiType, string notiIds, bool isStatusBarFinished)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.updateClientNotification(clientId, notiType, notiIds, isStatusBarFinished), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
            return Json("");
        }
    }
}