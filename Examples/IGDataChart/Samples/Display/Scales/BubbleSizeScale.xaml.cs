using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Scales
{
    public partial class BubbleSizeScale : Infragistics.Samples.Framework.SampleContainer
    {
        public BubbleSizeScale()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        private void OnFilterWorldData(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita >= 20000 && 
                             model.GdpPerCapita <= 80000;
            }
        }

        #region Event Handlers

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.BubbleSizeScaleMinimumSlider.ValueChanged += OnBubbleSizeScaleMinimumSliderValueChanged;
            this.BubbleSizeScaleMaximumSlider.ValueChanged += OnBubbleSizeScaleMaximumSliderValueChanged;
            this.BubbleSizeLogCheckBox.Click += OnBubbleSizeScaleLogCheckBoxClick;

            this.BubbleSizeScaleMinimumSlider.Value = 10;
            this.BubbleSizeScaleMaximumSlider.Value = 100;
        }
        private void OnBubbleSizeScaleMinimumSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            ((BubbleSeries)this.DataChart.Series[0]).RadiusScale.MinimumValue = e.NewValue;
        }
        private void OnBubbleSizeScaleMaximumSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.DataChart == null) return;
            ((BubbleSeries)this.DataChart.Series[0]).RadiusScale.MaximumValue = e.NewValue;
        }
        private void OnBubbleSizeScaleLogCheckBoxClick(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            if (chk == null) return;
            if (chk.IsChecked != null)
            {
                var isLogarithmic = chk.IsChecked.Value;
                if (this.DataChart == null) return;
                ((BubbleSeries)this.DataChart.Series[0]).RadiusScale.IsLogarithmic = isLogarithmic;
            }
        }

        #endregion

    }
}
