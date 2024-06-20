using System; 
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework; 
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes;

namespace IGDataChart.Samples.Styling
{ 
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();         

            this.Loaded += OnSampleLoaded;
        }

        protected StockMarketDataCollection StockMarketData;

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            StockMarketData = this.Resources["StockMarketData"] as StockMarketDataCollection;
        }
         
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        private void CleanupChartAxis()
        {
            var axis = FinancialPriceChart.Axes.OfType<CategoryXAxis>().FirstOrDefault();
            FinancialPriceChart.Axes.Remove(axis);
        }

        private void RestoreChartAxis()
        {
            var series = FinancialPriceChart.Series.OfType<FinancialPriceSeries>().FirstOrDefault();
            if (series != null)
            {
                var axis = new CategoryXAxis();
                axis.Label = "{Date:MM/dd}";
                axis.ItemsSource = StockMarketData;
                FinancialPriceChart.Axes.Add(axis);
                series.XAxis = axis;
            }
        }

    }
}