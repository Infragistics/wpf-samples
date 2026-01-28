using System;
using System.Windows.Controls;
using System.Windows.Media;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Controls;

namespace IGGeographicMap.Samples
{
    public partial class GeoProportionalSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoProportionalSeries()
        {
            InitializeComponent();
            
            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            WorldCitiesViewModel = new WorldCitiesViewModel();
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImportCompleted;
            Shapefile.ShapefileSource = new Uri(path + "world-cities.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource = new Uri(path + "world-cities.dbf", UriKind.RelativeOrAbsolute);

            this.DataContext = WorldCitiesViewModel;
        }
        protected int LoadedItemsCount;
        protected ShapefileConverter Shapefile;
        protected WorldCitiesViewModel WorldCitiesViewModel;
        protected GeographicProportionalSymbolSeries GeoSeries;
        protected BrushCollection SelectedBrushes = new BrushCollection();
      
        #region Event Handlers
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        { 
            // parse loaded data from DBF and SHP files
            foreach (var record in Shapefile)
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
                    if (city.Population > 0)
                    {
                        this.WorldCitiesViewModel.WorldCities.Add(city);
                    }
                }
            }
            this.WorldCitiesViewModel.SortCitiesBy("Population");
            InitializeGeoMap();
              
        }
        #endregion

        private void InitializeGeoMap()
        {
            if (this.WorldCitiesViewModel.WorldCities.Count > 0)
            {
                GeoSeries = this.GeoMap.Series["ProportionalSeries"] as GeographicProportionalSymbolSeries;
                if (GeoSeries != null)
                {
                    // bind geo-series to collection of world cities
                    GeoSeries.ItemsSource = this.WorldCitiesViewModel.WorldCitiesViewSource.View;
                    UpdateGeoSeries();
                }
            }

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class 
            
        }
        private void radiusMinSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }

        private void radiusMaxSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }
        private void fillScaleMinSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }

        private void fillScaleMaxSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }
        private void fillScaleIsLogarithmicCheckbox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGeoSeries();
        }  
        private void fillScaleBrushPaletteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var brushPalette = (ColorsPalette)e.AddedItems[0];

            this.SelectedBrushes = new BrushCollection();
            foreach (var color in brushPalette)
            {
                this.SelectedBrushes.Add(new SolidColorBrush(color));
            }
            UpdateGeoSeries();
        }
        private void UpdateGeoSeries()
        {
            if (GeoSeries != null)
            {
                // update radius scale  
                GeoSeries.RadiusScale.MinimumValue = System.Math.Round(this.RadiusMinSlider.Value);
                GeoSeries.RadiusScale.MaximumValue = System.Math.Round(this.RadiusMaxSlider.Value);
                // update fill scale  
                var scale = GeoSeries.FillScale as ValueBrushScale;
                if (scale != null)
                {
                    var isChecked = this.FillScaleIsLogarithmicCheckbox.IsChecked;
                    if (isChecked != null)
                        scale.IsLogarithmic = isChecked.Value;
                    scale.MinimumValue = System.Math.Round(this.FillScaleMinSlider.Value);
                    scale.MaximumValue = System.Math.Round(this.FillScaleMaxSlider.Value);
                    scale.Brushes = this.SelectedBrushes;
                }
            }
        }

    }
}
