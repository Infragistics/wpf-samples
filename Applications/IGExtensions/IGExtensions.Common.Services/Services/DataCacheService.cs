using System;
using System.Collections.Generic;

namespace Infragistics.Framework.Services
{
    /// <summary> Represents a service for caching data  </summary>
    public class DataCacheService
    {  
        private DataCacheService()
        {
            _cacheData = new DataCacheDictionary();
        }

        #region Members
        public static readonly DataCacheService Instance = new DataCacheService();

        private readonly DataCacheDictionary _cacheData;

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
        public void UpdateCache(string key, List<object> data)
        {
            if (_cacheData.ContainsKey(key))
            {
                _cacheData[key].Data = data;
            }
            else
            {
                _cacheData.Add(key, new DataCacheList { Data = data });
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

    ///<summary> Represents data cache dictionary </summary>
    internal class DataCacheDictionary : Dictionary<string, DataCacheList>
    {
        
    }

    ///<summary> Represents data cache list </summary>
    internal class DataCacheList : DataCacheItem<List<object>>
    {
        
    }
    ///<summary> Represents data cache item</summary>
    internal class DataCacheItem<T>
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