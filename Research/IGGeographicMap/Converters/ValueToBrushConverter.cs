using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using Infragistics;
using Infragistics.Controls.Charts;

namespace IGGeographicMap.Converters
{
    public class ValueToBrushConverter : IValueConverter
    {
        public ValueToBrushConverter()
        {
            Scale = new LinearScale { MinimumValue = 1, MaximumValue = 100 };
            
            _brushes = new BrushCollection
            {
                new SolidColorBrush(Colors.White),
                new SolidColorBrush(Colors.Red)
            };
        }
        public LinearScale Scale { get; set; }
       
        private BrushCollection _brushes;

        public BrushCollection Brushes
        {
            get { return _brushes; }
            set
            {
                _brushes = new BrushCollection();
                if (value != null)
                {
                    foreach (Brush brush in value)
                    {
                        _brushes.Add(brush);
                    }
                }
               
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Colors.Blue;

            return Scale.GetScaledBrush((double)value, Brushes);
            //return BrushScale.GetScaledBrush((double)value, Brushes);

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }
    
   
    public abstract class ValueScale
    {
        /// <summary>
        /// Sets or gets the Minimum value for this scale.
        /// </summary>
        public virtual double MinimumValue { get; set; }

        /// <summary>
        /// Sets or gets the Maximum value for this scale.
        /// </summary>
        public virtual double MaximumValue { get; set; }
        public virtual DoubleCollection ValueStops { get; set; }

        internal abstract double GetUnscaledValue(double v);
        /// <summary>
        /// Gets a scaled value from the parameter value. 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal abstract double GetScaledValue(double v);
        /// <summary>
        /// Gets a scaled brush from the parameter value.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="brushes"></param>
        /// <returns></returns>
        internal abstract Brush GetScaledBrush(double v, BrushCollection brushes);

    }
    public sealed class LinearScale : ValueScale
    {
        public LinearScale()
        {

            MinimumValue = 0;
            MaximumValue = 100;
        }

        internal override double GetUnscaledValue(double v)
        {
            return MathTool.Clamp(MinimumValue + v * (MaximumValue - MinimumValue), MinimumValue, MaximumValue);
        }
        internal override double GetScaledValue(double v)
        {
            if (double.IsNaN(v) || double.IsInfinity(v))
            {
                return v;
            }

            return MathUtil.Clamp((v - MinimumValue) / (MaximumValue - MinimumValue), 0.0, 1.0);
        }
        internal override Brush GetScaledBrush(double v, BrushCollection brushes)
        {
            double index = GetScaledValue(v) * (brushes.Count - 1);
            return index > brushes.Count ?
                new SolidColorBrush(Colors.Purple) :
                brushes[GetScaledValue(v) * (brushes.Count - 1)];
        }
    }
    public static class MathTool
    {
        /// <summary>
        /// Returns the specified value clamped to the specified range.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minimum">Range minimum.</param>
        /// <param name="maximum">Range maximum.</param>
        /// <returns>Clamped value.</returns>
        public static double Clamp(double value, double minimum, double maximum)
        {
            return System.Math.Min(maximum, System.Math.Max(minimum, value));
        }
    }


    
}