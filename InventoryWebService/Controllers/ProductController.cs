using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using InventoryWebService.Models;
using InventoryWebService.DBConnection;
using InventoryWebService.General;

namespace InventoryWebService.Controllers
{
    [System.Web.Http.RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString());
        Procedure procedure = new Procedure();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProduct")]
        public IHttpActionResult GetProduct()
        {
            ProductModels model = new ProductModels();
            IList<ProductModels> list = new List<ProductModels>();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Procedure.getProduct, conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model = new ProductModels();
                model.ProductID = Convert.ToInt32(reader["ProductID"]);
                model.Code = Convert.ToString(reader["Code"]);
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                model.SubMenuID = Convert.ToInt32(reader["SubMenuID"]);
                model.PhotoUrl = Convert.ToString(reader["PhotoUrl"]);
                model.Description = Convert.ToString(reader["Description"]);
                list.Add(model);
            }
            reader.Close();
            conn.Close();

            return Json(list);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetProduct")]
        public IHttpActionResult GetProduct(int productId)
        {
            ProductModels model = new ProductModels();
            conn.Open();
            SqlCommand cmd = new SqlCommand(procedure.getProductByID(productId), conn);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.SalePrice = Convert.ToInt32(reader["SalePrice"]);
                model.PhotoUrl = Convert.ToString(reader["PhotoUrl"]);
                model.Description = Convert.ToString(reader["Description"]);
            }
            reader.Close();
            conn.Close();

            return Json(model);
        }
    }
}