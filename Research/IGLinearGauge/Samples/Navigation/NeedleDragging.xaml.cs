using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using System.Windows.Input;
using System.Windows.Media;
using Infragistics.Controls.Gauges; 

namespace IGLinearGauge.Samples.Navigation
{
    public partial class NeedleDragging : Infragistics.Samples.Framework.SampleContainer
    {
        public NeedleDragging()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded; 
        }
 
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.transitionDurationSlider.Value = 0;
        }

        private void transitionDurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (transitionDurationSlider != null && labeltransitionDuration != null)
            {
                this.gauge.TransitionDuration = ((int)this.transitionDurationSlider.Value) * 1000;
                this.labeltransitionDuration.Text = this.transitionDurationSlider.Value == 0 ? "0" : this.transitionDurationSlider.Value.ToString("#.##");
            }
        }
    }
}
