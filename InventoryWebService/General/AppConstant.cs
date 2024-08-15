using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.General
{
    public class AppConstant
    {
        public const string ConnectionString = "ConnectionString";
        public const short NotiNewProduct = 1;
        public const short NotiUpdateOrder = 2;

        //Action Codes and Names
        public const string NewActionCode = "1";  //SysAction table value
        public const string EditActionCode = "2";
        public const string DeleteActionCode = "3";
        public const string NewActionName = "New";
        public const string EditActionName = "Edit";
        public const string DeleteActionName = "Delete";
    }
}