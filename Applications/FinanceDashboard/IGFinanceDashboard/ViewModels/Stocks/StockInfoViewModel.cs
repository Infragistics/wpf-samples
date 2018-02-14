using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;

namespace IGShowcase.FinanceDashboard.ViewModels
{
    public class StockInfoViewModel : ObservableModel //INotifyPropertyChanged
	{
		#region Private Member Variables
		private static Dictionary<double, int> 	_rangeIntervals; // in months
		//private static Dictionary<int, int> 	_rangeIntervalYLookup;

		private StockTickerDetails				_details = new StockTickerDetails();
        private ObservableCollection<StockTickerData> _data = new ObservableCollection<StockTickerData>();
		private StockViewModel					_parent;

        private double  _rangeFilter = 0;        // in months
		private int 	_rangeIntervalX	= 4;
		private int 	_rangeIntervalY	= 1;
		private Brush	_brush			= new SolidColorBrush(Colors.Yellow);
		#endregion Private Member Variables
        
		#region Constructor
		/// <summary>
		/// Initializes the <see cref="StockInfoViewModel"/> class.
		/// </summary>
		static StockInfoViewModel()
		{
            //TOOD-MT implement updating of range interval based on range filter
            _rangeIntervals = new Dictionary<double, int>();
            _rangeIntervals[120] = 60;	    // 10 Years
            _rangeIntervals[60] = 60;	    // 5 Years
            _rangeIntervals[48] = 60;	    // 4 Years
            _rangeIntervals[36] = 60;	    // 3 Years
            _rangeIntervals[24] = 60;	    // 2 Years
            _rangeIntervals[12] = 60;	    // 1 Year
            _rangeIntervals[6] = 30;	    // 6 Months
            _rangeIntervals[3] = 21;	    // 3 Months
            _rangeIntervals[2] = 10;	    // 2 Months
            _rangeIntervals[1] = 5;	        // 1 Month
            _rangeIntervals[0.25] = 1;	    // 1 week
            _rangeIntervals[0] = 60; 	    // unlimited

            //_rangeIntervalYLookup = new Dictionary<int, int>();
            //_rangeIntervalYLookup[12] 	= 60;	// 50 $ 
            //_rangeIntervalYLookup[6] 	= 30;	// 25 $ 
            //_rangeIntervalYLookup[3] 	= 21;	// 10 $ 
            //_rangeIntervalYLookup[2] 	= 14;	// 5 $ 
            //_rangeIntervalYLookup[1] 	= 7;	// 1 $
            //_rangeIntervalYLookup[0]	= 120; 	// unlimited
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StockInfoViewModel"/> class.
		/// </summary>
		public StockInfoViewModel()
		{
			
		}

        public static int GlobalCreationIndex = 1;
	    /// <summary>
	    /// Initializes a new instance of the <see cref="StockInfoViewModel"/> class.
	    /// </summary>
	    /// <param name="parent">The parent.</param>
	    /// <param name="symbol">The symbol.</param>
	    /// <param name="brush">The brush.</param>
	    /// <param name="range">The range.</param>
	    public StockInfoViewModel(StockViewModel parent, string symbol, Brush brush, int range)
            : this(parent, symbol, brush, brush, range)
        { }
        public StockInfoViewModel(StockViewModel parent, string symbol, Brush brush, Brush outline, double range)
        {
            if (outline == null) 
                outline = brush;
            this.Outline = outline;
            this.Brush = brush;
           
            _parent = parent;
            _details.Symbol = symbol;

            //this.LayoutIndex = --GlobalLayoutIndex;
            _layoutCreationIndex = GlobalCreationIndex++;
            _layoutIndex = _layoutCreationIndex;

            this.RangeFilter = range == 0 ? 1 : range;
        }

	    #endregion Constructor

		#region Public Properties
		/// <summary>
		/// Gets the symbol.
		/// </summary>
		/// <value>The symbol.</value>
		public string Symbol
		{
			get { return Details != null ? Details.Symbol : string.Empty; }
			set { Details.Symbol = value; }
		}

		/// <summary>
		/// Gets or sets the details.
		/// </summary>
		/// <value>The details.</value>
		public StockTickerDetails Details
		{
			get { return _details; }
			set { if (value == _details) return; _details = value; OnPropertyChanged("Details"); OnPropertyChanged("Symbol"); }
		}

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>The data.</value>
        public ObservableCollection<StockTickerData> Data
		{
			get { return _data; }
			set { if (value == _data) return;

                //if (_data != null) _data.CollectionChanged -= OnDataCollectionChanged;
                _data = value;
                //if (_data != null) _data.CollectionChanged += OnDataCollectionChanged;

                OnPropertyChanged("Data");
                UpdateFilteredData(); 
                Parent.OnPropertyChanged("Tickers"); Parent.OnPropertyChanged("SelectedStock");
            }
		}
        void OnDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //if (e.Action == NotifyCollectionChangedAction.Replace && 
            //    _data.Any() && _filteredData.Any())
            //{
            //    var lastDataItem = _data.Last();
            //    var lastFilteredItem = _filteredData.Last();
            //    // assuming we update only the last item
            //    if (lastDataItem.Date.DayOfYear == lastFilteredItem.Date.DayOfYear)
            //    {
            //        _filteredData[_filteredData.Count() - 1] = lastDataItem;
            //        OnPropertyChanged("FilteredData");
            //    }
            // }
        }

