using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Framework;

namespace Infragistics.Samples.Services
{
    /// <summary>
    /// Represents a service that retrieves stock data from Yahoo!
    /// </summary>
    public class StockDataService
    {
        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public static readonly StockDataService Instance = new StockDataService(StockServices.Yahoo);
        #endregion Public Properties

        #region Private Static Variables
        private const string UrlStockData = "http://pipes.yahooapis.com/pipes/pipe.run?_id=98aa7fee9ad07efbdbde5699c53f1bcb&_render=json&s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d";
        private const string UrlStockDetails_Yahoo = "http://pipes.yahooapis.com/pipes/pipe.run?_id=3e1b7fc9a1a63ea0772d20ce4573d792&_render=json&symbol={0}";
        private const string UrlStockDetails_Google = "http://finance.google.com/finance/info?client=ig&q={0}{1}";
        private const string UrlSectorSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=fb8f8c258507b5600900c436ff598107&_render=json";
        private const string UrlIndustrySummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=6c1670df0b504fd8fa8207c44581c870&_render=json&sector={0}";
        private const string UrlComaniesSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=353b196a14d6bf89f37314f1e13ce88f&_render=json&industry={0}";
        #endregion Private Static Variables

        #region Private Memeber Variables
        private StockDataCacheService _cache;
        private StockServices _serviceProvider;
        #endregion Private Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StockDataService"/> class.
        /// </summary>
        private StockDataService(StockServices serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _cache = StockDataCacheService.Instance;
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
                var stockSymbols = symbols.Aggregate(string.Empty, (current, symbol) => current + (symbol + " "));

                DebugManager.Log("StockDataService.Requested refresh of details for multiple stocks: " + stockSymbols);
                var wc = new WebClient();
                wc.OpenReadCompleted += OnRefreshDetailsComplete;

                Uri uri = null;
                switch (_serviceProvider)
                {
                    case StockServices.Yahoo:
                        {
                            uri = new Uri(string.Format(UrlStockDetails_Yahoo, symbols.Aggregate((x, y) => string.Format("{0}+{1}", x, y))));
                            DebugManager.Log(uri.ToString());
                            break;
                        }
                    case StockServices.Google:
                        {
                            uri = new Uri(string.Format(UrlStockDetails_Google, symbols.Aggregate((x, y) => string.Format("{0}+{1}", x, y))));
                            break;
                        }
                }
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
                SynchronizationContext.Current.Post(x => callback(symbol, (IEnumerable<StockTickerData>)x), _cache.GetFromCache(symbol));

                DebugManager.Log("StockDataService.Detected redundant query refresh of data for stock: " + symbol);
                return;
            }

            DebugManager.Log("StockDataService.Requested refresh of data for stock: " + symbol);

            var wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshDataComplete;
            wc.OpenReadAsync(new Uri(string.Format(UrlStockData,
                                                        symbol,
                                                        startDate.Month - 1,
                                                        startDate.Day,
                                                        startDate.Year,
                                                        endDate.Month - 1,
                                                        endDate.Day,
                                                        endDate.Year)),
                                                        new Tuple<string, Action<string, IEnumerable<StockTickerData>>>(symbol, callback));

        }

        /// <summary>
        /// Refreshes the sector summary.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RefreshSectorSummary(Action<IDictionary<string, StockData>> callback)
        {
            DebugManager.Log("StockDataService.Requested refresh of sector summary");
            var wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            var uri = new Uri(UrlSectorSummary);
            wc.OpenReadAsync(uri, callback);
        }

        /// <summary>
        /// Refreshes the industry summary per sector.
        /// </summary>
        /// <param name="index">The index of the sector.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshIndustrySummary(int index, Action<IDictionary<string, StockData>> callback)
        {
            DebugManager.Log("StockDataService.Requested refresh of industry summary: " + index);
            var wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            var uri = new Uri(string.Format(UrlIndustrySummary, index));
            wc.OpenReadAsync(uri, callback);
        }

