using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取或设置与指定键关联的值。
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">要获取的值的键。</param>
        /// <returns>与指定键关联的值。</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取或设置与指定键关联的值。
        /// </summary>
        /// <param name="key">要获取的值的键。</param>
        /// <returns>与指定键关联的值。</returns>
        object Get(string key);

        /// <summary>
        /// 将指定的键和对象添加到缓存。
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Data</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 将指定的键和对象添加到缓存。(永不过期)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Data</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object value);

        /// <summary>
        /// 获取一个值，该值指示是否缓存与指定键关联的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool Contains(string key);

        /// <summary>
        /// 从缓存中删除具有指定键的值
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// 清除到数据缓存
        /// </summary>
        void Clear();
    }
}
