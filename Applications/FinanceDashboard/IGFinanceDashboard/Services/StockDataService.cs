using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using Infragistics.Framework.Services;
using Infragistics.Framework;

namespace IGShowcase.FinanceDashboard 
{ 
    /// <summary> 
    /// Represents a service for downloading latest stock data from www.quandl.com
    /// </summary>
    public class StockDataService
    {
        #region Members
        public static readonly StockDataService Instance = new StockDataService();

        protected WebServiceCache Cache;

        private const string ApiAddress = "https://www.quandl.com/";
        private const string ApiVersion = "api/v3/";
        private const string ApiKey = "?api_key=pygU9YZDGTYVrfFcFZex"; //oQsASCAbMm1zSZ-KZbyS
        private const string ApiBase = ApiAddress + ApiVersion;
      
        private const string StockOutputJson = ".json";
        private const string StockOutputXml = ".xml";
        private const string StockOutputCsv = ".csv";
        private const string StockDataset = "datasets/WIKI/";
        private const string StockDateFormat = "yyyy-MM-dd";
           
        /// <summary> Gets or sets StartDate </summary>
        public static DateTime HistoryStartDate { get; set; }
         
        #endregion
 
        public StockDataService()
        { 
            HistoryStartDate = new DateTime(2010, 1, 1); 
		
            Cache = WebServiceCache.Instance;
        }

        public List<StockDataItem> GetStockHistoryCache(string symbol)
        {
            List<StockDataItem> stockHistory = null;
          
            if (Cache.HasCacheData(symbol) &&
               !Cache.HasCacheExpired(symbol))
            {
                stockHistory = Cache.GetFromCache(symbol) as List<StockDataItem>;
                Logs.Message(this, "Requesting stock history for " + symbol + " already cached");
            }

            return stockHistory;
        }
       
        protected static string GetStartDate(DateTime startDate)
        {
            const string format = "{0:" + StockDateFormat + "}";
            return "&start_date=" + string.Format(format, startDate);
        }

        #region Public Methods

        public string GetStockHistoryUrl(string stockSymbol, DateTime historyStart)
        {
            // https://www.quandl.com/api/v3/datasets/WIKI/TSLA.json?api_key=pygU9YZDGTYVrfFcFZex

            var stockUrl = ApiBase + StockDataset + stockSymbol;
            stockUrl += StockOutputCsv + ApiKey + GetStartDate(historyStart);

            return stockUrl;
        }

        public string GetStockHistoryUrl(string stockSymbol)
        {
            return GetStockHistoryUrl(stockSymbol, HistoryStartDate);
        }
         
        public event EventHandler<StockHistoryRequestedEventArgs> RequestStockHistoryCompleted;
        protected virtual void OnRequestCompleted(StockHistoryRequestedEventArgs eventArgs)
        {
            var eventHandler = this.RequestStockHistoryCompleted;
            if (eventHandler != null)
                eventHandler(this, eventArgs);

            Logs.Trace(this, "RequestStockHistoryCompleted");
        }
        protected virtual void OnRequestCompleted(List<StockDataItem> data, object state)
        {
            var eventArgs = new StockHistoryRequestedEventArgs(data, null, false, state);
            OnRequestCompleted(eventArgs);
        }
        protected virtual void OnRequestCompleted(List<StockDataItem> data, object state, Exception error)
        {
            var eventArgs = new StockHistoryRequestedEventArgs(data, error, false, state);
            OnRequestCompleted(eventArgs);
        }

