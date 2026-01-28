using System;
using System.Collections.ObjectModel;


namespace Infragistics.Samples.Shared.Models
{
    public class PolarDataModel : ObservableModel
    {
        public PolarDataModel()
        {
            Random = new Random();
            _data = new PolarDataCollection();
            _settings = new PolarDataSettings();
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }

        protected Random Random;
        private void OnSettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }

        public void Generate()
        {
            _data = new PolarDataCollection();
            double radius = _settings.RadiusStart;
            double angle = _settings.AngleStart;
            for (int i = 0; i <= _settings.DataPoints; i++)
            {
                _data.Add(new PolarDataPoint { Angle = angle, Radius = radius });
                angle += _settings.AngleInterval;
                radius += _settings.RadiusInterval;

                radius %= _settings.RadiusMax; // normalize radius
                angle %= _settings.AngleMax; // normalize angle
            }
        }
        #region Properties
        private PolarDataCollection _data;
        public PolarDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private PolarDataSettings _settings;
        public PolarDataSettings Settings
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
        #endregion

    }
    public class PolarDataSettings : DataSettings
    {
        public PolarDataSettings()
        {
            DataPoints = 24;

            RadiusMin = 0;
            RadiusInterval = 5;
            RadiusMax = 100;

            RadiusStart = (RadiusMax + RadiusMin) / 2;

            AngleMin = 0;
            AngleInterval = 15;
            AngleMax = 360;

            AngleStart = AngleMin;
        }
        #region Properties
        private double _radiusMin;
        public double RadiusMin
        {
            get { return _radiusMin; }
            set { if (_radiusMin == value) return; _radiusMin = value; OnPropertyChanged("RadiusMin"); }
        }

        private double _radiusInterval;
        public double RadiusInterval
        {
            get { return _radiusInterval; }
            set { if (_radiusInterval == value) return; _radiusInterval = value; OnPropertyChanged("RadiusInterval"); }
        }

        private double _radiusStart;
        public double RadiusStart
        {
            get { return _radiusStart; }
            set { if (_radiusStart == value) return; _radiusStart = value; OnPropertyChanged("RadiusStart"); }
        }

        private double _radiusMax;
        public double RadiusMax
        {
            get { return _radiusMax; }
            set { if (_radiusMax == value) return; _radiusMax = value; OnPropertyChanged("RadiusMax"); }
        }

        private double _angleStart;
        public double AngleStart
        {
            get { return _angleStart; }
            set { if (_angleStart == value) return; _angleStart = value; OnPropertyChanged("AngleStart"); }
        }

        private double _angleMin;
        public double AngleMin
        {
            get { return _angleMin; }
            set { if (_angleMin == value) return; _angleMin = value; OnPropertyChanged("AngleMin"); }
        }

        private double _angleInterval;
        public double AngleInterval
        {
            get { return _angleInterval; }
            set { if (_angleInterval == value) return; _angleInterval = value; OnPropertyChanged("AngleInterval"); }
        }

        private double _angleMax;
        public double AngleMax
        {
            get { return _angleMax; }
            set { if (_angleMax == value) return; _angleMax = value; OnPropertyChanged("AngleMax"); }
        }
        #endregion

    }
   
    #region Refactor

    public class PolarVarAngleDataCollection : ObservableCollection<PolarDataPoint>
    {
        public PolarVarAngleDataCollection()
        {
            const int r = 600;
            for (int i = 0; i <= 12; i++)
            {
                //r += i * 50;
                int a = i * 30;
                Add(new PolarDataPoint { Angle = a, Radius = r });
            }
        }
    }
    public class PolarVarRadiusDataCollection : ObservableCollection<PolarDataPoint>
    {
        public PolarVarRadiusDataCollection()
        {
            const int a = 0;
            for (int i = 0; i <= 12; i++)
            {
                int r = i * 50;
                //a = i * 30;
                Add(new PolarDataPoint { Angle = a, Radius = r });
            }
        }
    }
    public class PolarVarDataCollection : ObservableCollection<PolarDataPoint>
    {
        public PolarVarDataCollection()
        {
            for (int i = 0; i <= 12; i++)
            {
                int r = i * 50;
                int a = i * 30;
                Add(new PolarDataPoint { Angle = a, Radius = r });

            }
        }
    }
    public class SpiralDataCollection : ObservableCollection<PolarDataPoint>
    {
        public double AngleMin { get; set; }
        public double AngleIntervals { get; set; }
        public double RadiusMin { get; set; }
        public double RadiusIntervals { get; set; }
        public int DataPoints { get; set; }

        public SpiralDataCollection()
        {
            DataPoints = 25;

            AngleMin = 0;
            AngleIntervals = 15;

            RadiusMin = 0;
            RadiusIntervals = 25;

            this.GenerateData();
        }
        public void GenerateData()
        {
            this.Clear();

            double r = RadiusMin; double a = AngleMin;
            for (int i = 0; i <= DataPoints; i++)
            {
                this.Add(new PolarDataPoint { Angle = a, Radius = r });
                a += AngleIntervals;
                r += RadiusIntervals;
                a %= 360;

            }
        }
    }

    #endregion
    
}
