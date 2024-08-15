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
    public class TableTypeController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTableType()
        {
            TableTypeModels model = new TableTypeModels();
            IList<TableTypeModels> list = new List<TableTypeModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getTableType, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new TableTypeModels();
                model.TableTypeID = Convert.ToInt32(reader["TableTypeID"]);
                model.TableTypeName = Convert.ToString(reader["TableTypeName"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }
    }
}