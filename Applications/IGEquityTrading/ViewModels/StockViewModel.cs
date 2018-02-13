//-------------------------------------------------------------------------
// <copyright file="StockViewModel.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using Infragistics;
using IGTrading.Commands;
using IGTrading.Models;
using IGTrading.Services;

namespace IGTrading.ViewModels
{
	public class StockViewModel : INotifyPropertyChanged
	{
		#region Private Member Variables
        //private bool _areInitialTransactionsPerformed = false;
        private Random r = new Random(DateTime.Now.Millisecond);
		private readonly YahooStockDataService _stockService;

        //stores the accounts which can be used for trading
        private readonly ObservableCollection<StockTradingAccount> _accounts = new ObservableCollection<StockTradingAccount>();
        //stores data related to all stocks traded
        private readonly ObservableCollection<StockInfoViewModel> _stocks = new ObservableCollection<StockInfoViewModel>();
        //stores current pricing details of all stocks traded
        private readonly ObservableCollection<StockTickerDetailsViewModel> _allStockDetails = new ObservableCollection<StockTickerDetailsViewModel>();
        //stores data relevant to stocks currently in the portfolio
        private readonly ObservableCollection<StockPositionViewModel> _stockPositions = new ObservableCollection<StockPositionViewModel>();
        //stores transaction data
        private readonly ObservableCollection<StockTransactionViewModel> _stockTransactionHistory = new ObservableCollection<StockTransactionViewModel>();
        //stores symbol-stock detail information pairs
        private readonly Dictionary<string, StockTickerDetailsViewModel> _stockDetailsDictionary = new Dictionary<string, StockTickerDetailsViewModel>();
        
        private StockInfoViewModel _selectedStock;

        private ICommand _commandAddStock;
        private ICommand _commandBuyStock;
        private ICommand _commandRemoveStock;
        private ICommand _commandSelectStock;
        private ICommand _commandSellStock;
        private ICommand _commandStockDisplayRange;
		private ICommand _commandMoveToNextStock;
		private ICommand _commandChartType;

        private readonly DispatcherTimer _syntheticUpdateTimer;

		private int					_selectedChartType = 3;//default chart type is set to candlestick

        private BrushCollection 	_stockColors;

		private string 									_stockSearchFilter;
		//stores the full list of available stocks
        private static IList<StockSearchItemViewModel> 	_stockSearchList = new List<StockSearchItemViewModel>();
        #endregion Private Member Variables

        private bool _suspendUpdates = false;
        public void SuspendUpdates()
        {
            _suspendUpdates = true;
        }

        public void ResumeUpdates()
        {
            _suspendUpdates = false;
        }

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="StockViewModel"/> class.
		/// </summary>
		public StockViewModel()
		{
			SynchronizationContext.Current.Post(x => OnLoadStockSearchData(), null);

            var acc = new StockTradingAccount
            {
                Balance = 10000,
                ID = "080228"
            };

		    _accounts.Add(acc);

		    this.SelectedAccount = acc;

			_stockService = YahooStockDataService.Instance;

			_syntheticUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
            _syntheticUpdateTimer.Tick += (s, e) => UpdateValuesForWindowRecords();
            _syntheticUpdateTimer.Start();
        }
		#endregion Constructors

		#region Public Properties
        /// <summary>
        /// Gets the transaction history
        /// </summary>
        public ObservableCollection<StockTransactionViewModel> StockTransactionHistory
        {
            get { return _stockTransactionHistory; }
        }
        /// <summary>
        /// Gets the stock positions
        /// </summary>
        public ObservableCollection<StockPositionViewModel> StockPositions
        {
            get { return _stockPositions; }
        }
        /// <summary>
        /// Gets the information for all stocks
        /// </summary>
        public ObservableCollection<StockTickerDetailsViewModel> AllStocksInfo
        {
            get { return _allStockDetails; }
        }
		/// <summary>
		/// Gets the information for all stocks
		/// </summary>
		/// <value>The tickers.</value>
		public ObservableCollection<StockInfoViewModel> Tickers
		{
			get { return _stocks; }
		}

