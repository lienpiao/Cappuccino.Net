using Cappuccino.Common.Enum;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cappuccino.ViewModel;

namespace Cappuccino.IBLL
{
    public interface ISysActionButtonService : IBaseService<SysActionButton>
    {
        /// <summary>
        /// 根据用户和菜单按钮位置获得按钮列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        List<ButtonViewModel> GetButtonListByUserIdAndMenuId(int userId, string url, PositionEnum position);
    }
}
