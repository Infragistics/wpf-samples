using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Media;
using Infragistics.Controls.Gauges;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeNeedles : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeNeedles()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.needleEndExtentSlider.Value = 0.48;
            this.needleStartExtentSlider.Value = 0;
            this.needleStartWidthRatioSlider.Value = 0;
            this.needleEndWidthRatioSlider.Value = 0;
            this.needleValueSlider.Value = 50;
            this.needleHighlightValueSlider.Value = 0;
            this.transitionDurationSlider.Value = 2;
            this.radialGauge.TitleDisplaysValue = true;
            this.radialGauge.HighlightValue = 0;
            this.radialGauge.HighlightLabelDisplaysValue = true;
#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
#endif
        }

        private void needleValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.Value = this.needleValueSlider.Value;
            this.needleValueLabel.Text = this.needleValueSlider.Value == 0 ? "0" : this.needleValueSlider.Value.ToString("#.##");
        }

        private void needleStartExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.NeedleStartExtent = this.needleStartExtentSlider.Value;
            this.needleStartExtentLabel.Text = this.needleStartExtentSlider.Value == 0 ? "0" : this.needleStartExtentSlider.Value.ToString("#.##");
        }

        private void needleEndExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.NeedleEndExtent = this.needleEndExtentSlider.Value;
            this.needleEndExtentLabel.Text = this.needleEndExtentSlider.Value == 0 ? "0" : this.needleEndExtentSlider.Value.ToString("#.##");
        }

        private void needleStartWidthRatioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.NeedleStartWidthRatio = this.needleStartWidthRatioSlider.Value;
            this.needleStartWidthRatioLabel.Text = this.needleStartWidthRatioSlider.Value == 0 ? "0" : this.needleStartWidthRatioSlider.Value.ToString("#.##");
        }

        private void needleEndWidthRatioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.NeedleEndWidthRatio = this.needleEndWidthRatioSlider.Value;
            this.needleEndWidthRatioLabel.Text = this.needleEndWidthRatioSlider.Value == 0 ? "0" : this.needleEndWidthRatioSlider.Value.ToString("#.##");
        }

        private void NeedleOutline_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush NeedleOutlineColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.NeedleOutline = NeedleOutlineColor;
        }

        private void NeedleBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush NeedleBrushColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.NeedleBrush = NeedleBrushColor;
        }

        private void NeedlePivotOutlineBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush NeedlePivotOutlineColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.NeedlePivotOutline = NeedlePivotOutlineColor;
        }

        private void NeedlePivotBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush NeedlePivotBrushColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.NeedlePivotBrush = NeedlePivotBrushColor;
        }

        private void EnumValuesProvider_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.radialGauge.NeedleShape = (RadialGaugeNeedleShape)e.NewValue;
            if (radialGauge.NeedleShape == RadialGaugeNeedleShape.Rectangle || radialGauge.NeedleShape == RadialGaugeNeedleShape.Trapezoid || radialGauge.NeedleShape == RadialGaugeNeedleShape.RectangleWithBulb || radialGauge.NeedleShape == RadialGaugeNeedleShape.TrapezoidWithBulb)
                this.needleEndWidthRatioSlider.IsEnabled = true;
            else
                this.needleEndWidthRatioSlider.IsEnabled = false;
        }

        private void pivotShape_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.radialGauge.NeedlePivotShape = (RadialGaugePivotShape)e.NewValue;
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (transitionDurationSlider != null && labeltransitionDuration != null)
            {
                this.radialGauge.TransitionDuration = ((int)this.transitionDurationSlider.Value) * 1000;
                this.labeltransitionDuration.Text = this.transitionDurationSlider.Value == 0 ? "0" : this.transitionDurationSlider.Value.ToString("#.##");

            }
        }

        private void needleHighlightValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.HighlightValue = this.needleHighlightValueSlider.Value;
            this.needleHighlightValueLabel.Text = this.needleHighlightValueSlider.Value == 0 ? "0" : this.needleHighlightValueSlider.Value.ToString("#.##");
        }
    }
}
