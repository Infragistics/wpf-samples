using System;
using System.Globalization;
using System.Windows.Data;

namespace IGExtensions.Framework
{
    public class NumericDate : ObservableModel
    {
        public NumericDate()
        // : this(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
        { }
        public NumericDate(double date)
        {
            _year = (int)date;
            _month = (int)(Math.Ceiling((date - _year) * 12) + 1);
            _day = 1;
            _value = GetCurrentDate();
        }
        public NumericDate(DateTime dateTime)
            : this(dateTime.Year, dateTime.Month, dateTime.Day)
        { }
        public NumericDate(int year, int month)
            : this(year, month, 1)
        { }
        public NumericDate(int year, int month, int day)
        {
            _day = day;
            _month = month;
            _year = year;
            _value = GetCurrentDate();
        }

        public new string ToString()
        {
            //return Browser + ", " + Year + ", " + Month + ", " + Share;
            return GetCurrentDate() + ", " + Year + ", " + Month;
        }
        #region Methods
        public double GetPreviousDate()
        {
            //if (double.IsNaN(Year)) return double.NaN;
            //if (double.IsNaN(Month)) return double.NaN;

            var date = Year + ((Month - 2) / 12.0);
            return date;
        }
        public double GetCurrentDate()
        {
            //if (double.IsNaN(Year)) return double.NaN;
            //if (double.IsNaN(Month)) return double.NaN;

            var date = Year + ((Month - 1) / 12.0);
            return date;
        }
        public double GetNextDate()
        {
            //if (double.IsNaN(Year)) return double.NaN;
            //if (double.IsNaN(Month)) return double.NaN;

            var date = Year + (Month / 12.0);
            return date;
        }

        public void RefreshDate()
        {
            //if (double.IsNaN(Year)) return;
            //if (double.IsNaN(Month)) return;

            _value = GetCurrentDate();
        }
        public void RefreshDateTime()
        {
            _year = (int)Value;
            _month = (int)(System.Math.Ceiling((Value - _year) * 12) + 1);

        }
        #endregion

        #region Properties

        private double _value;
        /// <summary>
        /// Gets or sets Value property 
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; RefreshDateTime(); OnPropertyChanged("Value"); }
        }

        private int _day;
        /// <summary>
        /// Gets or sets Day property 
        /// </summary>
        public int Day
        {
            get { return _day; }
            set { if (_day == value) return; _day = value; RefreshDate(); OnPropertyChanged("Day"); }
        }

        public DateTime DateTime { get { return new DateTime(Year, Month, Day); } }

        private int _month;
        /// <summary>
        /// Gets or sets Month property 
        /// </summary>
        public int Month
        {
            get { return _month; }
            set { if (_month == value) return; _month = value; RefreshDate(); OnPropertyChanged("Month"); }
        }
        private int _year;
        /// <summary>
        /// Gets or sets Year property 
        /// </summary>
        public int Year
        {
            get { return _year; }
            set { if (_year == value) return; _year = value; RefreshDate(); OnPropertyChanged("Year"); }
        }
        #endregion

    }

    /// <summary>
    /// Represents value converter for converting <see cref="NumericDate"/> to a string
    /// </summary>
    public class DoubleToDateConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is double)) return 2000;

            var doubleValue = (double)value;

            var nd = new NumericDate(doubleValue);
            //var year = (int)doubleValue;
            //var month = (int)(System.Math.Ceiling((doubleValue - year) * 12) + 1);

            //var date = new DateTime(year, month, 1);
            return string.Format("{0:yyyy, MMM}", nd.DateTime);

        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

        #endregion

    }
   
}