using System.Windows;
using Infragistics.Controls;

namespace IGTreemap.Samples.Controls
{
    /// <summary>
    /// The base value scale class.
    /// </summary>
    public abstract class ZoombarValueScale : XamZoombar
    {
        #region DataMinimumOffset (Dependency Property)
        /// <summary>
        /// Gets the offset of the DataMinimum.
        /// </summary>
        public double DataMinimumOffset
        {
            get { return (double)GetValue(DataMinimumOffsetProperty); }
            protected set { SetValue(DataMinimumOffsetProperty, value); }
        }

        public static readonly DependencyProperty DataMinimumOffsetProperty = DependencyProperty.Register
            ("DataMinimumOffset", typeof(double), typeof(ZoombarValueScale), new PropertyMetadata((double)0));
        #endregion

        #region DataMaximumOffset (Dependency Property)
        /// <summary>
        /// Gets the offset of the DataMaximum.
        /// </summary>
        public double DataMaximumOffset
        {
            get { return (double)GetValue(DataMaximumOffsetProperty); }
            protected set { SetValue(DataMaximumOffsetProperty, value); }
        }

        public static readonly DependencyProperty DataMaximumOffsetProperty = DependencyProperty.Register
            ("DataMaximumOffset", typeof(double), typeof(ZoombarValueScale), new PropertyMetadata((double)1));
        #endregion
    }
}
