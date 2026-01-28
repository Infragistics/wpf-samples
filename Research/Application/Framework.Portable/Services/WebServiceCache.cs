using System;
using System.Collections.Generic;

namespace Infragistics.Framework.Services
{
    /// <summary> 
    /// Represents a service for caching data 
    /// </summary>
    public class WebServiceCache
    {  
        private WebServiceCache()
        {
            _cacheData = new WebCacheDictionary();
        }

        #region Members
        public static readonly WebServiceCache Instance = new WebServiceCache();

        private readonly WebCacheDictionary _cacheData;

        private TimeSpan _cacheDuration = new TimeSpan(0, 15, 0);
       
        #endregion

        #region Methods
        /// <summary> Sets the duration of the cache </summary> 
        public void SetCacheDuration(TimeSpan duration)
        {
            _cacheDuration = duration;

            UpdateCache();
        }

        /// <summary> Updates the cache </summary> 
        public void UpdateCache(string key, object data)
        {
            if (_cacheData.ContainsKey(key))
            {
                _cacheData[key].Data = data;
            }
            else
            {
                _cacheData.Add(key, new WebCacheItem<object> { Data = data });
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
        public object GetFromCache(string key)
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

    ///<summary> Represents data cache dictionary </summary>
    internal class WebCacheDictionary : Dictionary<string, WebCacheItem<object>>
    {
        
    }
      
    /// <summary> 
    /// Represents cache item for request of <see cref="WebService"/>
    /// </summary>
    internal class WebCacheItem<T>
    {
        private T _data;

        /// <summary> 
        /// Gets or sets the cached data
        /// </summary>
        public T Data
        {
            get { return _data; }
            set { _data = value; IsExpired = false; LastUpdate = DateTime.Now; }
        }
        /// <summary> 
        /// Gets last updated time for the cached web response
        /// </summary>
        public DateTime LastUpdate { get; private set; }
        /// <summary>
        /// Gets whether cache has expired for the cached web response
        /// </summary>
        public bool IsExpired { get; set; }
    }
}