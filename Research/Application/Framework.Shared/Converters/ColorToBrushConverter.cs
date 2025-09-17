using System;
using System.Collections.Generic;
using System.Globalization; 
using System.Windows;

#if WPF
using System.Windows.Data;
using System.Windows.Media; 
#else
using Xamarin.Forms;
#endif

namespace Infragistics.Framework
{
    //TODO-DEV copy this Converter to project that implements BrushCollection (DV)
    // AKA PaletteConverter

    /// <summary>
    /// Converts Color, List<Color>, or Color[] object to SolidColorBrush/LinearGradientBrush 
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        public ColorToBrushConverter()
        {
            StartPoint = new Point(0, 0);
            EndPoint = new Point(1, 0);

            Orientation = GradientOrientation.Default;
        }
        /// <summary>
        /// Gets or sets EndPoint of Gradient Brush, used if passed value is IList<Color> 
        /// </summary>
        public Point StartPoint { get; set; }
        /// <summary>
        /// Gets or sets EndPoint of Gradient Brush, used if passed value is IList<Color> 
        /// </summary>
        public Point EndPoint { get; set; }
        /// <summary>
        /// Gets or sets Orientation of Gradient Brush, used if passed value is IList<Color> 
        /// </summary>
        public GradientOrientation Orientation { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = null;
            if (value is Color)
            {
                var color = (Color)value;
                brush = new SolidColorBrush((Color)value);
            }
            else if (value is Color[])
            {
                var colors = (Color[])value;
                brush = colors.ToBrush(this.StartPoint, this.EndPoint);
            }
            else if (value is IList<Color>)
            {
                var colors = value as IList<Color>;
                if (Orientation != GradientOrientation.Default)
                    brush = colors.ToBrush(this.Orientation);
                else
                    brush = colors.ToBrush(this.StartPoint, this.EndPoint);
            }
            else if (value is IList<SolidColorBrush>)
            {
                var brushes = value as IList<SolidColorBrush>;
                if (Orientation != GradientOrientation.Default)
                    brush = brushes.ToBrush(this.Orientation);
                else
                    brush = brushes.ToBrush(this.StartPoint, this.EndPoint);
            }
            if (brush == null)
            {
                throw new NotSupportedException("Type " + value.GetType().Name);
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
