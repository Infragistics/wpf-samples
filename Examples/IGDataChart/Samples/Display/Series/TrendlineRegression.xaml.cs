
using Infragistics.Samples.Shared.Models;
using IGDataChart.Resources;
using Infragistics.Samples.Shared.Resources;
using System.Windows.Controls;
using System.Windows;
namespace IGDataChart.Samples.Display.Series
{
    public partial class TrendlineRegression : Infragistics.Samples.Framework.SampleContainer
    {
        public TrendlineRegression()
        {
            InitializeComponent();

            this.cmbTrendLineType.SelectedIndex = this.cmbTrendLineType.Items.Count - 1;
            this.sldTrendLineThickness.Value = 3.0;

            this.cmbChartType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(cmbChartType_SelectionChanged);
        }

        void cmbChartType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var cb = this.cmbChartType as ComboBox;
            if (cb == null) return;

            var str = ((ComboBoxItem)cb.SelectedItem).Tag.ToString();

            switch (str)
            {
                case "CandleStickChart":
                    this.DataChart.Visibility = System.Windows.Visibility.Visible;
                    this.PointChart.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "PointChart":
                    this.PointChart.Visibility = System.Windows.Visibility.Visible;
                    this.DataChart.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
        }

    }
}
