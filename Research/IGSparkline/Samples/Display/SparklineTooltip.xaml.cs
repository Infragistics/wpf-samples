using System;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;

namespace IGSparkline.Samples.Display
{
    public partial class SparklineTooltip : SampleContainer
    {
        // Temporary holder for the tooltip
        private DataTemplate defaultTooltip = null;

        public SparklineTooltip()
        {
            InitializeComponent();
            this.SampleLoaded += new EventHandler(SparklineTooltip_SampleLoaded);
        }

        // Create new instance of the tooltip holder (defaultTooltip).
        void SparklineTooltip_SampleLoaded(object sender, EventArgs e)
        {
            this.defaultTooltip = new DataTemplate();
        }
        // Chart type selection.
        private void ComboBoxChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = this.ComboBoxChartType as ComboBox;
            if (cb == null) return;

            switch (cb.SelectedIndex)
            {
                case 0:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Line;
                    break;
                case 1:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Area;
                    break;
                case 2:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.Column;
                    break;
                case 3:
                    this.xamSparkline1.DisplayType = SparklineDisplayType.WinLoss;
                    break;
            }
        }
        // Tooltip
        private void checkBoxTooltip_Click(object sender, RoutedEventArgs e)
        {
            var cb = this.checkBoxTooltip as CheckBox;
            if (cb == null) return;

            if (cb.IsChecked == true)
            {
                // keep the dafult tooltip object in the tooltip holder.
                this.defaultTooltip = this.xamSparkline1.ToolTip as DataTemplate;

                // Set the Sparkline tooltip to user defined custom tooltip.
                this.xamSparkline1.ToolTip = this.Resources["customTooltip"] as DataTemplate;
            }
            else
            {
                // Reset the tooltip back to default.
                this.xamSparkline1.ToolTip = (DataTemplate)this.defaultTooltip;
            }
        }
    }
}
