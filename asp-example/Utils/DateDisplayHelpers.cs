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
            if (date.Date.Equals(DateTime.Now.Date))
                return "Today";

            if (date.Date.AddDays(7) > DateTime.Now.Date)
                return date.DayOfWeek.ToString();

            return date.Date.ToShortDateString();
        }

        public static string GetElapsedDaysClass(this DateTime date)
        {
            return date.Date.AddDays(4) > DateTime.Now.Date ? "three-days-elapsed" :
                date.Date.AddDays(3) > DateTime.Now.Date ? "two-days-elapsed" :
                date.Date.AddDays(2) > DateTime.Now.Date ? "one-day-elapsed" :
                string.Empty;
        }
    }
}