using System;
using System.Globalization;

#if Xamarin
using Xamarin.Forms;
using Colors = Xamarin.Forms.Colors;
using SolidColorBrush = Infragistics.Core.Graphics.SolidColorBrush;
using LinearGradientBrush = Infragistics.Core.Graphics.LinearGradientBrush;
#else
using System.Windows.Media;
#endif

namespace Infragistics.Framework
{
    public class BooleanToColorConverter : IValueConverter
    {
        public BooleanToColorConverter()
        {
            FalseColor = Colors.Gray; // Paints.Gray.ToColor();
            TrueColor = Colors.DodgerBlue;
        }

        /// <summary> Gets or sets TrueColor </summary>
        public Color TrueColor { get; set; }
        /// <summary> Gets or sets FalseColor </summary>
        public Color FalseColor { get; set; }

        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new Exception("Cannot convert null value to a Color");

            var isTrue = value.ToString().ToLower() == "true";
            return isTrue ? TrueColor : FalseColor;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class BrushToColorConverter : IValueConverter
    {  
        #region IValueConverter Members 
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.Transparent;
            }

            var solid = value as SolidColorBrush;
            if (solid != null)
            {
                var c = solid.Color.ToString();
                return Color.FromHex(solid.Color.ToString());
            }

            var gradient = value as LinearGradientBrush;
            if (gradient != null)
            {
                var c = gradient.GradientStops[0].Color;
                return Color.FromHex(c.ToString()); 
            }
            return Color.Red;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object. This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class RectConverter : IValueConverter
    {          
        #region IValueConverter Members
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new Exception("Cannot convert null value to a string");
             
            if (value is Infragistics.XamarinForms.Rect)
            {
                var rect = (Infragistics.XamarinForms.Rect)value;
                return rect.Left.ToString("L=0.00, ") + rect.Top.ToString("T=0.00, ") + rect.Right.ToString("R=0.00, ") + rect.Bottom.ToString("B=0.00");
            }             
            return value.GetType().Name;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class PointConverter : IValueConverter
    {
        #region IValueConverter Members
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new Exception("Cannot convert null value to a string");

            if (value is Point)
            {
                var point = (Point)value;
                return point.X.ToString("X=0.00, ") + point.Y.ToString("Y=0.00");
            }
            return value.GetType().Name;
        }
        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}