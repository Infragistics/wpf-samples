using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGExtensions.Common.Frameworks;               // AnimationFramework
using IGExtensions.Common.Models;                   // StockTickerData
using IGExtensions.Framework;                       // DebugManager
using IGExtensions.Framework.Controls;              // NavigationPage
using IGShowcase.FinanceDashboard.Resources;
using IGShowcase.FinanceDashboard.ViewModels;       // StockViewModel
using Infragistics.Controls.Charts;

namespace IGShowcase.FinanceDashboard.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class StocksView : NavigationPage
	{
		#region Private Variables
		private StockViewModel _vm;
		#endregion Private Variables

	    protected bool UseAreaVolumeChart = true;
		#region Constructors
		/// <summary>
        /// Initializes a new instance of the <see cref="StocksView"/> class.
		/// </summary>
        public StocksView() 
		{
			InitializeComponent();
           
            // Lets grab the data context
            _vm = DataContext as StockViewModel;
            if (_vm == null)
            {
                var error = new NullReferenceException("DataContext must be of type StockViewModel");
                DebugManager.LogError(error);
                throw error;
            }
		    
            _vm.PropertyChanged += OnViewModelPropertyChanged;
            // If we have any saved information for last run, lets load it
            //_vm.Load();

            // Add some default data
            _vm.AddStockToWatchList(new List<string> { "ADBE", "MSFT", "INTC" });
            //_vm.AddStockToWatchList(new List<string> { "MSFT", });

            // get names of all axis scales
            var listAxisScales = new List<string>
                                 {
                                     AppStrings.Chart_AxisScale_Linear,
                                     AppStrings.Chart_AxisScale_Logarithmic
                                 };
           
            // bind axis scales to the chart's scale ComboBox
            this.ChartScaleComboBox.ItemsSource = listAxisScales;
            this.ChartScaleComboBox.SelectedIndex = 0;

            // get names of all axis scales
            var listTrendlineTypes = new List<TrendLineType>
                                 {
                                     TrendLineType.None,
                                     TrendLineType.CubicFit,
                                     TrendLineType.CumulativeAverage,
                                     TrendLineType.ExponentialAverage,
                                     TrendLineType.ExponentialFit,
                                     TrendLineType.LinearFit,
                                     TrendLineType.LogarithmicFit,
                                     TrendLineType.ModifiedAverage,
                                     TrendLineType.PowerLawFit,
                                     TrendLineType.QuadraticFit,
                                     TrendLineType.QuarticFit,
                                     TrendLineType.QuinticFit,
                                     TrendLineType.SimpleAverage,
                                     TrendLineType.WeightedAverage
                                 };
            // bind trendline types to the chart's trendline ComboBox
            this.ChartTrendLineComboBox.ItemsSource = listTrendlineTypes;
            this.ChartTrendLineComboBox.SelectedIndex = 0;
            
            // Select the First Stock
            //  _vm.SelectStockCommand.Execute("INTC");
            this.Loaded += OnNavigationPageLoaded;
		}

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsInitialStockDetailsLoading")
            {
                this.BusyIndicator.Visibility = _vm.IsInitialStockDetailsLoading ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (e.PropertyName == "SelectedStock" || e.PropertyName == "Tickers")
            {
            }
            else if (e.PropertyName == "UserLastStockSymbol")
            {
                //TODO update axis scale based on range of price series of user stock and existing stocks
            }
            else if (e.PropertyName == "NewTicker")
            {
            }
            else if (e.PropertyName == "MinimumValue")// || e.PropertyName == "NewTicker")
            {
                if (UseAreaVolumeChart && this.StockVolumeChart.Series.Count >= 2)
                {
                    // hide column series and show area series
                    var rangeYears = (_vm.RangeInMonths / 12);
                    this.StockVolumeChart.Series[0].Visibility = rangeYears < 2 ? Visibility.Visible : Visibility.Collapsed;
                    this.StockVolumeChart.Series[1].Visibility = rangeYears >= 2 ? Visibility.Visible : Visibility.Collapsed;
                }
           
                string xAxisLabel;
                var rangeMonths = _vm.RangeInMonths; 
                if (rangeMonths <= 9)
                {
                    xAxisLabel = "{Date:MMM dd}";
                } 
                else if (rangeMonths < 12)
                {
                    xAxisLabel = "{Date:MMM}";
                }
                else if (rangeMonths < 120)
                {
                    xAxisLabel = "{Date:MMM yyyy}";
                }
                else //if (rangeMonths <= 60)
                {
                    xAxisLabel = "{Date:yyyy}";
                }

                if (this.StockZoombarChart.Axes.Count > 0)
                {
                    this.StockZoombarChart.Axes[0].Label = xAxisLabel;
                }
                if (this.StockPriceChart.Axes.Count > 1)
                {
                    foreach (var axis in this.StockPriceChart.Axes.OfType<CategoryXAxis>())
                    {
                        axis.Label = xAxisLabel;
                    }
                }
                this.StockPriceChart.WindowRect = new Rect(0,0,1,1);
                //if (this.StockZoombarChart2.Axes.Count > 1)
                //{
                //    foreach (var axis in this.StockZoombarChart2.Axes.OfType<CategoryXAxis>())
                //    {
                //        axis.Label = xAxisLabel;
                //    }
                //}

            }
        }
		#endregion Constructors

		#region Event Handlers
		private void OnNavigationPageLoaded(object sender, RoutedEventArgs e)
		{
            this.BusyIndicator.Visibility = _vm.IsInitialStockDetailsLoading ? Visibility.Visible : Visibility.Collapsed;

            StocksAutoCompleteBox.ItemFilter += OnStocksFilter;

            DebugManager.Log("StocksView.OnNavigationPageLoaded...");
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
                if (_vm.AddStockCommand.CanExecute(stockMatch.Symbol))
                {
                    _vm.AddStockCommand.Execute(stockMatch.Symbol);
                }
            }
        }

        private void StockListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            if (e.AddedItems == null || e.AddedItems.Count == 0) return;
            if (e.AddedItems[0] == null) return;
            if (e.AddedItems[0] is StockInfoViewModel)
            {
                var stock = (StockInfoViewModel) e.AddedItems[0];
                _vm.SelectStockCommand.Execute(stock.Details.Symbol);
            }
        }
        private void OnAssigningVolumeSeriesHighlightStyle(object sender, AssigningCategoryStyleEventArgs args)
        {
            double minOpacity = .3, opacity = 1.0;
            if (args.SumAllSeriesHighlightingProgress > 0.0)
            {
                var progress = 0.0;
                if (args.HighlightingInfo != null)
                {
                    progress = args.HighlightingInfo.Progress;

                    if (args.HighlightingInfo.State != HighlightingState.Out)
                    {
                        var brush = Application.Current.Resources["VolumeSeriesHighlighBrush"] as Brush ??
                                    new SolidColorBrush(Color.FromArgb(255, 75, 146, 219));
                        args.Fill = brush;
                    }
                    else
                    {
                        args.Fill = new SolidColorBrush(Colors.Gray);
                    }
                }

                progress = progress - args.SumAllSeriesHighlightingProgress;
                opacity = minOpacity + (1.0 + progress) * (1.0 - minOpacity);

                args.Opacity = opacity;
                args.HighlightingHandled = true;
           
                for (var i = 0; i < this.StockVolumeChart.Series.Count; i++)
                {
                    var curr = this.StockVolumeChart.Series[i];
                    var series = sender as Infragistics.Controls.Charts.Series;
                    if (series != null && series.Name != curr.Name)
                    {
                        curr.NotifyVisualPropertiesChanged();
                    }
                }
            }
        }
      
        #endregion Event Handlers

        private void OnStockVolumeChartPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
            
	    }

	    private void ChartScaleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
            var index = this.ChartScaleComboBox.SelectedIndex;

            if (this.StockVolumeChart.Axes.Count > 2)
            {
                var volumeAxis = this.StockVolumeChart.Axes[2] as NumericYAxis;
                if (volumeAxis != null) volumeAxis.IsLogarithmic = index == 1;
            }
            if (this.StockPriceChart.Axes.Count > 1)
            {
                this.StockPriceChart.AxisValueIsLogarithmic = index == 1;
                //var priceAxis = this.StockPriceChart.Axes[0] as NumericYAxis;
                //if (priceAxis != null) priceAxis.IsLogarithmic = index == 1;
            }
        }

        private void ChartTrendLineComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
            var trendline = (TrendLineType)this.ChartTrendLineComboBox.SelectedItem;
            _vm.SetTrendlineType(trendline);
	    }
	}
}
