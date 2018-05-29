using System.Windows;

namespace Infragistics.Controls.Charts
{
    public class ValueBrushScaleView : ValueBrushScale, IScaleViewRange
    {
        public ValueBrushScaleView()
        {
            ValueStringFormat = "0.0";
            MinimumValue = DefaultMinimumRange;
            MaximumValue = DefaultMaximumRange;
        }
        public string ValueStringFormat { get; set; }
        #region MinimumRange Dependency Property
        protected const double DefaultMinimumRange = 0.0;
        protected const double DefaultMaximumRange = 100.0;
     
        internal const string MinimumRangePropertyName = "MinimumRange";
        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumRangeProperty =
            DependencyProperty.Register(MinimumRangePropertyName, typeof(double), typeof(ValueBrushScaleView),
            new PropertyMetadata(DefaultMinimumRange, (o, e) =>
            {
                (o as ValueBrushScaleView).RaisePropertyChanged(MinimumRangePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the minimum size for this scale.
        /// </summary>
        public double MinimumRange
        {
            get { return (double)GetValue(MinimumRangeProperty); }
            set { SetValue(MinimumRangeProperty, value); }
        }
        #endregion
        #region MaximumRange Dependency Property
        internal const string MaximumRangePropertyName = "MaximumRange";
        /// <summary>
        /// Identifies the MaximumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximumRangeProperty =
            DependencyProperty.Register(MaximumRangePropertyName, typeof(double), typeof(ValueBrushScaleView),
            new PropertyMetadata(DefaultMaximumRange, (o, e) =>
            {
                (o as ValueBrushScaleView).RaisePropertyChanged(MaximumRangePropertyName, e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Gets or sets the maximum size for this scale.
        /// </summary>
        public double MaximumRange
        {
            get { return (double)GetValue(MaximumRangeProperty); }
            set { SetValue(MaximumRangeProperty, value); }
        }
        #endregion

    }

    public interface IScaleViewRange
    {
        double MinimumRange { get; set; }
        double MaximumRange { get; set; }
        string ValueStringFormat { get; set; }
    }
}