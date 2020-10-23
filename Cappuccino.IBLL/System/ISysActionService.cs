using Cappuccino.Common.Enum;
using Cappuccino.Model;
using Cappuccino.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IBLL
{
    public interface ISysActionService : IBaseService<SysAction>
    {
        /// <summary>
        /// 根据用户获取所拥有的权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        List<SysAction> GetPermission(int userId);

        /// <summary>
        /// 根据用户获取所拥有的菜单或按钮
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">权限类型</param>
        /// <returns></returns>
        List<SysAction> GetPermissionByType(int userId, ActionTypeEnum type);

        /// <summary>
        /// 获取dtree数据格式的权限
        /// </summary>
        /// <returns></returns>
        List<DtreeData> GetDtree(int roleId);

        /// <summary>
        /// 是否包含该权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool HasPermission(int id, string permission);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        List<DtreeData> GetMenuTree();
        bool DeleteByIds(int[] ids);
    }
}
