using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Infragistics.Framework
{
    /// <summary>
    /// Converts Color, List<Color>, or Color[] object to DiffuseMaterial 
    /// </summary>
    public class ColorToMaterialConverter : ColorToBrushConverter
    {
        public new object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = base.Convert(value, targetType, parameter, culture) as Brush;
            if (brush == null)
            {
                throw new NotSupportedException("Type " + value.GetType().Name);
            }

            return new DiffuseMaterial(brush);
        }
        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
 }
