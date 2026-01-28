using System;
using System.Globalization;
using System.Windows.Data;
using IGGeographicMap.Extensions;

namespace IGGeographicMap.Converters
{
    public class GeoSymbolSizeConverter : GeoMarkerSizeScale, IValueConverter
    {
        public GeoSymbolSizeConverter()
        {
            //Scale = new LinearScale { MinimumValue = 1, MaximumValue = 100 };
            this.MinimumValue = 1;
            this.MaximumValue = 10;
        }
        //public LinearScale Scale { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return this.MinimumValue;
            double newValue = this.GetScaledSize((double)value);
            return newValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }
    public class GeoSymbolSizeConverter2 : IValueConverter
    {
        public GeoSymbolSizeConverter2()
        {
            Scale = new LinearScale { MinimumValue = 1, MaximumValue = 100 };

        }
        public LinearScale Scale { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Scale.MinimumValue;

            return Scale.GetScaledValue((double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }
}