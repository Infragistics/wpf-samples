#define UseCachedData

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using IGTrading.Models;

namespace IGTrading.Services
{
    public class YahooStockDataService
    {
        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public static readonly YahooStockDataService Instance = new YahooStockDataService();
        #endregion Public Properties

        #region Private Static Variables
        private const string _urlStockData = "http://pipes.yahooapis.com/pipes/pipe.run?_id=98aa7fee9ad07efbdbde5699c53f1bcb&_render=json&s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d";
        private const string _urlStockDetails = "http://pipes.yahooapis.com/pipes/pipe.run?_id=75908c686f120602872a5c42759c3a3f&_render=json&symbol={0}&timestamp={1}";
        //private const string _urlStockDetails = "http://pipes.yahooapis.com/pipes/pipe.run?_id=3e1b7fc9a1a63ea0772d20ce4573d792&_render=json&symbol={0}&timestamp={1}";
        //old:3e1b7fc9a1a63ea0772d20ce4573d792
        private const string _urlSectorSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=fb8f8c258507b5600900c436ff598107&_render=json";
        private const string _urlIndustrySummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=6c1670df0b504fd8fa8207c44581c870&_render=json&sector={0}";
        private const string _urlComaniesSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=353b196a14d6bf89f37314f1e13ce88f&_render=json&industry={0}";
        #endregion Private Static Variables

        #region Private Memeber Variables
        private StockDataCacheService _cache;
        #endregion Private Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="YahooStockDataService"/> class.
        /// </summary>
        private YahooStockDataService()
        {
#if UseCachedData
            //sets the historical stock data cache to load pre-cached data - this increases memory usage but removes the dependence on the Yahoo service
            //to disable using cached data and force retrieving data from the Yahoo service, comment the UseCachedData pre-compile directive declaration above
            //MT
            _cache = StaticStockDataCacheService.Instance;
#else
            //sets the historical stock price data cache to retrieve data using a Yahoo service
            //to disable retrieving data from the Yahoo service, and force using cached data, make sure the UseCachedData pre-compile directive is declared
            _cache = StockDataCacheService.Instance;
#endif


        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Refreshes the details.
        /// </summary>
        /// <param name="symbols">The symbols.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshDetails(IList<string> symbols, Action<IDictionary<string, StockTickerDetails>> callback)
        {
            if (symbols.Count > 0)
            {
                var uri = new Uri(string.Format(_urlStockDetails,
                   symbols.Aggregate((x, y) => string.Format("{0}+{1}", x, y)), DateTime.Now.ToLongTimeString()));

                System.Diagnostics.Debug.WriteLine("\n\n StockDataService -> Requesting details link: " + uri.AbsoluteUri.Replace("http://", ""));
     
                var wc = new WebClient();
                wc.OpenReadCompleted += OnRefreshDetailsComplete;
                wc.OpenReadAsync(uri, callback);
            }
        }

        /// <summary>
        /// Refreshes the data.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshData(string symbol, DateTime startDate, Action<string, IEnumerable<StockTickerData>> callback)
        {
            RefreshData(symbol, startDate, DateTime.Now.AddDays(-1.0), callback);
        }

        protected int CacheCounter = 0;
        /// <summary>
        /// Refreshes the data.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshData(string symbol, DateTime startDate, DateTime endDate, Action<string, IEnumerable<StockTickerData>> callback)
        {
            // Is this a redundant query?
            if (_cache.IsSymbolInCache(symbol) && !_cache.HasCacheExpired(symbol))
            {
                CacheCounter++;
                if (CacheCounter < 10)
                    System.Diagnostics.Debug.WriteLine("StockDataService -> cache: " + symbol);
     
                if(callback != null)
                    SynchronizationContext.Current.Post(x => callback(symbol, (IEnumerable<StockTickerData>)x), _cache.GetFromCache(symbol));

                return;
            }
            var uri = new Uri(string.Format(_urlStockData,
                                                       symbol,
                                                       startDate.Month - 1,
                                                       startDate.Day,
                                                       startDate.Year,
                                                       endDate.Month - 1,
                                                       endDate.Day,
                                                       endDate.Year));
            System.Diagnostics.Debug.WriteLine("\n\n StockDataService -> Requesting data link: " + uri.AbsoluteUri.Replace("http://", ""));
     
            var wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshDataComplete;
            wc.OpenReadAsync(uri, new Tuple<string, Action<string, IEnumerable<StockTickerData>>>(symbol, callback));
            //wc.OpenReadAsync(new Uri(string.Format(_urlStockData,
            //                                            symbol,
            //                                            startDate.Month - 1,
            //                                            startDate.Day,
            //                                            startDate.Year,
            //                                            endDate.Month - 1,
            //                                            endDate.Day,
            //                                            endDate.Year)),
            //                                            new Tuple<string, Action<string, IEnumerable<StockTickerData>>>(symbol, callback));

        }

        /// <summary>
        /// Refreshes the sector summary.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RefreshSectorSummary(Action<IDictionary<string, FinancialData>> callback)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            Uri uri = new Uri(_urlSectorSummary);
            wc.OpenReadAsync(uri, callback);
        }

