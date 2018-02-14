using System;
using System.Collections.Generic;
using IGTrading.Models;

namespace IGTrading.Services
{
    public class StaticStockDataCacheService : StockDataCacheService
    {
        public new static readonly StaticStockDataCacheService Instance = new StaticStockDataCacheService();
	
        public StaticStockDataCacheService()
        {
            //load cached data
            try
            {
                System.Windows.Resources.StreamResourceInfo sriStockPriceHistoricalData = 
                    System.Windows.Application.GetResourceStream(new Uri("/Assets/CachedHistoricalData.bin", UriKind.Relative));

                if (sriStockPriceHistoricalData != null && sriStockPriceHistoricalData.Stream != null)
                {
                    //overwrite the cached data file with the new data
                    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    CacheData = (Dictionary<string, CacheItem<List<StockTickerData>>>)formatter.Deserialize(sriStockPriceHistoricalData.Stream);

                    //modifying the date values of historical stock prices to keep application data current
                    foreach (string key in CacheData.Keys)
                    {
                        CacheItem<List<StockTickerData>> tickerData = CacheData[key];
                        int dayCount = tickerData.Data.Count;
                        foreach (StockTickerData tData in tickerData.Data)
                        {
                            tData.Date = DateTime.Today.Subtract(TimeSpan.FromDays(dayCount--));
                        }
                    }
                }
            }
            //exceptions due to cache failure
            catch 
            { 
            }
        }
        
        public override bool HasCacheExpired(string symbol)
        {
            //cache never expires
            return false;
        }

        public override void UpdateCache(string symbol, List<Models.StockTickerData> data)
        {
            base.UpdateCache(symbol, data);
        }
    }
}
