using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using IGExtensions.Common.Commands;
using IGExtensions.Common.DataProviders;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using IGShowcase.FinanceDashboard.Resources;
using Infragistics;
using Infragistics.Controls.Charts;
 
using Infragistics.Framework;
using Infragistics.Framework.Services;

#if SILVERLIGHT
using IGShowcase.FinanceDashboard.StockDataServiceProxy;
using RequestStockHistoryCompletedEventArgs = IGShowcase.FinanceDashboard.StockDataServiceProxy.RequestStockHistoryCompletedEventArgs;
#endif

namespace IGShowcase.FinanceDashboard.ViewModels
{
    public class StockViewModel : ObservableModel 
	{
		#region Private Member Variables

#if SILVERLIGHT
        protected StockDataServiceProxyClient StockService = new StockDataServiceProxyClient();
#else
        protected StockDataService StockService = StockDataService.Instance;
#endif
        //private readonly StockYahooDataService _stockService;
        
        private readonly ObservableCollection<StockInfoViewModel> _stocks;
		private StockInfoViewModel _selectedStock;
       
        private ICommand    _commandAddStock;
        private ICommand    _commandRemoveStock;
        private ICommand    _commandSelectStock;
        private ICommand    _commandStockDisplayRange;
		private ICommand 	_commandMoveToNextStock;
		private ICommand	_commandChartType;

		protected readonly DispatcherTimer		StockSimulationTimer;
		protected readonly DispatcherTimer		StockServiceTimer;
		private StockChartType                  _selectedChartType = StockChartType.Area;
        private double _stockDisplayRange = 12; // number of months
        private BrushCollection 	            _stockBrushes;
        private BrushCollection                 _stockOutlines;

		private string 									_stockSearchFilter;
		private static IList<StockSearchItemViewModel> 	_stockSearchList;
        protected static Dictionary<string, StockSearchItemViewModel> StockSearchDictionary;
        #endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="StockViewModel"/> class.
		/// </summary>
		public StockViewModel()
		{
            XmlDataProvider = new XmlDataProvider();
            XmlDataProvider.LoadXmlDataCompleted += OnLoadStockSearchDataCompleted;
            SynchronizationContext.Current.Post(x => LoadStockSearchData(), null);
           
            this.IsInitialStockDataLoading = true;
            this.IsInitialStockDetailsLoading = true;

            StockSearchDictionary = new Dictionary<string, StockSearchItemViewModel>();

#if SILVERLIGHT
            StockService.RequestStockHistoryCompleted += OnRequestStockHistoryCompleted;
#endif
            //StockService = StockQuandlService.Instance;
             
            _stocks = new ObservableCollection<StockInfoViewModel>();
             
            //RefreshDetails();
            
			StockServiceTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(10)};
			StockServiceTimer.Tick += (s,e) => RefreshDetails();
			//StockServiceTimer.Start();

            StockSimulationTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.5) };
            StockSimulationTimer.Tick += (s, e) => OnSimulateStockChanges();
		}

#if SILVERLIGHT
        void OnRequestStockHistoryCompleted(object sender, RequestStockHistoryCompletedEventArgs e)
        {
            try
            {
                var stocks = e.Result; 
                    if (stocks.Count > 0)
                {
                    var last = stocks.Last();
                    OnRefreshData(last.Symbol, stocks.ToList());
                }
            }
            catch (Exception)
            {

            }
        } 
