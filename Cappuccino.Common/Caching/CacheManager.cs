using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cappuccino.Common.Caching
{
    /// <summary>
    /// 缓存管理类
    /// </summary>
    public class CacheManager
    {
        public static ICacheManager Cache { get; set; }
        static CacheManager()
        {
            Cache = new HttpRuntimeCacheManager();
        }

        public static void Clear()
        {
            Cache.Clear();
        }

        public static bool Contains(string key)
        {
            return Cache.Contains(key);
        }

        public static T Get<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        public static void Set(string key, object value, TimeSpan cacheTime)
        {
            Cache.Set(key, value, cacheTime);
        }

        public static void Set(string key, object value)
        {
            Cache.Set(key, value);
        }
    }
}
