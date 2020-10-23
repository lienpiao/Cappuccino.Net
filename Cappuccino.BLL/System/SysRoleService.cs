using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using Cappuccino.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
    {
        #region 依赖注入
        ISysRoleDao dao;
        ISysActionDao SysActionDao;
        public SysRoleService(ISysRoleDao dao, ISysActionDao actionDao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            SysActionDao = actionDao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion

        public void Add(int id, List<DtreeResponse> dtrees)
        {
            List<int> actionIds = new List<int>();
            foreach (var item in dtrees)
            {
                actionIds.Add(Convert.ToInt32(item.NodeId));
            }
            var role = dao.GetList(x => x.Id == id).FirstOrDefault();
            var actions = SysActionDao.GetList(x => actionIds.Contains(x.Id)).ToList();
            //清空权限关系
            role.SysActions.Clear();
            foreach (var item in actions)
            {
                role.SysActions.Add(item);
            }
            dao.Update(role);
            dao.SaveChanges();
        }

    }
}
