using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebService.General
{
    public class AppSetting
    {
        public DateTime getDateOnly(string date)  // input dd/MM/yyyy
        {
            // change input to MM/dd/yyyy        
            string[] arr = date.Split('/');
            string day = arr[0];
            string month = arr[1];
            string year = arr[2];
            date = month + "/" + day + "/" + year;

            // parse string to datetime format
            DateTime dateTime = DateTime.Parse(date);
            DateTime dateOnly = dateTime.Date;

            return dateOnly;
        }

        public DateTime getMMLocalDate()
        {
            DateTime MyanmarStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Myanmar Standard Time");
            return MyanmarStd.Date;
        }

        public DateTime getMMLocalDateTime()
        {
            DateTime MyanmarStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Myanmar Standard Time");
            return MyanmarStd;
        }
    }
}