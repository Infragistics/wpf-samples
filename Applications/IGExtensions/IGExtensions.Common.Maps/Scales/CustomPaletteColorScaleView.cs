//using Infragistics.Controls.Charts;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Infragistics.Controls.Charts
{
    public class CustomPaletteColorScaleView : CustomPaletteColorScale, IScaleViewRange
    {
        public CustomPaletteColorScaleView()
        {
            this.ValueStringFormat = "0.00";
            this.MinimumValue = 0.1;
            this.MaximumValue = 1.0;
            this.Palette = new ObservableCollection<Color> {Colors.Yellow, Colors.Orange, Colors.Red };
        }
        public string ValueStringFormat { get; set; }
        
        #region MinimumRange Dependency Property
        protected const double DefaultMinimumRange = 0.1;
        protected const double DefaultMaximumRange = 1.0;

        internal const string MinimumRangePropertyName = "MinimumRange";
        /// <summary>
        /// Identifies the MinimumValue dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimumRangeProperty =
            DependencyProperty.Register(MinimumRangePropertyName, typeof(double), typeof(CustomPaletteColorScaleView),
            new PropertyMetadata(DefaultMinimumRange, (o, e) =>
            {
                (o as CustomPaletteColorScaleView).PropertyUpdated(MinimumRangePropertyName, e.OldValue, e.NewValue);
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
            DependencyProperty.Register(MaximumRangePropertyName, typeof(double), typeof(CustomPaletteColorScaleView),
            new PropertyMetadata(DefaultMaximumRange, (o, e) =>
            {
                (o as CustomPaletteColorScaleView).PropertyUpdated(MaximumRangePropertyName, e.OldValue, e.NewValue);
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
}