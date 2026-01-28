using System;
using System.Windows;
using System.Windows.Media.Animation;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Editing
{
    public partial class ChartHighlighting : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartHighlighting()
        {
            InitializeComponent();
        }

        private void AnimatePropertyTo(DependencyObject target, double targetValue, string propertyName, double seconds)
        {
            DoubleAnimation da = new DoubleAnimation
            {
                To = targetValue,
                Duration = TimeSpan.FromSeconds(seconds)
            };
            Storyboard sb = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(seconds)
            };
            sb.Children.Add(da);
            Storyboard.SetTarget(da, target);
            Storyboard.SetTargetProperty(da, new PropertyPath(propertyName));

            sb.Begin();
        }

        private void HighlightSeries(Series series)
        {
            AnimatePropertyTo(series, 0.5, "Opacity", 0.3);
            AnimatePropertyTo(series, 5.0, "Thickness", 0);
        }

        private void UnHighlightSeries(Series series)
        {
            AnimatePropertyTo(series, 1.0, "Opacity", 0.3);
            AnimatePropertyTo(series, 2.0, "Thickness", 0);
        }

        private void OnSeriesMouseEnter(object sender, ChartMouseEventArgs e)
        {
            if (e.Series != null)
            {
                // optional custom highlighting
                HighlightSeries(e.Series);
            }
        }

        private void OnSeriesMouseLeave(object sender, ChartMouseEventArgs e)
        {
            if (e.Series != null)
            {
                // optional custom highlighting
                UnHighlightSeries(e.Series);
            }
        }

        private void OnLegendItemMouseEnter(object sender, LegendMouseEventArgs e)
        {
            if (e.Series != null)
            {
                HighlightSeries((Series)e.Series);
            }
        }

        private void OnLegendItemMouseLeave(object sender, LegendMouseEventArgs e)
        {
            if (e.Series != null)
            {
                UnHighlightSeries((Series)e.Series);
            }
        }
    }
}
