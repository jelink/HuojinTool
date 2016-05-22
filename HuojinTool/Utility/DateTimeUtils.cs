using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Microsoft.AppEx.Ingestion.Utilities
{
    public static class DateTimeUtils
    {
    
        public static DateTime convertToDateTime(String dateString, String dateformat)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(dateString, dateformat, null, DateTimeStyles.None, out dateTime))
                {
                    return dateTime.ToUniversalTime();
                }
            }
            throw new ArgumentException("dateString or dateformat is wrong");
        }
    }
}
