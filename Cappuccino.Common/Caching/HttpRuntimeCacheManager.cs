using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cappuccino.Common.Caching
{
    public class HttpRuntimeCacheManager : ICacheManager
    {
        public void Clear()
        {
            var cache = HttpRuntime.Cache;
            var CacheEnum = cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                cache.Remove(CacheEnum.Key.ToString());
            }
        }

        public bool Contains(string key)
        {
            var data = HttpRuntime.Cache[key];
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, cacheTime);
        }

        public void Set(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }
    }
}
