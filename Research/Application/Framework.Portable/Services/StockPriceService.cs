using System;
using System.Collections.Generic; 
using System.ComponentModel;
using System.Globalization; 
using System.Linq; 
using System.Threading; 

namespace Infragistics.Framework.Services 
{ 
    /// <summary> 
    /// Represents a service for downloading latest stock data from www.quandl.com
    /// </summary>
    public class StockPriceService
    {
        #region Members
        public static readonly StockPriceService Instance = new StockPriceService();

        protected WebServiceCache Cache;

        private const string ApiAddress = "https://www.quandl.com/";
        private const string ApiVersion = "api/v3/";
        private const string ApiKey = "?api_key=pygU9YZDGTYVrfFcFZex";
        private const string ApiBase = ApiAddress + ApiVersion;
      
        private const string StockOutputJson = ".json";
        private const string StockOutputXml = ".xml";
        private const string StockOutputCsv = ".csv";
        private const string StockDataset = "datasets/WIKI/";
        private const string StockDateFormat = "yyyy-MM-dd";
           
        /// <summary> Gets or sets StartDate </summary>
        protected static DateTime StockStartDate { get; private set; }
        protected static Dictionary<string, DateTime> TradingDates = new Dictionary<string, DateTime>();

        public event EventHandler<GetStockDataCompletedEventArgs> GetStockDataCompleted;
        #endregion
 
        public StockPriceService()
        {
            StockStartDate = new DateTime(2000, 1, 1);		
            Cache = WebServiceCache.Instance;
        }
         
        #region Public Methods
                  
        /// <summary> Request Stock History from a date   </summary>
        public void GetStockDataAsync(string symbol, DateTime startDate)
        {
            GetStockDataAsync(symbol, startDate, null);
        }
        
        /// <summary> Request Stock History from default date 2000-1-1 </summary>
        public void GetStockDataAsync(string symbol)
        {
            GetStockDataAsync(symbol, StockStartDate, null);
        }
        
        /// <summary> Request Stock History from default date 2000-1-1 </summary>
        public void GetStockDataAsync(string symbol,
            Action<string, StockPriceData> callback)
        {
            GetStockDataAsync(symbol, StockStartDate, callback);
        }

        /// <summary> Request Stock History from specified date </summary>
        public void GetStockDataAsync(string symbol, DateTime startDate,
            Action<string, StockPriceData> callback)
        {
            if (string.IsNullOrEmpty(symbol)) return;

            if (callback == null)
                callback = OnGetStockDataCompleted;

            // skip if this a redundant query
            var stocks = GetStockDataCache(symbol);
            if (stocks == null)
            { 
                var uri = GetStockDataUrl(symbol, startDate);
                var stockToken = new StockToken();
                stockToken.Symbol = symbol;
                stockToken.Callback = callback;
                
                Logs.Message(this, "Requesting stock history for " + symbol + "...");
                 
                WebService.Request(uri, stockToken, OnRequestStockHistoryCompleted);
            }
            else
            {
                if (callback != null)
                {
                    SynchronizationContext.Current.Post(data =>
                        callback(symbol, (StockPriceData)data), stocks);
                } 
            } 
        }  
        #endregion
          
        protected StockPriceData GetStockDataCache(string symbol)
        {
            StockPriceData stockHistory = null;
          
            if (Cache.HasCacheData(symbol) &&
               !Cache.HasCacheExpired(symbol))
            {
                stockHistory = Cache.GetFromCache(symbol) as StockPriceData;
                Logs.Message(this, "Requesting stock history for " + symbol + " already cached");
            }

            return stockHistory;
        }

        protected string GetStockDataUrl(string stockSymbol)
        {
            return GetStockDataUrl(stockSymbol, StockStartDate);
        }

        protected string GetStockDataUrl(string stockSymbol, DateTime historyStart)
        {
            // https://www.quandl.com/api/v3/datasets/WIKI/TSLA.json?api_key=pygU9YZDGTYVrfFcFZex
            
            var stockUrl = ApiBase + StockDataset + stockSymbol;
            stockUrl += StockOutputCsv + ApiKey + GetStartDate(historyStart);

            return stockUrl;
        }

        protected string GetStartDate(DateTime startDate)
        {
            var format = "{0:" + StockDateFormat + "}";
            return "&start_date=" + string.Format(format, startDate);
        }

        protected virtual void OnGetStockDataCompleted(GetStockDataCompletedEventArgs eventArgs)
        { 
            if (GetStockDataCompleted != null)
                GetStockDataCompleted(this, eventArgs);
            Logs.Trace(this, "GetStockDataCompleted");
        }
        protected virtual void OnGetStockDataCompleted(StockPriceData data, object state)
        {
            var eventArgs = new GetStockDataCompletedEventArgs(data, null, false, state);
            OnGetStockDataCompleted(eventArgs);
        }
        protected virtual void OnGetStockDataCompleted(string symbol, StockPriceData data)
        {
            data.Titile = symbol;
            var eventArgs = new GetStockDataCompletedEventArgs(data, null, false, symbol);
            OnGetStockDataCompleted(eventArgs);
        }
        
