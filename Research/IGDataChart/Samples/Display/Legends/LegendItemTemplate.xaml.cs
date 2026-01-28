using System.Windows;
using System.Windows.Controls;

namespace IGDataChart.Samples.Display.Legends
{
    public partial class LegendItemTemplate : Infragistics.Samples.Framework.SampleContainer
    {
        public LegendItemTemplate()
        {
            InitializeComponent();
        }

        private void OnPriceSeriesVisibilityClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                xmDataChart.Series[0].Opacity = chb.IsChecked.Value ? 1 : 0;
        }

        private void OnVolumeSeriesVisibilityClick(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            if (chb != null && chb.IsChecked != null)
                xmDataChart.Series[1].Opacity = chb.IsChecked.Value ? 1 : 0;
        }

    }
}
