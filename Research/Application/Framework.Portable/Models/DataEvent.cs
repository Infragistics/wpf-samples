using System;
using System.Collections.Generic;
using System.Linq; 

namespace Infragistics.Framework 
{
    public class DataEvent : ObservableModel
    {
        public DataEvent()
        {
             
        }
        public DataEvent(string name, DateTime date)
        {
            Name = name;
            Date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            StartDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            EndDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            //EndDate = date.AddHours(23.9);
            Id = Date.Ticks;
        }

        #region Properties
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }

        private string _Visibility;
        /// <summary> Gets or sets Visibility </summary>
        public string Visibility
        {
            get { return _Visibility; }
            set { if (_Visibility == value) return; _Visibility = value; OnPropertyChanged("Visibility"); }
        }

        private string _Recurrence;
        /// <summary> Gets or sets Recurrence </summary>
        public string Recurrence
        {
            get { return _Recurrence; }
            set { if (_Recurrence == value) return; _Recurrence = value; OnPropertyChanged("Recurrence"); }
        }

        private string _Reminder;
        /// <summary> Gets or sets Reminder </summary>
        public string Reminder
        {
            get { return _Reminder; }
            set { if (_Reminder == value) return; _Reminder = value; OnPropertyChanged("Reminder"); }
        }

        private string _Location;
        /// <summary> Gets or sets Location </summary>
        public string Location
        {
            get { return _Location; }
            set { if (_Location == value) return; _Location = value; OnPropertyChanged("Location"); }
        }
         
        private DateTime _StartDate;
        /// <summary> Gets or sets StartDate </summary>
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { if (_StartDate == value) return; _StartDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime _EndDate;
        /// <summary> Gets or sets EndDate </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { if (_EndDate == value) return; _EndDate = value; OnPropertyChanged("EndDate"); }
        }
         

        /// <summary> Gets or sets StartTime </summary>
        public TimeSpan StartTime
        {
            get { return StartDate.TimeOfDay; }
            set
            {
                if (StartDate.TimeOfDay == value) return;
                StartDate = StartDate.SetTime(value);
                OnPropertyChanged("StartTime");
            }
        }

        /// <summary> Gets or sets EndTime </summary>
        public TimeSpan EndTime
        {
            get { return EndDate.TimeOfDay; }
            set
            {
                if (EndDate.TimeOfDay == value) return;
                EndDate = EndDate.SetTime(value);
                OnPropertyChanged("EndTime");
            }
        }
        private bool _IsAllDay;
        /// <summary> Gets or sets IsAllDay </summary>
        public bool IsAllDay
        {
            get
            {
                return _IsAllDay;
            }
            set
            {
                if (_IsAllDay == value) return; _IsAllDay = value;
                if (_IsAllDay)
                {
                    StartDate = StartDate.SetTime(0, 0, 0);
                    EndDate = StartDate.SetTime(0, 0, 0);
                } 
                OnPropertyChanged("IsAllDay");
                OnPropertyChanged("IsMeeting");
            }
        }
        /// <summary> Gets or sets IsMeeting </summary>
        public bool IsMeeting
        {
            get { return !IsAllDay; }
           // set { IsAllDay = !value; OnPropertyChanged("IsMeeting"); }
        }

        #endregion
        
        //public void Validate()
        //{
        //    //var duration = EndTime.Subtract(StartTime).TotalHours;
        //    //_IsAllDay = duration == 0 || duration == 24;
        //    //if (_IsAllDay)
        //    //{
        //    //    _StartDate = StartDate.SetTime(0, 0, 0);
        //    //    _EndDate = StartDate.SetTime(0, 0, 0);
        //    //}
        //}

