using System.Windows;
using System.Windows.Media;
using Infragistics.Controls;
using Infragistics.Controls.Gauges;

namespace IGTreemap.Samples.Controls
{
    /// <summary>
    /// A value scale for the ColorMapper value mapper.
    /// </summary>
    public class ZoombarColorScale : ZoombarValueScale
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoombarColorScale()
        {
            this.ZoomChanging += OnZoomChanging;
            this.ZoomChanged += OnZoomChanged;

            // prevent the gauges assembly from beeing excluded because used only in a ResourceDictionary
            Dummy = LinearGraphNeedleShape.Needle;
        }
        protected LinearGraphNeedleShape Dummy;
        #region Overriden Methods
        /// <summary>
        /// Overrides OnMinimumChanged and sets the RangeMinimum property.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected override void OnMinimumChanged(double oldValue, double newValue)
        {
            base.OnMinimumChanged(oldValue, newValue);

            this.Range.FromScaleScroll(1, 1);
        }

        /// <summary>
        /// Overrides OnMaximumChanges and sets the RangeMaximum property.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnMaximumChanged(double oldValue, double newValue)
        {
            base.OnMaximumChanged(oldValue, newValue);

            this.Range.FromScaleScroll(1, 1);
        }
        #endregion

        #region From (Dependency Property)
        /// <summary>
        /// Gets or sets the From color.
        /// </summary>
        public Color From
        {
            get { return (Color)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register
            ("From", typeof(Color), typeof(ZoombarColorScale), new PropertyMetadata(Colors.Red));
        #endregion

        #region To (Dependency Property)
        /// <summary>
        /// Gets or sets the To color.
        /// </summary>
        public Color To
        {
            get { return (Color)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register
            ("To", typeof(Color), typeof(ZoombarColorScale), new PropertyMetadata(Colors.Green));
        #endregion

        #region RangeMinimum (Dependency Property)
        /// <summary>
        /// Represents the minimum value of the range.
        /// </summary>
        public double RangeMinimum
        {
            get { return (int)GetValue(RangeMinimumProperty); }
            private set { SetValue(RangeMinimumProperty, value); }
        }

        public static readonly DependencyProperty RangeMinimumProperty = DependencyProperty.Register
            ("RangeMinimum", typeof(double), typeof(ZoombarColorScale), new PropertyMetadata((double)0));
        #endregion


        #region RangeMaximum (Dependency Property)
        /// <summary>
        /// Represents the maximum value of the range.
        /// </summary>
        public double RangeMaximum
        {
            get { return (double)GetValue(RangeMaximumProperty); }
            private set { SetValue(RangeMaximumProperty, value); }
        }

        public static readonly DependencyProperty RangeMaximumProperty = DependencyProperty.Register
            ("RangeMaximum", typeof(double), typeof(ZoombarColorScale), new PropertyMetadata((double)0));
        #endregion

        #region EventHandlers
        /// <summary>
        /// Calculates the DataMinimum and DataMaximum offsets.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">A ZoomChangeEventArgs object.</param>
        private void OnZoomChanging(object sender, ZoomChangeEventArgs e)
        {
            double range = this.Maximum - this.Minimum;
            this.DataMinimumOffset = (e.NewRange.Minimum - this.Minimum) / range;
            this.DataMaximumOffset = (e.NewRange.Maximum - this.Minimum) / range;
        }

        /// <summary>
        /// Updated the DataMinimum and DataMaximum values.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">A ZoomChangedEventArgs object.</param>
        private void OnZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            this.RangeMinimum = e.NewRange.Minimum;
            this.RangeMaximum = e.NewRange.Maximum;
        }
        #endregion
    }
}
