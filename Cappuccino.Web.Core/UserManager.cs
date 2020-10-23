using Cappuccino.Common;
using Cappuccino.Common.Helper;
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
            if (SessionHelper.Get<SysUser>(KeyManager.UserInfo) != null)
            {
                return SessionHelper.Get<SysUser>(KeyManager.UserInfo);
            }
            return new SysUser() { };
        }
    }
}
