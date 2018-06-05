using System.ComponentModel;
using System.Windows;

namespace IGExtensions.Common.Maps.Behaviors
{
    /// <summary>
    /// Represents a size scale for an element
    /// </summary>
    public class GeoMarkerSizeScale : DependencyObject, INotifyPropertyChanged
    {
        public GeoMarkerSizeScale()
        {
            
        }
        #region MinimumValue Dependency Property

        internal const string MinimumValuePropertyName = "MinimumValue";

        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register(MinimumValuePropertyName, typeof(double), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(1.0, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(MinimumValuePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the minimum value for this scale.
        /// </summary>
        public double MinimumValue
        {
            get
            {
                return (double)GetValue(MinimumValueProperty);
            }
            set
            {
                SetValue(MinimumValueProperty, value);
            }
        }
        #endregion

        #region MaximumValue Dependency Property
        internal const string MaximumValuePropertyName = "MaximumValue";

        /// <summary>
        /// Identifies the MaximumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register(MaximumValuePropertyName, typeof(double), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(100.0, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(MaximumValuePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum value for this scale.
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return (double)GetValue(MaximumValueProperty);
            }
            set
            {
                SetValue(MaximumValueProperty, value);
            }
        }
        #endregion

        #region MinimumValue Dependency Property

        internal const string MinimumSizePropertyName = "MinimumSize";

        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumSizeProperty =
            DependencyProperty.Register(MinimumSizePropertyName, typeof(double), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(10.0, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(MinimumSizePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the minimum size for this scale.
        /// </summary>
        public double MinimumSize
        {
            get
            {
                return (double)GetValue(MinimumSizeProperty);
            }
            set
            {
                SetValue(MinimumSizeProperty, value);
            }
        }
        #endregion

        #region MaximumValue Dependency Property
        internal const string MaximumSizePropertyName = "MaximumSize";

        /// <summary>
        /// Identifies the MaximumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumSizeProperty =
            DependencyProperty.Register(MaximumSizePropertyName, typeof(double), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(10.0, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(MaximumSizePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum size for this scale.
        /// </summary>
        public double MaximumSize
        {
            get
            {
                return (double)GetValue(MaximumSizeProperty);
            }
            set
            {
                SetValue(MaximumSizeProperty, value);
            }
        }
        #endregion
        
        #region IsLogarithmic Dependency Property
        internal const string IsLogarithmicPropertyName = "IsLogarithmic";

        /// <summary>
        /// Identifies the IsLogarithmic dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLogarithmicProperty =
            DependencyProperty.Register(IsLogarithmicPropertyName, typeof(bool), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(false, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(IsLogarithmicPropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets whether the scale is logarithmic.
        /// </summary>
        public bool IsLogarithmic
        {
            get
            {
                return (bool)GetValue(IsLogarithmicProperty);
            }
            set
            {
                SetValue(IsLogarithmicProperty, value);
            }
        }
        #endregion

        #region LogarithmBase Dependency Property
        internal const string LogarithmBasePropertyName = "LogarithmBase";

        /// <summary>
        /// Identifies the LogarithmBase dependency property.
        /// </summary>
        public static readonly DependencyProperty LogarithmBaseProperty =
            DependencyProperty.Register(LogarithmBasePropertyName, typeof(int), typeof(GeoMarkerSizeScale),
            new PropertyMetadata(10, (o, e) =>
            {
                (o as GeoMarkerSizeScale).OnPropertyChanged(LogarithmBasePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the logarithm base for this scale.
        /// </summary>
        public int LogarithmBase
        {
            get
            {
                return (int)GetValue(LogarithmBaseProperty);
            }
            set
            {
                SetValue(LogarithmBaseProperty, value);
            }
        }
        #endregion

        #region Methods
        public virtual double GetScaledValue(double value)
        {
            double min = double.IsNaN(this.MinimumValue) || double.IsInfinity(this.MinimumValue) ? 0 : this.MinimumValue;
            double max = double.IsNaN(this.MaximumValue) || double.IsInfinity(this.MaximumValue) ? 100 : this.MaximumValue;

            if (value < min)
            {
                value = min;
            }

            if (value > max)
            {
                value = max;
            }

            return GetLogarithmicScaledValue(min, max, value);
        }
        protected double GetLogarithmicScaledValue(double min, double max, double value)
        {
            if (this.IsLogarithmic && this.LogarithmBase > 0)
            {
                double newMin = System.Math.Log(min, this.LogarithmBase);
                double newMax = System.Math.Log(max, this.LogarithmBase);
                double newValue = System.Math.Log(value, this.LogarithmBase);

                return GetLinearScaledValue(newMin, newMax, newValue);
            }

            return GetLinearScaledValue(min, max, value);
        }
        protected double GetLinearScaledValue(double min, double max, double value)
        {
            //if the value is outside the range, return a null brush
            if (value < min || value > max) return double.NaN;

            double scaledValue = (value - min) / (max - min);
            return scaledValue;
        }
        public double GetScaledSize(double value)
        {
            double min = double.IsNaN(this.MinimumValue) || double.IsInfinity(this.MinimumValue) ? 0 : this.MinimumValue;
            double max = double.IsNaN(this.MaximumValue) || double.IsInfinity(this.MaximumValue) ? 10 : this.MaximumValue;

            double smallSize = double.IsNaN(this.MinimumSize) || double.IsInfinity(this.MinimumSize) ? 10 : this.MinimumSize;
            double largeSize = double.IsNaN(this.MaximumSize) || double.IsInfinity(this.MaximumSize) ? 50 : this.MaximumSize;

            if (value < min) value = min;
            if (value > max) value = max;

            if (this.IsLogarithmic && this.LogarithmBase > 0)
            {
               int logBase = this.LogarithmBase;
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
            double result = smallSize + ((largeSize - smallSize) / (max - min)) * (value - min);
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
            double newValue = System.Math.Log(value, logBase);
            double newMin = System.Math.Log(min, logBase);
            double newMax = System.Math.Log(max, logBase);
            return GetLinearSize(newMin, newMax, smallSize, largeSize, newValue);
        }

        #endregion

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Occurs when a property (including "effective" and non-dependency property) value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed and updated events.
        /// </summary>
        /// <param name="name">The name of the property being changed.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        public void OnPropertyChanged(string name, object oldValue, object newValue)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
   
}