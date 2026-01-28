using System.Windows;
using System.Windows.Data;
using Infragistics;
using Infragistics.Controls;

namespace IGDataChart.Samples.Navigation
{
    public partial class ChartZoombar : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartZoombar()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (this.xmZoombar != null && this.xmDataChart != null)
            {
                Binding binding = new Binding
                {
                    Source = this.xmDataChart,
                    Path = new PropertyPath("HorizontalZoombar.Range"),
                    Mode = BindingMode.TwoWay
                };
                this.xmZoombar.SetBinding(XamZoombar.RangeProperty, binding);
                this.xmZoombar.Range = new Range { Minimum = 0.3, Maximum = 0.7 };
            }

            // NOTE: this is an alternative way to synchronize zoom/scale values of xamZoombar and xamDataChart controls:
            //this.xmZoombar.ZoomChanged += OnXamZoombarZoomChanged;
            //this.xmDataChart.WindowRectChanged += OnXamDataChartWindowRectChanged;

        }
        public bool IsZoombarChanging;

        private void OnXamZoombarZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            if (this.xmDataChart == null) return;
            if (IsZoombarChanging) return;
            try
            {
                IsZoombarChanging = true;
                Range zoombarRange = e.NewRange;
                this.xmDataChart.WindowRect = new Rect
                {
                    Y = 0,
                    Height = 1,
                    X = zoombarRange.Minimum,
                    Width = (zoombarRange.Maximum - zoombarRange.Minimum)
                };
            }
            finally
            {
                IsZoombarChanging = false;
            }
        }

        private void OnXamDataChartWindowRectChanged(object sender, RectChangedEventArgs e)
        {
            if (IsZoombarChanging) return;
            try
            {
                IsZoombarChanging = true;
                Range newZoombarRange = new Range
                {
                    Minimum = e.NewRect.X,
                    Maximum = e.NewRect.X + e.NewRect.Width
                };
                if (this.xmZoombar != null)
                {
                    this.xmZoombar.Range = newZoombarRange;
                }
            }
            finally
            {
                IsZoombarChanging = false;
            }
        }
    }
}