        protected virtual void OnGetStockDataFailed(object state, Exception error)
        {
            var eventArgs = new GetStockDataCompletedEventArgs(null, error, false, state);
            OnGetStockDataCompleted(eventArgs);
        }

        protected void OnRequestStockHistoryCompleted(WebServiceResponse response)
        {
            var stocks = ReadStockHistory(response);
            var stockToken = response.UserState as StockToken;
            if (stockToken != null && 
                stockToken.Callback != null)
            {                
                stocks.Titile = stockToken.Symbol;

                SynchronizationContext.Current.Post(data =>
                    stockToken.Callback(stockToken.Symbol, (StockPriceData)data), stocks);
            }
        }

        protected StockPriceData ReadStockHistory(WebServiceResponse response)
        {
            var symbol = "????";
            var stockToken = response.UserState as StockToken;
            if (stockToken != null)
                symbol = stockToken.Symbol;

            var process = "Reading stock history for " + symbol;
            
            var stockHistory = new StockPriceData();
            if (response.ErrorOccured)
            {
                Logs.Error(this, process + " failed: \n\t at " + response.StatusInfo);
                 
                var error = new Exception(response.ErrorStack);

                //OnGetStockDataCompleted(stockHistory, symbol, error);

                return stockHistory;
            }
             
            Logs.Message(this, process + " status: " + response.StatusInfo);

            var stream = response.ResultStream;
            var csv = DataProvider.GetCsvFile(stream);
            var culture = CultureInfo.InvariantCulture;

            #region Parse csv data and create stock history
            //      0,    1,    2,   3,     4,      5,        6,           7, 
            //   Date, Open, High, Low, Close, Volume, Dividend, Split Ratio, Adj. Open,Adj. High,Adj. Low,Adj. Close,Adj. Volume
            // 2015-09-10,247.23,250.7231,245.33,248.381,2700138.0,0.0,1.0,247.23,250.7231,245.33,248.381,2700138.0
            // 2015-09-09,252.05,254.25,248.303,248.91,3363641.0,0.0,1.0,252.05,254.25,248.303,248.91,3363641.0

            for (var i = 1; i < csv.Count; i++)
            {
                var line = csv[i];
                var stock = new StockPrice();
                stock.Date = DateTime.ParseExact(line[0], StockDateFormat, culture);

                stock.Symbol = symbol;
                stock.Open = double.Parse(line[1]);
                stock.High = double.Parse(line[2]);
                stock.Low = double.Parse(line[3]);
                stock.Close = double.Parse(line[4]);
                stock.Volume = double.Parse(line[5]);

                stock.Dividend = double.Parse(line[6]);
                stock.Split = double.Parse(line[7]);

                stockHistory.Add(stock);

                var day = stock.Date.ToString();
                if (!TradingDates.ContainsKey(day))
                     TradingDates.Add(day, stock.Date);
            }
            //stockHistory.Sort((x, y) => +(Comparer<DateTime>.Default.Compare(x.Date, y.Date)));
            stockHistory.Sort((x, y) => SortBy.Ascending(x.Date, y.Date));
            //stockHistory.SortByProperty("Date", SortDirection.Ascending);

            Logs.Message(this, "Data parsed: " + stockHistory.Count);
        
            #endregion

            #region Align data
            if (stockHistory.Count > 0)
            {
                var first = stockHistory.First();
                while (first.Date > StockStartDate)
                {
                    var newStock = new StockPrice();
                    newStock.Date = first.Date.AddDays(-1);
                    newStock.Symbol = symbol;
                    if (TradingDates.ContainsKey(newStock.Date.ToString()))
                    {
                        stockHistory.Insert(0, newStock);
                    }

                    first = newStock;
                }
                var last = stockHistory.Last();
                var latestStock = new StockPrice(last);
                latestStock.Date = DateTime.Now;
                
                stockHistory.Add(latestStock);
            }
            Logs.Message(this, "Data aligned: " + stockHistory.Count);
           
            #endregion

            Cache.UpdateCache(symbol, new List<object> { stockHistory });

            //OnGetStockDataCompleted(stockHistory, symbol);

            return stockHistory;
        }
    }
     
    public class GetStockDataCompletedEventArgs : AsyncCompletedEventArgs
    {
        public GetStockDataCompletedEventArgs(StockPriceData results,
            Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            Result = results;
        }
        public GetStockDataCompletedEventArgs(StockPriceData results) :
            this(results, null, false, null)
        { }

        /// <summary> Gets or sets Result </summary>
        public StockPriceData Result { get; set; } 

        public bool HasResult { get { return this.Result != null; } } 
    }

    public class StockToken
    {
        /// <summary> Gets or sets Symbol </summary>
        public string Symbol { get; set; }

        /// <summary> Gets or sets Callback </summary>
        public Action<string, StockPriceData> Callback { get; set; }

        /// <summary> Gets or sets Start Date </summary>
        public DateTime StartDate { get; set; }
    }

   

    

}
