using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using IGExtensions.Common.Commands;
using IGExtensions.Framework;
using Infragistics;
using Infragistics.Framework;
using Infragistics.Controls.Charts;

namespace IGShowcase.FinanceDashboard
{
    public class StockViewModel : ObservableModel 
	{
		#region Private Member Variables

        protected StockDataService StockService = StockDataService.Instance;
         
        private ICommand _commandAddStock;
        private ICommand _commandRemoveStock;
        private ICommand _commandSelectStock;  

		protected readonly DispatcherTimer StockSimulationTimer;
        
		private static IList<StockSearchItem> _stockSearchList;
        private static StockSearchDictionary _stockSearchDictionary;
        #endregion Private Member Variables

		#region Constructors 
		public StockViewModel()
		{
            SynchronizationContext.Current.Post(x => LoadStockSearchData(), null);
           
            this.IsInitialStockDataLoading = true;
            this.IsInitialStockDetailsLoading = true;

            _stockSearchDictionary = new StockSearchDictionary();
             
            _stocks = new ObservableCollection<StockTradeData>(); 
             
            StockSimulationTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.5) };
            StockSimulationTimer.Tick += (s, e) => OnSimulateStockChanges();
             
		}  
        #endregion  

        #region Public Properties
        
        private readonly ObservableCollection<StockTradeData> _stocks;
		/// <summary>
		/// Gets the tickers.
		/// </summary>
		public ObservableCollection<StockTradeData> Tickers
		{
			get { return _stocks; }
		}

        private List<StockTradeData> _stockTickers;
		public List<StockTradeData> TickersData
		{
			get
            { 
                _stockTickers = new List<StockTradeData>();
                foreach (var item in _stocks)
                {  
                    _stockTickers.Add(item);
                }                
                return _stockTickers;
            }
		}
         
        private StockTradeData _selectedStock;
		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		public StockTradeData SelectedStock
		{
			get { return _selectedStock; }
			set 
			{
                if (value == null) return; 
				if (value != null && 
                    _selectedStock != null && 
                    _selectedStock.Symbol == value.Symbol) return;

                _selectedStock = value;

			    OnPropertyChanged("SelectedStock");
				OnPropertyChanged("IsSelectedStockValid");
            }
		}  

        public bool IsStockMatching(string search, object value)
        {
            var stockItem = value as StockSearchItem;
            return IsStockMatching(search, stockItem);
        }

        protected List<string> StockNameExclusions = new List<string>() { "Corp.", "Inc.", "Ltd.", "Co.", "S.A.", "L.L.C.", "L.P."};

        public bool IsStockMatching(string search, StockSearchItem stockItem)
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

        public StockSearchItem FindStockMatching(string search)
        {
            StockSearchItem match = null;
            //Logs.Task("FindStockMatching " + search);
            if (string.IsNullOrEmpty(search)) return null;

            if (_stockSearchDictionary.ContainsKey(search))
                match = _stockSearchDictionary[search];

            if (match != null)
            {
                foreach (var stockItem in _stockSearchDictionary.Values)
                {
                    if (IsStockMatching(search, stockItem))
                    {
                        match = stockItem; break;
                    } 
                } 
            }
            //Logs.Task("FindStockMatching " + search);
            return match;
        }
        
		private string _stockSearchFilter;
        /// <summary>  Gets the stocks. </summary>
		public IEnumerable<StockSearchItem> StockSearchList
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
        public bool IsInitialStockDataLoading
        {
            get { return _isInitialStockDataLoading; }
            set { _isInitialStockDataLoading = value; OnPropertyChanged("IsInitialStockDataLoading"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is retrieving stock details.
        /// </summary>
        public bool IsInitialStockDetailsLoading { get; set; }
        
		/// <summary>
		/// Gets a value indicating whether this instance is selected stock value.
		/// </summary> 
		public bool IsSelectedStockValid { get { return _selectedStock != null && _selectedStock.Count > 0; } }

		/// <summary>
		/// Gets a value indicating whether the selected stock instances details have been populated.
		/// </summary>
		public bool IsDetailsPopulated { get { return SelectedStock != null && SelectedStock.Details != null && !string.IsNullOrEmpty(SelectedStock.Details.Symbol); } }

		private BrushCollection _stockBrushes;
        /// <summary> Gets or sets the stock colors. </summary>
		public BrushCollection StockBrushes
		{
			get { return _stockBrushes; }
			set { if (value == _stockBrushes) return; _stockBrushes = value; OnPropertyChanged("StockBrushes"); }
		}

        private FinancialChartYAxisMode _YAxisMode = FinancialChartYAxisMode.Numeric;
        /// <summary> Gets or sets YAxisMode </summary>
        public FinancialChartYAxisMode YAxisMode
        {
            get { return _YAxisMode; }
            set { if (_YAxisMode == value) return; _YAxisMode = value; OnPropertyChanged("YAxisMode"); }
        } 
        #endregion Public Properties

        #region Public Commands
        /// <summary>
        /// Gets the add stock command.
        /// </summary>
        public ICommand CommandAddStock
		{
			get
			{
				if (_commandAddStock == null)
				{
					_commandAddStock = new RelayCommand<string>(
					  p => { WatchListAddStock(p); },
					  p => { return true; });

					OnPropertyChanged("CommandAddStock");
				}
				return (_commandAddStock);
			}
		}

		/// <summary>
		/// Gets the remove stock command.
		/// </summary>
		public ICommand CommandRemoveStock
		{
			get
			{
				if (_commandRemoveStock == null)
				{
					_commandRemoveStock = new RelayCommand<string>(
					  p => { WatchListRemoveStock(p); },
					  p => { return true; }
					  );

					OnPropertyChanged("CommandRemoveStock");
				}
				return (_commandRemoveStock);
			}
		}

		/// <summary>
		/// Gets the select stock command.
		/// </summary>
		public ICommand CommandSelectStock
		{
			get
			{
				if (_commandSelectStock == null)
				{
					_commandSelectStock = new RelayCommand<string>(
						p => { SetSelectedStock(p); },
						p => { return true; }
						);

					OnPropertyChanged("CommandSelectStock");
				} 
				return (_commandSelectStock);
			}
		}
         
		#endregion Public Commands
      
        #region Public Methods
   
        public bool ContainsStockSymbol(string symbol)
        {
            foreach (var viewModel in _stocks)
            {
                if (viewModel.Symbol == symbol)
                    return true;
            }
            return false;
        }
        /// <summary> Adds single stock to watch list. </summary> 
		public void WatchListAddStock(string symbol)
		{
            WatchListAddStock(new List<string> { symbol });
        }
        /// <summary> Adds multiple stocks to watch list. </summary> 
		public void WatchListAddStock(List<string> symbols)
        {
            foreach (var symbol in symbols)
            {
                // check if we already have the stock and if is valid
                if (ContainsStockSymbol(symbol)) continue;
                if (!IsValidStockSymbol(symbol)) continue;
                 
                // find the next brush to use
                var brush = _stockBrushes.ElementAtOrNext(_stocks.Count); 
                 
                var stock = new StockTradeData(this, symbol, brush);
                
                // get the data for the stock.
                StockService.RequestStockHistoryAsync(stock.Symbol, OnRefreshData);
                 
                // Add the stock to the other stocks.
                _stocks.Add(stock); 

                Logs.Message(this, "AddedStock: " + symbol);                
            } 

            if (SelectedStock == null && _stocks.Count > 0)
            {
                this.SelectedStock = _stocks.First();
                this.SelectedStock.IsSelected = true;
            }  
        }

        public bool IsValidStockSymbol(string symbol)
        {
            if (_stockSearchDictionary.Values.Count == 0) return true;

            var stockMatch = FindStockMatching(symbol);
            if (stockMatch == null) return false;
            //if (string.IsNullOrEmpty(symbol)) return false;

            // exclude already added stocks 
            var sivm = GetStockInfoWith(stockMatch.Symbol);
            return sivm == null;
        }

        public void UpdateStockBrushes()
        {  
            for (int i = 0; i < _stocks.Count; i++)
            {
                var brushId = i % _stockBrushes.Count;
                var brush = _stockBrushes.ElementAtOrNext(i); 
                _stocks[i].Brush = brush; 
            }
        }
       
        /// <summary>Removes the ticker from watch list. </summary>
		public void WatchListRemoveStock(string symbol)
		{
            if (_stocks.Count == 1) return;

			var sivm = GetStockInfoWith(symbol); 
			if (sivm == null) return;

			_stocks.Remove(sivm);
            Logs.Message(this, "RemovedStock: " + symbol);
      
            UpdateStockBrushes();
            // If we just removed the active stock, we should reset it.
            if (_stocks.Count > 0 && (sivm == SelectedStock || SelectedStock == null))
            {
                SelectedStock = _stocks.First();
            }

            if (_stocks.Count == 0)
            {
                SelectedStock = null;
            }

            //TODO-MT add when FC supports initial points with double.Nan
            //if (_stocks.Count == 1)
            //    this.YAxisMode = FinancialChartYAxisMode.Numeric;
            //else
            //    this.YAxisMode = FinancialChartYAxisMode.PercentChange;

            OnPropertyChanged("Tickers");
			OnPropertyChanged("TickersData");
		}
          
		/// <summary>
		/// Sets the selected stock.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void SetSelectedStock(string symbol)
		{
			var sivm = GetStockInfoWith(symbol);
			if (sivm == null) return;
            
            if (this.SelectedStock != null && 
                this.SelectedStock.Symbol == sivm.Symbol) 
                return;
		    
            if (this.SelectedStock != null) 
                this.SelectedStock.IsSelected = false;

			this.SelectedStock = sivm;
            this.SelectedStock.IsSelected = true;
		}
         
		#endregion Public Methods

		#region Private Methods
         
        /// <summary>
        /// Gets the name of the stock info by symbol.
        /// </summary>
		private StockTradeData GetStockInfoWith(string symbol)
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
		/// Called when service is done with refreshing stock data
		/// </summary> 
        private void OnRefreshData(string symbol, List<StockDataItem> data)
		{
            Logs.Message(this, "OnRefreshData for " + symbol + " stock items: " + data.Count);
			var sivm = GetStockInfoWith(symbol);
			if (sivm == null) return;
			
		    var yearAgo = DateTime.Now.AddYears(-1);
            var yearLow = double.MaxValue;
            var yearHigh = double.MinValue;
            
            foreach (var item in data)
            { 
                sivm.Add(item);

                // find low/high range for last year
                if (item.Date > yearAgo)
                {
                    yearLow = Math.Min(yearLow, item.Low);
                    yearHigh = Math.Max(yearHigh, item.High);
                }
            } 
            
		    var name = symbol;
            var stockMatch = FindStockMatching(symbol);
            if (stockMatch != null)
            {
                name = stockMatch.Name;
                if (name.EndsWith("Corp.")) name = name.Replace("Corp.", "Corporation");
                if (name.EndsWith("Inc")) name = name.Replace("Inc", "Inc.");
                if (name.EndsWith("Ltd")) name = name.Replace("Ltd", "Ltd.");
            }
             
            if (sivm.Any())
            {
                var lastTrade = sivm.Last();
                Logs.Message(this, "OnRefreshData for " + symbol + " stock details");

                sivm.Details = new StockTradeDetails
                {
                    Symbol = symbol,
                    Name = name,
                    DailyHigh = lastTrade.High,
                    DailyLow = lastTrade.Low,
                    Open = lastTrade.Open,
                    Volume = lastTrade.Volume,
                    PreviousClose = lastTrade.Close,
                    LastUpdate =  lastTrade.Date,
                    LastTradeAmount = lastTrade.Open,
                    LastTradeDate = lastTrade.Date.ToString(CultureInfo.InvariantCulture),
                    LastTradeTime = lastTrade.Date.TimeOfDay.ToString(), 
                    YearlyLow = yearLow,
                    YearlyHigh = yearHigh,
                };
            }
              
			OnPropertyChanged("SelectedStock");
            //OnPropertyChanged("Tickers");
            OnPropertyChanged("TickersData");

            //TODO-MT add when FC supports initial points with double.Nan
            //if (_stocks.Count == 1)
            //    this.YAxisMode = FinancialChartYAxisMode.Numeric;
            //else
            //    this.YAxisMode = FinancialChartYAxisMode.PercentChange;

            StockSimulationTimer.Start();

            this.OnInitialDataLoaded();
             
            this.IsInitialStockDataLoading = false;
        }

		/// <summary> load stock search data </summary>
		private void LoadStockSearchData()
		{ 
            var asmb = this.GetType().Assembly;
            var stockSymbols = DataProvider.GetCsvTable("StockSymbols.csv", asmb);

            _stockSearchList = new List<StockSearchItem>();
            for (int i = 1; i < stockSymbols.Rows.Count; i++)
            {
                var csvRow = stockSymbols.Rows[i];
                if (csvRow.Count == 7)                
                {
                    var stock = new StockSearchItem();
                    stock.Exchange = csvRow[0];
                    stock.Symbol = csvRow[1];
                    stock.Name = csvRow[2];
                    stock.MarketCap = csvRow[3];
                    stock.Sector = csvRow[5];
                    stock.Industry = csvRow[6];

                    if (!csvRow[4].Equals("N/A"))
                        stock.IPO = int.Parse(csvRow[4]);

                    _stockSearchList.Add(stock); 

                    if (!_stockSearchDictionary.ContainsKey(stock.Symbol))
                         _stockSearchDictionary.Add(stock.Symbol, stock); 
                }
            }
              
            StockNameExclusions = StockNameExclusions.Select(v => v.ToLower()).ToList();
            OnPropertyChanged("StockSearchList"); 
        }
     	#endregion Private Methods

		#region Event Handlers
        public event EventHandler InitialDataLoaded;
         
        private void OnInitialDataLoaded()
        {
            if (InitialDataLoaded != null)
                InitialDataLoaded(this, EventArgs.Empty);
        }

		#endregion
	}
     
}
