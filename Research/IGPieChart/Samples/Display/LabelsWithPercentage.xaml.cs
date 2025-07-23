using System.Windows.Controls;
using Infragistics.Controls.Charts;
using IGPieChart.Model;
using System.Windows;

namespace IGPieChart.Samples.Display
{
    public partial class LabelsWithPercentage : Infragistics.Samples.Framework.SampleContainer
    {
        public LabelsWithPercentage()
        {
            InitializeComponent();
            this.labelsPositionCombo.SelectionChanged += new SelectionChangedEventHandler(labelsPositionCombo_SelectionChanged);
            this.labelsPositionCombo.SelectedIndex = 4;
        }
        // select label type for PieChart
        private void labelsPositionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as ComboBoxItem;
            if (item != null)
            {
                var p = item.Content as LabelsPositionMode;
                if (p != null && this.pieChart != null)
                {
                    this.pieChart.LabelsPosition = p.LabelsPosition;
                }
            }
        }
        // toggle slice explosion
        private void pieChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded;
        }
        private void rb1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.pieChart.LabelTemplate = this.Resources["labelTemplate1"] as DataTemplate;
        }
        private void rb2_Click(object sender, RoutedEventArgs e)
        {
            this.pieChart.LabelTemplate = this.Resources["labelTemplate2"] as DataTemplate;
        }
        private void rb3_Click(object sender, RoutedEventArgs e)
        {
            this.pieChart.LabelTemplate = this.Resources["labelTemplate3"] as DataTemplate;
        }
    }
}