        private ObservableCollection<StockTickerData> _filteredData = new ObservableCollection<StockTickerData>();
        private IEnumerable<StockTickerData> _filteredOldData;
        private IEnumerable<StockTickerData> _filteredActualData;
        public IEnumerable<StockTickerData> FilteredOldData
        {
            get
            {
                return _filteredOldData;
            }
            set
            {
                if (value == _filteredOldData) return;
                _filteredOldData = value;
                OnPropertyChanged("FilteredOldData"); Parent.OnPropertyChanged("SelectedStock");
            }
        }
        
        /// <summary>
        /// Returns Stock Data based on the current Range Filter (if range filter = 0 than all data is returned)
        /// </summary>
        public ObservableCollection<StockTickerData> FilteredData
        {
            get
            {
                return _filteredData;
            }
            set
            {
                if (value == _filteredData) return; 
                _filteredData = value; 
                OnPropertyChanged("FilteredData"); Parent.OnPropertyChanged("Tickers"); Parent.OnPropertyChanged("SelectedStock");
            }
        }

        // TODO-MT remove
        private StockTickerData _highlightedDataItem;
        public StockTickerData HighlightedDataItem
        {
            get
            {
                return _highlightedDataItem;
            }
            set
            {
                if (value == _highlightedDataItem) return;
                _highlightedDataItem = value;
                OnPropertyChanged("HighlightedDataItem");
                UpdateHighlightedData();  OnPropertyChanged("HighlightedData");
            }
        }

        // TODO-MT remove
        private void UpdateHighlightedData()
        {
            //if (_highlightedDataItem == null) 
            //{
            //    _highlightedData = null; // _highlightedData;
            //}
            //else
            //{
            //    _highlightedData = GetFilteredData();
            //    for (int i = 0; i < _highlightedData.Count(); i++)
            //    {
            //        if (_highlightedData[i].Date == _highlightedDataItem.Date)
            //            _highlightedData[i] = _filteredData.ElementAt(i).Copy();
            //        else
            //        {
            //            var date = _filteredData.ElementAt(i).Date;
            //            _highlightedData[i] = new StockTickerData { Date = date};
            //        }
            //    }
            //}
                
        }
        // TODO-MT remove
        private List<StockTickerData> _highlightedData;
        public List<StockTickerData> HighlightedData
        {
            get
            {
                return _highlightedData;
            }
            set
            {
                if (value == _highlightedData) return;
                _highlightedData = value;
                OnPropertyChanged("HighlightedData"); 
            }
        }
        private int FindRangeInterval()
        {
            if (_rangeIntervals.ContainsKey(_rangeFilter))
            {
                return _rangeIntervals[_rangeFilter];
            }
            return 1;
        }
        #region Methods
        public void UpdateFilteredData()
        {
            DebugManager.Time("StockInfoViewModel.UpdateFilteredData");
            if (_data == null) _filteredData = _data;
            if (_rangeFilter == 0) _filteredData = _data;
            else
            {
                _filteredData = GetFilteredData();
                _filteredActualData = GetFilteredData(); 
            }
            DebugManager.Time("StockInfoViewModel.UpdateFilteredData");

            OnPropertyChanged("FilteredData");
            RangeIntervalX = FindRangeInterval();
        }

