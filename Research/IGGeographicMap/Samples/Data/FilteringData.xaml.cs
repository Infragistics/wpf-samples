using System;
using System.Collections.Specialized;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Globalization;
using System.Threading;

namespace IGGeographicMap.Samples.Data
{
    public partial class FilteringData : Infragistics.Samples.Framework.SampleContainer
    {
        public FilteringData()
        {
            InitializeComponent();
            this.GeoMap.EnableMouseWheelZoomOnMapHover();

            this.EarthQuakeMapViewModel = new EarthQuakeMapViewModel();
            this.DataContext = EarthQuakeMapViewModel;

            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImported;
            Shapefile.ShapefileSource = new Uri(path + "earthquakes-usgs.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "earthquakes-usgs.dbf", UriKind.RelativeOrAbsolute);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

        }                

        protected ShapefileConverter Shapefile;
        protected EarthQuakeMapViewModel EarthQuakeMapViewModel;
                
        private void OnShapefileImported(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {             
            // parse loaded data from .DBF file
            foreach (ShapefileRecord record in Shapefile)
            {
                // filtering some ShapefileRecord objects
                if (record.Fields != null && double.Parse(record.Fields["magnitude"].ToString()) > 5.5)
                {
                    var earthQuakeItem = new EarthQuakeViewModel();
                    earthQuakeItem.Points = record.Points;

                    earthQuakeItem.EarthQuakeData.Magnitude = (double)(record.Fields["magnitude"]);
                    earthQuakeItem.EarthQuakeData.Depth = (double)(record.Fields["depth"]);
                    earthQuakeItem.EarthQuakeData.Longitude = (double)(record.Fields["lon"]);
                    earthQuakeItem.EarthQuakeData.Latitude = (double)(record.Fields["lat"]);
                    earthQuakeItem.EarthQuakeData.Name = (string)(record.Fields["region"]);

                    // add loaded EarthQuakeData to the view model
                    this.EarthQuakeMapViewModel.EarthQuakeData.Add(earthQuakeItem);
                }
            }
            if (this.EarthQuakeMapViewModel.EarthQuakeData.Count > 0)
            {
                this.GeoMap.Series[0].ItemsSource = this.EarthQuakeMapViewModel.EarthQuakeFilteredData.View;
                this.EarthQuakeMapViewModel.EarthQuakeFilteredData.View.Refresh();
            }

            // zoom in geo-map a specific geographic region of the world using GeoMapAdapter class
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion);  
            
        }
       
      
    }

  

}
