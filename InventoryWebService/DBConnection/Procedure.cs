using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryWebService.General;

namespace InventoryWebService.DBConnection
{
    public class Procedure
    {
        public const string getDivision = "Select DivisionID,DivisionName"
            + " From SDivision Order By Code";

        public string getTownshipByDivision(int divisionId)
        {
            return "Select TownshipID,TownshipName"
            + " From STownship Where DivisionID=" + divisionId + " Order By Code";
        }

        public const string getMainMenu = "Select MainMenuID,MainMenuName,PhotoUrl"
            + " From SMainMenu Order By SortCode";

        public const string getSubMenu = "Select SubMenuID,MainMenuID,SubMenuName,PhotoUrl"
           + " From SSubMenu Order By SortCode";

        public const string getProduct = "Select ProductID,Code,ProductName,SalePrice,SubMenuID,PhotoUrl,Description"
          + " From SProduct Order By SortCode";

        public const string getCustomer = "Select CustomerID,CustomerName,isnull(IsDefault,0) AS IsDefault"
            + " From SCustomer";

        public const string getLocation = "Select LocationID,ShortName"
            + " From SLocation";

        public const string getPayment = "Select PaymentID,Keyword"
           + " From SysPayment";

        public const string getPaymentMethod = "Select PayMethodID,PayMethodName"
           + " From SysPayMethod";

        public const string getBankPayment = "Select BankPaymentID,BankPaymentName"
          + " From SBankPayment";

        public const string getLimitedDay = "Select LimitedDayID,LimitedDayName"
         + " From SysLimitedDay";

        public const string getStaff = "Select StaffID,StaffName"
            + " From SStaff";

        public const string getTableType = "Select TableTypeID,TableTypeName"
            + " From STableType";

        public const string getTable = "Select TableID,TableTypeID,TableName"
           + " From STable";

        public const string getTaste = "Select TasteID,TasteName"
           + " From STaste";

        public const string getTasteMenu = "Select ID,TasteName,isnull(Price,0) AS Price,MainMenuID"
          + " From STasteMenu";

        public string getNotiCountByClient(int clientId)
        {
            return "Select Count(ID) AS NotiCount"
            + " From SClientNoti Where ClientID=" + clientId;
        }

        public const string PrcCLInsertClient = "PrcCLInsertClient";
        public const string PrcCLUpdateClientPassword = "PrcCLUpdateClientPassword";
        public const string PrcCLUpdateClientToken = "PrcCLUpdateClientToken";
        public const string PrcCLUpdateClient = "PrcCLUpdateClient";
        public const string PrcCLCheckClientByPhone = "PrcCLCheckClientByPhone";
        public const string PrcCLInsertCustomer = "PrcCLInsertCustomer";
        public const string PrcCLInsertSale = "PrcCLInsertSale";
        public const string PrcCLGetVoucherSetting = "PrcCLGetVoucherSetting";
        public const string PrcCLGetMasterSaleByDate = "PrcCLGetMasterSaleByDate";
        public const string PrcCLGetMasterSaleByFromToDate = "PrcCLGetMasterSaleByFromToDate";
        public const string PrcCLGetMasterSaleByValue = "PrcCLGetMasterSaleByValue";
        public const string PrcCLGetSaleItemByDate = "PrcCLGetSaleItemByDate";
        public const string PrcCLGetSaleItemByFromToDate = "PrcCLGetSaleItemByFromToDate";
        public const string PrcCLGetSaleItemByValue = "PrcCLGetSaleItemByValue";
        public const string PrcCLInsertSaleOrder = "PrcCLInsertSaleOrder";
        public const string PrcCLGetMasterSaleOrderByDate = "PrcCLGetMasterSaleOrderByDate";
        public const string PrcCLGetMasterSaleOrderByFromToDate = "PrcCLGetMasterSaleOrderByFromToDate";
        public const string PrcCLGetMasterSaleOrderByValue = "PrcCLGetMasterSaleOrderByValue";
        public const string PrcCLGetTranSaleOrderBySaleOrderID = "PrcCLGetTranSaleOrderBySaleOrderID";
        public const string PrcCLGetCompanySetting = "PrcCLGetCompanySetting";
        public const string PrcCLGetSaleOrderItemByDate = "PrcCLGetSaleOrderItemByDate";
        public const string PrcCLGetSaleOrderItemByFromToDate = "PrcCLGetSaleOrderItemByFromToDate";
        public const string PrcCLGetSaleOrderItemByValue = "PrcCLGetSaleOrderItemByValue";
        public const string PrcCLGetClientNotification = "PrcCLGetClientNotification";
        public const string PrcCLGetSummaryData = "PrcCLGetSummaryData";
        public const string PrcCLGetMasterSaleOrder = "PrcCLGetMasterSaleOrder";
        public const string PrcCLGetTranSaleOrder = "PrcCLGetTranSaleOrder";
        public const string PrcCLGetTranSaleBySaleID = "PrcCLGetTranSaleBySaleID";
        public const string PrcCLGetMasterSaleBySaleID = "PrcCLGetMasterSaleBySaleID";
        public const string PrcCLUpdateSale = "PrcCLUpdateSale";
        public const string PrcCLDeleteSale = "PrcCLDeleteSale";

        public string getMasterSaleOrder(int saleOrderId)
        {
            return "SELECT convert(varchar(10),OrderDateTime, 103) + right(convert(varchar(32),OrderDateTime,100),8) AS OrderDateTime,OrderNumber,Total,CustomerName,Subtotal,TaxAmt,ChargesAmt,Remark"
                    + " FROM TCLMasterSaleOrder ms LEFT JOIN SCustomer c ON ms.CustomerID = c.CustomerID"
                    + " WHERE SaleOrderID = " + saleOrderId;
        }

        public string deleteClientNotification(int clientId, int notiId, int notiType)
        {
            string query = "";
            if (notiType == AppConstant.NotiNewProduct)            
                query = "DELETE FROM SClientNoti WHERE ClientID=" + clientId + " AND NewProductID=" + notiId;           
            else if (notiType == AppConstant.NotiUpdateOrder)            
                query = "DELETE FROM SClientNoti WHERE ClientID=" + clientId + " AND UpdateSaleOrderID=" + notiId;            
            return query;
        }

        public string deleteClientNotification(int clientId)
        {
            return "DELETE FROM SClientNoti WHERE ClientID=" + clientId;
        }

        public string getProductByID(int productId)
        {
            return "Select ProductName, SalePrice, PhotoUrl, Description"
                + " From SProduct Where ProductID=" + productId;
        }

        public string updateClientNotification(int clientId, short notiType, string notiIds, bool isStatusBarFinished)
        {
            short status = 0;
            string query = "";
            if (isStatusBarFinished) status = 1;
            if (notiType == AppConstant.NotiNewProduct)
                query = "UPDATE SClientNoti SET IsStatusBarFinished=" + status + " WHERE ClientID=" + clientId + " AND NewProductID IN (" + notiIds + ")";
            else if (notiType == AppConstant.NotiUpdateOrder)
                query = "UPDATE SClientNoti SET IsStatusBarFinished=" + status + " WHERE ClientID=" + clientId + " AND UpdateSaleOrderID IN (" + notiIds + ")";
            return query;
        }

        public string getAccessClientApp(int clientId)
        {
            return "Select isnull(IsEditAccessClientApp,0) AS IsEditAccessClientApp, isnull(IsDeleteAccessClientApp,0) AS IsDeleteAccessClientApp"
                + " From SClient Where ClientID=" + clientId;
        }
    }
}