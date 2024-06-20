using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Infragistics.Controls.Gauges;

namespace IGRadialGauge.Samples.Styling
{
    public partial class GaugeConfigurableBacking : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeConfigurableBacking()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.backingStrokeThicknessSlider.Value = 10;
#if WPF
            this.radialGauge.SnapsToDevicePixels = false;
#endif
        }

        private void BackingBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush BackingColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.BackingBrush = BackingColor;
        }

        private void ScaleSweepDirection_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.radialGauge.ScaleSweepDirection = (SweepDirection)e.NewValue;
        }

        private void backingShapeCombo_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.radialGauge.BackingShape = (RadialGaugeBackingShape)e.NewValue;
        }
        
        private void backingOversweepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.BackingOversweep = this.backingOversweepSlider.Value;
        }

        private void BackingOutlineBrush_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            SolidColorBrush BackingOutlineColor = new SolidColorBrush((Color)e.NewColor);
            this.radialGauge.BackingOutline = BackingOutlineColor;
        }

        private void backingStrokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.radialGauge.BackingStrokeThickness = this.backingStrokeThicknessSlider.Value;
        }
    }
}
