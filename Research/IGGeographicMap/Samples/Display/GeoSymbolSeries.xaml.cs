using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System;

namespace IGGeographicMap.Samples
{
    public partial class GeoSymbolSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoSymbolSeries()
        {
            InitializeComponent();

            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";
             
            WorldCitiesViewModel = new WorldCitiesViewModel();

            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImportCompleted;
            Shapefile.ShapefileSource = new Uri(path + "world-cities.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "world-cities.dbf", UriKind.RelativeOrAbsolute);
              
            this.DataContext = WorldCitiesViewModel;
        }
        protected ShapefileConverter Shapefile;
        protected WorldCitiesViewModel WorldCitiesViewModel;
        
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        { 
            // parse loaded data from DBF and SHP files
            foreach (ShapefileRecord record in Shapefile)
            {
                var city = new WorldCityModel();
                if (record.Fields != null && double.Parse(record.Fields["POPULATION"].ToString()) > 1300000)
                {
                    // get geo-location of a city from shape file (SHP)
                    city.Longitude = record.Points[0][0].X;
                    city.Latitude = record.Points[0][0].Y;
                    // get data about a city from database file (DBF)
                    city.Population = (double)(record.Fields["POPULATION"]);
                    city.CountryName = (string)(record.Fields["COUNTRY"]);
                    city.CityName = (string)(record.Fields["NAME"]);

                    // add a city to the view model
                    this.WorldCitiesViewModel.WorldCities.Add(city);
                }
            }
            // bind geo-symbol series to collection of world cities
            if (this.WorldCitiesViewModel.WorldCities.Count > 0)
                this.GeoMap.Series["SymbolSeries"].ItemsSource = this.WorldCitiesViewModel.WorldCities;

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class 
        }
       
    }
    
}
