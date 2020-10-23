using Cappuccino.Model;
using Cappuccino.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IBLL
{
    public interface ISysRoleService : IBaseService<SysRole>
    {
        /// <summary>
        /// 根据权限Ids 构建角色权限中间关系
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtrees"></param>
        /// <returns></returns>
        void Add(int id, List<DtreeResponse> dtrees);
    }
}