        /// <summary>
        /// Refreshes the industry summary per sector.
        /// </summary>
        /// <param name="index">The index of the sector.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshIndustrySummary(int index, Action<IDictionary<string, FinancialData>> callback)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            Uri uri = new Uri(string.Format(_urlIndustrySummary, index));
            wc.OpenReadAsync(uri, callback);
        }

        /// <summary>
        /// Refreshes the companies summary per industry.
        /// </summary>
        /// <param name="index">The index of the industry.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshCompanySummary(int index, Action<IDictionary<string, FinancialData>> callback)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            Uri uri = new Uri(string.Format(_urlComaniesSummary, index));
            wc.OpenReadAsync(uri, callback);
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Called when [refresh details complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnRefreshDetailsComplete(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                System.Diagnostics.Debug.WriteLine("StockDataService -> Requesting stock details failed: \n   at " + e.Error);
                return;
            }

            var result = ProcessStockDetails(e.Result);

            var callback = (Action<IDictionary<string, StockTickerDetails>>)e.UserState;

            SynchronizationContext.Current.Post(x => callback((IDictionary<string, StockTickerDetails>)x), result);
        }

        /// <summary>
        /// Called when [refresh data complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnRefreshDataComplete(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                System.Diagnostics.Debug.WriteLine("StockDataService -> Requesting stock data failed: \n   at " + e.Error);
                return;
            }

            var result = ProcessStockData(e.Result);

            var objectState = (Tuple<string, Action<string, IEnumerable<StockTickerData>>>)e.UserState;

            var symbol = objectState.Item1;
            var callback = objectState.Item2;

            _cache.UpdateCache(symbol, result);
            if(callback != null)
                SynchronizationContext.Current.Post(x => callback(symbol, (IEnumerable<StockTickerData>)x), result);
        }

        /// <summary>
        /// Processes the stock details.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private IDictionary<string, StockTickerDetails> ProcessStockDetails(Stream data)
        {
            try
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(JsonRoot<StockTickerDetails>),
                                                   new[]
                                                       {
                                                           typeof (JsonRoot<StockTickerDetails>),
                                                           typeof (JsonContent<StockTickerDetails>),
                                                           typeof (StockTickerDetails),
                                                           typeof (List<StockTickerDetails>)
                                                       });

                var processedData = (JsonRoot<StockTickerDetails>)(serializer.ReadObject(data));

                var result = new Dictionary<string, StockTickerDetails>(processedData.value.items.Count);

                foreach (var item in processedData.value.items)
                {
                    item.LastUpdate = DateTime.Now;
                    result.Add(item.Symbol, item);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid JASON", ex);
            }
        }

        /// <summary>
        /// Processes the stock data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private List<StockTickerData> ProcessStockData(Stream data)
        {
            try
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(JsonRoot<StockTickerData>),
                                                   new[]
                                                       {
                                                           typeof (JsonRoot<StockTickerData>),
                                                           typeof (JsonContent<StockTickerData>),
                                                           typeof (StockTickerData),
                                                           typeof (List<StockTickerData>)
                                                       });
                var result = (JsonRoot<StockTickerData>)(serializer.ReadObject(data));

                return result.value.items.OrderBy(x => x.Date).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid JASON", ex);
            }
        }

        /// <summary>
        /// Called when [refresh sector summary complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnRefreshFinancialDataComplete(object sender, OpenReadCompletedEventArgs e)
        {
            IDictionary<string, FinancialData> result = null;

            if (e.Cancelled || e.Error != null)
            {
                //TODO: We should add logging or throw exception here
                System.Diagnostics.Debug.WriteLine("StockDataService -> Requesting financial data failed: \n   at " + e.Error);
                return;
            }
            if (!e.Cancelled && e.Error == null)
            {
                result = ProcessFinancialData(e.Result);
            }

            var callback = (Action<IDictionary<string, FinancialData>>)e.UserState;

            SynchronizationContext.Current.Post(x => callback((IDictionary<string, FinancialData>)x), result);
        }

        /// <summary>
        /// Processes the sector summary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private IDictionary<string, FinancialData> ProcessFinancialData(Stream data)
        {
#if DEBUG
            string jsonData = string.Empty;
            if (data.CanSeek)
            {
                StreamReader sr = new StreamReader(data);
                jsonData = sr.ReadToEnd();
                data.Position = 0;
            }
#endif
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(JsonRoot<FinancialData>),
                                                                                       new[]
		                                                                       			{
		                                                                       				typeof (JsonRoot<FinancialData>),
		                                                                       				typeof (JsonContent<FinancialData>),
		                                                                       				typeof (FinancialData),
		                                                                       				typeof (List<FinancialData>)
		                                                                       			});


                var processedData = (JsonRoot<FinancialData>)(serializer.ReadObject(data));
                var filteredData = processedData.value.items.Where(x => x.Description != string.Empty).ToList();

                var result = new Dictionary<string, FinancialData>(filteredData.Count);

                foreach (var item in filteredData)
                {
                    if (!result.ContainsKey(item.Description))
                    {
                        result.Add(item.Description, item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception("Invalid JSON: " + jsonData, ex);
#else
				throw new Exception("Invalid JSON", ex);
#endif
            }
        }
        #endregion Private Methods
    }
}
