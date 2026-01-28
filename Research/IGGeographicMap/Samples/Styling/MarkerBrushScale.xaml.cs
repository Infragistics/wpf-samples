using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGGeographicMap.Models;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Controls;
using System;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace IGGeographicMap.Samples.Styling
{
    public partial class MarkerBrushScale : Infragistics.Samples.Framework.SampleContainer
    {
        public MarkerBrushScale()
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
        protected BrushCollection SelectedBrushes = new BrushCollection();
       
        #region Event Handlers
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // filtering ShapefileRecord objects
            var dataSource = Shapefile.Where(i => double.Parse(i.Fields["magnitude"].ToString()) > 5.5).ToList();
            // sort ShapefileRecord objects
            dataSource.OrderByDescending(x => x.Fields["magnitude"]);
            
            this.GeoMap.DataContext = dataSource;
            // zoom in geo-map to map view without polar regions
            this.GeoMap.WindowRect = MapViews.WorldNonPolarMapView.WindowRect;
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
            foreach (Color color in brushPalette)
            {
                this.SelectedBrushes.Add(new SolidColorBrush(color));
            }
            UpdateGeoSeries();
        }
        #endregion
        private void UpdateGeoSeries()
        {
            if (!this.IsSampleLoaded) return;
            if (this.GeoMap == null) return;
            if (this.GeoMap.Series == null) return;
           
            var geoSeries = this.GeoMap.Series[0] as GeographicProportionalSymbolSeries;
            if (geoSeries != null)
            {
                // update fill scale  
                var scale = geoSeries.FillScale as ValueBrushScale;
                if (scale != null)
                {
                    scale.MinimumValue = System.Math.Round(this.fillScaleMinSlider.Value);
                    scale.MaximumValue = System.Math.Round(this.fillScaleMaxSlider.Value);
                    scale.Brushes = this.SelectedBrushes;
                }
            }
        }
    }
   
}
