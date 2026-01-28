
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class DateTimeEx
    {
        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsWorkday(this DateTime date)
        {
            return !date.IsWeekend();
        }

        /// <summary>
        /// Returns a closest date of DayOfWeek that is before or on specified date
        /// </summary>  
        public static DateTime BeforeOr(this DayOfWeek targetDay, DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            while (dayOfWeek != targetDay)
            {
                date = date.AddDays(-1);
                dayOfWeek = date.DayOfWeek;
            }
            return date;
        }
        /// <summary>
        /// Returns a closest date of DayOfWeek that is after or on specified date
        /// </summary>  
        public static DateTime AfterOr(this DayOfWeek targetDay, DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;
            while (dayOfWeek != targetDay)
            {
                date = date.AddDays(1);
                dayOfWeek = date.DayOfWeek;
            }
            return date;
        }

        /// <summary>
        /// Returns a work date (M through F) for the current holiday
        /// </summary>  
        public static DateTime GetWorkday(this DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }
        
        public static DateTime SetTime(this DateTime current, TimeSpan time)
        {
            var date = new DateTime(current.Year, current.Month, current.Day,
                                    time.Hours, time.Minutes, time.Seconds);
            return date;
        }
        public static DateTime SetTime(this DateTime current, int hours, int minutes, int seconds)
        {
            var date = new DateTime(current.Year, current.Month, current.Day,
                                    hours, minutes, seconds);
            return date;
        }

        public static DateTime SetDate(this DateTime current, int year, int month, int day)
        {
            var date = new DateTime(year, month, day,
                                    current.TimeOfDay.Hours, current.TimeOfDay.Minutes, current.TimeOfDay.Seconds);
            return date;
        }

        public static DateTime SetDate(this DateTime current, DateTime newDate)
        {
            var date = new DateTime(newDate.Year, newDate.Month, newDate.Day,
                                    current.TimeOfDay.Hours, current.TimeOfDay.Minutes, current.TimeOfDay.Seconds);
            return date;
        }
    }

    

    public class DateSpan
    {
        /// <summary> Gets or sets Years </summary>
        public int Years { get; set; }
        /// <summary> Gets or sets Months </summary>
        public int Months { get; set; }
        /// <summary> Gets or sets Days </summary>
        public int Days { get; set; }

        /// <summary> Gets or sets Hours </summary>
        public int Hours { get; set; }
        /// <summary> Gets or sets Minutes </summary>
        public int Minutes { get; set; }
        /// <summary> Gets or sets Seconds </summary>
        public int Seconds { get; set; }
        /// <summary> Gets or sets Milliseconds </summary>
        public int Milliseconds { get; set; }

        public TimeSpan Time { get { return new TimeSpan(Days, Hours, Minutes, Milliseconds); } }

        public DateSpan()
        {
             
        }
        public DateSpan(DateTime d1, DateTime d2)
        { 
            //12/1    
            // 3/6      2/1
            var d1Months = (((d1.Year - 1) * 12) + d1.Month);
            var d2Months = (((d2.Year - 1) * 12) + d2.Month);

            var spanMonth = d1Months - d2Months;
            Months = spanMonth % 12;
            Years = (spanMonth - Months) / 12;

            d2 = d2.AddYears(Years);
            d2 = d2.AddMonths(Months);

            var span = d1.Subtract(d2);
            Days = span.Days;
            Hours = span.Hours;
            Minutes = span.Minutes;
            Milliseconds = span.Milliseconds;

            //Time = new TimeSpan(Days, Hours, Minutes, Milliseconds);
        }
    }
    public static class DateCalc
    {
        public static DateSpan GetDateSpan(this DateTime date, DateTime other)
        {
            return new DateSpan(date, other);
        }
        public static DateTime Add(this DateTime date, DateSpan span)
        {
            var newDate = date.AddYears(span.Years);
            newDate = newDate.AddMonths(span.Months);
            newDate = newDate.Add(span.Time);
            return newDate;
        }

        public static DateTime Add(this DateTime date, int years, int months, int days = 0)
        {
            var newDate = date.AddYears(years);
            newDate = newDate.AddMonths(months);
            newDate = newDate.AddDays(days);
            return newDate;
        }

        #region Methods for Century
        /// <summary> Gets start of century for specified year </summary>
        public static DateTime GetCenturyStart(int year)
        {
            year -= 1;
            year = year - (year % 100);
            return GetDayStart(year + 1, 1, 1);
        }
        /// <summary> Gets end of century for specified year </summary>
        public static DateTime GetCenturyEnd(int year)
        {
            year = GetCenturyStart(year).Year + 99;
            return GetDayEnd(year, 12, 31);
        }
        /// <summary> Gets start of century for specified date </summary>
        public static DateTime GetCenturyStart(this DateTime date)
        {
            return GetCenturyStart(date.Year);
        }
        /// <summary> Gets end of century for specified date  </summary>
        public static DateTime GetCenturyEnd(this DateTime date)
        {
            return GetCenturyEnd(date.Year);
        }
        public static int GetCentury(this DateTime date)
        {
            var century = GetCenturyStart(date).Year - 1; 
            return (century + 100) / 100; 
        }
        /// <summary> Gets name of century for specified date, e.g.  21st for 2025 </summary>
        public static string GetCenturyName(this DateTime date)
        {
            var century = GetCentury(date);
            if (century % 10 == 1) return century + "st";
            if (century % 10 == 2) return century + "nd";
            if (century % 10 == 3) return century + "rd";
            return century + "th";
        } 
        #endregion

        #region Methods for Decades
        /// <summary> Gets start of decade for specified year </summary>
        public static DateTime GetDecadeStart(int year)
        {
            year = year - (year % 10);
            return GetDayStart(year, 1, 1);
        }
        /// <summary> Gets end of decade for specified year </summary>
        public static DateTime GetDecadeEnd(int year)
        {
            year = year + (9 - (year % 10));
            return GetDayEnd(year, 12, 31);
        }
        /// <summary> Gets start of decade for specified date </summary>
        public static DateTime GetDecadeStart(this DateTime date)
        {
            return GetDecadeStart(date.Year);
        }
        /// <summary> Gets end of decade for specified date  </summary>
        public static DateTime GetDecadeEnd(this DateTime date)
        {
            return GetDecadeEnd(date.Year);
        }
        /// <summary> Gets name of decade for specified date, e.g. 1980s for 1985 </summary>
        public static string GetDecadeName(this DateTime date)
        {
            return GetDecadeStart(date) + "s"; 
        } 
        #endregion

        #region Methods for Days
        public static DateTime GetDayStart(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0);
        }
        public static DateTime GetDayEnd(int year, int month, int day)
        {
            return new DateTime(year, month, day, 23, 59, 59);
        }
        public static DateTime GetDayStart(this DateTime date)
        {
            return GetDayStart(date.Year, date.Month, date.Day);
        }
        public static DateTime GetDayEnd(this DateTime date)
        {
            return GetDayEnd(date.Year, date.Month, date.Day);
        } 
        #endregion
        #region Methods for Years
        /// <summary> Gets start of year for specified date  </summary>
        public static DateTime GetYearStart(this DateTime date)
        {
            return GetYearStart(date.Year);
        }
        /// <summary> Gets end of year for specified date  </summary>
        public static DateTime GetYearEnd(this DateTime date)
        {
            return GetYearEnd(date.Year);
        }
        /// <summary> Gets start of specified year </summary>
        public static DateTime GetYearStart(int year)
        {
            return GetDayStart(year, 1, 1);
        }
        /// <summary> Gets end of specified year </summary>
        public static DateTime GetYearEnd(int year)
        {
            return GetDayEnd(year, 12, 31);
        }
        #endregion

        #region Methods for Months
        /// <summary> Gets start of month for specified date </summary>
        public static DateTime GetMonthStart(this DateTime date)
        {
            return GetMonthStart(date.Year, date.Month);
        }
        /// <summary> Gets end of month for specified date </summary>
        public static DateTime GetMonthEnd(this DateTime date)
        {
            return GetMonthEnd(date.Year, date.Month);
        }
        public static DateTime GetMonthStart(int year, int month)
        {
            return GetDayStart(year, month, 1);
        }
        public static DateTime GetMonthEnd(int year, int month)
        {
            var days = DateTime.DaysInMonth(year, month);
            return GetDayEnd(year, month, days);
        }
        #endregion

        #region Methods for Quarters
        
        public static int GetQuarter(int month)
        {
            var quarters = new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4 };
            return quarters[month - 1];
        }
        public static int GetQuarter(this DateTime date)
        {
            return GetQuarter(date.Month);
        }
        /// <summary> Gets start of quarter for specified date </summary>
        public static DateTime GetQuarterStart(this DateTime date)
        {
            var quarter = date.GetQuarter();
            var quarterMonth = ((quarter - 1) * 3) + 1;
            return GetDayStart(date.Year, quarterMonth, 1);
        }
        /// <summary> Gets end of quarter for specified date </summary>
        public static DateTime GetQuarterEnd(this DateTime date)
        {
            var quarter = date.GetQuarter();
            var quarterMonth = quarter * 3;
            var days = DateTime.DaysInMonth(date.Year, quarterMonth);
            return GetDayEnd(date.Year, quarterMonth, days);
        } 
        #endregion
    }

}