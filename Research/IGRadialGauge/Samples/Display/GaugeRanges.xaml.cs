using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Editors;

namespace IGRadialGauge.Samples.Editing
{
    public partial class GaugeRanges : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeRanges()
        {
            InitializeComponent();
            this.SampleLoaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, System.EventArgs e)
        {
            this.StartValueSlider.Value = 70;
            this.EndValueSlider.Value = 100;
            this.InnerExtentStartSlider.Value = .6;
            this.InnerExtentEndSlider.Value = .6;
            this.OuterEndExtentSlider.Value = 0.74;
            this.OuterStartExtentSlider.Value = 0.67;
//#if WPF
//            this.radialGauge.SnapsToDevicePixels = false;
//#endif

        }
        private void innerStartExtent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.Ranges[0].InnerStartExtent = this.InnerExtentStartSlider.Value;
        }

        private void innerEndExtent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.Ranges[0].InnerEndExtent = this.InnerExtentEndSlider.Value;
        }

        private void outerEndExtent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.Ranges[0].OuterEndExtent = this.OuterEndExtentSlider.Value;
        }

        private void outerStartExtent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.Ranges[0].OuterStartExtent = this.OuterStartExtentSlider.Value;
        }

        private void startValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (StartValueSlider != null)
                this.radialGauge.Ranges[0].StartValue = this.StartValueSlider.Value;
        }

        private void endValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (EndValueSlider != null)
                this.radialGauge.Ranges[0].EndValue = this.EndValueSlider.Value;
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider != null)
            {
                this.radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value) * 1000;
           }

        }


        private void RangeBrush_OnSelectedColorChanged(object sender, SelectedColorChangedEventArgs e)
        {
            if (e.NewColor != null)
            {
                var rangeBrushColor = new SolidColorBrush((Color)e.NewColor);
                this.radialGauge.Ranges[0].Brush = rangeBrushColor;
            }
        }

        private void RangeOutlineBrush_OnSelectedColorChanged(object sender, SelectedColorChangedEventArgs e)
        {
            if (e.NewColor != null)
            {
                var rangeOutlineBrushColor = new SolidColorBrush((Color)e.NewColor);
                this.radialGauge.Ranges[0].Outline = rangeOutlineBrushColor;
            }
        }
    }

}
