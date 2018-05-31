using System;
using System.Collections.Generic;
using IGTrading.Models;

namespace IGTrading.Services
{
    public class StockDataCacheService
    {
		#region Public Member Variables
		/// <summary>
		/// 
		/// </summary>
		public static readonly StockDataCacheService Instance = new StockDataCacheService();
		#endregion Public  Member Variables

		#region Private Member Variables
        protected Dictionary<string, CacheItem<List<StockTickerData>>> CacheData = new Dictionary<string, CacheItem<List<StockTickerData>>>();
        protected TimeSpan CacheDuration = new TimeSpan(0, 15, 0);

        #endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="StockDataCacheService"/> class.
		/// </summary>
		protected StockDataCacheService()
		{
		}
		#endregion Constructors

		#region Public Methods

        /// <summary>
        /// Sets the duration of the cache.
        /// </summary>
        /// <param name="duration">The duration.</param>
        public void SetCacheDuration(TimeSpan duration)
        {
            CacheDuration = duration;

            UpdateCache();
        }

        /// <summary>
        /// Updates the cache.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="data">The data.</param>
        public virtual void UpdateCache(string symbol, List<StockTickerData> data)
        {
            if (CacheData.ContainsKey(symbol))
            {
                CacheData[symbol].Data = data;
            }
            else
            {
                CacheData.Add(symbol, new CacheItem<List<StockTickerData>> { Data = data });
            }
        }

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public virtual IEnumerable<StockTickerData> GetFromCache(string symbol)
        {
            if (HasCacheExpired(symbol)) return null;

            return CacheData[symbol].Data;
        }

        /// <summary>
        /// Determines whether [has cache expired] [the specified symbol].
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// 	<c>true</c> if [has cache expired] [the specified symbol]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool HasCacheExpired(string symbol)
        {
            UpdateCache();

            if (!IsSymbolInCache(symbol)) return true;

            return CacheData[symbol].IsExpired;
        }

        /// <summary>
        /// Determines whether [is symbol in cache] [the specified symbol].
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// 	<c>true</c> if [is symbol in cache] [the specified symbol]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSymbolInCache(string symbol)
        {
            return CacheData.ContainsKey(symbol);
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Updates the cache.
        /// </summary>
        private void UpdateCache()
        {
            foreach (var key in CacheData.Keys)
            {
				if ((CacheData[key].LastUpdate != DateTime.MinValue) && 
					((CacheData[key].LastUpdate - DateTime.Now) > CacheDuration))
                {
                    CacheData[key].IsExpired = true;
                }
            }
        }
        #endregion Private Methods
    }

    [Serializable]
    public class CacheItem<T>
    {
        private T _data;

        public T            Data { get { return _data; } set { _data = value; IsExpired = false; LastUpdate = DateTime.Now; } }
        public DateTime     LastUpdate { get; private set; }
        public bool         IsExpired { get; set; }
    }
}