#endif
      
        public void SetTrendlineType(TrendLineType trendLineType)
        {
            foreach (var stock in _stocks)
            {
                stock.StockTrendLineType = trendLineType;
            }
        }
		#endregion Constructors

		#region Public Properties
        /// <summary>
        /// Gets or sets the MinimumValue property for the chart axes.
        /// </summary>
        public DateTime MinimumValue { get; private set; }

        /// <summary>
        /// Gets the MaximumValue property for the chart axes.
        /// </summary>
        public DateTime MaximumValue { get { return DateTime.Now; } }
        //public DateTime MaximumValue { get { return DateTime.Now.AddDays(-1); } }
     
        /// <summary>
        /// Gets the MaximumValue property for the chart axes.
        /// </summary>
        public TimeSpan RangeValue { get { return DateTime.Now.Subtract(MinimumValue); } }
        public int RangeInMonths { get { return DateTime.Now.MonthsDifference(MinimumValue); } }
        
		/// <summary>
		/// Gets the tickers.
		/// </summary>
		public ObservableCollection<StockInfoViewModel> Tickers
		{
			get { return _stocks; }
		}

		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		public StockInfoViewModel SelectedStock
		{
			get { return _selectedStock; }
			set 
			{ 
				if (value == _selectedStock) return; 
				
				_selectedStock = (value == null && _stocks.Count > 0) ? _stocks.First() : value;

                if (_selectedStock != null)
                {
                     DebugManager.Log("StockViewModel.SelectedStock changed: " + _selectedStock.Details.Symbol);

                }
			    OnPropertyChanged("SelectedStock");
				OnPropertyChanged("IsSelectedStockValid");
            }
		}
        private string _userLastStockSymbol = null;
        /// <summary>
        /// Gets or sets the last added stock.
        /// </summary>
        public string UserLastStockSymbol 
        {
            get { return _userLastStockSymbol; }
            set
            {
                if (value == _userLastStockSymbol) return;
                _userLastStockSymbol = value;
                if (_userLastStockSymbol != null)
                {
                    DebugManager.Log("StockViewModel.UserLastStockSymbol changed: " + _userLastStockSymbol);
                }
                OnPropertyChanged("UserLastStockSymbol");
            }
        }

		/// <summary>
		/// Gets or sets the type of the selected chart.
		/// </summary>
		/// <value>The type of the selected chart.</value>
        public StockChartType SelectedChartType
		{
			get { return _selectedChartType; }
            set
            {
                if (value == _selectedChartType || value == StockChartType.None ) return; 
                _selectedChartType = value;
                DebugManager.Log(" StockViewModel.SelectedChartType: " + _selectedChartType);
                OnPropertyChanged("SelectedChartType"); OnPropertyChanged("Tickers"); OnPropertyChanged("SelectedStock");
            }
		}

        public bool IsStockMatching(string search, object value)
        {
            var stockItem = value as StockSearchItemViewModel;
            return IsStockMatching(search, stockItem);
        }

        protected List<string> StockNameExclusions = new List<string>() { "Corp.", "Inc.", "Ltd.", "Co.", "S.A.", };
        public bool IsStockMatching(string search, StockSearchItemViewModel stockItem)
        {
            if (string.IsNullOrEmpty(search)) return false;
            
            search = search.ToLower();
            if (StockNameExclusions.Contains(search)) return false;

            if (stockItem != null)
            {
                if (stockItem.Symbol.ToLower().StartsWith(search))
                    return true;
                var words = stockItem.Name.ToLower().Split(' ').ToList();
                if (words.Any(word => word.StartsWith(search)))
                {
                    return true;
                }
                if (stockItem.Name.ToLower().StartsWith(search))
                    return true;
           }
            return false; // if no match
        }
        public StockSearchItemViewModel FindStockMatching(string search)
        {
            if (string.IsNullOrEmpty(search)) return null;

            if (StockSearchDictionary.ContainsKey(search))
                return StockSearchDictionary[search];

            foreach (var stockItem in StockSearchDictionary.Values)
            {
                if (IsStockMatching(search, stockItem))
                    return stockItem;
            }
            return null;
        }

        /// <summary>
		/// Gets the stocks.
		/// </summary>
		/// <value>The stocks.</value>
		public IEnumerable<StockSearchItemViewModel> StockSearchList
		{
			get
			{
			    if (string.IsNullOrEmpty(_stockSearchFilter)) return _stockSearchList;
				var filterLower = _stockSearchFilter.ToLower();
				return _stockSearchList.Where(x => { 
                    var symbolLower = x.Symbol.ToLower();
                    var nameLower = x.Name.ToLower();
                    return (symbolLower.StartsWith(filterLower) ||
                            nameLower.StartsWith(filterLower)); 
                });
			}
		}

		/// <summary>
		/// Gets or sets the stock search filter.
		/// </summary>
		/// <value>The stock search filter.</value>
		public string StockSearchFilter
		{
			get { return _stockSearchFilter; }
			set { _stockSearchFilter = value; OnPropertyChanged("StockSearchFilter"); OnPropertyChanged("StockSearchList"); }
		}

        private bool _isInitialStockDataLoading;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is retrieving stock data.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is retrieving data; otherwise, <c>false</c>.
		/// </value>
        public bool IsInitialStockDataLoading
        {
            get { return _isInitialStockDataLoading; }
            set { _isInitialStockDataLoading = value; OnPropertyChanged("IsInitialStockDataLoading"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is retrieving stock details.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is retrieving data; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialStockDetailsLoading { get; set; }


		/// <summary>
		/// Gets a value indicating whether this instance is selected stock value.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected stock value; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelectedStockValid { get { return _selectedStock != null && _selectedStock.Data != null; } }

		/// <summary>
		/// Gets a value indicating whether the selected stock instances details have been populated.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is details populated; otherwise, <c>false</c>.
		/// </value>
		public bool IsDetailsPopulated { get { return SelectedStock != null && SelectedStock.Details != null && !string.IsNullOrEmpty(SelectedStock.Details.Symbol); } }

		/// <summary>
		/// Gets or sets the stock colors.
		/// </summary>
		/// <value>The stock colors.</value>
		public BrushCollection StockBrushes
		{
			get { return _stockBrushes; }
			set { if (value == _stockBrushes) return; _stockBrushes = value; OnPropertyChanged("StockBrushes"); }
		}
        /// <summary>
        /// Gets or sets the stock outlines.
		/// </summary>
		/// <value>The stock outlines.</value>
        public BrushCollection StockOutlines
		{
            get { return _stockOutlines; }
            set { if (value == _stockOutlines) return; _stockOutlines = value; OnPropertyChanged("StockOutlines"); }
		}
        
		#endregion Public Properties

		#region Public Commands
		/// <summary>
		/// Gets the add stock command.
		/// </summary>
		/// <value>The add stock command.</value>
		public ICommand AddStockCommand
		{
			get
			{
				if (_commandAddStock == null)
				{
					_commandAddStock = new RelayCommand<string>(
					  p => { AddStockToWatchList(p); },
					  p => { return true; });

					OnPropertyChanged("AddStockCommand");
				}
				return (_commandAddStock);
			}
		}

		/// <summary>
		/// Gets the remove stock command.
		/// </summary>
		/// <value>The remove stock command.</value>
		public ICommand RemoveStockCommand
		{
			get
			{
				if (_commandRemoveStock == null)
				{
					_commandRemoveStock = new RelayCommand<string>(
					  p => { RemoveSymbolFromWatchList(p); },
					  p => { return true; }
					  );

					OnPropertyChanged("RemoveStockCommand");
				}
				return (_commandRemoveStock);
			}
		}

		/// <summary>
		/// Gets the select stock command.
		/// </summary>
		/// <value>The select stock command.</value>
		public ICommand SelectStockCommand
		{
			get
			{
				if (_commandSelectStock == null)
				{
					_commandSelectStock = new RelayCommand<string>(
						p => { SetSelectedStock(p); },
						p => { return true; }
						);

					OnPropertyChanged("SelectStockCommand");
				}

				return (_commandSelectStock);
			}
		}

		/// <summary>
		/// Gets the move to next stock.
		/// </summary>
		/// <value>The move to next stock.</value>
		public ICommand MoveToNextStock
		{
			get
			{
				if (_commandMoveToNextStock == null)
				{
					_commandMoveToNextStock = new RelayCommand<int>(
						p => { MoveToStock(p); },
						p => { return true; }
						);
				}

				return (_commandMoveToNextStock);
			}
		}

        /// <summary>
        /// Gets the stock display range command
        /// </summary>
        public ICommand StockDisplayRangeCommand
        {
            get
            {
                if (_commandStockDisplayRange == null)
                {
                   _commandStockDisplayRange = new RelayCommand<double>(
                        p => { SetStockDisplayRange(p); },
                        p => { return true; }
                    );

                    OnPropertyChanged("StockDisplayRangeCommand");
                }

                return (_commandStockDisplayRange);
            }
        }

		/// <summary>
		/// Gets the selected chart type command.
		/// </summary>
		/// <value>The selected chart type command.</value>
		public ICommand SelectedChartTypeCommand
		{
			get
			{
				if (_commandChartType == null)
				{
                    _commandChartType = new RelayCommand<StockChartType>(
						p => { SetChartType(p); },
						p => { return true; }
					);

					OnPropertyChanged("SelectedChartTypeCommand");
				}

				return (_commandChartType);
			}
		}
		#endregion Public Commands
      
        #region Public Methods
 
        private void UpdateStocksLayout()
        {
            foreach (var stock in _stocks)
            {
                //if (!stock.IsSelected)
                //{
                //    //stock.LayoutIndex = (int)(10000 - stock.FilteredData.Last().Close);
                //}
                stock.LayoutIndex = (int)(10000 - stock.Details.LastTradeAmount);
                //DebugManager.Log("StockViewModel.LayoutIndex: " + stock.Symbol + " has index " + stock.LayoutIndex);
            }
        }

        public bool ContainsStockTicker(string symbol)
        {
            foreach (var viewModel in _stocks)
            {
                if(viewModel.Symbol == symbol)
                    return true;
            }
            return false;
        }
        /// <summary>
		/// Adds the stock to watch list.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void AddStockToWatchList(string symbol)
		{
            if (ContainsStockTicker(symbol)) return;

            // check if the symbol is actually valid and known stock
            if (!IsValidStockSymbol(symbol)) return;

			// If we have more stocks than brushes, lets find the next brush to use
            var brush = GetStockNextFillBrush();
            var outline = GetStockNextOutlineBrush();
 
            var range = _stocks.Count > 0 ? _stocks[0].RangeFilter : _stockDisplayRange;
            var stock = new StockInfoViewModel(this, symbol, brush, outline, range);

            //Get the data for the stock.
            RefreshData(stock);

            //Add the stock to the other stocks.
            _stocks.Add(stock);
        
            DebugManager.Log("StockViewModel.AddedStock: " + symbol);
            OnPropertyChanged("NewTicker");
            OnPropertyChanged("Tickers");
            UserLastStockSymbol = symbol;

			// If this is the first stock, lets automatically select it.
			if (_stocks.Count == 1 || SelectedStock == null)
			{
				this.SelectedStock = _stocks.First();
			}
            
        }
        public void AddStockToWatchList(List<string> symbols)
        {
            foreach (var symbol in symbols)
            {
                if (ContainsStockTicker(symbol)) continue;

                // check if the symbol is actually valid and known stock
                if (IsValidStockSymbol(symbol))
                {
                    // If we have more stocks than brushes, lets find the next brush to use
                    var brush = GetStockNextFillBrush();
                    var outline = GetStockNextOutlineBrush();

                    var range = _stocks.Count > 0 ? _stocks[0].RangeFilter : 1;
                    var stock = new StockInfoViewModel(this, symbol, brush, outline, range);

                    //Get the data for the stock.
                    RefreshData(stock);

                    //Add the stock to the other stocks.
                    _stocks.Add(stock);
                    DebugManager.Log("StockViewModel.AddedStock: " + symbol);
                }
            }

            OnPropertyChanged("Tickers");
            if (SelectedStock == null && _stocks.Count > 0)
            {
                this.SelectedStock = _stocks.Last();
            }
        }

        public bool IsValidStockSymbol(string symbol)
        {
            if (StockSearchDictionary.Values.Count == 0) return true;

            var stockMatch = FindStockMatching(symbol);
            if (stockMatch == null) return false;
            //if (string.IsNullOrEmpty(symbol)) return false;

            // exclude already added stocks 
            var sivm = GetStockInfoBySymbolName(stockMatch.Symbol);
            return sivm == null;
        }
        /// <summary>
        /// Gets the stock next brush from <seealso cref="StockBrushes"/> collection
        /// </summary>
        /// <returns></returns>
        public Brush GetStockNextFillBrush()
        {
            Brush brush = new SolidColorBrush(Colors.White);
            if (_stockBrushes == null)
                DebugManager.LogWarning("StockViewModel could not find any StockBrushes and will default to White brush.");

            // If we have more stocks than brushes, lets find the next brush to use
            if (_stockBrushes != null)
            {
                int brushIndex = _stocks.Count();
                while (brushIndex > _stockBrushes.Count - 1)
                {
                    brushIndex = brushIndex - _stockBrushes.Count;
                }

                brush = brushIndex < _stockBrushes.Count ? _stockBrushes[brushIndex] : null;
            }
            return brush;
        }
        /// <summary>
        /// Gets the stock next outline from <seealso cref="StockOutlines"/> collection
        /// </summary>
        /// <returns></returns>
        public Brush GetStockNextOutlineBrush()
        {
            Brush brush = new SolidColorBrush(Colors.White);
            if (_stockOutlines == null)
                DebugManager.LogWarning("StockViewModel could not find any StockOutlines and will default to White outline brush.");

            // If we have more stocks than outlines, lets find the next outline brush to use
            if (_stockOutlines != null)
            {
                int brushIndex = _stocks.Count();
                while (brushIndex > _stockOutlines.Count - 1)
                {
                    brushIndex = brushIndex - _stockOutlines.Count;
                }

                brush = brushIndex < _stockOutlines.Count ? _stockOutlines[brushIndex] : null;
            }
            return brush;
        }
		/// <summary>
		/// Removes the ticker from watch list.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void RemoveSymbolFromWatchList(string symbol)
		{
			var sivm = GetStockInfoBySymbolName(symbol);

			if (sivm == null) return;

			_stocks.Remove(sivm);
            DebugManager.Log("StockViewModel.RemovedStock: " + symbol);
      
            // If we just removed the active stock, we should reset it.
            if (_stocks.Count > 0 && (sivm == SelectedStock || SelectedStock == null))
            {
                SelectedStock = _stocks.First();
            }

            if (_stocks.Count == 0)
            {
                SelectedStock = null;
            }
            //SetStockDisplayRange(_stockDisplayRange);

			OnPropertyChanged("Tickers");
		}

		/// <summary>
		/// Moves to stock based on index.
		/// </summary>
		/// <param name="index">The index.</param>
		public void MoveToStock(int index)
		{
			int iCurrentIndex = _stocks.IndexOf(SelectedStock);
			iCurrentIndex += index;

			if (iCurrentIndex < 0 || iCurrentIndex >= _stocks.Count || _stocks.Count == 0) return;

            this.SelectedStock = _stocks[iCurrentIndex];
		}

		/// <summary>
		/// Sets the selected stock.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void SetSelectedStock(string symbol)
		{
			var sivm = GetStockInfoBySymbolName(symbol);
			if (sivm == null) return;
            
            if (this.SelectedStock != null && this.SelectedStock.Symbol == sivm.Symbol) 
                return;
		    
            if (this.SelectedStock != null) 
                this.SelectedStock.IsSelected = false;
			this.SelectedStock = sivm;
            this.SelectedStock.IsSelected = true;
		}

        /// <summary>
        /// Set the Range Filter for the currently selected stock
        /// </summary>
        /// <param name="numberOfMonths"></param>
        public void SetStockDisplayRange(double numberOfMonths)
        {
            if (_stocks.Count == 0) return;

            DebugManager.Time("StockViewModel.StockDisplayRangeChanged");
            //var numberOfDays = DateTime.Now.Subtract(startDate).TotalDays;
            var numberOfDays = TimeSpanEx.FromMonths(numberOfMonths).TotalDays;
            _stockDisplayRange = numberOfMonths;
            _stocks.Each(x => x.RangeFilter = numberOfMonths);
            var minimumValue = DateTime.Now;

            if (numberOfDays == 0)
            {
                foreach (StockInfoViewModel stock in _stocks)
                {
                    if (stock.Data != null)
                    {
                        if (stock.Data.Count() > 0)
                        {
                            DateTime date = stock.Data.First().Date;
                            if (date < minimumValue)
                            {
                                minimumValue = date;
                            }
                        }
                    }
                }
            }
            else
            {
                //minimumValue = DateTime.Now.AddMonths(-1 * numberOfMonths);
                minimumValue = DateTime.Now.AddDays(-1 * numberOfDays);
            }

            DebugManager.Log("StockViewModel.StockDisplayRangeChanged to " + numberOfMonths + " months");
            DebugManager.Time("StockViewModel.StockDisplayRangeChanged");
            this.MinimumValue = minimumValue;

            OnPropertyChanged("MinimumValue");
        }

		/// <summary>
		/// Sets the type of the chart.
		/// </summary>
		/// <param name="chartType">Type of the chart.</param>
        public void SetChartType(StockChartType chartType)
		{
            this.SelectedChartType = chartType;
		}

		/// <summary>
		/// Refreshes the details.
		/// </summary>
		public void RefreshDetails()
		{
            //StockService.RefreshDetails(_stocks.Select(x => x.Symbol).ToList(), OnRefreshDetails);
		}

		/// <summary>
		/// Refreshes the data.
		/// </summary>
		public void RefreshData(StockInfoViewModel stock)
		{
#if SILVERLIGHT
            StockService.RequestStockHistoryAsync(stock.Symbol);
#else
            StockService.RequestStockHistoryAsync(stock.Symbol, OnRefreshData);
#endif

            //StockService.RequestStockHistory(stock.Symbol, OnRefreshData, DateTime.Now.AddYears(-100));
       
		}
		#endregion Public Methods

		#region Private Methods

        protected XmlDataProvider XmlDataProvider;

        /// <summary>
        /// Gets the name of the stock info by symbol.
        /// </summary>
		StockInfoViewModel GetStockInfoBySymbolName(string symbol)
		{
			var sivm = (from item in _stocks
						where item.Symbol == symbol
						select item).DefaultIfEmpty(null).FirstOrDefault();
			return sivm;
		}
        protected bool UseStockSimulation = true;

        public void OnSimulateStockChanges()
        {
            if (SelectedStock != null)
            {
                SimulateStockChanges();
                //SelectedStock.Details = currentStock;
            }
        }

        protected Random Random = Randomizer.Instance;
        public void SimulateStockChanges()
        {
            foreach (var stock in _stocks)
            {
                if (stock.Details.IsStockTrading)
                {
                    stock.SimmulateChanges();
                }
            }
        }

        protected bool IsStockDetailsUpdating = false;
        /// <summary>
		/// Called when [refresh details].
		/// </summary>
		/// <param name="data">The data.</param>
		private void OnRefreshDetails(IDictionary<string, StockTickerDetails> data)
        {
            StockSimulationTimer.Stop();
            foreach (var stock in _stocks)
			{
				// It is possible a new stock was added while a query was in progress
				// If so, we can ignore it as it will be picked up on the next query
                if (data.ContainsKey(stock.Symbol))
                {   
                    // update stock details
                    stock.Details = data[stock.Symbol];
               
                    if (!stock.Details.IsStockTrading)
                    {
                        stock.Details.Name = stock.Symbol + " " + AppStrings.Stock_DataIsMissing;
                        continue;
                    }

                    if (stock.Data.Any() && (stock.Details.Open == 0 || stock.Details.Volume == 0))
                    {
                        var lastTrade = stock.Data.Last();
                        stock.Details.Open = lastTrade.Open;
                        stock.Details.LastTradeAmount = lastTrade.Close;
                        stock.Details.DailyHigh = lastTrade.High;
                        stock.Details.DailyLow = lastTrade.Low;
                        //stock.Details.c = lastTrade.;
                        stock.Details.Volume = lastTrade.Volume;
                        
                        stock.SimmulateChanges(false);
                    }
				    
                    // update stock data with latest values
				    if (stock.Data.Count != 0)
				    {
                        stock.UpdateLastStockData(stock.Details);
                        DebugManager.LogData("StockViewModel.StockDetails changed: " + stock.Details.Change);
                    }
				}
			}
            UpdateStocksLayout();
            
            this.IsInitialStockDetailsLoading = false;
            OnPropertyChanged("IsInitialStockDetailsLoading");
        
			OnPropertyChanged("SelectedStock");
            OnPropertyChanged("Tickers");
			OnPropertyChanged("IsDetailsPopulated");

            IsStockDetailsUpdating = false;
            StockSimulationTimer.Start();
		}

       

        /// <summary>
		/// Called when [refresh data].
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		/// <param name="data">The data.</param>
        private void OnRefreshData(string symbol, List<StockPriceItem> data)
		{
            Logs.Message(this, "OnRefreshData for " + symbol + " stock: " + data.Count);
			var sivm = GetStockInfoBySymbolName(symbol);
			if (sivm == null)
			{
				return;
			}
		    var yearAgo = DateTime.Now.AddYears(-1);
            var low = double.MaxValue;
            var high = double.MinValue;
            var collection = new ObservableCollection<StockTickerData>();
            foreach (var item in data)
            {
                var stock = new StockTickerData();
                stock.Open = item.Open;
                stock.High = item.High;
                stock.Low = item.Low;
                stock.Close = item.Close;
                stock.Volume = item.Volume;
                stock.Date = item.Date;

                collection.Add(stock);
                // find low/high range for last year
                if (item.Date > yearAgo)
                {
                    low = Math.Min(low, item.Low);
                    high = Math.Max(high, item.High);
                }
            }
            sivm.Data = collection;
          
            SetStockDisplayRange(_stockDisplayRange);

		    var name = symbol;
            var stockMatch = FindStockMatching(symbol);
            if (stockMatch != null)
            {
                name = stockMatch.Name;
                if (name.EndsWith("Corp.")) name = name.Replace("Corp.", "Corporation");
                if (name.EndsWith("Inc")) name = name.Replace("Inc", "Inc.");
                if (name.EndsWith("Ltd")) name = name.Replace("Ltd", "Ltd.");
            }
             
            if (sivm.Data.Any())
            {
                var lastDataPoint = sivm.Data.Last();
                DebugManager.LogData("StockViewModel.OnRefreshData initializing stock details : " + symbol);

                sivm.Details = new StockTickerDetails
                {
                    Symbol = symbol,
                    Name = name,
                    DailyHigh = lastDataPoint.High,
                    DailyLow = lastDataPoint.Low,
                    Open = lastDataPoint.Open,
                    Volume = lastDataPoint.Volume,
                    PreviousClose = lastDataPoint.Close,
                    LastUpdate =  lastDataPoint.Date,
                    LastTradeAmount = lastDataPoint.Open,
                    LastTradeDate = lastDataPoint.Date.ToString(CultureInfo.InvariantCulture),
                    LastTradeTime = lastDataPoint.Date.TimeOfDay.ToString(), 
                    YearlyLow = low,
                    YearlyHigh = high,
                };
            }
            
            UpdateStocksLayout();

			OnPropertyChanged("SelectedStock");
            OnPropertyChanged("Tickers");
            //OnPropertyChanged("IsDetailsPopulated");			

            StockSimulationTimer.Start();
            StockServiceTimer.Start();

            this.OnInitialDataLoaded();
            //TODO-MT wait for all stocks to load
            this.IsInitialStockDataLoading = false;
        }

		/// <summary>
		/// Called when [load stock search data].
		/// </summary>
		private void LoadStockSearchData()
		{
			try
			{ 
                this.XmlDataProvider.LoadXmlDataResource("Assets/Data/", "StockSymbols.xml");
			}
			catch (Exception ex)
			{
                DebugManager.LogError("StockViewModel.OnLoadStockSearchData(): " + ex);
				// We are hiding this exception because it is most likely caused
				// by blend not being able to find the stocksymbols.xml file
				// This only happens until the project is built at least once inside blend
			}
		}
        void OnLoadStockSearchDataCompleted(object sender, LoadXmlDataCompletedEventArgs e)
        {
            if (!e.HasError)
            {
               _stockSearchList = (from xNode in e.XmlDocument.Element("stockSymbols").Elements("stock")
                                    select new StockSearchItemViewModel
                                    {
                                        Symbol = xNode.Attribute("Symbol").Value,
                                        Exchange = xNode.Attribute("Source").Value,
                                        Name = xNode.Attribute("Description").Value,
                                        //NameShort = xNode.Attribute("Description").Value.Remove(StockNameExclusions),
                                        Parent = this
                                    }).ToList();

               foreach (var item in _stockSearchList)
               {
                   PopulateStockSearchDictionary(item);
               }
           
               StockNameExclusions = StockNameExclusions.Select(v => v.ToLower()).ToList();
                
               OnPropertyChanged("StockSearchList");
            }
        }
        protected void PopulateStockSearchDictionary(StockSearchItemViewModel stockItem)
        {
            if (!StockSearchDictionary.ContainsKey(stockItem.Symbol))
            {
                StockSearchDictionary.Add(stockItem.Symbol, stockItem);
            }
            //if (!StockSearchByName.ContainsKey(stockItem.NameShort))
            //{
            //    StockSearchByName.Add(stockItem.NameShort, stockItem);
            //}

        }
		#endregion Private Methods

		#region Event Handlers
        public event EventHandler InitialDataLoaded;
         
        private void OnInitialDataLoaded()
        {
            if (InitialDataLoaded == null) return;

            InitialDataLoaded(this, EventArgs.Empty);
        }

		#endregion
	}

    public enum StockChartType
    {
        None = -1,
        Line = 0,
        Area = 1,
        CandleStick = 2,
        OpenHighLowClose = 3,
        StepArea = 4,
        RangeArea = 5,
    }
}
