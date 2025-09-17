using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGGeographicMap.Extensions
{
    public class GeoSeriesExtensions : DependencyObject
    {
        #region GeographicSymbolSeries.SymbolFillScale

        internal const string SymbolFillScalePropertyName = "SymbolFillScale";
        public static readonly DependencyProperty SymbolFillScaleProperty =
            DependencyProperty.RegisterAttached(SymbolFillScalePropertyName,
            typeof(GeoSymbolFillScale), typeof(GeoSeriesExtensions),
            new PropertyMetadata(null, (o, e) => OnSymbolFillScaleChanged(o as GeographicSymbolSeries,
                    e.OldValue as GeoSymbolFillScale,
                    e.NewValue as GeoSymbolFillScale)));

        public static GeoSymbolFillScale GetSymbolFillScale(DependencyObject target)
        {
            return target.GetValue(SymbolFillScaleProperty) as GeoSymbolFillScale;
        }
        public static void SetSymbolFillScale(DependencyObject target, GeoSymbolFillScale behavior)
        {
            target.SetValue(SymbolFillScaleProperty, behavior);
        }

        private static void OnSymbolFillScaleChanged(GeographicSymbolSeries geoSeries, GeoSymbolFillScale oldValue, GeoSymbolFillScale newValue)
        {
            if (geoSeries == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.OnDetach(geoSeries);
            }
            if (newValue != null)
            {
                newValue.OnAttach(geoSeries);
            }
        }
        #endregion
        #region GeographicSymbolSeries.SymbolFillScale

        internal const string SymbolRadiusScalePropertyName = "SymbolRadiusScale";
        public static readonly DependencyProperty SymbolRadiusScaleProperty =
            DependencyProperty.RegisterAttached(SymbolRadiusScalePropertyName,
            typeof(GeoSymbolRadiusScale), typeof(GeoSeriesExtensions),
            new PropertyMetadata(null, (o, e) => OnSymbolRadiusScaleChanged(o as GeographicSymbolSeries,
                    e.OldValue as GeoSymbolRadiusScale,
                    e.NewValue as GeoSymbolRadiusScale)));

        public static GeoSymbolRadiusScale GetSymbolRadiusScale(DependencyObject target)
        {
            return target.GetValue(SymbolRadiusScaleProperty) as GeoSymbolRadiusScale;
        }
        public static void SetSymbolRadiusScale(DependencyObject target, GeoSymbolRadiusScale behavior)
        {
            target.SetValue(SymbolRadiusScaleProperty, behavior);
        }

        private static void OnSymbolRadiusScaleChanged(GeographicSymbolSeries geoSeries, GeoSymbolRadiusScale oldValue, GeoSymbolRadiusScale newValue)
        {
            if (geoSeries == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.OnDetach(geoSeries);
            }
            if (newValue != null)
            {
                newValue.OnAttach(geoSeries);
            }
        }
        #endregion
    }
    public class GeoSymbolFillScale : GeoMarkerBrushScale
    {
        public GeoSymbolFillScale()
        {
            
        }
        //private XamGeographicMap _geoMap;
        //private GeographicSymbolSeries _geoSeries;

        public void OnAttach(GeographicSymbolSeries geoSeries)
        {
            //_geoSeries = geoSeries;
       
        }
        public void OnDetach(GeographicSymbolSeries geoSeries)
        {
            //geoMap.MouseEnter -= OnGeoMapMouseEnter;
     
        }
        public void OnAttach(XamGeographicMap geoMap)
        {
            //_geoMap = geoMap;
        }
        public void OnDetach(XamGeographicMap geoMap)
        {
            //geoMap.MouseEnter -= OnGeoMapMouseEnter;
        }
    }
    public class GeoSymbolRadiusScale : GeoMarkerSizeScale
    {
        public GeoSymbolRadiusScale()
        {

        }
        //public GeoSymbolRadiusScale(double min, double max)
        //{
        //    this.
        //}
        //private XamGeographicMap _geoMap;
        //private GeographicSymbolSeries _geoSeries;

        public void OnAttach(GeographicSymbolSeries geoSeries)
        {
            //_geoSeries = geoSeries;

        }
        public void OnDetach(GeographicSymbolSeries geoSeries)
        {
            //geoMap.MouseEnter -= OnGeoMapMouseEnter;

        }
        public void OnAttach(XamGeographicMap geoMap)
        {
            //_geoMap = geoMap;
        }
        public void OnDetach(XamGeographicMap geoMap)
        {
            //geoMap.MouseEnter -= OnGeoMapMouseEnter;
        }
    }
    
  
}