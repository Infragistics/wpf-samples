using System;

namespace IGExtensions.Framework
{
    public class DoubleFilter : ObservableModel
    {
        public DoubleFilter()
        {

        }
        public DoubleFilter(double min, double max)
        {
            _min = min;
            _max = max;
        }

        public double Range { get { return Max - Min; } }
        
        public bool Contains(double value)
        {
            return value >= Min && value <= Max;
        }
        public bool IsCustom { get; set; }
        private double _min = 0.0;
        public double Min
        {
            get { return _min; }
            set
            {
                if (_min != value)
                {
                    _min = value;
                    IsCustom = true;
                    OnPropertyChanged("Min");
                }
            }
        }
        private double _max = 10000.0;
        public double Max
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    IsCustom = true;
                    OnPropertyChanged("Max");
                }
            }
        }

    }

    public class TimeFilter : ObservableModel
    {
        public TimeFilter()
        {
            
        }
        public TimeFilter(DateTime min, DateTime max)
        {
            _min = min;
            _max = max;
        }
        public TimeSpan Range { get { return Max.Subtract(Min); } }
        public bool Contains(DateTime value)
        {
            return value >= Min && value <= Max;
        }
        public bool IsCustom { get; set; }
        private DateTime _min = DateTime.MinValue;
        public DateTime Min
        {
            get { return _min; }
            set
            {
                if (_min != value)
                {
                    _min = value;
                    IsCustom = true;
                    OnPropertyChanged("Min");
                }
            }
        }
        private DateTime _max = DateTime.MaxValue;
        public DateTime Max
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    IsCustom = true;
                    OnPropertyChanged("Max");
                }
            }
        }

    }

 }