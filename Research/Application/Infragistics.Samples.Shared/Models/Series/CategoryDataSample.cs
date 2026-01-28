using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Infragistics.Samples.Shared.Models
{
    public class CategoryDataSample : CategoryDataCollection
    {
        public CategoryDataSample()
        {
            this.Add(new CategoryDataPoint { Category = "A", Value = 85, Low = 40, High = 95 });
            this.Add(new CategoryDataPoint { Category = "B", Value = 50, Low = 25, High = 65 });
            this.Add(new CategoryDataPoint { Category = "C", Value = 75, Low = 40, High = 90 });
            this.Add(new CategoryDataPoint { Category = "D", Value = 100, Low = 55, High = 95 });
            this.Add(new CategoryDataPoint { Category = "E", Value = 80, Low = 45, High = 80 });
            this.Add(new CategoryDataPoint { Category = "F", Value = 40, Low = 25, High = 65 });
            this.Add(new CategoryDataPoint { Category = "G", Value = 80, Low = 15, High = 50 });
            this.Add(new CategoryDataPoint { Category = "H", Value = 75, Low = 50, High = 90 });
            this.Add(new CategoryDataPoint { Category = "I", Value = 80, Low = 35, High = 80 });
            this.Add(new CategoryDataPoint { Category = "J", Value = 85, Low = 60, High = 90 });
            this.Add(new CategoryDataPoint { Category = "K", Value = 50, Low = 35, High = 80 });
            this.Add(new CategoryDataPoint { Category = "L", Value = 85, Low = 50, High = 90 });
            this.Add(new CategoryDataPoint { Category = "M", Value = 50, Low = 35, High = 60 });
            this.Add(new CategoryDataPoint { Category = "N", Value = 75, Low = 50, High = 90 });
            this.Add(new CategoryDataPoint { Category = "O", Value = 100, Low = 45, High = 95 });
            this.Add(new CategoryDataPoint { Category = "P", Value = 80, Low = 35, High = 80 });
            this.Add(new CategoryDataPoint { Category = "Q", Value = 40, Low = 25, High = 60 });
            this.Add(new CategoryDataPoint { Category = "R", Value = 80, Low = 25, High = 70 });
            this.Add(new CategoryDataPoint { Category = "S", Value = 75, Low = 40, High = 90 });
            this.Add(new CategoryDataPoint { Category = "T", Value = 80, Low = 45, High = 80 });
            this.Add(new CategoryDataPoint { Category = "U", Value = 85, Low = 50, High = 90 });
            this.Add(new CategoryDataPoint { Category = "W", Value = 50, Low = 35, High = 60 });
            this.Add(new CategoryDataPoint { Category = "V", Value = 85, Low = 40, High = 90 });
            this.Add(new CategoryDataPoint { Category = "X", Value = 50, Low = 25, High = 80 });
            this.Add(new CategoryDataPoint { Category = "Y", Value = 75, Low = 40, High = 90 });
            this.Add(new CategoryDataPoint { Category = "Z", Value = 100, Low = 35, High = 95 });

            int i = 0;
            foreach (CategoryDataPoint dataPoint in this)
            {
                dataPoint.Index = i++;
            }
        }
    }
    public class CategoryDataCollection : ObservableCollection<CategoryDataPoint>
    {
        public CategoryDataCollection()
        {
            _title = "";
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title.Equals(value)) return;
                _title = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            }
        }
    }
    public class CategoryDataPoint : ObservableModel
    {
        public CategoryDataPoint() { }
        public CategoryDataPoint(double value, string label)
        {
            this.Value = value;
            this.Category = label;
        }
        public CategoryDataPoint(CategoryDataPoint dataPoint)
        {
            this.Index = dataPoint.Index;
            this.Category = dataPoint.Category;
            this.Value = dataPoint.Value;
            this.Date = dataPoint.Date;
            this.High = dataPoint.High;
            this.Low = dataPoint.Low;
        }
      
        public double Change
        {
            get { return High - Low; }
        }
        public double ChangePerCent
        {
            get { return (Change / High) * 100; }
        }

        private double _index;
        public double Index
        {
            get { return _index; }
            set { if (_index == value) return; _index = value; OnPropertyChanged("Index"); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { if (_value == value) return; _value = value; OnPropertyChanged("Value");}
        }

        private double _high;
        public double High
        {
            get { return _high; }
            set { if (_high == value) return; _high = value; OnPropertyChanged("High"); }
        }

        private double _low;
        public double Low
        {
            get { return _low; } 
            set { if (_low == value) return; _low = value; OnPropertyChanged("Low"); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { if (_category == value) return; _category = value; OnPropertyChanged("Category"); }
        }
        public string Label
        {
            get { return _category; }
            set { if (_category == value) return; _category = value; OnPropertyChanged("Label"); }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; } 
            set { if (_date == value) return; _date = value; OnPropertyChanged("Date"); }
        }

        public new string ToString()
        {
            return String.Format("Category {0}, Value {1}", Category, Value);
        }
        public CategoryDataPoint Clone()
        {
            return new CategoryDataPoint(this);
        } 
    }

    public class CategoryDataRandomSample : CategoryDataCollection
    {
        public CategoryDataRandomSample()
        {
            _settings = new CategoryDataSettings
                            {
                                DataPoints = 10,
                                ValueStart = 100,
                                ValueChange = 10,
                                DateStart = new DateTime(2010, 1, 1),
                                DateInterval = TimeSpan.FromSeconds(1),
                            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public void Generate()
        {
            this.Clear();
            CategoryDataPoint dataPoint = new CategoryDataPoint();
            dataPoint.Index = 0;
            dataPoint.Value = this.Settings.ValueStart;
            dataPoint.Date = this.Settings.DateStart;
            dataPoint.High = this.Settings.ValueStart + this.Settings.ValueChange;
            dataPoint.Low = this.Settings.ValueStart - this.Settings.ValueChange;
            dataPoint.Category = (dataPoint.Index + 1).ToString();

            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                this.Add(dataPoint.Clone());

                if (CategoryDataGenerator.Random.NextDouble() > .5)
                {
                    dataPoint.Value += (CategoryDataGenerator.Random.NextDouble() * this.Settings.ValueChange);
                    //dataPoint.Value += this.Settings.ValueChange;
                }
                else
                {
                    dataPoint.Value -= (CategoryDataGenerator.Random.NextDouble() * this.Settings.ValueChange);
                    //dataPoint.Value -= this.Settings.ValueChange;
                }
                dataPoint.High = dataPoint.Value + this.Settings.ValueChange;
                dataPoint.Low = dataPoint.Value - this.Settings.ValueChange;
                dataPoint.Date = dataPoint.Date.Add(this.Settings.DateInterval);
                dataPoint.Index += 1;
                dataPoint.Category = (dataPoint.Index + 1).ToString();
            }
        }
        private CategoryDataSettings _settings;
        public CategoryDataSettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value) return;
                _settings = value;
                this.Generate();
            }
        } 

        
    }
    public class CategoryDataSettings : ObservableModel
    {
        public CategoryDataSettings()
        {
            DataPoints = 10;

            ValueStart = 100;
            ValueChange = 10;

            DateStart = new DateTime(2010, 1, 1);
            DateInterval = TimeSpan.FromSeconds(1);
        }
        
        public double ValueChange { get; set; }
        public double ValueStart { get; set; }

        public int DataPoints { get; set; }

        /// <summary>
        /// Determines time intervals between data points
        /// </summary>
        public TimeSpan DateInterval { get; set; }
        /// <summary>
        /// Determines starting date value for the first data point
        /// </summary>
        public DateTime DateStart { get; set; }
   
    }
    public static class CategoryDataGenerator
    {
        public static Random Random = new Random();
    }
}