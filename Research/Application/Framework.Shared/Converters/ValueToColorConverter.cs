
using System;
using System.Collections.Generic;
using System.Globalization;

#if Xamarin
using Xamarin.Forms;
#else
using System.Windows;
#endif

namespace Infragistics.Framework
{ 
    public class ValueToColorConverter : IValueConverter
    {
        public ValueToColorConverter()
        {
            ThresholdValue = 0;
            ThresholdBelow = Color.FromHex("#FFC91C1C");  
            ThresholdAbove = Color.FromHex("#FF1FC11F");
        }
        /// <summary> Gets or sets Threshold Value </summary>
        public double ThresholdValue { get; set; }

        /// <summary> Gets or sets Color below Threshold Value </summary>
        public Color ThresholdBelow { get; set; }
        /// <summary> Gets or sets Color above Threshold Value </summary>
        public Color ThresholdAbove { get; set; }
        
        #region IValueConverter Members 
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.Black;

            var threshold = ThresholdValue;
            if (parameter == null || !double.TryParse(parameter.ToString(), out threshold))
            {
                threshold = ThresholdValue;
            }

            double actual;
            if (double.TryParse(value.ToString(), out actual))
            {
                if (actual >= threshold)
                    return this.ThresholdAbove;
                
                return this.ThresholdBelow;
            }
            return Colors.Black;
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

    public class ValueToBrushConverter : IValueConverter
    {
        public ValueToBrushConverter()
        {
            ThresholdValue = 0;
            ThresholdBelow = Color.FromHex("#FFC91C1C");//.ToSolidBrush();
            ThresholdAbove = Color.FromHex("#FF1FC11F");//.ToSolidBrush();
        }
        /// <summary> Gets or sets Threshold Value </summary>
        public double ThresholdValue { get; set; }

        /// <summary> Gets or sets Color below Threshold Value </summary>
        public Color ThresholdBelow { get; set; }
        /// <summary> Gets or sets Color above Threshold Value </summary>
        public Color ThresholdAbove { get; set; }

        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Color.Black.ToSolidBrush();

            var threshold = ThresholdValue;
            if (parameter == null || !double.TryParse(parameter.ToString(), out threshold))
            {
                threshold = ThresholdValue;
            }

            double actual;
            if (double.TryParse(value.ToString(), out actual))
            {
                if (actual >= threshold)
                    return this.ThresholdAbove.ToSolidBrush();

                return this.ThresholdBelow.ToSolidBrush();
            }
            return Color.Black.ToSolidBrush();
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