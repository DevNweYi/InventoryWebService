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
    public class TableController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTable()
        {
            TableModels model = new TableModels();
            IList<TableModels> list = new List<TableModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getTable, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TableModels();
                model.TableID = Convert.ToInt32(reader["TableID"]);
                model.TableTypeID = Convert.ToInt32(reader["TableTypeID"]);
                model.TableName = Convert.ToString(reader["TableName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}