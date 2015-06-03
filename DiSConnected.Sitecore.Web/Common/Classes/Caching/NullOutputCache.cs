using System;
using System.Collections.Generic;
using WebApi.OutputCache.Core.Cache;

namespace DiSConnected.Sitecore.Web.Common.Classes.Caching
{
    /// <summary>
    /// Added if you would like to disable caching depending on web.config setup (transforms having DisableServiceOutputCache = true)
    /// </summary>
    public class NullOutputCache : IApiOutputCache
    {
        public void RemoveStartsWith(string key)
        {

        }

        public T Get<T>(string key) where T : class
        {
            return null;
        }

        public object Get(string key)
        {
            return null;
        }

        public void Remove(string key)
        {

        }

        public bool Contains(string key)
        {
            return false;
        }

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {

        }

        public IEnumerable<string> AllKeys
        {
            get { return new String[0]; }
            private set { }
        }
    }
}