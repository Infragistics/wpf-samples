using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
 
namespace Infragistics.Samples.Shared.Models
{
    public class BubbleDataSample : BubbleDataCollection
    {
        public BubbleDataSample()
        {
            List<BubbleDataPoint> items = new List<BubbleDataPoint>
            {
                new BubbleDataPoint {X = 100, Y = 100, Radius = 50},
                new BubbleDataPoint {X = 80, Y = 80, Radius = 35},
                new BubbleDataPoint {X = 80, Y = 160, Radius = 35},
                new BubbleDataPoint {X = 160, Y = 80, Radius = 35},
                new BubbleDataPoint {X = 160, Y = 160, Radius = 35},
                new BubbleDataPoint {X = 60, Y = 60, Radius = 25},
                new BubbleDataPoint {X = 60, Y = 140, Radius = 25},
                new BubbleDataPoint {X = 140, Y = 60, Radius = 25},
                new BubbleDataPoint {X = 140, Y = 140, Radius = 25},
                new BubbleDataPoint {X = 40, Y = 40, Radius = 15},
                new BubbleDataPoint {X = 40, Y = 120, Radius = 15},
                new BubbleDataPoint {X = 120, Y = 40, Radius = 15},
                new BubbleDataPoint {X = 120, Y = 120, Radius = 15},
                new BubbleDataPoint {X = 20, Y = 20, Radius = 10},
                new BubbleDataPoint {X = 20, Y = 180, Radius = 10},
                new BubbleDataPoint {X = 180, Y = 20, Radius = 10},
                new BubbleDataPoint {X = 180, Y = 180, Radius = 10}
            };

            var sortedItems = items.OrderByDescending(item => item.Radius).ToArray();
            for (int i = 0; i < sortedItems.Count(); i++)
            {
                sortedItems[i].Label = "Item " + (i + 1);
                this.Add(sortedItems[i]);
            }
        }
    }
    public class BubbleDataRandomSample : BubbleDataCollection
    {
        public BubbleDataRandomSample()
        {
            _settings = new BubbleDataSettings
            {
                DataPoints = 15,
                RadiusMin = 5, RadiusMax = 50, 
                XMin = 10, XMax = 200, 
                YMin = 10, YMax = 200
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            List<BubbleDataPoint> randomItems = new List<BubbleDataPoint>();
            for (int i = 0; i < Settings.DataPoints; i++)
            {
                double r = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax);
                double x = BubbleDataGenerator.Random.Next((int)Settings.XMin, (int)Settings.XMax);
                double y = BubbleDataGenerator.Random.Next((int)Settings.YMin, (int)Settings.YMax);
                randomItems.Add(new BubbleDataPoint { X = x, Y = y, Radius = r });
            }
            var sortedItems = randomItems.OrderByDescending(item => item.Radius).ToArray();
            for (int i = 0; i < sortedItems.Count(); i++)
            {
                sortedItems[i].Label = "Item " + (i + 1);
                this.Add(sortedItems[i]);
            }
        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private BubbleDataSettings _settings;
        public BubbleDataSettings Settings
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
    public class BubbleDataScatterSample : BubbleDataCollection
    {
        public BubbleDataScatterSample()
        {
            _settings = new BubbleDataSettings
            {
                DataPoints = 100,
                RadiusMin = 5,
                RadiusMax = 40,
                XMin = 0,
                YMin = 0,
            };
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }
        protected void Generate()
        {
            this.Clear();
            int value = (int)this.Settings.YMin;

            ObservableCollection<BubbleDataPoint> collectionToBeSorted = new ObservableCollection<BubbleDataPoint>();

            for (int i = 0; i < this.Settings.DataPoints; i++)
            {
                double change = BubbleDataGenerator.Random.NextDouble();
                if (change > .5)
                {
                    value += (int)(change * 20);
                }
                else
                {
                    value -= (int)(change * 20);
                }


                collectionToBeSorted.Add(new BubbleDataPoint
                {
                    X = BubbleDataGenerator.Random.Next(i, i + 5),
                    Y = BubbleDataGenerator.Random.Next(value - 50, value + 50),
                    Radius = BubbleDataGenerator.Random.Next((int)Settings.RadiusMin, (int)Settings.RadiusMax)
                });
            }

            var x = from item in collectionToBeSorted orderby item.X select item;

            foreach (BubbleDataPoint bdp in x)
            {
                this.Add(bdp);
            }

        }
        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private BubbleDataSettings _settings;
        public BubbleDataSettings Settings
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
  
    public static class BubbleDataGenerator
    {
        public static Random Random = new Random();
    }
    public class BubbleDataSettings : ObservableModel
    {
        public BubbleDataSettings()
        {
            DataPoints = 100;

            RadiusMin = 10;
            RadiusMax = 50;

            XMin = 10;
            XInterval = 5;
            XMax = 90;

            YMin = 10;
            YInterval = 5;
            YMax = 90;
        }
        public double XInterval { get; set; }
        public double YInterval { get; set; }

        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; OnPropertyChanged("DataPoints"); }
        }

        private double _radiusMin;
        public double RadiusMin
        {
            get { return _radiusMin; }
            set { if (_radiusMin == value) return; _radiusMin = value; OnPropertyChanged("RadiusMin"); }
        }
        private double _radiusMax;
        public double RadiusMax
        {
            get { return _radiusMax; }
            set { if (_radiusMax == value) return; _radiusMax = value; OnPropertyChanged("RadiusMax"); }
        }

        private double _xMin;
        public double XMin
        {
            get { return _xMin; }
            set { if (_xMin == value) return; _xMin = value; OnPropertyChanged("XMin"); }
        }

        private double _xMax;
        public double XMax
        {
            get { return _xMax; }
            set { if (_xMax == value) return; _xMax = value; OnPropertyChanged("XMax"); }
        }

        private double _yMin;
        public double YMin
        {
            get { return _yMin; }
            set { if (_yMin == value) return; _yMin = value; OnPropertyChanged("YMin"); }
        }

        private double _yMax;
        public double YMax
        {
            get { return _yMax; }
            set { if (_yMax == value) return; _yMax = value; OnPropertyChanged("YMax"); }
        }
    }
    public class BubbleDataCollection : ObservableCollection<BubbleDataPoint>
    { }
    public class BubbleDataPoint : ObservableModel
    {

        #region Properties

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

        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set { if (_radius == value) return; _radius = value; OnPropertyChanged("Radius"); }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set { if (_label == value) return; _label = value; OnPropertyChanged("Label"); }
        }

        #endregion

        public new string ToString()
        {
            return String.Format("X {0}, Y {1}, Radius {2}", X, Y, Radius);
        }

    }
   
}