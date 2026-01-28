using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Navigation
{
    public partial class ChartElements : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartElements()
        {
            InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbk = (sender as CheckBox);
            if (cbk == null) return;
            string tag = cbk.Tag.ToString();

            if (cbk.IsChecked != null)
            {
                #region Series Chart Elements
                // #######################################################################################################
                if (tag.Equals("ShowVolumeSeries"))
                {
                    this.xmFinancialChart.Series[0].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowPriceSeries"))
                {
                    this.xmFinancialChart.Series[1].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                // #######################################################################################################
                if (tag.Equals("ShowIndicatorTRIX"))
                {
                    this.xmIndicatorChart.Series[0].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowIndicatorTrendline"))
                {
                    this.xmIndicatorChart.Series[1].Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                #endregion
                #region Axis Chart Elements
                if (tag.Equals("ShowAxisLabels"))
                {
                    this.xmFinancialChart.Axes[0].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmFinancialChart.Axes[1].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;

                    this.xmIndicatorChart.Axes[0].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.Axes[1].LabelSettings.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowAxisTickmarks"))
                {
                    this.xmFinancialChart.Axes[0].TickLength = cbk.IsChecked.Value ? 5 : 0;
                    this.xmFinancialChart.Axes[1].TickLength = cbk.IsChecked.Value ? 5 : 0;
                
                    this.xmIndicatorChart.Axes[0].TickLength = cbk.IsChecked.Value ? 5 : 0;
                    this.xmIndicatorChart.Axes[1].TickLength = cbk.IsChecked.Value ? 5 : 0;
                }

                if (tag.Equals("ShowAxisMajorLines"))
                {
                    this.xmFinancialChart.Axes[0].MajorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmFinancialChart.Axes[1].MajorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);

                    this.xmIndicatorChart.Axes[0].MajorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmIndicatorChart.Axes[1].MajorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                }
                if (tag.Equals("ShowAxisMinorLines"))
                {
                    this.xmFinancialChart.Axes[0].MinorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmFinancialChart.Axes[1].MinorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);

                    this.xmIndicatorChart.Axes[0].MinorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmIndicatorChart.Axes[1].MinorStroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                }
                if (tag.Equals("ShowAxisLines"))
                {
                    this.xmFinancialChart.Axes[0].Stroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmFinancialChart.Axes[1].Stroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);

                    this.xmIndicatorChart.Axes[0].Stroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                    this.xmIndicatorChart.Axes[1].Stroke = cbk.IsChecked.Value ? this.Resources["StrokeBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                }
                if (tag.Equals("ShowAxisStripes"))
                {
                    this.xmFinancialChart.Axes[1].Strip = cbk.IsChecked.Value ? this.Resources["StripBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);

                    this.xmIndicatorChart.Axes[1].Strip = cbk.IsChecked.Value ? this.Resources["StripBrush"] as SolidColorBrush : new SolidColorBrush(Colors.Transparent);
                }
                #endregion
                #region Other Chart Elements
                // #######################################################################################################
                if (tag.Equals("ShowLegends"))
                {
                    this.xmFinancialChart.Series[0].Legend.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmFinancialChart.Series[1].Legend.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.Series[0].Legend.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.Series[1].Legend.Visibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowCrosshairs"))
                {
                    this.xmFinancialChart.CrosshairVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.CrosshairVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowHorzNavBar"))
                {
                    this.xmFinancialChart.HorizontalZoombarVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.HorizontalZoombarVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowVertNavBar"))
                {
                    this.xmFinancialChart.VerticalZoombarVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    this.xmIndicatorChart.VerticalZoombarVisibility = cbk.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }
                if (tag.Equals("ShowMarkers"))
                {
                    ((MarkerSeries)this.xmFinancialChart.Series[0]).MarkerType = cbk.IsChecked.Value ? MarkerType.Circle : MarkerType.None;
                }
                // #######################################################################################################
                #endregion
            }
        }
    }
}
