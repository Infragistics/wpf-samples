
namespace System
{
    public static class DateTimeEx
    {
        public static int MonthsDifference(this DateTime lValue, DateTime rValue)
        {
            var delta = (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
            return System.Math.Abs(delta);
        }

        public static DateTime Epoch = new DateTime(1970, 1, 1);
               
        /// <summary>
        /// Converts a long value to date time 
        /// </summary>
        /// <param name="longTime">In in milliseconds since the epoch: 1970, 1, 1 </param>
        public static DateTime ToDateTime(this long longTime)
        {
            var epoch = new DateTime(1970, 1, 1);
            var date = epoch.AddMilliseconds(longTime);
            return date;
        }
        /// <summary>
        /// Converts a long value to local date time 
        /// </summary>
        /// <param name="longTime">In in milliseconds since the epoch: 1970, 1, 1 </param>
        public static DateTime ToLocalTime(this long longTime)
        {
            var date = longTime.ToDateTime().ToLocalTime();
            return date;
        }
    }

    public static class TimeSpanEx
    {
        public static TimeSpan FromYears(double years)
        {
            var yy = Math.Floor(years);
            var monthsFraction = (years - yy) * 12;
            var timespan = FromMonths(monthsFraction);

            var minDate = DateTime.Now;
            minDate = minDate.AddYears(-(int)yy);
            minDate = minDate.Subtract(timespan);
            
            //var daysFraction = (monthsFraction - months) * 30;
            //var dd = Math.Floor((years - yy) * 12 - mm);

            //var minDate = DateTime.Now.AddYears(-(int)yy);
            //minDate = minDate.AddMonths(-(int)mm);
            //minDate = minDate.AddDays(-(int)dd);

            return DateTime.Now.Subtract(minDate);
        }
        public static TimeSpan FromMonths(double months)
        {
            var mm = Math.Floor(months);
            var daysFraction = (months - mm) * 30;
            //var days = Math.Floor(daysFraction);
            //var hoursFraction = (daysFraction - days) * 24;
            //var hours = Math.Floor(hoursFraction);
            //var minutesFraction = (hoursFraction - hours) * 60;
            //var minutes = Math.Floor(minutesFraction);
            //var secondsFraction = (minutesFraction - minutes) * 60;
            //var seconds = Math.Floor(secondsFraction);

            var timespan = TimeSpan.FromDays(daysFraction);

            var minDate = DateTime.Now;
            minDate = minDate.AddMonths(-(int)mm);
            minDate = minDate.Subtract(timespan);
          
            return DateTime.Now.Subtract(minDate);
        }

        

    }
}