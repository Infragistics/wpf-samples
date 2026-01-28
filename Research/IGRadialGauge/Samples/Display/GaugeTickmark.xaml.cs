using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Media;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeTickmark : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeTickmark()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
            DisableGaugeAnimation();
#endif
            this.tickMarkStartExtentSlider.Value = 0.5;
            this.tickMarkEndExtentSlider.Value = 0.57;
            this.minorTickMarkStartExtentSlider.Value = 0.57;
            this.minorTickMarkEndExtentSlider.Value = 0.54;
            this.minorTickMarkTickCountSlider.Value = 3;
       
        }

        private void tickMarkStartExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.TickStartExtent = this.tickMarkStartExtentSlider.Value;
            this.tickMarkStartExtentLabel.Text = this.tickMarkStartExtentSlider.Value == 0 ? "0" : this.tickMarkStartExtentSlider.Value.ToString("#.##");
        }

        private void tickMarkEndExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.TickEndExtent = this.tickMarkEndExtentSlider.Value;
            this.tickMarkEndExtentLabel.Text = this.tickMarkEndExtentSlider.Value == 0 ? "0" : this.tickMarkEndExtentSlider.Value.ToString("#.##");
        }

        private void tickMarkStrokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.TickStrokeThickness = this.tickMarkStrokeThicknessSlider.Value;
            this.tickMarkStrokeThicknessLabel.Text = 
                this.tickMarkStrokeThicknessSlider.Value == 0 ? "0" : 
                this.tickMarkStrokeThicknessSlider.Value.ToString("#");
        }

        private void minorTickMarkStartExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.MinorTickStartExtent = this.minorTickMarkStartExtentSlider.Value;
            this.minorTickMarkStartExtentLabel.Text = this.minorTickMarkStartExtentSlider.Value == 0 ? "0" : this.minorTickMarkStartExtentSlider.Value.ToString("#.##");
        }

        private void minorTickMarkEndExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.MinorTickEndExtent = this.minorTickMarkEndExtentSlider.Value;
            this.minorTickMarkEndExtentLabel.Text = this.minorTickMarkEndExtentSlider.Value == 0 ? "0" : this.minorTickMarkEndExtentSlider.Value.ToString("#.##");
        }

        private void minorTickMarkTickCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.MinorTickCount = this.minorTickMarkTickCountSlider.Value;
            this.minorTickMarkTickCountLabel.Text = this.minorTickMarkTickCountSlider.Value == 0 ? "0" : this.minorTickMarkTickCountSlider.Value.ToString("#.##");
        }

        private void minorTickMarkStrokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.MinorTickStrokeThickness = this.minorTickMarkStrokeThicknessSlider.Value;
            this.minorTickMarkStrokeThicknessLabel.Text = 
                this.minorTickMarkStrokeThicknessSlider.Value == 0 ? "0" : 
                this.minorTickMarkStrokeThicknessSlider.Value.ToString("#");
        }

        private void TickMarkBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            var tickBrushColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.TickBrush = tickBrushColor;
        }

        private void TickMarkMinorBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            var minorTickBrushColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.MinorTickBrush = minorTickBrushColor;
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider != null && this.TransitionDurationLabel != null)
            {
                this.radialGauge.TransitionDuration = ((int)this.TransitionDurationSlider.Value) * 1000;
                this.TransitionDurationLabel.Text =
                    this.TransitionDurationSlider.Value == 0 ? "0" :
                    this.TransitionDurationSlider.Value.ToString("#.##");

                
            }
        }

        private void DisableGaugeAnimation()
        {
            this.radialGauge.TransitionDuration = 0;
            radialGauge.TransitionEasingFunction = null;

            TransitionDurationSlider.Visibility = Visibility.Collapsed;
            TransitionDurationLabel.Visibility = Visibility.Collapsed;
            TransitionTextBlock.Visibility = Visibility.Collapsed;
        }
     
    }
}
