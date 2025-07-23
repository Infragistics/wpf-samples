using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System;
using System.Linq;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingWeatherData : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingWeatherData()
        {
            InitializeComponent();
            WeatherViewModel = new WeatherViewModel();
              
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            // ShapefileConverter loads data from database (DBF) file and
            // stores it in the Fields property of Dictionary<string, object> type
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImported;
            Shapefile.ShapefileSource = new Uri(path + "world-cities.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "world-cities.dbf", UriKind.RelativeOrAbsolute);
        }

        protected ShapefileConverter Shapefile;
        protected WeatherViewModel WeatherViewModel;

        private bool viewModelCreated = false;

        private void OnShapefileImported(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {            
            // parse loaded data from DBF and SHP files
            foreach (ShapefileRecord record in Shapefile)
            {
                var location = new WeatherLocation();
                if (record.Fields != null && double.Parse(record.Fields["POPULATION"].ToString()) > 1300000)
                {
                    // get geo-location of a location from shape file (SHP)
                    location.Longitude = record.Points[0][0].X;
                    location.Latitude = record.Points[0][0].Y;
                    // get data about a location from database file (DBF)
                    location.CountryName = (string)(record.Fields["COUNTRY"]);
                    location.CityName = (string)(record.Fields["NAME"]);
                    location.Weather.Conditions = WeatherGenerator.GenerateWeatherCondition(location);
                    location.Weather.Temperature = WeatherGenerator.GenerateTemperature(location);

                    // add a city to the view model)
                    this.WeatherViewModel.WeatherLocations.Add(location);
                    viewModelCreated = true;
                }
            }

            // bind geo-symbol series to collection of world cities
            if (this.WeatherViewModel.WeatherLocations.Count > 0)
                this.GeoMap.Series["GeoSymbolSeries"].ItemsSource = this.WeatherViewModel.WeatherLocations;

            // zooming geo-map to a specific geographic region of the world using GeoMapAdapter
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion);            
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModelCreated)
            {
                var box = (ComboBox)sender;
                var locations = this.WeatherViewModel.WeatherLocations;
                foreach (var loc in locations)
                {
                    loc.Weather.Temperature.Scale = (TemperatureScale)box.SelectedIndex;
                }
            }
        }
    }
}
