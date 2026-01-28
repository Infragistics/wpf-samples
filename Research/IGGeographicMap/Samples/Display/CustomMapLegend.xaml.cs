using System.Collections.ObjectModel;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class CustomMapLegend : Infragistics.Samples.Framework.SampleContainer
    {
        public CustomMapLegend()
        {
            InitializeComponent();

            ShapefileSource = (ShapefileConverter)this.Resources["shapeCitiesSource"];
            ShapefileSource.ImportCompleted += OnShapefileImportCompleted;
        
            WorldCities = new ObservableCollection<WorldCityModel>();
             
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }
        protected ShapefileConverter ShapefileSource;
        protected ObservableCollection<WorldCityModel> WorldCities;
        
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var shapeFileConverter = (ShapefileConverter)sender;
            // parse loaded data from DBF and SHP files
            foreach (ShapefileRecord record in shapeFileConverter)
            {
                var city = new WorldCityModel();
                if (record.Fields != null)
                {
                    // get geo-location of a city from shape file (SHP)
                    city.Longitude = record.Points[0][0].X;
                    city.Latitude = record.Points[0][0].Y;
                    // get data about a city from database file (DBF)
                    city.Population = (double)(record.Fields["POPULATION"]);
                    city.CountryName = (string)(record.Fields["COUNTRY"]);
                    city.CityName = (string)(record.Fields["NAME"]);

                    // add a city to the view model
                    this.WorldCities.Add(city);
                }
            }

            // bind geo-symbol series to collection of world cities
            if (this.WorldCities.Count > 0)
            {
                var minorCities = (WorldCitiesViewModel) this.Resources["MinorCities"];
                minorCities.WorldCities = this.WorldCities;
                this.GeoMap.Series["MinorCitiesSymbolSeries"].ItemsSource = minorCities.WorldCitiesViewSource.View;
        
                var majorCities = (WorldCitiesViewModel)this.Resources["MajorCities"];
                majorCities.WorldCities = this.WorldCities;
                this.GeoMap.Series["MajorCitiesSymbolSeries"].ItemsSource = majorCities.WorldCitiesViewSource.View;
        
                var biggestCities = (WorldCitiesViewModel)this.Resources["BiggestCities"];
                biggestCities.WorldCities = this.WorldCities;
                this.GeoMap.Series["BiggestCitiesSymbolSeries"].ItemsSource = biggestCities.WorldCitiesViewSource.View;
            }

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
        }
       
    }
   

  
}
