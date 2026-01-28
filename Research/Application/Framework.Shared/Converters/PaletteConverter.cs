using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq; 
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Infragistics.Framework
{
    //TODO-DEV copy this Converter to project that implements BrushCollection (DV?)
    
    public class PaletteConverter : IValueConverter
    {
        public PaletteConverter()
        {
            StartPoint = new Point(0, 1);
            EndPoint = new Point(1, 0);
        }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }


        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colors = value as Collection<Color>;
            if (colors != null)
                return CreateBrush(colors);

            var brushes = value as Collection<SolidColorBrush>;
            if (brushes != null)
                return CreateBrush(brushes.Select(b => b.Color).ToList());

            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
         
        public LinearGradientBrush CreateBrush(IList<Color> colors)
        {
            var gradient = new LinearGradientBrush();
            gradient.EndPoint = this.EndPoint;
            gradient.StartPoint = this.StartPoint;

            var offset = 0.0;
            var step = 1.0 / colors.Count();
            foreach (var color in colors)
            {
                var stop = new GradientStop {Color = color, Offset = offset};
                gradient.GradientStops.Add(stop);

                offset += step;
            }
            return gradient;
        }
    }
    
}