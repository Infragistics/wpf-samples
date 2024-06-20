using Infragistics.Controls.Charts;
using Infragistics.Controls.Layouts;

namespace IGDataChart.Samples.Navigation
{
    public partial class ChartOverviewPane : Infragistics.Samples.Framework.SampleContainer
    {
        public ChartOverviewPane()
        {
            InitializeComponent();

            OpdPaneVisibility.ItemsSource = System.Enum.GetValues(typeof(System.Windows.Visibility));
        }
        private void OpdHorizontalAlignmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var alignment = (System.Windows.HorizontalAlignment)e.AddedItems[0];

            if (this.ChartTileManager == null) return;
            if (this.ChartTileManager.Items == null) return;

            foreach (XamTile chartTile in this.ChartTileManager.Items)
            {
                if (chartTile != null)
                {
                    var chart = chartTile.Content as XamDataChart;
                    if (chart != null) chart.OverviewPlusDetailPaneHorizontalAlignment = alignment;
                }
            }
     
        }

        private void OpdVerticalAlignmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var alignment = (System.Windows.VerticalAlignment)e.AddedItems[0];

            if (this.ChartTileManager == null) return;
            if (this.ChartTileManager.Items == null) return;

            foreach (XamTile chartTile in this.ChartTileManager.Items)
            {
                if (chartTile != null)
                {
                    var chart = chartTile.Content as XamDataChart;
                    if (chart != null) chart.OverviewPlusDetailPaneVerticalAlignment = alignment;
                }
            }
        
        }

        private void OpdPaneVisibility_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var visibility = (OpdPaneVisibility.SelectedIndex == 0) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

            if (this.ChartTileManager == null) return;
            if (this.ChartTileManager.Items == null) return;

            foreach (XamTile chartTile in this.ChartTileManager.Items)
            {
                if (chartTile != null)
                {
                    var chart = chartTile.Content as XamDataChart;
                    if (chart != null) chart.OverviewPlusDetailPaneVisibility = visibility;
                }
            }
        }
    }
}
