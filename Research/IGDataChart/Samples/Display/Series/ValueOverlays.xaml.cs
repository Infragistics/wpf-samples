using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Shared.Models;

namespace IGDataChart.Samples.Display.Series
{
    public partial class ValueOverlays : Infragistics.Samples.Framework.SampleContainer
    {
        public ValueOverlays()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void OnMedianValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["MedianValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnMeanValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["MeanValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnVarianceValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["VarianceValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnHighestValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["HighestValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnLowestValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["LowestValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnFixedValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["FixedValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        private void OnEditableValueOverlayCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                this.DataChart.Series["EditableValueOverlay"].Opacity = chb.IsChecked.Value ? 0.5 : 0;
        }
        #endregion
    }
    public class NumericDataSample : NumericDataCollection
    {
        public NumericDataSample()
        {
            this.Add(new NumericDataPoint { X = 1, Y = 1 });
            this.Add(new NumericDataPoint { X = 2, Y = 2 });
            this.Add(new NumericDataPoint { X = 3, Y = 6 });
            this.Add(new NumericDataPoint { X = 4, Y = 8 });
            this.Add(new NumericDataPoint { X = 5, Y = 2 });
            this.Add(new NumericDataPoint { X = 6, Y = 6 });
            this.Add(new NumericDataPoint { X = 7, Y = 4 });
            this.Add(new NumericDataPoint { X = 8, Y = 2 });
            this.Add(new NumericDataPoint { X = 9, Y = 1 });

            int index = 0;
            foreach (NumericDataPoint dataPoint in Items)
            {
                dataPoint.Index = index++;
            }
        }
    }
}