        /// <summary> Request Stock History from default date 2000-1-1 </summary>
        public void RequestStockHistoryAsync(string symbol,
            Action<string, List<StockDataItem>> callback)
        {
            RequestStockHistoryAsync(symbol, HistoryStartDate, callback);
        }
        /// <summary> Request Stock History from specified date </summary>
        public void RequestStockHistoryAsync(string symbol, DateTime startDate,
            Action<string, List<StockDataItem>> callback)
        {
            if (string.IsNullOrEmpty(symbol)) return;

            // skip if this a redundant query
            var stocks = GetStockHistoryCache(symbol);
            if (stocks == null)
            { 
                var uri = GetStockHistoryUrl(symbol, startDate);
                var stockToken = new StockDataToken();
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
                        callback(symbol, (List<StockDataItem>)data), stocks);
                }
            } 
        }  
        #endregion
         
        public void OnRequestStockHistoryCompleted(WebServiceResponse response)
        {
            var stocks = ReadStockHistory(response);

            var stockToken = response.UserState as StockDataToken;
            if (stockToken != null && 
                stockToken.Callback != null)
            {
                SynchronizationContext.Current.Post(data =>
                    stockToken.Callback(stockToken.Symbol, (List<StockDataItem>)data), stocks);
            }
        }
        private DateTime SetTime(DateTime date, int hour, int minute, int second)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second); 
        }
        public List<StockDataItem> ReadStockHistory(WebServiceResponse response)
        {
            var symbol = "????";
            var stockToken = response.UserState as StockDataToken;
            if (stockToken != null)
                symbol = stockToken.Symbol;

            var process = "Reading stock history for " + symbol;
            
            var stockHistory = new List<StockDataItem>();
            if (response.ErrorOccured)
            {
                Logs.Error(this, process + " failed: \n\t at " + response.StatusInfo);
                 
                var error = new Exception(response.ErrorStack); 
                //OnRequestCompleted(stockHistory, symbol, error); 
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
                var stock = new StockDataItem();
                stock.Date = DateTime.ParseExact(line[0], StockDateFormat, culture);
                stock.Date = SetTime(stock.Date, 16, 30, 00);

                stock.Symbol = symbol;
                stock.Open = double.Parse(line[1]);
                stock.High = double.Parse(line[2]);
                stock.Low = double.Parse(line[3]);
                stock.Close = double.Parse(line[4]);
                stock.Volume = double.Parse(line[5]);

                stock.Dividend = double.Parse(line[6]);
                stock.Split = double.Parse(line[7]);

                stockHistory.Add(stock);                
            }
            
            stockHistory.Sort((a, b) => +Comparer<DateTime>.Default.Compare(a.Date, b.Date));

            Logs.Message(this, "Data parsed: " + stockHistory.Count);
        
            #endregion

            #region Align data
            var alingnedDays = 0;
            Logs.Message(this, "Data aligned start: " + stockHistory.Count);
            if (stockHistory.Count > 0)
            {
                var first = stockHistory.First();
                while (first.Date > HistoryStartDate)
                {
                    var newStock = new StockDataItem();
                    newStock.Date = first.Date.AddDays(-1);                    
                    newStock.Symbol = symbol;
                     
                    if (newStock.Date.DayOfWeek != DayOfWeek.Saturday &&
                        newStock.Date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        alingnedDays++;
                        stockHistory.Insert(0, newStock);
                    }

                    first = newStock;
                } 

                var last = stockHistory.Last();
                var latestStock = new StockDataItem(last);
                latestStock.Date = DateTime.Now;
                
                stockHistory.Add(latestStock);
            }
            Logs.Message(this, "Data aligned end: " + stockHistory.Count + " alingnedDays=" + alingnedDays);
           
            #endregion

            Cache.UpdateCache(symbol, stockHistory);

            return stockHistory;
        }
    }
     
    public class StockHistoryRequestedEventArgs : AsyncCompletedEventArgs
    {
        public StockHistoryRequestedEventArgs(List<StockDataItem> results,
            Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            Result = results;
        }
        public StockHistoryRequestedEventArgs(List<StockDataItem> results) :
            this(results, null, false, null)
        { }

        /// <summary> Gets or sets Result </summary>
        public List<StockDataItem> Result { get; set; } 
    }

    public class StockDataToken
    {
        /// <summary> Gets or sets Symbol </summary>
        public string Symbol { get; set; }

        /// <summary> Gets or sets Callback </summary>
        public Action<string, List<StockDataItem>> Callback { get; set; }

        /// <summary> Gets or sets Start Date </summary>
        public DateTime StartDate { get; set; }
    } 
}