        public static List<DataEvent> GetAnnualEvents(int year)
        {
            var national = new List<DataEvent>();
            national.Add(new DataEvent("MKL Day", GetMKL(year)));
            national.Add(new DataEvent("Memorial Day", GetMemorialDay(year)));
            national.Add(new DataEvent("Independence Day", GetIndependenceDay(year)));
            national.Add(new DataEvent("Labor Day", GetLaborDay(year)));
            national.Add(new DataEvent("Veterns Day", GetVeternsDay(year)));
            national.Add(new DataEvent("Thanksgiving Day", GetThanksgivingDay(year)));

            foreach (var item in national)
            {
                item.Location = "United States";
                item.Type = "National Holiday";
            }

            var regional = new List<DataEvent>();
            regional.Add(new DataEvent("Lincoln’s Day", new DateTime(year, 2, 12)));
            regional.Add(new DataEvent("President Day", GetPresidentDay(year)));
            regional.Add(new DataEvent("Mother's Day", GetMothersDay(year)));
            regional.Add(new DataEvent("Father's Day", GetFathersDay(year)));
            regional.Add(new DataEvent("Columbus Day", GetColumbusDay(year)));
            regional.Add(new DataEvent("Black Friday", GetBlackFriday(year)));
            foreach (var item in regional)
            {
                item.Location = "United States";
                item.Type = "Regional Holiday";
            }

            var other = new List<DataEvent>();
            other.Add(new DataEvent("Daylight Saving Day (Spring)", GetDaylightSavingSpring(year)));
            other.Add(new DataEvent("Daylight Saving Day (Fall)", GetDaylightSavingFall(year)));
            other.Add(new DataEvent("Tax Day", new DateTime(year, 4, 15)));
            foreach (var item in other)
            {
                item.Location = "United States";
                item.Type = "Other";
            }

            var worldwide = new List<DataEvent>();
            worldwide.Add(new DataEvent("New Year's Day", new DateTime(year, 1, 1)));
            worldwide.Add(new DataEvent("New Year's Holiday", GetNewYears(year)));
            worldwide.Add(new DataEvent("Christmas Day", GetChristmasDay(year)));
            worldwide.Add(new DataEvent("Easter Sunday", GetEasterSunday(year)));
            foreach (var item in worldwide)
            {
                item.Location = "Worldwide";
                item.Type = "Holiday";
            }

            var events = new List<DataEvent>();
            events.AddRange(national);
            events.AddRange(regional);
            events.AddRange(other);
            events.AddRange(worldwide);

            return events;
        }
        #region Holidays
        public static DateTime GetDaylightSavingSpring(int year)
        {
            return DayOfWeek.Sunday.BeforeOr(new DateTime(year, 3, 14));
        }
        public static DateTime GetDaylightSavingFall(int year)
        {
            return DayOfWeek.Sunday.AfterOr(new DateTime(year, 11, 1));
        }

        public static DateTime GetMothersDay(int year)
        {
            return DayOfWeek.Sunday.BeforeOr(new DateTime(year, 5, 14));
        }
        public static DateTime GetFathersDay(int year)
        {
            return DayOfWeek.Sunday.BeforeOr(new DateTime(year, 6, 21));
        }

        public static DateTime GetNewYears(int year)
        {
            return DayOfWeek.Monday.AfterOr(new DateTime(year, 1, 1));
        }
        //Martin Luther King Day - third monday in January
        public static DateTime GetMKL(int year)
        {
            return DayOfWeek.Monday.BeforeOr(new DateTime(year, 1, 21));
        }
        //President's Day - third monday in February
        public static DateTime GetPresidentDay(int year)
        {
            return DayOfWeek.Monday.BeforeOr(new DateTime(year, 2, 21));
        }
        //memorial day - last monday in May 
        public static DateTime GetMemorialDay(int year)
        {
            return DayOfWeek.Monday.BeforeOr(new DateTime(year, 5, 31));
        }
        //independence day 
        public static DateTime GetIndependenceDay(int year)
        {
            return new DateTime(year, 7, 4).GetWorkday();
        }
        //labor day - 1st Monday in September 
        public static DateTime GetLaborDay(int year)
        {
            return DayOfWeek.Monday.AfterOr(new DateTime(year, 9, 1));
        }
        //thanksgiving - 4th Thursday in November 
        public static DateTime GetThanksgivingDay(int year)
        {
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            return new DateTime(year, 11, thanksgiving);
        }

        public static DateTime GetChristmasDay(int year)
        {
            return new DateTime(year, 12, 25).GetWorkday();
        }
    
        public static DateTime GetEasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }

        public static DateTime GetVeternsDay(int year)
        {
            return new DateTime(year, 11, 11);
        }

        //Columbus Day -- 2nd monday in October
        public static DateTime GetColumbusDay(int year)
        {
            return DayOfWeek.Monday.BeforeOr(new DateTime(year, 10, 14));
        }

        //BlackFriday-day after Thanks Giving 
        public static DateTime GetBlackFriday(int year)
        {
            return GetThanksgivingDay(year).AddDays(1);
        }
        #endregion

    }
}
