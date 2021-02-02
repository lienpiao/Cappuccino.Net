using Cappuccino.Common;
using Cappuccino.Common.Caching;
using Cappuccino.Common.Helper;
using Cappuccino.Common.Util;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Web.Core
{
    /// <summary>
    /// 负责管理用户的相关操作的
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// 负责获取当前登录用户的实体对象
        /// </summary>
        /// <returns></returns>
        public static SysUser GetCurrentUserInfo()
        {
            var cacheId = GetCurrentUserCacheId();
            if (!string.IsNullOrEmpty(GetCurrentUserCacheId()))
            {
                return CacheManager.Cache.Get<SysUser>(cacheId);
            }
            return new SysUser() { };
        }

        /// <summary>
        /// 获取当前登录用户缓存信息的Key
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserCacheId()
        {
            List<string> list = DESUtils.Decrypt(CookieHelper.Get(KeyManager.IsMember)).ToList<string>();
            if (list != null || list.Count() != 2)
            {
                return list[0];
            }
            return "";
        }
    }
}