        private ObservableCollection<StockTickerData> GetFilteredData()
        {
            return GetFilteredData(this.RangeFilter);
        }
        private ObservableCollection<StockTickerData> GetFilteredData(double rangeFilter)
        {
            //DateTime rangeMinDate = DateTime.Now.AddMonths(-1 * rangeFilter);
            var numberOfDays = TimeSpanEx.FromMonths(rangeFilter).TotalDays;
            var rangeMinDate = DateTime.Now.AddDays(-numberOfDays);
            return GetFilteredData(rangeMinDate);
        }
        private ObservableCollection<StockTickerData> GetFilteredData(DateTime rangeMinDate)
        {
            var filteredData = _data.Where(x => x.Date.Date >= rangeMinDate.Date).ToObservableCollection();
            return filteredData;
        }

        public void AnimateFilteredData()
        {
            foreach (var item in _filteredData)
            {
                item.Close = 0;
                item.Volume = 0;
                //var newDataPoint = new StockTickerData { Date = item.Date, Close = item.Close, Open = item.Open, High = item.High, Low = item.Low };
                //filteredTempData.Add(newDataPoint);
            }
            OnPropertyChanged("FilteredData");
            //  OnPropertyChanged("FilteredData");

            for (int i = 0; i < _filteredData.Count(); i++)
            {
                _filteredData.ElementAt(i).Close = _filteredActualData.ElementAt(i).Close;
            }
            OnPropertyChanged("FilteredData");

        }
       // private IEnumerable<StockTickerData> _highlightData;

        #endregion
        /// <summary>
        /// Sets a filter for the data (based in number of months)
        /// </summary>
        public double RangeFilter
        {
            get { return _rangeFilter; }
            set 
			{
				if (value == _rangeFilter) return;

				// This indicates a number in months
				_rangeFilter 	= value;

                //// What is the X Axis interval going to look like?
                //bool bFoundInterval = false;
                //int i = _rangeFilter;

                //// Set the default value (the unlimited value)
                //_rangeIntervalX = _rangeIntervals[0];
                //_rangeIntervalY = _rangeIntervalYLookup[0];

                //// Find the one we are looking for or the next closest
                //while (!bFoundInterval && i > 0)
                //{
                //    if (_rangeIntervals.ContainsKey(i))
                //    {
                //        _rangeIntervalX = _rangeIntervals[i];
                //        bFoundInterval = true;
                //    } else i--;
                //}

                //i = _rangeFilter;
                //while (!bFoundInterval && i > 0)
                //{
                //    if (_rangeIntervalYLookup.ContainsKey(i))
                //    {
                //        _rangeIntervalY = _rangeIntervalYLookup[i];
                //        bFoundInterval = true;
                //    }
                //    else i--;
                //}

                OnPropertyChanged("RangeFilter");
                UpdateFilteredData(); // OnPropertyChanged("FilteredData");
				OnPropertyChanged("RangeIntervalX");
				OnPropertyChanged("RangeIntervalY");
			}

        }
        // TODO-MT remove
		/// <summary>
		/// Gets or sets the range interval.
		/// </summary>
		public int RangeIntervalX
		{
			get { return _rangeIntervalX; }
            set { if (value == _rangeIntervalX) return; _rangeIntervalX = value; 
                OnPropertyChanged("RangeIntervalX");
                //UpdateFilteredData();  // OnPropertyChanged("FilteredData");
            }
		}

