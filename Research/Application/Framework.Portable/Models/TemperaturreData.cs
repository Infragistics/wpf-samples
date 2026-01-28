using System;
using System.Collections.Generic; 

namespace Infragistics.Framework
{
    public class TemperaturreData : List<MonthDayValueModel>
    {
        public TemperaturreData()
        {
            var tempMax = 40;
            var tempVariance = 5;

            for (var i = 1; i <= 12; i++)
            {
                var rad = (i * 25) * Math.PI / 180;
                rad -= Math.PI;// 3;

                var temp = Math.Cos(rad) * tempMax;

                for (var ii = 1; ii <= 30; ii++)
                {
                    var dist = (ii * 20) * Math.PI / 180;
                    temp = temp + (Math.Sin(dist) * tempVariance);

                    this.Add(new MonthDayValueModel { Month = i, Day = ii, Value = (int)temp });

                }
            }

        }
    }
    public class MonthDayValueModel : ObservableModel
    {
        private int _value;
        /// <summary> Gets or sets Value </summary>
        public int Value
        {
            get { return _value; }
            set { if (_value == value) return; _value = value; OnPropertyChanged("Value"); }
        }

        private int _month;
        /// <summary> Gets or sets Month </summary>
        public int Month
        {
            get { return _month; }
            set { if (_month == value) return; _month = value; OnPropertyChanged("Month"); }
        }

        private int _day;
        /// <summary> Gets or sets Day </summary>
        public int Day
        {
            get { return _day; }
            set { if (_day == value) return; _day = value; OnPropertyChanged("Day"); }
        }


    }
}