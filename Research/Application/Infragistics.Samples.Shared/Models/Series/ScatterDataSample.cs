using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

 
namespace Infragistics.Samples.Shared.Models
{
    public class ScatterDataSample : ScatterDataCollection
    {
        public ScatterDataSample()
        {
            int value = 50;
            for (int i = 0; i < 100; i++)
            {
                double change = ScatterDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }
                value %= 100;
                this.Add(new ScatterDataPoint
                {
                    X = ScatterDataGenerator.Random.Next(i, i + 5),
                    Y = ScatterDataGenerator.Random.Next(value - 50, value + 50)
                });
            }
        }
    }
    public class ScatterDataCollection : ObservableCollection<ScatterDataPoint> { }
    public class ScatterDataPoint : ObservableModel
    {
        private double _y;
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }

        private double _x;
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }
        public new string ToString()
        {
            return String.Format("X {0}, Y {1}", X, Y);
        }
    }

    public class ScatterDataRandomSample : ScatterDataCollection
    {
        public ScatterDataRandomSample()
        {
            _settings = new ScatterDataSettings
            {
                DataPoints = 100,
                XStart = 0, XChange = 5,
                YStart = 0, YChange = 50
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            int x = (int)this.Settings.XStart;
            int y = (int)this.Settings.YStart;
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = ScatterDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    y += (int)(change * 20);
                }
                else
                {
                    y -= (int)(change * 20);
                }

                this.Add(new ScatterDataPoint
                {
                    X = ScatterDataGenerator.Random.Next(x, x + (int)Settings.XChange),
                    Y = ScatterDataGenerator.Random.Next(y - (int)Settings.YChange, y + (int)Settings.YChange)
                });
                x++;
            }
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private ScatterDataSettings _settings;
        public ScatterDataSettings Settings
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
    public class ScatterDataRangeSample : ScatterDataCollection
    {
        public ScatterDataRangeSample()
        {
            _settings = new ScatterDataSettings
            {
                DataPoints = 20,
                XMin = 10, XMax = 180,
                YMin = 10, YMax = 180
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double x = ScatterDataGenerator.Random.Next((int)Settings.XMin, (int)Settings.XMax);
                double y = ScatterDataGenerator.Random.Next((int)Settings.YMin, (int)Settings.YMax);
                this.Add(new ScatterDataPoint { X = x, Y = y });
            }
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private ScatterDataSettings _settings;
        public ScatterDataSettings Settings
        {
            get { return _settings; }
            set { if (_settings == value) return; _settings = value; this.Generate(); }
        }
    }
    public class ScatterDataSettings : ObservableModel
    {
        public ScatterDataSettings()
        {
            DataPoints = 100;

            XStart = 10;
            XChange = 5;
            //XMax = 90;

            YStart = 10;
            YChange = 5;
            //YMax = 90;
        }
        public double XChange { get; set; }
        public double YChange { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }

        public double YMin { get; set; }
        public double YMax { get; set; }
      
        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        private double _xMin;
        public double XStart
        {
            get { return _xMin; }
            set { if (_xMin == value) return; _xMin = value; OnPropertyChanged("XMin"); }
        }

        private double _yMin;
        public double YStart
        {
            get { return _yMin; }
            set { if (_yMin == value) return; _yMin = value; OnPropertyChanged("YMin"); }
        }

      
    }
    public static class ScatterDataGenerator
    {
        public static Random Random = new Random();
    }
   
}