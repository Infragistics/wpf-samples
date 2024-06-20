using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Threading;
using IGGeographicMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class MapProgression : Infragistics.Samples.Framework.SampleContainer
    {
        public MapProgression()
        {
            InitializeComponent();
                      
            this.MapProgressionModel = new MapProgressionModel();
            this.MapProgressionModel.LoadDataProgressions();
            this.MapProgressionModel.CurrentProgressionChanged += OnCurrentMapProgressionChanged;

            this.MapProgressionSlider.ValueChanged += OnMapProgressionSliderValueChanged;
            this.MapProgressionToggleButton.Click += OnMapProgressionToggleButtonClick;

            this.DataContext = this.MapProgressionModel;

        }
        protected MapProgressionModel MapProgressionModel;

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {           
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
       

        private void OnCurrentMapProgressionChanged(object sender, EventArgs e)
        {
            this.MapProgressionSlider.Value = this.MapProgressionModel.CurrentProgression;
            if (this.MapProgressionModel.IsProgressing)
            {
                this.MapProgressionSlider.IsEnabled = false;
                this.MapProgressionToggleButton.IsChecked = true;
            }
            else
            {
                this.MapProgressionSlider.IsEnabled = true;
                this.MapProgressionToggleButton.IsChecked = false;
            }
        }
        private void OnMapProgressionToggleButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.MapProgressionModel.IsProgressing)
                this.MapProgressionModel.StopProgression();
            else
            {
                this.MapProgressionModel.StartProgression();
            }
        }
        private void OnMapProgressionSliderValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            int day = (int)e.NewValue;

            if (day < this.MapProgressionModel.WeatherProgressionMax)
            {
                var series = this.GeoMap.Series["shapeTempSeries"] as GeographicShapeSeries;
                if (series != null)
                {
                    // get weather map progression for a given day and bind it to the ShapeSeries
                    ShapefileConverter progression = this.MapProgressionModel.GetWeatherProgression(day);
                    series.ItemsSource = progression;
                }
            }
        }
    }

    public class MapProgressionModel : ObservableModel
    {
        public MapProgressionModel()
        {
            this.WeatherDataProvider = new WeatherTemperatureDataProvider();

            this.IsProgressing = false;
            this.UpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            this.UpdateTimer.Tick += OnUpdateTimerTick;
        }
        protected DispatcherTimer UpdateTimer;
        public WeatherTemperatureDataProvider WeatherDataProvider { get; private set; }
        public int CurrentProgression { get; set; }

        public bool IsProgressing { get; private set; }

        public ShapefileConverter GetWeatherProgression(int progressionIndex)
        {
            var progression = new ShapefileConverter();
            if (progressionIndex < this.WeatherProgressionMax)
            {
                progression = this.WeatherDataProvider.WeatherData[progressionIndex];
            }
            return progression;
        }

        public int WeatherProgressionMax
        {
            get
            {
                return this.WeatherDataProvider.WeatherData.Count;
            }
        }
        public void LoadDataProgressions()
        {
            this.WeatherDataProvider.LoadData();
            this.CurrentProgression = 0;
        }

        public void StartProgression()
        {
            if (this.IsProgressing)
                StopProgression();

            if (this.CurrentProgression >= this.WeatherDataProvider.WeatherData.Count - 1)
            {
                this.CurrentProgression = -1;
            }

            this.IsProgressing = true;
            this.UpdateTimer.Start();
        }
        public void StopProgression()
        {
            this.IsProgressing = false;
            if (this.UpdateTimer != null && this.UpdateTimer.IsEnabled)
                this.UpdateTimer.Stop();

        }
        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            if (this.CurrentProgression < this.WeatherDataProvider.WeatherData.Count - 1)
            {
                this.CurrentProgression++;
            }
            else
            {
                this.StopProgression();
            }
            this.OnCurrentProgressionChanged();

        }
        public event EventHandler CurrentProgressionChanged;
        protected void OnCurrentProgressionChanged()
        {
            if (CurrentProgressionChanged != null)
                CurrentProgressionChanged(this, EventArgs.Empty);
        }
    }

    public class WeatherTemperatureDataProvider
    {
        public WeatherTemperatureDataProvider()
        {

            this.WeatherData = new List<ShapefileConverter>();
        }
        public event EventHandler<EventArgs> LoadDataCompleted;
        public List<ShapefileConverter> WeatherData { get; private set; }
        public void LoadData()
        {
            this.WeatherData = new List<ShapefileConverter>();
            var path = "/Infragistics.Samples.Services;component/shapefiles/Weather/SeaSurfaceTemp/"; 
            var shapefiles = new List<string>
            {
                path + "sst_20110904",
                path + "sst_20110911",
                path + "sst_20110814",
                path + "sst_20110821",
                path + "sst_20110925",
                path + "sst_20110828"
            };
            foreach (string shapefile in shapefiles)
            {
                var converter = new ShapefileConverter();
                converter.ShapefileSource = new Uri(shapefile + ".shp", UriKind.RelativeOrAbsolute);
                converter.DatabaseSource  = new Uri(shapefile + ".dbf", UriKind.RelativeOrAbsolute);

                this.WeatherData.Add(converter);
            }
            if (this.LoadDataCompleted != null)
            {
                this.LoadDataCompleted(this, EventArgs.Empty);
            }
        }


    }

}
