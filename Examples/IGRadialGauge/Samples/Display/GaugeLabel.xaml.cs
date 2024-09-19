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


        private void titleAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (radialGauge != null)
            {
                this.radialGauge.TitleAngle = this.titleAngleSlider.Value;
                this.radialGauge.SubtitleAngle = this.titleAngleSlider.Value;
            }

            if (labelTitleAngle != null)
                this.labelTitleAngle.Text = this.titleAngleSlider.Value == 0 ? "0" : this.titleAngleSlider.Value.ToString("#.##");

        }

        private void titleExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (radialGauge != null)
            {
                this.radialGauge.TitleExtent = this.titleExtentSlider.Value;
                this.radialGauge.SubtitleExtent = this.titleExtentSlider.Value + 0.25;
            }

            if (labelTitleExtent != null)
                this.labelTitleExtent.Text = this.titleExtentSlider.Value == 0 ? "0" : this.titleExtentSlider.Value.ToString("#.##");
        }

    }
}
