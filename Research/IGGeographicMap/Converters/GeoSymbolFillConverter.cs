using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using IGGeographicMap.Extensions;

namespace IGGeographicMap.Converters
{
    public class GeoSymbolFillConverter : GeoMarkerBrushScale, IValueConverter
    {
        public GeoSymbolFillConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.Blue;

            return this.GetBrushByValue((double)value); // Scale.GetScaledBrush((double)value, Brushes));
            //return BrushScale.GetScaledBrush((double)value, Brushes);

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

        //private XamGeographicMap _geoMap;
        //public void OnAttach(XamGeographicMap geoMap)
        //{
        //    _geoMap = geoMap;
        //    //geoMap.MouseEnter += OnGeoMapMouseEnter;
        //}
        //public void OnDetach(XamGeographicMap geoMap)
        //{
        //    //geoMap.MouseEnter -= OnGeoMapMouseEnter;

        //    //MakeVisible(geoMap, _vertical);
        //    //MakeVisible(geoMap, _horizontal);
        //    //_vertical = null;
        //    //_horizontal = null;
        //    //_first = true;
        //}
    }
}
    