//pygU9YZDGTYVrfFcFZex

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;
using IGExtensions.Framework;

namespace Infragistics.Framework 
{
    /// <summary>
    ///   
    /// </summary>
    public class StockQuandlService
    {
        public static readonly StockQuandlService Instance = new StockQuandlService();
        private DataCacheService _cache;

        private const string ApiAdress = "https://www.quandl.com/";
        private const string ApiVersionKey = "api/v3/";
        private const string ApiKey = "api_key=pygU9YZDGTYVrfFcFZex";
      
        private const string StockOutputJson = ".json";
        private const string StockOutputXml = ".xml";
        private const string StockOutputCsv = ".csv";
        private const string StockDataset = "datasets/WIKI/";


        //private string StockDataset = "datasets/WIKI/";

        //&start_date=2000-01-01

       
        
        


        // https://www.quandl.com/api/v3/datasets/WIKI/MSFT/metadata.json?api_key=dsahFHUiewjjd
        // https://www.quandl.com/api/v3/datasets/WIKI/MSFT/metadata.json?api_key=pygU9YZDGTYVrfFcFZex
        // https://www.quandl.com/api/v3/datasets/WIKI/AAPL.json?api_key=pygU9YZDGTYVrfFcFZex
        protected string StockUrl = ApiAdress + ApiVersionKey + StockDataset +
            "{0}" + StockOutputJson + "?" + ApiKey;

        /// <summary> Gets or sets StartDate </summary>
        public DateTime HistoryStartDate { get; set; }
        

        private const string UrlStockData = "http://pipes.yahooapis.com/pipes/pipe.run?_id=98aa7fee9ad07efbdbde5699c53f1bcb&_render=json&s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d";
        private const string UrlStockDetails = "http://pipes.yahooapis.com/pipes/pipe.run?_id=3e1b7fc9a1a63ea0772d20ce4573d792&_render=json&symbol={0}";
        private const string UrlSectorSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=fb8f8c258507b5600900c436ff598107&_render=json";
        private const string UrlIndustrySummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=6c1670df0b504fd8fa8207c44581c870&_render=json&sector={0}";
        private const string UrlComaniesSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=353b196a14d6bf89f37314f1e13ce88f&_render=json&industry={0}";

        public class StockDataList : List<StockTickerData>
        {
        
        }

        private StockQuandlService()
		{
            HistoryStartDate = new DateTime(2000, 1, 1);

            DebugManager.LoggingLevel = DebugLogLevel.Trace;
            _cache = DataCacheService.Instance;
		}

        protected string GetStartDate(DateTime startDate)
        {
            return string.Format("&start_date={0:yyyy-MM-dd}", startDate);
        }

        protected Uri GetStockHistoryUrl(string stockSymbol, DateTime historyStart)
        {
            var stockAddress = ApiAdress + ApiVersionKey + StockDataset +
            stockSymbol + StockOutputCsv + "?" + ApiKey + GetStartDate(historyStart);

            return new Uri(stockAddress);
        }

        public void RequestStockHistory(string symbol,
            Action<string, StockDataList> callback)
        {
            RequestStockHistory(symbol, callback, HistoryStartDate);
        }

        public void RequestStockHistory(string symbol,
            Action<string, StockDataList> callback, DateTime startDate)
        {
            if (string.IsNullOrEmpty(symbol)) return;

            // Is this a redundant query?
            if (_cache.HasCacheData(symbol) && !_cache.HasCacheExpired(symbol))
            {
                SynchronizationContext.Current.Post(data =>
                    callback(symbol, (StockDataList)data), _cache.GetFromCache(symbol));

                DebugManager.LogTrace("StockDataService -> Detected redundant query refresh of data for stock: " + symbol);
                return;
            }

            var uri = GetStockHistoryUrl(symbol, startDate);

            var uriToken = new Tuple<string, Action<string, StockDataList>>(symbol, callback);
            var wc = new WebClient();
            wc.OpenReadCompleted += OnRequestStockHistoryCompleted;
            wc.OpenReadAsync(uri, uriToken);
        }

        private void OnRequestStockHistoryCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                DebugManager.LogWarning("StockDataService -> Requesting stock details failed: \n   at " + e.Error);
                //ReportServerError(e.Error); return;
            }

            var stockData = ProcessStockHistory(e.Result);

