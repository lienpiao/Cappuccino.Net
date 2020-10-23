using Cappuccino.Model;
using Cappuccino.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IBLL
{
    public interface ISysUserActionService : IBaseService<SysUserAction>
    {
        /// <summary>
        /// 获取用户特权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserActionViewModel> GetUserActionList(int userId);

        /// <summary>
        /// 更新用户特殊权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userActions"></param>
        bool SaveUserAction(int userId, List<UserActionViewModel> userActions);
    }
}
