

namespace Infragistics.Samples.Shared.Models
{
    public class RadialDataModel : ObservableModel
    {
        public RadialDataModel()
        {
            _data = new RadialDataCollection();
            _settings = new RadialDataSettings();
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            this.Generate();
        }

        private void OnSettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        public void Generate()
        {
            _data = new RadialDataCollection();
            double radius = _settings.RadiusMin;
            for (int i = 0; i <= _settings.DataPoints; i++)
            {
                string lbl = i.ToString();
                _data.Add(new RadialDataPoint { Radius = radius, Category = lbl });
                radius += _settings.RadiusInterval;

                radius %= _settings.RadiusMax; // normalize radius
                
            }

        }

        private RadialDataCollection _data;
        public RadialDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private RadialDataSettings _settings;
        public RadialDataSettings Settings
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
   
    public class RadialDataSettings : DataSettings
    {
        public RadialDataSettings()
        {
            DataPoints = 20;
            
            RadiusMin = 0;
            RadiusInterval = 5;
            RadiusMax = 100;
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

        private double _radiusMax;
        public double RadiusMax
        {
            get { return _radiusMax; }
            set { if (_radiusMax == value) return; _radiusMax = value; OnPropertyChanged("RadiusMax"); }
        }
        #endregion
  

    }

    


}
