using System;
using System.ComponentModel;
 
namespace Infragistics.Samples.Shared.Models
{
    public class BubbleDataModel : ObservableModel
    {
        public BubbleDataModel()
        {
            _data = new BubbleDataCollection();

            _settings = new BubbleDataSettings();
            _settings.PropertyChanged += OnSettingsPropertyChanged;

            _function = new RandomFunction { DataPoints = _settings.DataPoints, ValueStart = 100 };
            _function.PropertyChanged += OnFunctionPropertyChanged;

            this.Generate();
        }

        private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }
        private void OnFunctionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Generate();
        }

        public void Generate()
        {
            _data = new BubbleDataCollection();
            Random rnd = new Random();
            double r = _settings.RadiusMin;
            double x = _settings.XMin;
            double y = _settings.YMin;
            for (int i = 0; i <= _settings.DataPoints; i++)
            {

                string lbl = i.ToString();
                
                _data.Add(new BubbleDataPoint { X = x, Y = y, Radius = r });

                NumericDataPoint ndp = _function.GenerateDataPoint(i);
                r = rnd.Next((int)_settings.RadiusMin, (int)_settings.RadiusMax);
                y = ndp.Y;
                x = ndp.X;

            }
        }
        #region Properties
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


        private BubbleDataCollection _data;
        public BubbleDataCollection Data
        {
            get { return _data; }
            set
            {
                if (_data == value) return;
                _data = value;
                OnPropertyChanged("Data");
            }
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
                OnPropertyChanged("Settings");
            }
        }
        #endregion

    }
    
}