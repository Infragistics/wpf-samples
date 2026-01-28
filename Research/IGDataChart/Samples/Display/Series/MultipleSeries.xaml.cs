using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Series
{
    public partial class MultipleSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public MultipleSeries()
        {
            InitializeComponent();
            InitializeBrushes();
        }
        
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var cbk = (sender as CheckBox);
            if (cbk == null || cbk.IsChecked == null) return;
            var isVisible = cbk.IsChecked.Value;

            var tag = cbk.Tag.ToString();
            if (tag.Equals("ShowPriceSeries"))
            {
                var series = DataChart.Series["PriceSeries"] as FinancialPriceSeries;
                series.Brush = isVisible ? PricePositiveBrush : HiddenBrush;
                series.NegativeBrush = isVisible ? PriceNegativeBrush : HiddenBrush;
                series.Outline = isVisible ? PriceOutlineBrush : HiddenBrush;
                series.NegativeOutline = isVisible ? PriceNegativeBrush : HiddenBrush;
            }
            else if (tag.Equals("ShowVolumeSeries"))
            {
                var series = DataChart.Series["VolumeSeries"] as LineSeries;
                series.Brush = isVisible ? VolumeSeriesBrush : HiddenBrush;
            }
            else if (tag.Equals("ShowVolumeTrendline"))
            {
                var series = DataChart.Series["VolumeSeries"] as LineSeries;
                series.TrendLineBrush = isVisible ? VolumeTrendLineBrush : HiddenBrush;
            }
            else if (tag.Equals("ShowPriceTrendline"))
            {
                var series = DataChart.Series["PriceSeries"] as FinancialPriceSeries;
                series.TrendLineBrush = isVisible ? PriceTrendLineBrush : HiddenBrush;
             }
        }

        private void InitializeBrushes()
        {
            var series = DataChart.Series["PriceSeries"] as FinancialPriceSeries;
            if (series != null)
            {
                PricePositiveBrush = series.Brush;
                PriceNegativeBrush = series.NegativeBrush;
                PriceOutlineBrush = series.Outline;
                PriceTrendLineBrush = series.TrendLineBrush;
                PriceNegativeOutlineBrush = series.NegativeOutline;
            }
            var vs = DataChart.Series["VolumeSeries"] as LineSeries;
            if (vs != null)
            {
                VolumeSeriesBrush = vs.Brush;
                VolumeTrendLineBrush = vs.TrendLineBrush;
            }
        }

        protected Brush PricePositiveBrush;
        protected Brush PriceNegativeBrush;
        protected Brush PriceTrendLineBrush;
        protected Brush PriceOutlineBrush;
        protected Brush PriceNegativeOutlineBrush;

        protected Brush VolumeSeriesBrush;
        protected Brush VolumeTrendLineBrush;
        protected Brush HiddenBrush = new SolidColorBrush(Colors.Transparent);

    }
}
