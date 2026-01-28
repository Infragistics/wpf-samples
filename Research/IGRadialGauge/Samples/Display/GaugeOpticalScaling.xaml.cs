using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Controls.Gauges;

namespace IGRadialGauge.Samples.Display
{
    public partial class GaugeOpticalScaling : Infragistics.Samples.Framework.SampleContainer
    {
        public GaugeOpticalScaling()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.radialGauge.SnapsToDevicePixels = false;
        }

        private void OpticalScaleEnabled_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (radialGauge != null && (bool)this.isOpticalScaleEnabled.IsChecked == true)
            {
                this.radialGauge.OpticalScalingEnabled = true;
            }
            else
            {
                this.radialGauge.OpticalScalingEnabled = false;
            }
        }

        private void OpticalScaleSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (radialGauge != null)
            //{
            //    this.radialGauge.OpticalScalingSize = this.OpticalScaleSize.Value;
            //    if(this.labelScalingSize != null)
            //    {
            //        this.labelScalingSize.Text = this.OpticalScaleSize.Value == 0 ? "0" : this.OpticalScaleSize.Value.ToString("#.##");
            //    }
            //}

            if (radialGauge != null & labelScalingSize != null)
            {
                this.radialGauge.Width = e.NewValue;
                this.radialGauge.Height = e.NewValue;
                labelScalingSize.Text = ((short)e.NewValue).ToString();
            }
        }
    }
}
