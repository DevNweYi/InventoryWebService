using InventoryWebService.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryWebService
{
    public class DataConnectorSQL
    {
        private string CONNECTION_STRING;
        private SqlConnection con = null;

        //public SqlConnection Connect()
        //{
        //    CONNECTION_STRING = System.Configuration.ConfigurationManager.AppSettings[AppConstant.ConnectionString].ToString();
        //    try
        //    {
        //        if (con == null)
        //            con = new SqlConnection(CONNECTION_STRING);
        //        if (con.State == ConnectionState.Closed)
        //            con.Open();
        //        return con;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public void Close()
        {
            con = null;
        }
    }
}