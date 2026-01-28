using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Infragistics.Samples.Controls
{
    public class ColorPalleteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colors = value as IList<Color>;
            if (colors != null)
            {
                return new SolidColorBrush((Color)value);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush != null)
            {
                return brush.Color;
            }
            return null;
        }
    }

    public class BrushPalleteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brushes = new BrushPalette();
            var collection = value as BrushCollection;
            if (collection != null)
            {
                System.Diagnostics.Debug.WriteLine("Convert " + collection.Count);

                foreach (var item in collection)
                {
                    brushes.Add(item);
                }
            }
            return brushes;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new BrushCollection();
            var brushes = value as IList<Brush>;
            if (brushes != null)
            {
                System.Diagnostics.Debug.WriteLine("ConvertBack " + brushes.Count);

                foreach (var item in brushes)
                {
                    collection.Add(item);
                }
            }
            return collection;
        }
    }
}
