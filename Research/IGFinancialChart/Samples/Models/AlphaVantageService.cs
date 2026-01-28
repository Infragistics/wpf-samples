//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net; 

namespace IGFinancialChart.Samples
{
    //internal class AlphaVantageService
    //{
    //    public AlphaVantageService()
    //    {
            
    //    }
    //    private const string API_KEY = "ZCOEMEHP7RSKEW78";
    //    private const string SERVICE_URL = "https://www.alphavantage.co/query?function={0}&symbol={1}&interval={2}&outputsize={3}&apikey=" + API_KEY;

    //    public static IEnumerable<IEnumerable<StockItem>> TimeSeriesDaily(IEnumerable<string> symbols, bool truncate)
    //    {
    //        foreach (string symbol in symbols) yield return TimeSeriesDaily(symbol, truncate).ToArray(); // using ToList() here to force enumeration
    //    }
    //    public static IEnumerable<StockItem> TimeSeriesDaily(string symbol, bool truncate)
    //    {
    //        string outputsize = truncate ? "compact" : "full";
    //        string url = string.Format(SERVICE_URL, "TIME_SERIES_INTRADAY", symbol, "1min", outputsize);
    //        WebRequest request = WebRequest.Create(url);
    //        WebResponse response = request.GetResponse();
    //        string responseString;
    //        using (Stream responseStream = response.GetResponseStream())
    //        {
    //            using (StreamReader reader = new StreamReader(responseStream))
    //            {
    //                responseString = reader.ReadToEnd();
    //            }
    //        }

    //        //JObject result = JObject.Parse(responseString);
    //        dynamic result = JsonConvert.DeserializeObject(responseString);
    //        JObject timeSeries = result["Time Series (1min)"];
    //        if (timeSeries == null) yield break;

    //        foreach (var stockStockItem in timeSeries)
    //        {
    //            StockItem price = new StockItem()
    //            {
    //                Time = DateTime.Parse(stockStockItem.Key),
    //                Open = double.Parse(stockStockItem.Value["1. open"].Value<string>()),
    //                High = double.Parse(stockStockItem.Value["2. high"].Value<string>()), // 2 high
    //                Low = double.Parse(stockStockItem.Value["3. low"].Value<string>()),
    //                Close = double.Parse(stockStockItem.Value["4. close"].Value<string>()),
    //                Volume = double.Parse(stockStockItem.Value["5. volume"].Value<string>())
    //            };
    //            yield return price;
    //        }
    //    }
    //}
}
