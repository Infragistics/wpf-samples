using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Linq;
using System;
using System.Globalization;
using System.Threading;

namespace IGGeographicMap.Samples.Styling
{
    public partial class MarkerSizeScale : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkerSizeScale()
        {
            InitializeComponent();
            
            var path = "/Infragistics.Samples.Services;component/shapefiles/world/";

            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            
            Shapefile = new ShapefileConverter();
            Shapefile.ImportCompleted += OnShapefileImportCompleted;
            Shapefile.ShapefileSource = new Uri(path + "earthquakes-usgs.shp", UriKind.RelativeOrAbsolute);
            Shapefile.DatabaseSource  = new Uri(path + "earthquakes-usgs.dbf", UriKind.RelativeOrAbsolute);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        protected ShapefileConverter Shapefile;  

        #region Event Handlers
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // filtering ShapefileRecord objects
            var dataSource = Shapefile.Where(i => double.Parse(i.Fields["magnitude"].ToString()) > 5.5).ToList();
            // sort ShapefileRecord objects
            dataSource.OrderByDescending(x => x.Fields["magnitude"]);
            

            this.GeoMap.DataContext = dataSource;

            // update radius scale with a new Minimum value  
            UpdateGeoSeries();

            // zooming geo-map to a specific geographic region of the world using GeoMapAdapter
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion);
        }                 
        #endregion
              
        private void radiusMinSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }

        private void radiusMaxSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeries();
        }
        private void UpdateGeoSeries()
        {
            if (SymbolSeries != null &&
                radiusMinSlider != null &&
                radiusMaxSlider != null)
            {
                // update radius scale  
                SymbolSeries.RadiusScale.MinimumValue = System.Math.Round(this.radiusMinSlider.Value);
                SymbolSeries.RadiusScale.MaximumValue = System.Math.Round(this.radiusMaxSlider.Value);
            }
        }
    }


    
}

