using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Media;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeScale : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeScale()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.transitionDurationSlider.Value = 2;
            this.scaleStartExtentSlider.Value = .5;
            this.scaleEndExtentSlider.Value = .58;
            this.scaleOverSweepSlider.Value = 2.5;
            this.ScaleBrush.SelectedColor = Color.FromArgb(255, 75, 172, 198);

#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
#endif
        }

        private void scaleStartExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.ScaleStartExtent = this.scaleStartExtentSlider.Value;
            this.scaleStartExtentLabel.Text = this.scaleStartExtentSlider.Value == 0 ? "0" : this.scaleStartExtentSlider.Value.ToString("#.##");
        }

        private void scaleEndExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.ScaleEndExtent = this.scaleEndExtentSlider.Value;
            this.scaleEndExtentLabel.Text = this.scaleEndExtentSlider.Value == 0 ? "0" : this.scaleEndExtentSlider.Value.ToString("#.##");
        }

        private void scaleOverSweepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.ScaleOversweep = this.scaleOverSweepSlider.Value;
            this.scaleOverSweepLabel.Text = this.scaleOverSweepSlider.Value == 0 ? "0" : this.scaleOverSweepSlider.Value.ToString("#.##");
        }

        private void ScaleBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush ScaleColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.ScaleBrush = ScaleColor;
        }

        private void ScaleSweepDirection_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.radialGauge.ScaleSweepDirection = (SweepDirection)e.NewValue;
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
