using System;
using System.Collections.ObjectModel;

 
namespace Infragistics.Samples.Shared.Models
{
    public class TimelineDataModel : ObservableModel
    {

        public TimelineDataModel()
        {
            _data = new TimelineDataCollection();
            
            _settings = new TimelineDataSettings();
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            
            _function = new RandomFunction { DataPoints = _settings.DataPoints, ValueStart = _settings.ValueMin };
            _function.PropertyChanged += OnFunctionPropertyChanged;
            
            this.Generate();
        }
        private void OnFunctionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private void OnSettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public void Generate()
        {
            DateTime current = _settings.DateMin;
            for (int i = 0; i < _function.DataPoints; i++)
            {
                NumericDataPoint ndp = _function.GenerateDataPoint(i);
                _data.Add(new TimelineDataPoint { Date = current, Index = i, Value = ndp.Y, Label = i.ToString()});
                current.Add(_settings.DateInterval);
            }
        }

        private Function _function;
        public Function GenerationFunction
        {
            get { return _function; }
            set
            {
                if (_function == value) return;
                _function = value;
                OnPropertyChanged("GenerationFunction");
            }
        }

        private TimelineDataCollection _data;
        public TimelineDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private TimelineDataSettings _settings;
        public TimelineDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
                OnPropertyChanged("Settings");
            }
        }
    }
    public class TimelineDataSettings : DataSettings
    {
        public TimelineDataSettings()
        {
            ValueMin = 0;
            ValueInterval = 5;
            ValueMax = 100;

            DateMin = new DateTime(2010, 1, 1, 12, 0, 0, 0);
            DateInterval = new TimeSpan(1, 0, 0, 0);
            DataPoints = 100;
        }

        public double ValueMin { get; set; }
        public double ValueInterval { get; set; }
        public double ValueMax { get; set; }

        public DateTime DateMin { get; set; }
        public TimeSpan DateInterval { get; set; }
        public DateTime DateMax { get; set; }
    }
 
    public class TimelineDataCollection : ObservableCollection<TimelineDataPoint>
    {

    }

    public class TimelineDataPoint : DataPoint
    {

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (_duration == value) return;
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        
        public new string Caption
        {
            get { return this.ToString(); }
        }

        public new string ToString()
        {
            return String.Format("Index {0}, Value {1}, Date {2}, Duration {3}", Index, Value, Date, Duration);
        }
    }

}
