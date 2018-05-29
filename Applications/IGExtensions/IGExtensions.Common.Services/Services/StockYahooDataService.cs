using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGExtensions.Common.Services
{
	/// <summary>
	/// Represents a service that retrieves stock data from Yahoo!
	/// </summary>
    [ObsoleteAttribute("This service is obsolete, use Infragistics.Framework.Services.StockDataService instead", false)] 
	public class StockYahooDataService
	{
		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public static readonly StockYahooDataService Instance = new StockYahooDataService();
		#endregion Public Properties

		#region Private Static Variables
		private const string UrlStockData = "http://pipes.yahooapis.com/pipes/pipe.run?_id=98aa7fee9ad07efbdbde5699c53f1bcb&_render=json&s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d";
		private const string UrlStockDetails = "http://pipes.yahooapis.com/pipes/pipe.run?_id=3e1b7fc9a1a63ea0772d20ce4573d792&_render=json&symbol={0}";
		private const string UrlSectorSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=fb8f8c258507b5600900c436ff598107&_render=json";
		private const string UrlIndustrySummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=6c1670df0b504fd8fa8207c44581c870&_render=json&sector={0}";
		private const string UrlComaniesSummary = "http://pipes.yahooapis.com/pipes/pipe.run?_id=353b196a14d6bf89f37314f1e13ce88f&_render=json&industry={0}";
		#endregion Private Static Variables

		#region Private Memeber Variables
		private StockDataCacheService _cache;
		#endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="StockYahooDataService"/> class.
		/// </summary>
		private StockYahooDataService()
		{
            DebugManager.LoggingLevel = DebugLogLevel.Trace;
            _cache = StockDataCacheService.Instance;
		}
		#endregion Constructors

	    protected string LastStockSymbols = "";
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
                var uri = new Uri(string.Format(UrlStockDetails, symbols.Aggregate((x, y) => string.Format("{0}+{1}", x, y))));

                DebugManager.LogTrace("StockDataService -> Requesting details for multiple stocks: " + stockSymbols);
                if (LastStockSymbols != stockSymbols)
                {
                    LastStockSymbols = stockSymbols;
                    DebugManager.LogTrace("StockDataService -> Requesting details link: " + uri.AbsoluteUri.Replace("http://", ""));
                }
                    
                DebugManager.Time("StockDataService -> Requesting details");
            
                var wc = new WebClient();
				wc.OpenReadCompleted += OnRefreshDetailsCompleted;
				wc.OpenReadAsync(uri, callback);
			}
		}

		/// <summary>
		/// Refreshes the data.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="callback">The callback.</param>
        public void RequestStockHistory(string symbol, DateTime startDate, Action<string, StockTickerList> callback)
		{
            RequestStockHistory(symbol, startDate, DateTime.Now.AddDays(-1.0), callback);
		}
        public void RequestStockHistory(string symbol,  Action<string, StockTickerList> callback)
        {
            DateTime startDate = new DateTime(2000,1,1);
            RequestStockHistory(symbol, startDate, DateTime.Now.AddDays(-1.0), callback);
        }

		/// <summary>
		/// Refreshes the data.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <param name="callback">The callback.</param>
        public void RequestStockHistory(string symbol, DateTime startDate, DateTime endDate, Action<string, StockTickerList> callback)
		{
			// Is this a redundant query?
			if (_cache.IsSymbolInCache(symbol) && !_cache.HasCacheExpired(symbol))
			{
				SynchronizationContext.Current.Post(x => callback(symbol, (StockTickerList)x), _cache.GetFromCache(symbol));

                DebugManager.LogTrace("StockDataService -> Detected redundant query refresh of data for stock: " + symbol);
				return;
			}
            var uri = new Uri(string.Format(UrlStockData,
														symbol,
														startDate.Month - 1,
														startDate.Day,
														startDate.Year,
														endDate.Month - 1,
														endDate.Day,
														endDate.Year));
            DebugManager.LogTrace("StockDataService -> Requesting data for stock: " + symbol);
            DebugManager.LogTrace("StockDataService -> Requesting data link: " + uri.AbsoluteUri.Replace("http://", ""));
            DebugManager.Time("StockDataService -> Requesting data");
            var wc = new WebClient();
			wc.OpenReadCompleted += OnRefreshDataCompleted;
            wc.OpenReadAsync(uri, new Tuple<string, Action<string, StockTickerList>>(symbol, callback));

		}

		/// <summary>
		/// Refreshes the sector summary.
		/// </summary>
		/// <param name="callback">The callback.</param>
		public void RefreshSectorSummary(Action<IDictionary<string, StockData>> callback)
		{
            DebugManager.LogTrace("StockDataService -> Requesting financial data with sector summary");
            DebugManager.Time("StockDataService -> Requesting financial data");
            var uri = new Uri(UrlSectorSummary);
            var wc = new WebClient();
			wc.OpenReadCompleted += OnRefreshFinancialDataCompleted;
			wc.OpenReadAsync(uri, callback);
		}

		/// <summary>
		/// Refreshes the industry summary per sector.
		/// </summary>
		/// <param name="index">The index of the sector.</param>
		/// <param name="callback">The callback.</param>
		public void RefreshIndustrySummary(int index, Action<IDictionary<string, StockData>> callback)
		{
            DebugManager.LogTrace("StockDataService -> Requesting financial data with industry summary: " + index);
            DebugManager.Time("StockDataService -> Requesting financial data");
            var uri = new Uri(string.Format(UrlIndustrySummary, index));
            var wc = new WebClient();
			wc.OpenReadCompleted += OnRefreshFinancialDataCompleted;
			wc.OpenReadAsync(uri, callback);
		}

		/// <summary>
		/// Refreshes the companies summary per industry.
		/// </summary>
		/// <param name="index">The index of the industry.</param>
		/// <param name="callback">The callback.</param>
		public void RefreshCompanySummary(int index, Action<IDictionary<string, StockData>> callback)
		{
            DebugManager.LogTrace("StockDataService -> Requesting financial data with company summary: " + index);
            DebugManager.Time("StockDataService -> Requesting financial data");
            var uri = new Uri(string.Format(UrlComaniesSummary, index));
            var wc = new WebClient();
			wc.OpenReadCompleted += OnRefreshFinancialDataCompleted;
			wc.OpenReadAsync(uri, callback);
		}

		#endregion Public Methods

		#region Private Methods
        /// <summary>
		/// Called when [refresh details complete].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private static void OnRefreshDetailsCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			if (e.Cancelled || e.Error != null)
			{
                DebugManager.LogWarning("StockDataService -> Requesting stock details failed: \n   at " + e.Error);
                ReportServerError(e.Error); return;
            }

			var result = ProcessStockDetails(e.Result);
            if (result.Any())
            {
                var stock = result.Values.ToList().Last();
                DebugManager.Log("StockDataService -> Requesting details returned: " + stock.Symbol + " " + stock.ToStockData().ToString());
            }
            DebugManager.Time("StockDataService -> Requesting details");

            var callback = (Action<IDictionary<string, StockTickerDetails>>)e.UserState;
            SynchronizationContext.Current.Post(x => callback((IDictionary<string, StockTickerDetails>)x), result);
		}

        #region Error Reporting
        const string SeviceProcessingError = "StockDataService failed on processing stock data. \n";

        const string ServerCommError = "Yahoo Stock Server appears to be down! \n" +
                                       "Please try again in a few minutes. ";
        
        protected static bool ServerErrorReported = false;
        private static void ReportServerError(Exception error)
        {
            if (ServerErrorReported) return;
            ServerErrorReported = true;
           // throw new Exception(ServerCommError, error);
        }
        protected static bool ServiceErrorReported = false;
        private static void ReportServiceError(Exception error, string message = SeviceProcessingError)
        {
            if (ServiceErrorReported) return;
            ServiceErrorReported = true;
           // throw new Exception(message, error);
        } 
        #endregion

        /// <summary>
		/// Called when [refresh data complete].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnRefreshDataCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			if (e.Cancelled || e.Error != null)
			{
                DebugManager.LogError("StockDataService -> Requesting stock data failed: \n   at " + e.Error);
                ReportServerError(e.Error); return;
			}
			try
			{
				var stockData = ProcessStockData(e.Result);
				UpdateStockTradingDates(stockData);
				var stockResult = AlignStockData(stockData);
				var objectState = (Tuple<string, Action<string, StockTickerList>>)e.UserState;
				var symbol = objectState.Item1;
				var callback = objectState.Item2;

				_cache.UpdateCache(symbol, stockResult);

                if (stockResult.Any())
                {
                    var stock = stockResult.ToList().Last();
                    DebugManager.Log("StockDataService -> Requesting data returned: " + symbol + " " + stock.ToString());
                }
            
                DebugManager.Time("StockDataService -> Requesting data");
                
                SynchronizationContext.Current.Post(x => callback(symbol, (StockTickerList)x), stockResult);
			}
			catch (Exception ex)
			{
                DebugManager.LogError("StockDataService -> Failed on OnRefreshDataCompleted(): " + ex);
                ReportServiceError(ex);
			}
		}
        
		public static DateTime StockStartingDate = new DateTime(1993, 7, 13, 12, 00, 0);
		public static bool UseStockStartingDate = true;
		/// <summary>
		/// Align stock data with other stocks by prepending empty data items 
		/// </summary>
		private static StockTickerList AlignStockData(StockTickerList data)
		{
		    var result = data;
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
				DebugManager.LogTrace("StockDataService -> Aligning stock data by adding " + newStockItemCounter + " items");
			}
			return result;
		}
		private static Dictionary<string, DateTime> stockTradingDates = new Dictionary<string, DateTime>();
		private static void UpdateStockTradingDates(StockTickerList data)
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
            //data.Close = double.NaN;
            //data.Volume = double.NaN;
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
				    if (item.Name.EndsWith("Corpora"))
				        item.Name = item.Name.Replace("Corpora", "Corporation");
					result.Add(item.Symbol, item);

					DebugManager.LogData("StockDataService -> Requesting stock details completed: " + 
                        item.Symbol + " " + item.LastTradeTime + " " + item.LastTradeAmount);
				}

				return result;
			}
			catch (Exception ex)
			{
				DebugManager.LogError("StockDataService -> Failed on ProcessStockDetails(): " + ex);
                throw new Exception(SeviceProcessingError, ex);
            }
		}

		/// <summary>
		/// Processes the stock data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		private static StockTickerList ProcessStockData(Stream data)
		{
		    //var result = new List<StockTickerData>();
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

                DebugManager.LogData("StockDataService -> Requesting stock data completed for " + result.value.items.Count + " items");

                var stockHistory = new StockTickerList();
                foreach (var item in result.value.items)
			    {
			        stockHistory.Add(item);
			    }
			   
                stockHistory.Sort((x, y) => +(Comparer<DateTime>.Default.Compare(x.Date, y.Date)));

                return stockHistory;
			}
			catch (Exception ex)
			{
				 DebugManager.LogError("StockDataService -> Failed on ProcessStockData(): \n" + ex);
                 throw new Exception(SeviceProcessingError, ex);
            }
		}

		/// <summary>
		/// Called when [refresh sector summary complete].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
		private static void OnRefreshFinancialDataCompleted(object sender, OpenReadCompletedEventArgs e)
		{
			IDictionary<string, StockData> result = null;
			
			if (e.Cancelled || e.Error != null)
			{
                DebugManager.LogError("StockDataService -> Requesting financial data failed: \n   at " + e.Error);
                ReportServerError(e.Error); return;
            }
            
			result = ProcessFinancialData(e.Result);
		    var callback = (Action<IDictionary<string, StockData>>)e.UserState;
            DebugManager.Time("StockDataService -> Requesting financial data");
            
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
			var jsonData = string.Empty;
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
                DebugManager.LogError("StockDataService -> Failed on ProcessFinancialData(): \n" + jsonData + "\n" + ex);
#endif
                throw new Exception(SeviceProcessingError, ex);
            }
		}
		#endregion Private Methods
	}
}
