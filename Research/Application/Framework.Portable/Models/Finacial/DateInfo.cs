using System;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a class with detailed information about a DateTime
    /// </summary>
    public class DateInfo
    {
        /// <summary> Gets the date </summary>
        public DateTime Date { get; private set; }

        //public string QuarterId { get; private set; }
        public string QuarterName { get; private set; }
        public int Quarter { get; private set; }

        public int Century { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }
        //public string MonthId { get; private set; }

        //public string YearId { get; private set; }

        public int Day { get; private set; }

        public DateRange RangeMonth { get; private set; }

        public DateRange RangeYear { get; private set; }

        public DateRange RangeQuarter { get; private set; }

        public static DateTime Now = DateTime.Now;

        public DateInfo()
            : this(Now)
        {

        }
        public DateInfo(DateTime date)
        {
            //if (isToDate) date = DateTime.Now;
            RangeYear = new DateRange(DateRangeSpan.Year, date);
            RangeMonth = new DateRange(DateRangeSpan.Month, date);
            RangeQuarter = new DateRange(DateRangeSpan.Quarter, date);

            Date = RangeMonth.RangeMin;

            Year = date.Year;
            Month = date.Month;
            Day = date.Day;

            Quarter = date.GetQuarter();
            QuarterName = "Q" + Quarter;

            //MonthId = RangeMonth.RangeId;
            //QuarterId = RangeQuarter.RangeId;
            //YearId = RangeYear.RangeId;

            Century = date.GetCentury();

            Preview = Year + "-" + QuarterName + "-" + date.ToString("MM");
        }

        protected string Preview;

        public override string ToString()
        {
            return Preview;
        }
    }

}