        #region SelectedAccount
        /// <summary>
        /// local variable _SelectedAccount
        /// </summary>
        private StockTradingAccount _selectedAccount;

        /// <summary>
        /// Identifies the SelectedAccount property.
        /// </summary>		
        public StockTradingAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                RaisePropertyChanged("SelectedAccount");
            }
        }
        #endregion  //SelectedAccount
        

		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		/// <value>The selected stock.</value>
		public StockInfoViewModel SelectedStock
		{
			get { return _selectedStock; }
			set 
			{ 
				if (value == _selectedStock) return; 
				
				_selectedStock = (value == null && _stocks.Count > 0) ? _stocks.First() : value; 
				
				RaisePropertyChanged("SelectedStock");
				RaisePropertyChanged("IsSelectedStockValid");
			}
		}

		/// <summary>
		/// Gets or sets the type of the selected chart.
		/// </summary>
		/// <value>The type of the selected chart.</value>
		public int SelectedChartType
		{
			get { return _selectedChartType; }
			set { if (value == _selectedChartType) return; _selectedChartType = value; 
                RaisePropertyChanged("SelectedChartType"); 
                RaisePropertyChanged("Tickers"); RaisePropertyChanged("SelectedStock"); }
		}

		/// <summary>
		/// Gets the stocks.
		/// </summary>
		/// <value>The stocks.</value>
		public IEnumerable<StockSearchItemViewModel> StockSearchList
		{
			get
			{
				if (string.IsNullOrEmpty(_stockSearchFilter)) 
                    return _stockSearchList;

				var filterLower = _stockSearchFilter.ToLower();

				return _stockSearchList.Where(x => { var symbolLower = x.Symbol.ToLower(); return (symbolLower.StartsWith(filterLower) || symbolLower.StartsWith(filterLower)); });
			}
		}

		/// <summary>
		/// Gets or sets the stock search filter.
		/// </summary>
		/// <value>The stock search filter.</value>
		public string StockSearchFilter
		{
			get { return _stockSearchFilter; }
			set { _stockSearchFilter = value; RaisePropertyChanged("StockSearchFilter"); RaisePropertyChanged("StockSearchList"); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is retrieving data.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is retrieving data; otherwise, <c>false</c>.
		/// </value>
		public bool IsInitialDataLoading { get; private set; }

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
		public BrushCollection StockColors
		{
			get { return _stockColors; }
			set { if (value == _stockColors) return; _stockColors = value; RaisePropertyChanged("StockColors"); }
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
					  p => { AddSymbolToWatchList(p); },
					  p => { return true; });

					RaisePropertyChanged("AddStockCommand");
				}
				return (_commandAddStock);
			}
		}

        /// <summary>
        /// Gets the add stock command.
        /// </summary>
        /// <value>The add stock command.</value>
        public ICommand BuyStockCommand
        {
            get
            {
                if (_commandBuyStock == null)
                {
                    _commandBuyStock = new RelayCommand<string>(
                      p => { AddSymbolToWatchList(p); },
                      p => { return true; });

                    RaisePropertyChanged("BuyStockCommand");
                }
                return (_commandBuyStock);
            }
        }

        /// <summary>
        /// Gets the sell stock command.
        /// </summary>
        /// <value>The sell stock command.</value>
        public ICommand SellStockCommand
        {
            get
            {
                if (_commandSellStock == null)
                {
                    _commandSellStock = new RelayCommand<string>(
                      p => { AddSymbolToWatchList(p); },
                      p => { return true; });

                    RaisePropertyChanged("SellStockCommand");
                }
                return (_commandSellStock);
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

					RaisePropertyChanged("RemoveStockCommand");
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

					RaisePropertyChanged("SelectStockCommand");
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
                    _commandStockDisplayRange = new RelayCommand<int>(
                        p => { SetStockDisplayRange(p); },
                        p => { return true; }
                    );

                    RaisePropertyChanged("StockDisplayRangeCommand");
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
					_commandChartType = new RelayCommand<int>(
						p => { SetChartType(p); },
						p => { return true; }
					);

					RaisePropertyChanged("SelectedChartTypeCommand");
				}

				return (_commandChartType);
			}
		}
		#endregion Public Commands
      
        #region Public Methods

        /// <summary>
        /// Goes through the StockSearchList and checks
        /// if the entered Stock symbol is actually an
        /// existing stock.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public bool CheckForExistingStockBySymbolName(string symbol)
        {
            return _stockDetailsDictionary.ContainsKey(symbol);
        }


        /// <summary>
		/// Adds the tick to watch list.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void AddSymbolToWatchList(string symbol)
		{
			if (string.IsNullOrEmpty(symbol)) return;

            //See if the symbol is actually an existing stock
            if (!CheckForExistingStockBySymbolName(symbol))
            {
                return;
            }

            //Get the stock info
			var sivm = GetStockInfoBySymbolName(symbol);

			if (sivm != null) return;

            int iBrushIndex = _stocks.Count();

			// If we have more stocks than brushes, lets find the next brush to use
			Brush brush = null;
			if (_stockColors != null)
			{
				while (iBrushIndex > _stockColors.Count - 1)
				{
					iBrushIndex = iBrushIndex - _stockColors.Count;
				}

				brush = iBrushIndex < _stockColors.Count ? _stockColors[iBrushIndex] : null;
			}
            StockInfoViewModel stockViewModel = new StockInfoViewModel(this, _stockDetailsDictionary[symbol], brush,
                    _stocks.Count > 0 ? _stocks[0].RangeFilter : 3);
            _stocks.Add(stockViewModel);
            
            RefreshStockData(stockViewModel);
			RaisePropertyChanged("Tickers");

			// If this is the first stock, lets automatically select it.
			if (_stocks.Count == 1 || SelectedStock == null)
			{
				SelectedStock = _stocks.First();
			}
		}

		/// <summary>
		/// Removes the ticker symbol from watch list.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void RemoveSymbolFromWatchList(string symbol)
		{
			var sivm = GetStockInfoBySymbolName(symbol);

			if (sivm == null) return;

			_stocks.Remove(sivm);

            // If we just removed the active stock, we should reset it.
            if (_stocks.Count > 0 && (sivm == SelectedStock || SelectedStock == null))
            {
                SelectedStock = _stocks.First();
            }

			RaisePropertyChanged("Tickers");
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

			SelectedStock = _stocks[iCurrentIndex];
		}

		/// <summary>
		/// Sets the selected stock.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void SetSelectedStock(string symbol)
		{
			var sivm = GetStockInfoBySymbolName(symbol);

			if (sivm == null) return;

			SelectedStock = sivm;
		}

        /// <summary>
        /// Set the Range Filter for the currently selected stock
        /// </summary>
        /// <param name="numberOfMonths"></param>
        public void SetStockDisplayRange(int numberOfMonths)
        {
            if (SelectedStock == null) return;

            _stocks.Each(x => x.RangeFilter = numberOfMonths);
        }

		/// <summary>
		/// Sets the type of the chart.
		/// </summary>
		/// <param name="chartType">Type of the chart.</param>
		public void SetChartType(int chartType)
		{
			SelectedChartType = chartType;
		}

		/// <summary>
		/// Refreshes the ticking stock details.
		/// </summary>
		public void RefreshDetails()
		{
            //refreshes overview data for all stocks
			_stockService.RefreshDetails(_stockSearchList.Select(x => x.Symbol).ToList(), OnRefreshDetails);

            //refreshes detailed stock price data for selected stocks
            RefreshData();
		}
        public void RefreshStockData(StockInfoViewModel stockInfoViewModel)
        {
            _stockService.RefreshData(stockInfoViewModel.Symbol, 
                DateTime.Now.AddYears(-1 * stockInfoViewModel.RangeFilter), OnRefreshData);
      
            //MT remove
            //_stockService.RefreshDetails(_stockSearchList.Select(x => x.Symbol).ToList(), OnRefreshDetails);

        }

		/// <summary>
		/// Refreshes the historical pricing data for all the watched stocks.
		/// </summary>
		public void RefreshData()
		{
            //load data if available in the cache
			foreach (var item in _stocks)
			{
                RefreshStockData(item);
			}
		}
		#endregion Public Methods

		#region Private Methods
        /// <summary>
        /// Gets the name of the stock info by symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
		StockInfoViewModel GetStockInfoBySymbolName(string symbol)
		{
			var sivm = (from item in _stocks
						where item.Symbol == symbol
						select item).DefaultIfEmpty(null).FirstOrDefault();

			return sivm;
		}

        /// <summary>
        /// Updates values for the ask, bid and last traded amount members of a stock ticker item
        /// </summary>
        private void UpdateValuesForWindowRecords()
        {
            if (!_suspendUpdates)
            {
                foreach (StockTickerDetailsViewModel currentStock in _stockDetailsDictionary.Values)
                {
                    //buy = ask
                    currentStock.Ask = Math.Round(currentStock.Open * (r.NextDouble()/10 + 0.95),2);
                    //sell = bid, will be just a bit smaller than the buy price
                    currentStock.Bid = Math.Round(currentStock.Ask - (r.NextDouble() / 10) * currentStock.Ask, 2);

                    currentStock.UpdateBuySellPrices();
                    currentStock.LastTradeAmount = Math.Round(currentStock.Bid + (r.NextDouble() / 10) * (currentStock.Ask - currentStock.Bid), 2);
                }
            
                RaisePropertyChanged("AllStocksInfo");
            }
        }

		/// <summary>
		/// Called when [refresh details].
		/// </summary>
		/// <param name="data">The data.</param>
		private void OnRefreshDetails(IDictionary<string, StockTickerDetails> data)
		{
            //copy the stock details data to the two repositories of stock data
            int i = 0;
            foreach (string key in data.Keys)
            {
                StockTickerDetailsViewModel stdvm = new StockTickerDetailsViewModel(data[key]);

                if (_stockDetailsDictionary.ContainsKey(key))
                {
                    _stockDetailsDictionary[key].Update(data[key]);
                    _allStockDetails[i++].Update(data[key]);
                }
                else
                {
                    _stockDetailsDictionary.Add(key, stdvm);
                    _allStockDetails.Add(stdvm);
                }
            }
            
            //copy the historic data for watched stocks
            foreach (var sivm in _stocks)
            {
                // It is possible a new stock was added while a query was in progress
                // If so, we can ignore it as it will be picked up on the next query
                if (data.ContainsKey(sivm.Symbol))
                {
                    sivm.Details.Update(data[sivm.Symbol]);
                }
            }

            //trigger updates for stock data
            RaisePropertyChanged("SelectedStock");
            RaisePropertyChanged("AllStocksInfo");
            RaisePropertyChanged("Tickers");
			RaisePropertyChanged("IsDetailsPopulated");			
		}

        /// <summary>
        /// Loads cached stock data
        /// </summary>
        private void LoadCachedStockDetails()
        {
            System.Windows.Resources.StreamResourceInfo sriStockPriceData = Application.GetResourceStream(new Uri("/Assets/StockPriceData.bin", UriKind.Relative));

            if (sriStockPriceData != null && sriStockPriceData.Stream != null)
            {
                //load previously serialized data
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                OnRefreshDetails((IDictionary<string, StockTickerDetails>)formatter.Deserialize(sriStockPriceData.Stream));
            }

            //perform some transactions to generate transaction records
            ExecuteInitialTransactions();
        }

        /// <summary>
        /// Executes a few sample buy/sell transactions
        /// </summary>
        private void ExecuteInitialTransactions()
        {
            //abort performing initial transactions if there're no stocks loaded
            if (_stockDetailsDictionary.Count == 0)
                return;

            //buy initial stocks at market prices
            while (_stockPositions.Count < 20)
            {
                int randomShareIndex = r.Next(_allStockDetails.Count);
                BuyStock(new StockTransactionViewModel
                {
                    Account = _accounts[0],
                    PricePerShare = _allStockDetails[randomShareIndex].Ask,
                    Quantity = r.Next(20) + 5,
                    StockTickerDetailsViewModel = _allStockDetails[randomShareIndex],
                    TransactionType = TransactionType.Buy
                });
            }
            //sell some of the stocks just bought at market prices
            int soldStocks = 0;
            while (soldStocks < 5)
            {
                int randomStockPositionIndex = r.Next(_stockPositions.Count);
                SellStock(new StockTransactionViewModel
                {
                    Account = _accounts[0],
                    PricePerShare = _stockPositions[randomStockPositionIndex].StockTickerDetails.Bid,
                    Quantity = _stockPositions[randomStockPositionIndex].Quantity,
                    StockPosition = _stockPositions[randomStockPositionIndex],
                    StockTickerDetailsViewModel = _stockPositions[randomStockPositionIndex].StockTickerDetails,
                    TransactionType = TransactionType.Sell
                });

                ++soldStocks;
            }
        }

        /// <summary>
        /// Executes a buy stock transaction
        /// </summary>
        /// <param name="stvm"></param>
        public void BuyStock(StockTransactionViewModel stvm)
        {
            //charge current stock trading account
            _accounts[0].Balance -= stvm.CurrentPricePerShare * stvm.Quantity;
            
            //add a new position
            _stockPositions.Add(new StockPositionViewModel
                {
                    PricePaid = stvm.CurrentPricePerShare,
                    Quantity = stvm.Quantity,
                    StockTickerDetails = _stockDetailsDictionary[stvm.Symbol]
                });
            //insert a transaction into history
            _stockTransactionHistory.Add(stvm);
        }

        /// <summary>
        /// Executes a sell stock transaction
        /// </summary>
        /// <param name="stvm"></param>
        public void SellStock(StockTransactionViewModel stvm)
        {
            //charge current stock trading account
            _accounts[0].Balance += stvm.CurrentPricePerShare * stvm.Quantity;
            
            //set the final price the transaction was executed for
            stvm.PricePerShare = stvm.CurrentPricePerShare;
            //remove a new position
            StockPositionViewModel stockToSell = _stockPositions.First(x => x.ID == stvm.StockPosition.ID);
            if (stockToSell.Quantity == stvm.Quantity)
            {
                _stockPositions.Remove(stockToSell);
            }
            else
            {
                stockToSell.Quantity -= stvm.Quantity;
            }
            //insert a transaction into history
            _stockTransactionHistory.Add(stvm);
        }
        
		/// <summary>
		/// Called when [refresh data].
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		/// <param name="data">The data.</param>
		private void OnRefreshData(string symbol, IEnumerable<StockTickerData> data)
		{
			IsInitialDataLoading = false;
			RaisePropertyChanged("IsInitialDataLoading");

			var sivm = GetStockInfoBySymbolName(symbol);

			if (sivm == null)
			{
				return;
			}

			sivm.Data = data;

			RaisePropertyChanged("SelectedStock");
            RaisePropertyChanged("Tickers");
		}
		/// <summary>
		/// Loads stock symbol and cached detail data
		/// </summary>
		private void OnLoadStockSearchData()
		{
			try
			{
				var sriStockSymbols = Application.GetResourceStream(new Uri("/Assets/StockList.xml", UriKind.Relative));
	
				if (sriStockSymbols == null || sriStockSymbols.Stream == null) return;
	
				var xDoc = XDocument.Load(sriStockSymbols.Stream);
	
				_stockSearchList = (from xNode in xDoc.Element("stockSymbols").Elements("stock")
									select new StockSearchItemViewModel
									{
										Symbol = xNode.Attribute("Symbol").Value,
										Exchange = xNode.Attribute("Source").Value,
										Name = xNode.Attribute("Name").Value,
										Parent = this
									}).ToList();

                RaisePropertyChanged("StockSearchList");

                //load cached details
                LoadCachedStockDetails();
			}
			catch 
			{
				// We are hiding this exception because it is most likely caused
				// but blend not being able to find the stocksymbols.xml file
				// This only happens until the project is built at least once inside 
				// blend
			}
		}
		#endregion Private Methods

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		internal void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged == null) return;
			
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}

    public static class EnumerableEx
    {
        // Methods
        public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T local in values)
            {
                action(local);
            }
            return values;
        }
    }


}
