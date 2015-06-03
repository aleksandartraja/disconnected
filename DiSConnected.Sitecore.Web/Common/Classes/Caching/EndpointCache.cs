using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using WebApi.OutputCache.Core.Cache;

namespace DiSConnected.Sitecore.Web.Common.Classes.Caching
{
    /// <summary>
    /// A basic endpoint result cache impementation using MemoryCache, feel free to implement/extend this to whatever caching mechanism you choose
    /// </summary>
    public class EndpointCache : IApiOutputCache
    {
        MemoryCache _memoryCache = new MemoryCache("endpointCache");

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            _memoryCache.Add(key, o, expiration);
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                return _memoryCache.Select(item => item.Key).ToList();
            }
        }

        public bool Contains(string key)
        {
            return _memoryCache.Contains(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return _memoryCache.Get(key) as T;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveStartsWith(string key)
        {
            IEnumerable<KeyValuePair<string, object>> matchedStartWith = _memoryCache.Where(x => x.Key.StartsWith(key));
            foreach (var matchedItem in matchedStartWith)
            {
                _memoryCache.Remove(matchedItem.Key);
            }
        }
    }
}