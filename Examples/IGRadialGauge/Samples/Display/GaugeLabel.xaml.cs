using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Controls.Gauges;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeLabel : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeLabel()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.labelIntervalSlider.Value = 10;
            this.labelExtentSlider.Value = 0.7;
            this.transitionDurationSlider.Value = 2;
#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
#endif
        }

        private void labelExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.LabelExtent = this.labelExtentSlider.Value;
            this.labelExtentLabel.Text = this.labelExtentSlider.Value == 0 ? "0" : this.labelExtentSlider.Value.ToString("#.##");
        }

        private void labelIntervalSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (radialGauge != null)
            {
                this.radialGauge.LabelInterval = this.labelIntervalSlider.Value;
            }
            if (labelIntervalLabel != null)
                this.labelIntervalLabel.Text = this.labelIntervalSlider.Value == 0 ? "0" : this.labelIntervalSlider.Value.ToString("#.##");
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (transitionDurationSlider != null && labeltransitionDuration != null)
            {
                this.radialGauge.TransitionDuration = ((int)this.transitionDurationSlider.Value) * 1000;
                this.labeltransitionDuration.Text = this.transitionDurationSlider.Value == 0 ? "0" : this.transitionDurationSlider.Value.ToString("#.##");

            }
        }

    }
}
