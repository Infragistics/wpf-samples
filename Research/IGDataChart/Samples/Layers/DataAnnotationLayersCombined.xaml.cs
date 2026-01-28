using System; 
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Media;
using System.Collections.Generic;

using Infragistics.Controls.Charts;
using IGDataChart.Models;

namespace IGDataChart.Samples.Layers
{
    public partial class DataAnnotationLayersCombined : Infragistics.Samples.Framework.SampleContainer
    { 
        public DataAnnotationLayersCombined()
        {
            InitializeComponent();
            UseDefaultTheme = true;

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var dataSource = new StockTeslaData();

            this.Chart.DataContext = dataSource;

            // examples of Stocks_StripAnnotationLayer:
            this.Chart.Series.Add(DataAnnotationStripLayerWithStocks.CreateStockMarketEvents(xAxisTop));

            // examples of DataAnnotationLineLayer:          
            this.Chart.Series.Add(DataAnnotationLineLayerWithStocks.CreateStock52WeekRange(yAxisRight));
            this.Chart.Series.Add(DataAnnotationLineLayerWithStocks.CreateStockGrowthAndDecline(xAxis));

            // examples of DataAnnotationSliceLayer:
            this.Chart.Series.Add(DataAnnotationSliceLayerrWithStocks.CreateStockSplitAnnotations(xAxisBottom));
            this.Chart.Series.Add(DataAnnotationSliceLayerrWithStocks.CreateStockEarningsMissAnnotations(xAxisBottom));
            this.Chart.Series.Add(DataAnnotationSliceLayerrWithStocks.CreateStockEarningsBeatAnnotations(xAxisBottom));

            var tooltip = new DataToolTipLayer();
            tooltip.IncludedColumns = new string[] { "High", "Low", "Open", "Close" };
            tooltip.LayoutMode = DataLegendLayoutMode.Vertical;

            this.Chart.Series.Add(tooltip);
        }
         
    }
}