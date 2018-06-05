using System.Windows;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGExtensions.Common.Scales
{
    public class ValueScale : ObservableObject
    {
        protected const double DefaultMinimum = 0.0;
        protected const double DefaultMaximum = 100.0;
        protected const int DefaultLogarithmBase = 10;

        #region MinimumValue Dependency Property
        internal const string MinimumValuePropertyName = "MinimumValue";
        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register(MinimumValuePropertyName, typeof(double), typeof(ValueScale),
            new PropertyMetadata(DefaultMinimum, (o, e) =>
            {
                (o as ValueScale).OnPropertyChanged(MinimumValuePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the minimum value for this scale.
        /// </summary>
        public double MinimumValue
        {
            get { return (double)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }
        #endregion

        #region MaximumValue Dependency Property
        internal const string MaximumValuePropertyName = "MaximumValue";
        /// <summary>
        /// Identifies the MaximumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register(MaximumValuePropertyName, typeof(double), typeof(ValueScale),
            new PropertyMetadata(DefaultMaximum, (o, e) =>
            {
                (o as ValueScale).OnPropertyChanged(MaximumValuePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum value for this scale.
        /// </summary>
        public double MaximumValue
        {
            get { return (double)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }
        #endregion

        #region IsLogarithmic Dependency Property
        internal const string IsLogarithmicPropertyName = "IsLogarithmic";

        /// <summary>
        /// Identifies the IsLogarithmic dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLogarithmicProperty =
            DependencyProperty.Register(IsLogarithmicPropertyName, typeof(bool), typeof(ValueScale),
            new PropertyMetadata(false, (o, e) =>
            {
                (o as ValueScale).OnPropertyChanged(IsLogarithmicPropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets whether the scale is logarithmic.
        /// </summary>
        public bool IsLogarithmic
        {
            get { return (bool)GetValue(IsLogarithmicProperty); }
            set { SetValue(IsLogarithmicProperty, value); }
        }
        #endregion

        #region LogarithmBase Dependency Property
        internal const string LogarithmBasePropertyName = "LogarithmBase";

        /// <summary>
        /// Identifies the LogarithmBase dependency property.
        /// </summary>
        public static readonly DependencyProperty LogarithmBaseProperty =
            DependencyProperty.Register(LogarithmBasePropertyName, typeof(int), typeof(ValueScale),
            new PropertyMetadata(DefaultLogarithmBase, (o, e) =>
            {
                (o as ValueScale).OnPropertyChanged(LogarithmBasePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the logarithm base for this scale.
        /// </summary>
        public int LogarithmBase
        {
            get { return (int)GetValue(LogarithmBaseProperty); }
            set { SetValue(LogarithmBaseProperty, value); }
        }
        #endregion

        #region Methods
        public virtual double GetScaledValue(double value)
        {
            var min = double.IsNaN(this.MinimumValue) || double.IsInfinity(this.MinimumValue) ? DefaultMinimum : this.MinimumValue;
            var max = double.IsNaN(this.MaximumValue) || double.IsInfinity(this.MaximumValue) ? DefaultMaximum : this.MaximumValue;

            if (value < min) value = min;
            if (value > max) value = max;

            return GetLogarithmicScaledValue(min, max, value);
        }
        protected double GetLogarithmicScaledValue(double min, double max, double value)
        {
            if (this.IsLogarithmic && this.LogarithmBase > 0)
            {
                var newMin = System.Math.Log(min, this.LogarithmBase);
                if (double.IsNegativeInfinity(newMin)) newMin = min;
                var newMax = System.Math.Log(max, this.LogarithmBase);
                if (double.IsNegativeInfinity(newMax)) newMax = max;
                var newValue = System.Math.Log(value, this.LogarithmBase);
                if (double.IsNegativeInfinity(newValue)) newValue = min;

                return GetLinearScaledValue(newMin, newMax, newValue);
            }
            return GetLinearScaledValue(min, max, value);
        }
        protected double GetLinearScaledValue(double min, double max, double value)
        {
            //if the value is outside the range, return a null brush
            //if (value < min || value > max) return double.NaN;
            if (value < min) return min;
            if (value > max) return max;

            var scaledValue = (value - min) / (max - min);
            return scaledValue;
        }
        #endregion
    }
    public class SizeScale : ValueScale
    {
        protected const double DefaultMinimumSize = 10.0;
        protected const double DefaultMaximumSize = 50.0;

        public SizeScale()
        {

        }

        #region MinimumSize Dependency Property

        internal const string MinimumSizePropertyName = "MinimumSize";
        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumSizeProperty =
            DependencyProperty.Register(MinimumSizePropertyName, typeof(double), typeof(SizeScale),
            new PropertyMetadata(DefaultMinimumSize, (o, e) =>
            {
                (o as SizeScale).OnPropertyChanged(MinimumSizePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the minimum size for this scale.
        /// </summary>
        public double MinimumSize
        {
            get { return (double)GetValue(MinimumSizeProperty); }
            set { SetValue(MinimumSizeProperty, value); }
        }
        #endregion

        #region MaximumSize Dependency Property
        internal const string MaximumSizePropertyName = "MaximumSize";
        /// <summary>
        /// Identifies the MaximumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumSizeProperty =
            DependencyProperty.Register(MaximumSizePropertyName, typeof(double), typeof(SizeScale),
            new PropertyMetadata(DefaultMaximumSize, (o, e) =>
            {
                (o as SizeScale).OnPropertyChanged(MaximumSizePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum size for this scale.
        /// </summary>
        public double MaximumSize
        {
            get { return (double)GetValue(MaximumSizeProperty); }
            set { SetValue(MaximumSizeProperty, value); }
        }
        #endregion

        #region Methods

        public double GetScaledSize(double value)
        {
            var min = double.IsNaN(this.MinimumValue) || double.IsInfinity(this.MinimumValue) ? 0 : this.MinimumValue;
            var max = double.IsNaN(this.MaximumValue) || double.IsInfinity(this.MaximumValue) ? 10 : this.MaximumValue;

            var smallSize = double.IsNaN(this.MinimumSize) || double.IsInfinity(this.MinimumSize) ? 10 : this.MinimumSize;
            var largeSize = double.IsNaN(this.MaximumSize) || double.IsInfinity(this.MaximumSize) ? 50 : this.MaximumSize;

            if (value < min) value = min;
            if (value > max) value = max;

            if (this.IsLogarithmic && this.LogarithmBase > 0)
            {
                var logBase = this.LogarithmBase;
                //return GetLogarighmicSize(min, max, min, max, logBase, value);
                return GetLogarighmicSize(min, max, smallSize, largeSize, logBase, value);
            }

            return GetLinearSize(min, max, smallSize, largeSize, value);
        }
        /// <summary>
        /// Returns the a marker size for a given value based on a linear scale.
        /// </summary>
        internal static double GetLinearSize(double min, double max, double smallSize, double largeSize, double value)
        {
            //smaller than min size or invalid
            if (value <= min || double.IsNaN(value) || double.IsInfinity(value))
            {
                return smallSize;
            }

            if (value >= max)
            {
                return largeSize;
            }
            //double result = Clamp((value - min) / (max - min), min, max);
            //double result = Clamp((value - min) / (max - min), 0.0, 1.0);
            var result = smallSize + ((largeSize - smallSize) / (max - min)) * (value - min);
            return result;
        }
        public static double Clamp(double value, double minimum, double maximum)
        {
            return System.Math.Min(maximum, System.Math.Max(minimum, value));
        }
        /// <summary>
        /// Returns the marker size for a given value based on a logarithmic scale.
        /// </summary>
        internal static double GetLogarighmicSize(double min, double max, double smallSize, double largeSize, double logBase, double value)
        {
            var newValue = System.Math.Log(value, logBase);
            var newMin = System.Math.Log(min, logBase);
            if (double.IsNegativeInfinity(newMin)) newMin = min;
            var newMax = System.Math.Log(max, logBase);
            if (double.IsNegativeInfinity(newMax)) newMax = max;

            return GetLinearSize(newMin, newMax, smallSize, largeSize, newValue);
        }

        #endregion

    }

    //TODO add ColorScale and ColorValueScale from ColorScaleSwatchPane
}