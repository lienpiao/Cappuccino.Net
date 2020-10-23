using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cappuccino.ViewModel;

namespace Cappuccino.IBLL
{
    public interface ISysActionMenuService : IBaseService<SysActionMenu>
    {
        /// <summary>
        /// 根据用户Id，拿到所拥有的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<PearMenuViewModel> GetMenu(int userId);
    }
}
