using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace Infragistics.Framework.Services 
{
    /// <summary> Represents a service for downloading latest stock data from www.quandl.com </summary>
    public sealed class StockQuandlService
    {

        #region Members
        public static readonly StockQuandlService Instance = new StockQuandlService();

        private DataCacheService _cache;

        private const string ApiAddress = "https://www.quandl.com/";
        private const string ApiVersion = "api/v3/";
        private const string ApiKey = "api_key=pygU9YZDGTYVrfFcFZex";

        private const string StockOutputJson = ".json";
        private const string StockOutputXml = ".xml";
        private const string StockOutputCsv = ".csv";
        private const string StockDataset = "datasets/WIKI/";
        private const string StockDateFormat = "yyyy-MM-dd";
           
        /// <summary> Gets or sets StartDate </summary>
        public DateTime HistoryStartDate { get; set; }

        #endregion
 
        private StockQuandlService()
        {
            HistoryStartDate = new DateTime(2000, 1, 1);
            //HistoryStartDate = new DateTime(1993, 7, 13, 12, 00, 0);
		
            _cache = DataCacheService.Instance;
        }

        internal string GetStartDate(DateTime startDate)
        {
            const string format = "{0:" + StockDateFormat + "}";
            return "&start_date=" + string.Format(format, startDate);
        }

        internal string GetStockHistoryUrl(string stockSymbol, DateTime historyStart)
        {
            // https://www.quandl.com/api/v3/datasets/WIKI/MSFT/metadata.json?api_key=dsahFHUiewjjd
            // https://www.quandl.com/api/v3/datasets/WIKI/MSFT/metadata.json?api_key=pygU9YZDGTYVrfFcFZex
            // https://www.quandl.com/api/v3/datasets/WIKI/AAPL.json?api_key=pygU9YZDGTYVrfFcFZex

            var stockUrl = ApiAddress + ApiVersion + StockDataset +
            stockSymbol + StockOutputCsv + "?" + ApiKey + GetStartDate(historyStart);

            return stockUrl;
        }

        #region Public Methods
        /// <summary> Request Stock History from default date 2000-1-1 </summary>
        public void RequestStockHistory(string symbol,
            Action<string, StockTickerList> callback)
        {
            RequestStockHistory(symbol, callback, HistoryStartDate);
        }

        /// <summary> Request Stock History from specified date </summary>
        public void RequestStockHistory(string symbol,
            Action<string, StockTickerList> callback, DateTime startDate)
        {
            if (string.IsNullOrEmpty(symbol)) return;

            var stockToken = new StockToken();
            stockToken.Symbol = symbol;
            stockToken.Callback = callback;
            stockToken.StartDate = startDate;
            
            var process = "StockQuandlService -> Requesting stock history for " + stockToken.Symbol;

            // skip if this a redundant query
            if (_cache.HasCacheData(symbol) && !_cache.HasCacheExpired(symbol))
            {
                Debug.WriteLine(process + " skipped");

                SynchronizationContext.Current.Post(data =>
                    callback(symbol, (StockTickerList)data), _cache.GetFromCache(symbol));

                return;
            }
            var uri =  new Uri(GetStockHistoryUrl(symbol, startDate));
         
            Debug.WriteLine(process + "...");
   
            // var wc = new WebDownload();
            //wc.OpenReadCompleted += OnRequestStockHistoryCompleted;
            //WebService.Download(uri, stockToken, OnRequestStockHistoryCompleted);

            var endDate = DateTime.Now.AddDays(-1.0);
            var uri2 = new Uri(string.Format(UrlStockData,
                                                  symbol,
                                                  startDate.Month - 1,
                                                  startDate.Day,
                                                  startDate.Year,
                                                  endDate.Month - 1,
                                                  endDate.Day,
                                                  endDate.Year));
        

            var wc = new WebClient();
            wc.OpenReadCompleted += OnRequestStockHistoryCompleted;
            wc.OpenReadAsync(uri, stockToken);
            //wc.OpenReadAsync(uri2, stockToken);
        } 
        #endregion
        private const string UrlStockData = "http://pipes.yahooapis.com/pipes/pipe.run?_id=98aa7fee9ad07efbdbde5699c53f1bcb&_render=json&s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d";
		
        protected static DateTimeDictionary TradingDates = new DateTimeDictionary();

        private void OnRequestStockHistoryCompleted(object sender, OpenReadCompletedEventArgs response)
        {
            var stockToken = new StockToken();
            if (response.UserState != null)
            {
                stockToken = (StockToken)response.UserState;
            }
             
            var process = "StockQuandlService -> Requesting stock history for " + stockToken.Symbol;

            if (response.Cancelled || response.Error != null)
            {
                Debug.WriteLine(process + " failed: \n   at " + response.Error);
                //ReportServerError(e.Error); 
                return;
            }

            Debug.WriteLine(process + " completed");
   
           
            var csv = DataProvider.GetCsvFile(response.Result);
            var culture = CultureInfo.InvariantCulture;
            var stockHistory = new StockTickerList();
             
            #region Parse csv data and create stock history
            //      0,    1,    2,   3,     4,      5,        6,           7, 
            //   Date, Open, High, Low, Close, Volume, Dividend, Split Ratio, Adj. Open,Adj. High,Adj. Low,Adj. Close,Adj. Volume
            // 2015-09-10,247.23,250.7231,245.33,248.381,2700138.0,0.0,1.0,247.23,250.7231,245.33,248.381,2700138.0
            // 2015-09-09,252.05,254.25,248.303,248.91,3363641.0,0.0,1.0,252.05,254.25,248.303,248.91,3363641.0

            for (var i = 1; i < csv.Count; i++)
            {
                var line = csv[i];
                var stock = new StockTickerData();
                stock.Date = DateTime.ParseExact(line[0], StockDateFormat, culture);

                stock.Open = double.Parse(line[1]);
                stock.High = double.Parse(line[2]);
                stock.Low = double.Parse(line[3]);
                stock.Close = double.Parse(line[4]);
                stock.Volume = double.Parse(line[5]);

                //stock.Dividend = double.Parse(line[6]);
                //stock.Split = double.Parse(line[7]);

                stockHistory.Add(stock);

                var day = stock.Date.ToString();
                if (!TradingDates.ContainsKey(day))
                     TradingDates.Add(day, stock.Date);
            } 
            #endregion
           
            stockHistory.Sort((x, y) => +(Comparer<DateTime>.Default.Compare(x.Date, y.Date)));

           // stockHistory.Sort((x, y) => Comparer.Ascending(x.Date, y.Date));

            Debug.WriteLine(process + " data parsed: " + stockHistory.Count);
   
            if (stockHistory.Count > 0)
            {
                var first = stockHistory.First();
                var count = stockHistory.Count;
                while (first.Date > stockToken.StartDate)
                {
                    var newStock = new StockTickerData { Date = first.Date.AddDays(-1)};
                    if (TradingDates.ContainsKey(newStock.Date.ToString()))
                    {
                        stockHistory.Insert(0, newStock);
                    }

                    first = newStock;
                }
                var delta = stockHistory.Count - count;
                if (delta > 0)
                    Debug.WriteLine("StockQuandlService -> Aligned stock data by adding " + delta + " items");
            
            }
            Debug.WriteLine(process + " data aligned: " + stockHistory.Count);
          
            _cache.UpdateCache(stockToken.Symbol, new List<object> { stockHistory });
            

            SynchronizationContext.Current.Post(data =>
                stockToken.Callback(stockToken.Symbol, (StockTickerList)data), stockHistory);
        }
    }

    public class StockToken
    {
        /// <summary> Gets or sets Symbol </summary>
        public string Symbol { get; set; }

        /// <summary> Gets or sets Callback </summary>
        public Action<string, StockTickerList> Callback { get; set; }

        /// <summary> Gets or sets Start Date </summary>
        public DateTime StartDate { get; set; }
    }

   

    

}
