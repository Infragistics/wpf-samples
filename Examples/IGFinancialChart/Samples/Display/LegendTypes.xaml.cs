using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using Infragistics.Controls.Charts;

namespace IGFinancialChart.Samples.Display
{ 
    public partial class LegendTypes : SampleContainer
    {
        public LegendTypes()
        { 
            InitializeComponent();
            
            var interval = TimeSpan.FromDays(7);
            var dataSource = new List<StockList>();
            dataSource.Add(new StockList(2020, 1, 1, 2022, 7, 1, "AMZN", interval, 600, 30000));
            dataSource.Add(new StockList(2020, 1, 1, 2022, 7, 1, "GOOG", interval, 500, 20000));
            dataSource.Add(new StockList(2020, 1, 1, 2022, 7, 1, "APPL", interval, 400, 20000));

            this.DataContext = dataSource;

            this.Loaded += LegendTypes_Loaded;
        }

        private void LegendTypes_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Chart.IsToolbarVisible = false;
        }

        private void CustomLegendCheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CustomLegendButton.IsChecked.Value)
            {
                this.Chart.ChartType = FinancialChartType.Bar;
                this.Chart.IsLegendVisible = false;
                this.Chart.Legend = CustomLegend;
                this.Chart.ChartType = FinancialChartType.Candle; 
            }
            else
            {
                this.Chart.ChartType = FinancialChartType.Bar;
                this.Chart.IsLegendVisible = false;
                this.Chart.Legend = null;
                this.Chart.IsLegendVisible = true;
                this.Chart.ChartType = FinancialChartType.Candle;
            }
        }
    }
}