            //UpdateStockTradingDates(stockData);
            //var stockResult = AlignStockData(stockData);
            var objectState = (Tuple<string, Action<string, StockDataList>>)e.UserState;
            var symbol = objectState.Item1;
            var callback = objectState.Item2;

            _cache.UpdateCache(symbol, new List<object> {stockData});


            if (stockData.Any())
            {
                var stock = stockData.ToList().Last();
                DebugManager.Log("StockDataService -> Requesting details returned: " + symbol + " " + stock.ToString());
            }
            DebugManager.Time("StockDataService -> Requesting details");

            SynchronizationContext.Current.Post(x => callback(symbol, (StockDataList)x), stockData);
		
            //var callback = (Action<IDictionary<string, StockTickerDetails>>)e.UserState;
            //SynchronizationContext.Current.Post(x => callback((IDictionary<string, StockTickerDetails>)x), result);
        }
        private static StockDataList ProcessStockHistory(Stream data)
        {
            var results = new List<string>();
            string line;
            try
            {
              
               // {"count":5000,"value":{"title":"Yahoo Finance Historical Data",
               //     "description":"Pipes Output",
               //     "link":"http:\/\/pipes.yahooapis.com\/pipes\/pipe.info?_id=98aa7fee9ad07efbdbde5699c53f1bcb",
               //     "pubDate":"Thu, 10 Sep 2015 17:48:39 +0000",
               //     "generator":"http:\/\/pipes.yahooapis.com\/pipes\/",
               //     "callback":"",
                // "items":[{"Date":"2015-09-09","Open":"80.790001",
               //     "High":"81.510002","Low":"78.230003","Close":"78.519997",
               //     "Volume":"2448600","description":null,"title":null},

               // {"dataset":{"id":9775409,"dataset_code":"AAPL","database_code":"WIKI",
               // "name":"Apple Inc. (AAPL)","description":"End of day open, high, low, close and volume, dividends and splits, and split/dividend adjusted open, high, low close and volume for Apple Inc. (AAPL). Ex-Dividend is non-zero on ex-dividend dates. Split Ratio is 1 on non-split dates. Adjusted prices are calculated per CRSP (\u003ca href=\"http://www.crsp.com/products/documentation/crsp-calculations\" rel=\"nofollow\" target=\"blank\"\u003ewww.crsp.com/products/documentation/crsp-calculations\u003c/a\u003e)\r\n\r\n\u003cp\u003eThis data is in the public domain. You may copy, distribute, disseminate or include the data in other products for commercial and/or noncommercial purposes.\u003c/p\u003e\r\n\u003cp\u003eThis data is part of Quandl's Wiki initiative to get financial data permanently into the public domain. Quandl relies on users like you to flag errors and provide data where data is wrong or missing. Get involved: \u003ca href=\"mailto:connect@quandl.com\" rel=\"nofollow\" target=\"blank\"\u003econnect@quandl.com\u003c/a\u003e",
               //     "refreshed_at":"2015-09-09T21:50:56.967Z","newest_available_date":"2015-09-09",
               //     "oldest_available_date":"1980-12-12",
               //     "column_names":["Date","Open","High","Low","Close","Volume","Ex-Dividend","Split Ratio","Adj. Open","Adj. High","Adj. Low","Adj. Close","Adj. Volume"],
               //     "frequency":"daily","type":"Time Series","premium":false,"limit":null,"transform":null,
               //     "column_index":null,"start_date":"1980-12-12","end_date":"2015-09-09",
               //     "data":[
               //     ["2015-09-09",113.76,114.02,109.77,110.15,83442728.0,0.0,1.0,113.76,114.02,109.77,110.15,83442728.0],
               //     ["2015-09-08",111.65,112.56,110.32,112.21,53458236.0,0.0,1.0,111.65,112.56,110.32,112.21,53458236.0],

                var serializer =
                    new DataContractJsonSerializer(typeof(JsonRoot<StockTickerDetails>),
                                                   new[]
													   {
														   typeof (JsonRoot<StockTickerDetails>),
														   typeof (JsonContent<StockTickerDetails>),
														   typeof (StockTickerDetails),
														   typeof (List<StockTickerDetails>)
													   });
                var stockHistory = new StockDataList();

                var reader = new StreamReader(data);
                 
                //var file = reader.ReadToEnd();
                //line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        results.Add(line);
                }
                reader.Close();
                reader.Dispose();


                //var result = (JsonRoot<StockDataList>)(serializer.ReadObject(data));

                return stockHistory;
                //return result.value.items.OrderBy(x => x.Date).ToList();
            }
            catch (Exception ex)
            {
                DebugManager.LogError("StockDataService -> Failed on ProcessStockDetails(): " + ex);
                return null;
                //throw new Exception(SeviceProcessingError, ex);
            }
        }
    }

    //{"dataset":{"id":9775827,"dataset_code":"MSFT","database_code":"WIKI",
    //"name":"Microsoft Corporation (MSFT) Prices, Dividends, Splits and Trading Volume",
    //"description":"End of day open, high, low, close and volume, dividends and splits, and split/dividend adjusted open, high, low close and volume for Microsoft Corporation (MSFT). Ex-Dividend is non-zero on ex-dividend dates. Split Ratio is 1 on non-split dates. Adjusted prices are calculated per CRSP (www.crsp.com/products/documentation/crsp-calculations)\n\nThis data is in the public domain. You may copy, distribute, disseminate or include the data in other products for commercial and/or noncommercial purposes.\n\nThis data is part of Quandl's Wiki initiative to get financial data permanently into the public domain. Quandl relies on users like you to flag errors and provide data where data is wrong or missing. Get involved: connect@quandl.com\n",
    //"refreshed_at":"2015-09-08T21:50:50.071Z","newest_available_date":"2015-09-08",
    //"oldest_available_date":"1986-03-13",
    //"column_names":["Date","Open","High","Low","Close","Volume","Ex-Dividend","Split Ratio","Adj. Open","Adj. High","Adj. Low","Adj. Close","Adj. Volume"],
    //"frequency":"daily","type":"Time Series","premium":false,"database_id":4922}}


    /// <summary>
    /// Represents a service for caching data 
    /// </summary>
    public class DataCacheService
    {
        #region Members 
        public static readonly DataCacheService Instance = new DataCacheService();

        private readonly Dictionary<string, DataCacheItem<List<object>>> _cacheData;

        private TimeSpan _cacheDuration = new TimeSpan(0, 15, 0);
        #endregion 

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCacheService"/> class.
        /// </summary>
        private DataCacheService()
        {
            _cacheData = new Dictionary<string, DataCacheItem<List<object>>>();
        }
        #endregion  

        #region Methods
        /// <summary>
        /// Sets the duration of the cache.
        /// </summary>
        /// <param name="duration">The duration.</param>
        public void SetCacheDuration(TimeSpan duration)
        {
            _cacheDuration = duration;

            UpdateCache();
        }

        /// <summary> Updates the cache </summary> 
        public void UpdateCache(string key, List<object> data)
        {
            if (_cacheData.ContainsKey(key))
            {
                _cacheData[key].Data = data;
            }
            else
            {
                _cacheData.Add(key, new DataCacheItem<List<object>> { Data = data });
                //_cacheData.Add(symbol, new DataCacheItem<IEnumerable<StockTickerData>> { Data = data });
            }
        }

        /// <summary> Updates the cache </summary>
        private void UpdateCache()
        {
            foreach (var key in _cacheData.Keys)
            {
                if ((_cacheData[key].LastUpdate != DateTime.MinValue) &&
                   ((_cacheData[key].LastUpdate - DateTime.Now) > _cacheDuration))
                {
                    _cacheData[key].IsExpired = true;
                }
            }
        }
        /// <summary> Gets from cached data if exists otherwise null </summary> 
        public IEnumerable<object> GetFromCache(string key)
        {
            if (HasCacheExpired(key)) return null;

            return _cacheData[key].Data;
        }

        /// <summary> Determines whether data cache has expired for specified key </summary> 
        public bool HasCacheExpired(string key)
        {
            UpdateCache();

            if (!HasCacheData(key)) return true;

            return _cacheData[key].IsExpired;
        }

        /// <summary> Determines whether data is in cache for specified key </summary> 
        public bool HasCacheData(string key)
        {
            return _cacheData.ContainsKey(key);
        } 
        #endregion  
    }

    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class DataCacheItem<T>
    {
        private T _data;

        ///<summary>
        ///</summary>
        public T Data { get { return _data; } set { _data = value; IsExpired = false; LastUpdate = DateTime.Now; } }
        ///<summary>
        ///</summary>
        public DateTime LastUpdate { get; private set; }
        ///<summary>
        ///</summary>
        public bool IsExpired { get; set; }
    }
}