        // TODO-MT remove
        /// <summary>
		/// Gets or sets the range interval.
		/// </summary>
		public int RangeIntervalY
		{
			get { return _rangeIntervalY; }
			set { if (value == _rangeIntervalY) return; _rangeIntervalY = value; 
                OnPropertyChanged("RangeIntervalY");
                //UpdateFilteredData(); // OnPropertyChanged("FilteredData");
            }
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		public StockViewModel Parent
		{
			get { return _parent; }
		}

        /// <summary>
        /// Gets or sets the brush for this stock.
        /// </summary>
        public Brush Brush
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the outline for this stock.
        /// </summary>
        public Brush Outline
        {
            get;
            set;
        }

	    private bool _isSelected;
        /// <summary>
        /// Gets or sets the selection state.
        /// </summary>
         public bool IsSelected
        {
            get { return _isSelected; }
            set 
            { 
                if (value == _isSelected) return;
                _isSelected = value;
                //this.LayoutIndex = _isSelected ? 10000 : _layoutCreationIndex;
                OnPropertyChanged("IsSelected"); 
            }
        }

        private readonly int _layoutCreationIndex = 1;
        private int _layoutIndex = 1;
        /// <summary>
        /// Gets or sets the layout order (Canvas.ZIndex).
        /// </summary>
        public int LayoutIndex
        {
            get { return _layoutIndex; }
            set { if (value == _layoutIndex) return; _layoutIndex = value; OnPropertyChanged("LayoutIndex"); }
        }

        private TrendLineType _trendLineType = TrendLineType.None;
        /// <summary>
        /// Gets or sets the TrendLine Type of series
        /// </summary>
        public TrendLineType StockTrendLineType
        {
            get { return _trendLineType; }
            set { if (value == _trendLineType) return; _trendLineType = value; OnPropertyChanged("TrendLineType"); }
        }
		#endregion Public Properties

		#region Public Commands
		#endregion Public Commands

		#region Public Methods
		#endregion Public Methods

	    public void UpdateLastStockData()
	    {
            UpdateLastStockData(this.Details.ToStockData());
	    }

	    public void UpdateLastStockData(StockTickerDetails stockDetails)
	    {
	        UpdateLastStockData(stockDetails.ToStockData());
	    }

	    public void UpdateLastStockData(StockTickerData stockData)
	    {
            if (Data.Any() && FilteredData.Any())
            {
                var lastStockData = this.Data.Last();
                if (lastStockData.Date.DayOfYear == stockData.Date.DayOfYear)
                {
                    this.Data[this.Data.Count - 1] = stockData;
                    this.FilteredData[this.FilteredData.Count - 1] = stockData;
                }
                else
                {
                    this.Data.Add(stockData);
                    this.FilteredData.Add(stockData);
                }
            }
	    }
        protected Random Random = Randomizer.Instance;
        public void SimmulateChanges(bool updateStockData = true)
        {
            var currentDetails = this.Details;
            //buy = ask and sell = bid, will be just a bit smaller than the buy price
            currentDetails.Ask = Math.Round(currentDetails.Open * (Random.NextDouble() / 20 + 0.975), 2);
            currentDetails.Bid = Math.Round(currentDetails.Ask - ((Random.NextDouble() / 20) * currentDetails.Ask), 2);
            var delta = currentDetails.Ask - currentDetails.Bid;
            currentDetails.LastTradeAmount = Math.Round(currentDetails.Bid + ((Random.NextDouble() / 20) * delta), 2);

            if (currentDetails.DailyLow == 0 || 
                currentDetails.DailyLow > currentDetails.LastTradeAmount)
                currentDetails.DailyLow = currentDetails.LastTradeAmount;
            if (currentDetails.DailyHigh == 0 ||
                currentDetails.DailyHigh < currentDetails.LastTradeAmount)
                currentDetails.DailyHigh = currentDetails.LastTradeAmount;

            currentDetails.UpdateTradeChange();
            currentDetails.Volume = currentDetails.Volume * (Random.NextDouble() / 5 + 0.90);
            //currentDetails.LastUpdate = DateTime.Now;

            // update stock data with latest values
            if (updateStockData)
                UpdateLastStockData(currentDetails);
        }
    
    }

    public class DesignStockViewModel  
    {
        public DesignStockViewModel()
        {
            Details = new StockTickerDetails();
            FilteredData = new DesignStockDataList();
        }
        public StockTickerDetails Details { get; set; }
        public DesignStockDataList FilteredData { get; set; }
        public string Symbol { get; set; }
        
    }
    public class DesignStockDataList : List<StockTickerData>
    {
        
    }
}