        /// <summary>
        /// Refreshes the companies summary per industry.
        /// </summary>
        /// <param name="index">The index of the industry.</param>
        /// <param name="callback">The callback.</param>
        public void RefreshCompanySummary(int index, Action<IDictionary<string, StockData>> callback)
        {
            DebugManager.Log("StockDataService.Requested refresh of company summary: " + index);
            var wc = new WebClient();
            wc.OpenReadCompleted += OnRefreshFinancialDataComplete;
            var uri = new Uri(string.Format(UrlComaniesSummary, index));
            wc.OpenReadAsync(uri, callback);
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Called when [refresh details complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private static void OnRefreshDetailsComplete(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                DebugManager.LogWarning("StockDataService.Failed to refresh stock details: " + e.Error);
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
                DebugManager.LogWarning("StockDataService.Failed to refresh stock data: " + e.Error);
                return;
            }
            try
            {
                var stockData = ProcessStockData(e.Result);
                UpdateStockTradingDates(stockData);
                var stockResult = AlignStockData(stockData);

                var objectState = (Tuple<string, Action<string, IEnumerable<StockTickerData>>>)e.UserState;

                var symbol = objectState.Item1;
                var callback = objectState.Item2;

                _cache.UpdateCache(symbol, stockResult);

                SynchronizationContext.Current.Post(x => callback(symbol, (IEnumerable<StockTickerData>)x), stockResult);
            }
            catch (Exception ex)
            {
                DebugManager.LogError("StockDataService.Error: " + ex);
                throw;
            }

        }
        public static DateTime StockStartingDate = new DateTime(1993, 7, 13, 12, 00, 0);
        public static bool UseStockStartingDate = true;
        /// <summary>
        /// Align stock data with other stocks by prepending empty data items 
        /// </summary>
        private static IEnumerable<StockTickerData> AlignStockData(IEnumerable<StockTickerData> data)
        {
            var result = data.ToList();
            if (UseStockStartingDate && result.Count > 0)
            {
                var first = result.First();
                var newStockItemCounter = 0;
                while (first.Date.Date > StockStartingDate.Date)
                {
                    var newStockItem = CreateEmptyStockData(first.Date.Subtract(TimeSpan.FromDays(1)));
                    newStockItemCounter++;
                    //if (newStockItem.Date.DayOfWeek != DayOfWeek.Sunday &&
                    //    newStockItem.Date.DayOfWeek != DayOfWeek.Saturday)
                    if (stockTradingDates.Keys.Contains(newStockItem.Date.ToString(CultureInfo.InvariantCulture)))
                        result.Insert(0, newStockItem);
                    first = newStockItem;
                }
                DebugManager.LogError("StockDataService prepended " + newStockItemCounter + " stock data items");
            }
            return result;
        }
        private static Dictionary<string, DateTime> stockTradingDates = new Dictionary<string, DateTime>();
        private static void UpdateStockTradingDates(IEnumerable<StockTickerData> data)
        {
            foreach (var stock in data)
            {
                var key = stock.Date.Date.ToString(CultureInfo.InvariantCulture);
                if (!stockTradingDates.Keys.Contains(key))
                {
                    stockTradingDates.Add(key, stock.Date);
                }
            }
        }
        private static StockTickerData CreateEmptyStockData(DateTime dateTime)
        {
            var data = new StockTickerData();
            data.Date = dateTime;
            return data;
        }
        /// <summary>
        /// Processes the stock details.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDictionary<string, StockTickerDetails> ProcessStockDetails(Stream data)
        {
            try
            {
                var serializer =
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

                    DebugManager.LogData("StockDataService.Recived stock details: " + item.Symbol + " " + item.LastTradeTime + " " + item.LastTradeAmount);
                }

                return result;
            }
            catch (Exception ex)
            {
                DebugManager.LogError("StockDataService.Failed on ProcessStockDetails(): " + ex);
                throw new Exception("Invalid JASON", ex);
            }
        }

        /// <summary>
        /// Processes the stock data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IEnumerable<StockTickerData> ProcessStockData(Stream data)
        {
            try
            {
                var serializer =
                    new DataContractJsonSerializer(typeof(JsonRoot<StockTickerData>),
                                                   new[]
													   {
														   typeof (JsonRoot<StockTickerData>),
														   typeof (JsonContent<StockTickerData>),
														   typeof (StockTickerData),
														   typeof (List<StockTickerData>)
													   });
                var result = (JsonRoot<StockTickerData>)(serializer.ReadObject(data));

                return result.value.items.OrderBy(x => x.Date);
            }
            catch (System.Exception ex)
            {
                DebugManager.LogError("StockDataService.Failed on ProcessStockData(): " + ex);
                throw new Exception("Invalid JASON", ex);
            }
        }

        /// <summary>
        /// Called when [refresh sector summary complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private static void OnRefreshFinancialDataComplete(object sender, OpenReadCompletedEventArgs e)
        {
            IDictionary<string, StockData> result = null;

            if (e.Cancelled || e.Error != null)
            {
                DebugManager.Log("<WARNING> StockDataService.Failed to refresh financial data: " + e.Error);
                return;
            }

            //if (!e.Cancelled && e.Error == null)
            //{
            //    result = ProcessFinancialData(e.Result);
            //}
            result = ProcessFinancialData(e.Result);

            var callback = (Action<IDictionary<string, StockData>>)e.UserState;

            SynchronizationContext.Current.Post(x => callback((IDictionary<string, StockData>)x), result);
        }

        /// <summary>
        /// Processes the sector summary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDictionary<string, StockData> ProcessFinancialData(Stream data)
        {
#if DEBUG
            string jsonData = string.Empty;
            if (data.CanSeek)
            {
                var sr = new StreamReader(data);
                jsonData = sr.ReadToEnd();
                data.Position = 0;
            }
#endif
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(JsonRoot<StockData>),
                                                                                       new[]
																						{
																							typeof (JsonRoot<StockData>),
																							typeof (JsonContent<StockData>),
																							typeof (StockData),
																							typeof (List<StockData>)
																						});


                var processedData = (JsonRoot<StockData>)(serializer.ReadObject(data));
                var filteredData = processedData.value.items.Where(x => x.Description != string.Empty).ToList();

                var result = new Dictionary<string, StockData>(filteredData.Count);

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
                DebugManager.LogError("StockDataService.Failed on ProcessStockData(): " + jsonData + Environment.NewLine + ex);
                throw new Exception("Invalid JSON: " + jsonData, ex);
#else
				throw new Exception("Invalid JSON", ex);
#endif
            }
        }
        #endregion Private Methods
    }

    public enum StockServices
    {
        Google,
        Yahoo
    }
}
