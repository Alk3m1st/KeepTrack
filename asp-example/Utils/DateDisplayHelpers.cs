using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Utils
{
    public static class DateDisplayHelpers
    {
        public static string GetNiceDateFormat(this DateTime date)
        {
            if (date.ToLocalTime().Date.Equals(DateTime.UtcNow.ToLocalTime().Date))
                return "Today";

            if (date.ToLocalTime().Date.AddDays(7) > DateTime.UtcNow.ToLocalTime().Date)
                return date.ToLocalTime().DayOfWeek.ToString();

            return date.ToLocalTime().Date.ToShortDateString();
        }

        // TODO: Fix this, not working properly...
        public static string GetElapsedDaysClass(this DateTime date)
        {
            return date.ToLocalTime().Date.AddDays(4) > DateTime.UtcNow.ToLocalTime().Date ? "three-days-elapsed" :
                date.ToLocalTime().Date.AddDays(3) > DateTime.UtcNow.ToLocalTime().Date ? "two-days-elapsed" :
                date.ToLocalTime().Date.AddDays(2) > DateTime.UtcNow.ToLocalTime().Date ? "one-day-elapsed" :
                string.Empty;
        }
    }
}