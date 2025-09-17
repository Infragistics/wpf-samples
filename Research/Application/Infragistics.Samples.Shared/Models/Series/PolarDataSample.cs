using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models
{
    public class PolarDataSample : PolarDataCollection
    {
        public PolarDataSample()
        {
            this.Add(new PolarDataPoint { Angle = 0, Radius = 80 });
            this.Add(new PolarDataPoint { Angle = 30, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 45, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 60, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 90, Radius = 80 });
            this.Add(new PolarDataPoint { Angle = 120, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 135, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 150, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 180, Radius = 20 });
            this.Add(new PolarDataPoint { Angle = 210, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 225, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 240, Radius = 60 });

            this.Add(new PolarDataPoint { Angle = 270, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 300, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 315, Radius = 40 });
            this.Add(new PolarDataPoint { Angle = 330, Radius = 60 });
            this.Add(new PolarDataPoint { Angle = 360, Radius = 80 });
        }
    }
    public class PolarDataCollection : ObservableCollection<PolarDataPoint>
    { }
    public class PolarDataPoint : ObservableModel
    {
        private double _angle;
        public double Angle
        {
            get { return _angle; }
            set
            {
                if (_angle == value) return;
                _angle = value;
                OnPropertyChanged("Angle");
            }
        }

        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set
            {
                if (_radius == value) return;
                _radius = value;
                OnPropertyChanged("Radius");
            }
        }

        public new string ToString()
        {
            return String.Format("Angle {0}, Radius {1}}", Angle, Radius);
        }

    }

}