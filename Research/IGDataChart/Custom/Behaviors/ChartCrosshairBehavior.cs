using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace IGDataChart.Custom.Behaviors
{
    public class ChartCrosshairBehaviors : DependencyObject
    {
        #region Chart Crosshair Visibility Behavior

        internal const string CrosshairVisibilityPropertyName = "CrosshairVisibility";
        public static readonly DependencyProperty CrosshairVisibilityProperty =
            DependencyProperty.RegisterAttached(CrosshairVisibilityPropertyName,
            typeof(ChartCrosshairVisibilityBehavior), typeof(ChartCrosshairBehaviors),
            new PropertyMetadata(null, (o, e) => OnChartCrosshairsBehaviorChanged(o as XamDataChart,
                    e.OldValue as ChartCrosshairVisibilityBehavior,
                    e.NewValue as ChartCrosshairVisibilityBehavior)));

        public static ChartCrosshairVisibilityBehavior GetCrosshairVisibility(DependencyObject target)
        {
            return target.GetValue(CrosshairVisibilityProperty) as ChartCrosshairVisibilityBehavior;
        }
        public static void SetCrosshairVisibility(DependencyObject target, ChartCrosshairVisibilityBehavior behavior)
        {
            target.SetValue(CrosshairVisibilityProperty, behavior);
        }

        private static void OnChartCrosshairsBehaviorChanged(XamDataChart chart, ChartCrosshairVisibilityBehavior oldValue, ChartCrosshairVisibilityBehavior newValue)
        {
            if (chart == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.OnDetach(chart);
            }
            if (newValue != null)
            {
                newValue.OnAttach(chart);
            }
        }
        #endregion
    }

    public class ChartCrosshairVisibilityBehavior 
    {
        public ChartCrosshairVisibilityBehavior()
        {
            _invisibleStyle = new Style(typeof(Line));
            _invisibleStyle.Setters.Add(new Setter(Shape.StrokeProperty, new SolidColorBrush(Colors.Transparent)));
        }

        private readonly Style _invisibleStyle;
        public bool ShowHorizontalCrosshair { get; set; }
        public bool ShowVerticalCrosshair { get; set; }

        public void OnAttach(XamDataChart chart)
        {
            chart.MouseEnter += OnChartMouseEnter;
        }
        public void OnDetach(XamDataChart chart)
        {
            chart.MouseEnter -= OnChartMouseEnter;
            MakeVisible(chart, _vertical);
            MakeVisible(chart, _horizontal);
            _vertical = null;
            _horizontal = null;
            _first = true;
        }

        private bool _first = true;
        private Line _vertical;
        private Line _horizontal;

        private void MakeInvisible(XamDataChart chart, Line crosshairLine)
        {
            crosshairLine.Style = _invisibleStyle;
        }
        private void MakeVisible(XamDataChart chart, Line crosshairLine)
        {
            if (chart == null || crosshairLine == null)
            {
                return;
            }
            crosshairLine.Style = chart.CrosshairLineStyle;
        }

        void OnChartMouseEnter(object sender, MouseEventArgs e)
        {
            var chart = sender as XamDataChart;
            if (_first && chart != null)
            {
                _first = false;
                var crosshairs =
                    from line in chart.VisualDescendants()
                    where line is Line && (line as Line).Style
                          == chart.CrosshairLineStyle
                    select line as Line;
                var lines = crosshairs.ToList();

                var uninitialized = lines.Count == 0 || (lines[0].X1 == 0.0 && lines[0].X2 == 0.0 &&
                                     lines[1].X1 == 0.0 && lines[1].X2 == 0.0);
                if (uninitialized)
                {
                    _first = true;
                    chart.MouseMove += OnChartMouseEnter;
                    return;
                }
                if (lines[0].X1 == lines[0].X2)
                {
                    _vertical = lines[0];
                    _horizontal = lines[1];
                }
                else
                {
                    _vertical = lines[1];
                    _horizontal = lines[0];
                }

                if (!this.ShowHorizontalCrosshair)
                {
                    MakeInvisible(chart, _horizontal);
                }
                if (!this.ShowVerticalCrosshair)
                {
                    MakeInvisible(chart, _vertical);
                }
                chart.MouseMove -= OnChartMouseEnter;
            }
        }
    }

}