using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models
{
    public class RadialDataSample : RadialDataCollection
    {
        public RadialDataSample()
        {
            this.Add(new RadialDataPoint { Category = "A", Radius = 90 });
            this.Add(new RadialDataPoint { Category = "B", Radius = 60 });
            this.Add(new RadialDataPoint { Category = "C", Radius = 45 });
            this.Add(new RadialDataPoint { Category = "D", Radius = 90 });
            this.Add(new RadialDataPoint { Category = "E", Radius = 80 });
            this.Add(new RadialDataPoint { Category = "F", Radius = 40 });
            this.Add(new RadialDataPoint { Category = "G", Radius = 80 });
            this.Add(new RadialDataPoint { Category = "H", Radius = 75 });
            this.Add(new RadialDataPoint { Category = "I", Radius = 60 });
            this.Add(new RadialDataPoint { Category = "J", Radius = 85 });
            this.Add(new RadialDataPoint { Category = "K", Radius = 50 });
            this.Add(new RadialDataPoint { Category = "L", Radius = 85 });
            this.Add(new RadialDataPoint { Category = "M", Radius = 50 });
            this.Add(new RadialDataPoint { Category = "N", Radius = 75 });
            this.Add(new RadialDataPoint { Category = "O", Radius = 90 });
            this.Add(new RadialDataPoint { Category = "P", Radius = 80 });
            this.Add(new RadialDataPoint { Category = "Q", Radius = 40 });
            this.Add(new RadialDataPoint { Category = "R", Radius = 80 });
            this.Add(new RadialDataPoint { Category = "S", Radius = 65 });
            this.Add(new RadialDataPoint { Category = "T", Radius = 80 });
            this.Add(new RadialDataPoint { Category = "U", Radius = 85 });
            this.Add(new RadialDataPoint { Category = "W", Radius = 50 });
            this.Add(new RadialDataPoint { Category = "V", Radius = 85 });
            this.Add(new RadialDataPoint { Category = "X", Radius = 50 });
            this.Add(new RadialDataPoint { Category = "Y", Radius = 75 });
            this.Add(new RadialDataPoint { Category = "Z", Radius = 90 });
        }
    }
    public class RadialDataCollection : ObservableCollection<RadialDataPoint>
    { }

    public class RadialDataPoint : ObservableModel
    {

        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set { if (_radius == value) return; _radius = value; OnPropertyChanged("Radius"); }
        }

        private string _label;
        public string Category
        {
            get { return _label; }
            set { if (_label == value) return; _label = value; OnPropertyChanged("Category"); }
        }
        public new string ToString()
        {
            return String.Format("Category {0}, Radius {1}", Category, Radius);
        }
    }

}