using System;
using System.Collections.Generic;

namespace Infragistics.Framework
{ 
    public enum DateRangeSpan
    {
        Month,
        Quarter,
        Year,
        Decade,
        Century,
    }

    public class DateRange
    {
        /// <summary> Gets start date </summary>
        public DateTime RangeMin { get; private set; }

        /// <summary> Gets end date </summary>
        public DateTime RangeMax { get; private set; }

        public TimeSpan TimeSpan { get; private set; }

        public bool IsRangeToDate { get; private set; }

        public DateRangeSpan RangeSpan { get; private set; }

        public string RangeName { get; private set; }

        public string RangeId { get; private set; }

        public string RangeLabel { get; set; }

        public string RangeDetails { get; set; }

        public DateRange(DateRangeSpan range)
            : this(range, Now, true)
        {


        }
        public override int GetHashCode()
        {
            return RangeMin.GetHashCode() ^ RangeMax.GetHashCode();
        }
        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null) return false;

            // If parameter cannot be cast to Point return false.
            var p = obj as DateRange;
            if (p == null) return false;

            // Return true if the fields match:
            return (RangeSpan == p.RangeSpan) &&
                   (RangeMin == p.RangeMin) &&
                   (RangeMax == p.RangeMax);
        }
        public bool Equals(DateRange p)
        {
            // If parameter is null return false:
            if (p == null) return false;

            // Return true if the fields match:
            return (RangeSpan == p.RangeSpan) &&
                   (RangeMin == p.RangeMin) &&
                   (RangeMax == p.RangeMax);
        }
        public static bool operator ==(DateRange x, DateRange y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return (x.RangeSpan == y.RangeSpan) &&
                   (x.RangeMin == y.RangeMin) &&
                   (x.RangeMax == y.RangeMax);
        }

        public static bool operator !=(DateRange x, DateRange y)
        {
            return !(x == y);
        }
        public static DateTime Now = DateTime.Now;

        public DateRange(DateRangeSpan span, DateTime date, bool isToDate = false)
        {
            //IsRangeToDate = isToDate;
            RangeSpan = span;

            if (span == DateRangeSpan.Month)
            {
                IsRangeToDate = Now.Year == date.Year && Now.Month == date.Month;

                RangeId = date.ToString("yyyy-MM");
                RangeMin = date.GetMonthStart();
                RangeMax = IsRangeToDate ? Now : date.GetMonthEnd();
                RangeLabel = IsRangeToDate ? "TO DATE" : date.ToString("MMM");
                //RangeDetails = RangeMin.Day + "-" + RangeMax.Day + ", " + date.Year;
            }
            else if (span == DateRangeSpan.Quarter)
            {
                IsRangeToDate = Now.Year == date.Year && Now.GetQuarter() == date.GetQuarter();

                RangeId = date.Year + "-Q" + date.GetQuarter();
                RangeMin = date.GetQuarterStart();
                RangeMax = IsRangeToDate ? Now : date.GetQuarterEnd();
                RangeLabel = IsRangeToDate ? "TO DATE" : "Q" + date.GetQuarter();
                //RangeDetails = RangeMin.ToString("MMM") + "-" +
                //                   RangeMax.ToString("MMM") + ", " + date.Year;
            }
            else if (span == DateRangeSpan.Year)
            {
                IsRangeToDate = Now.Year == date.Year;

                RangeId = date.Year.ToString();
                RangeMin = date.GetYearStart();
                RangeMax = IsRangeToDate ? Now : date.GetYearEnd();
                RangeLabel = IsRangeToDate ? "TO DATE" : date.Year.ToString();
                //RangeDetails = RangeMin.ToString("MMM") + "-" + RangeMax.ToString("MMM");
                //RangeDetails = RangeMin.ToString("MMM") + " " + RangeMin.ToString("dd") + "-" +
                //                   RangeMax.ToString("MMM") + " " + RangeMax.ToString("dd");
                if (IsRangeToDate) RangeDetails += ", " + RangeMax.Year;
            }
            else if (span == DateRangeSpan.Decade)
            {
                IsRangeToDate = Now.GetDecadeName() == date.GetDecadeName();

                RangeId = date.Year.ToString();
                RangeMin = date.GetDecadeStart();
                RangeMax = IsRangeToDate ? Now : date.GetDecadeEnd();
                RangeLabel = IsRangeToDate ? "TO DATE" : date.GetDecadeName();
                //RangeDetails = RangeMin.ToString("yyyy") + "-" + RangeMax.ToString("yyyy");
            }
            RangeName = RangeSpan.ToString().ToUpper();
            RangeId = RangeName + "-" + RangeId;

            // 2014-year     |to date|2013
            // 2014-quarter  |to date|q1|q2
            // 2014-month    |to date|01|02

            RangeDetails = RangeMin.ToString("MM/dd") + " - " +
                           RangeMax.ToString("MM/dd");

            if (!IsRangeToDate) RangeDetails += ", " + date.Year;

            TimeSpan = RangeMax.Subtract(RangeMin);

            Preview = RangeName + " " + RangeLabel + " " +
                      RangeDetails + " " + TimeSpan.TotalDays.ToString("#");
        }

        protected string Preview;

        public override string ToString()
        {
            return Preview;
        }
    }

    public class DateRangesList : List<DateRange>
    {
        static DateRangesList()
        {
            GenerateRanges();
        }

        /// <summary> Gets or sets Year Ranges </summary>
        public static DateRangesList YearRanges { get; set; }
        /// <summary> Gets or sets Quarter Ranges </summary>
        public static DateRangesList QuarterRanges { get; set; }
        /// <summary> Gets or sets Month Ranges </summary>
        public static DateRangesList MonthRanges { get; set; }

        public static void GenerateRanges(int years = 2)
        {
            YearRanges = new DateRangesList();
            MonthRanges = new DateRangesList();
            QuarterRanges = new DateRangesList();
            var date = DateTime.Now.AddYears(-years);

            for (var month = 0; month < years * 12; month++)
            {
                MonthRanges.Add(new DateRange(DateRangeSpan.Month, date));

                if (month % 3 == 0)
                    QuarterRanges.Add(new DateRange(DateRangeSpan.Quarter, date));

                if (month % 12 == 0)
                    YearRanges.Add(new DateRange(DateRangeSpan.Year, date));

                date = date.AddMonths(1);
            }
            // add year to date, month to date, quarter to date, 
            YearRanges.Add(new DateRange(DateRangeSpan.Year, DateTime.Now, true));
            MonthRanges.Add(new DateRange(DateRangeSpan.Month, DateTime.Now, true));
            QuarterRanges.Add(new DateRange(DateRangeSpan.Quarter, DateTime.Now, true));

            YearRanges.Reverse();
            MonthRanges.Reverse();
            QuarterRanges.Reverse();
        }
    }

}