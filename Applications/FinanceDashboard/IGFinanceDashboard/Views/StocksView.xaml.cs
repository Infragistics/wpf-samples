using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using IGExtensions.Framework.Controls; // NavigationPage 
using Infragistics.Controls.Charts;
using Infragistics.Framework;

namespace IGShowcase.FinanceDashboard
{ 
    public partial class StocksView : NavigationPage
	{ 
		private StockViewModel _vm;  
         
        public StocksView() 
		{ 
			InitializeComponent();
           
            // Lets grab the data context
            _vm = DataContext as StockViewModel;
            if (_vm == null)
            {
                var error = new NullReferenceException("DataContext must be of type StockViewModel");
                Logs.Error(error.Message);
                throw error;
            }
           
            // Add some default data 
            _vm.WatchListAddStock(new List<string> { "MSFT", "TSLA",  "INTC", });
               
            this.Loaded += OnNavigationPageLoaded;
		}
        		
		#region Event Handlers

		private void OnNavigationPageLoaded(object sender, RoutedEventArgs e)
		{
            this.BusyIndicator.Visibility = _vm.IsInitialStockDetailsLoading ? Visibility.Visible : Visibility.Collapsed;

            StocksAutoCompleteBox.ItemFilter += OnStocksFilter;

            Logs.Message("StocksView.OnNavigationPageLoaded...");

            Chart.PropertyChanged += Chart_PropertyChanged;
            Chart.SeriesAdded += Chart_SeriesAdded;
		}
         
        string oldSeries; 
        private void Chart_SeriesAdded(object sender, ChartSeriesEventArgs args)
        {
            // synconize visuals of price pane with zoom slider pane
            if (!args.Series.Name.Contains("FinancialPrice")) return;

            var newSeries = args.Series.GetType().Name;
            if (args.Series is FinancialPriceSeries)
            {
                var priceSeries = args.Series as FinancialPriceSeries;
                newSeries += "_" + priceSeries.DisplayType.ToString(); 
            }

            if (oldSeries != newSeries)
            {
                oldSeries = newSeries;
                var zoomType = FinancialChartZoomSliderType.Bar;
                
                if (args.Series is LineSeries)
                    zoomType = FinancialChartZoomSliderType.Line;             
                else if (args.Series is ColumnSeries)
                    zoomType = FinancialChartZoomSliderType.Column;                
                else if (args.Series is AreaSeries)
                    zoomType = FinancialChartZoomSliderType.Area;
                else if (args.Series is FinancialPriceSeries)
                {
                    var priceSeries = args.Series as FinancialPriceSeries;
                    if (priceSeries.DisplayType == PriceDisplayType.Candlestick)
                        zoomType = FinancialChartZoomSliderType.Candle;
                    else
                        zoomType = FinancialChartZoomSliderType.Bar;
                }
                this.Chart.ZoomSliderType = zoomType;
            }
        }
         
        private void Chart_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Logs.Message("Chart PropertyChanged " + e.PropertyName); 
        }
  
        private bool OnStocksFilter(string filter, object value)
        {
            if (_vm != null)
            {
                return _vm.IsStockMatching(filter, value);
            }
            return false;
        }
        /// <summary>
        /// Handles the DropDownClosed event of the NewSymbolAutoCompleteBox control.
        /// </summary>
        private void OnNewSymbolAutoCompleteBox_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var symbol = ((AutoCompleteBox)sender).Text.ToUpper();
            var stockMatch = _vm.FindStockMatching(symbol);
            if (stockMatch != null)
            {
                ((AutoCompleteBox)sender).Text = "";
                if (_vm.CommandAddStock.CanExecute(stockMatch.Symbol))                
                    _vm.CommandAddStock.Execute(stockMatch.Symbol);
                
            }
        }

        private void StockListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null) return;
            if (e.AddedItems.Count == 0) return;
            var stock = e.AddedItems[0] as StockTradeData;
            if (stock == null) return;
            
            if (_vm != null) 
                _vm.CommandSelectStock.Execute(stock.Details.Symbol);
        }
        //private void OnAssigningVolumeSeriesHighlightStyle(object sender, AssigningCategoryStyleEventArgs args)
        //{
        //    double minOpacity = .3, opacity = 1.0;
        //    if (args.SumAllSeriesHighlightingProgress > 0.0)
        //    {
        //        var progress = 0.0;
        //        if (args.HighlightingInfo != null)
        //        {
        //            progress = args.HighlightingInfo.Progress;

        //            if (args.HighlightingInfo.State != HighlightingState.Out)
        //            {
        //                var brush = Application.Current.Resources["VolumeSeriesHighlighBrush"] as Brush ??
        //                            new SolidColorBrush(Color.FromArgb(255, 75, 146, 219));
        //                args.Fill = brush;
        //            }
        //            else
        //            {
        //                args.Fill = new SolidColorBrush(Colors.Gray);
        //            }
        //        }

        //        progress = progress - args.SumAllSeriesHighlightingProgress;
        //        opacity = minOpacity + (1.0 + progress) * (1.0 - minOpacity);

        //        args.Opacity = opacity;
        //        args.HighlightingHandled = true;

        //        for (var i = 0; i < this.StockVolumeChart.Series.Count; i++)
        //        {
        //            var curr = this.StockVolumeChart.Series[i];
        //            var series = sender as Infragistics.Controls.Charts.Series;
        //            if (series != null && series.Name != curr.Name)
        //            {
        //                curr.NotifyVisualPropertiesChanged();
        //            }
        //        }
        //    }
        //}

        #endregion Event Handlers

     }
     
}
