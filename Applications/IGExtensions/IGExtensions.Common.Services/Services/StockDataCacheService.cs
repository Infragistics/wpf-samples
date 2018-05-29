using System;
using System.Collections.Generic;
using IGExtensions.Common.Models;

namespace IGExtensions.Common.Services
{
    /// <summary>
    /// Represents a service for caches stock market quotes 
    /// </summary>
    [Obsolete("This service is obsolete, use Infragistics.Framework.Services.WebServiceCache", true)]
    public class StockDataCacheService
    {
		#region Public Member Variables
		/// <summary>
		/// 
		/// </summary>
		public static readonly StockDataCacheService Instance = new StockDataCacheService();
		#endregion Public  Member Variables

		#region Private Member Variables
		private readonly Dictionary<string, CacheItem<IEnumerable<StockTickerData>>> _cacheData;
        
        private TimeSpan _cacheDuration = new TimeSpan(0, 15, 0);
        #endregion Private Member Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="StockDataCacheService"/> class.
		/// </summary>
		private StockDataCacheService()
		{
            _cacheData = new Dictionary<string, CacheItem<IEnumerable<StockTickerData>>>();
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
        /// Sets the duration of the cache.
        /// </summary>
        /// <param name="duration">The duration.</param>
        public void SetCacheDuration(TimeSpan duration)
        {
            _cacheDuration = duration;

            UpdateCache();
        }

        /// <summary>
        /// Updates the cache.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="data">The data.</param>
        public void UpdateCache(string symbol, IEnumerable<StockTickerData> data)
        {
            if (_cacheData.ContainsKey(symbol))
            {
                _cacheData[symbol].Data = data;
            }
            else
            {
                _cacheData.Add(symbol, new CacheItem<IEnumerable<StockTickerData>> { Data = data });
            }
        }

        /// <summary>
        /// Gets from cache.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public IEnumerable<StockTickerData> GetFromCache(string symbol)
        {
            if (HasCacheExpired(symbol)) return null;

            return _cacheData[symbol].Data;
        }

        /// <summary>
        /// Determines whether [has cache expired] [the specified symbol].
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        /// 	<c>true</c> if [has cache expired] [the specified symbol]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCacheExpired(string symbol)
        {
            UpdateCache();

            if (!IsSymbolInCache(symbol)) return true;

            return _cacheData[symbol].IsExpired;
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
            return _cacheData.ContainsKey(symbol);
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Updates the cache.
        /// </summary>
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
        #endregion Private Methods
    }

    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class CacheItem<T>
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
