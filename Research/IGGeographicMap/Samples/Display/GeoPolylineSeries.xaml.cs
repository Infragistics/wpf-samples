using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class GeoPolylineSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoPolylineSeries()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {   
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.UnitedStatesLower48Region);   
        }
        
        private void ShapeResolutionSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GeoMap != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicPolylineSeries>().First();
                series.Resolution = e.NewValue;
            }
        }

        private void ShapeFilterSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GeoMap != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicPolylineSeries>().First();
                series.ShapeFilterResolution = e.NewValue;
            }
        }

        private void ShapeThicknessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GeoMap != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicPolylineSeries>().First();
                series.Thickness = e.NewValue;
            }

        }
    }
}
