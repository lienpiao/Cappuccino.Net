using Cappuccino.Common.Enum;
using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using Cappuccino.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public class SysActionService : BaseService<SysAction>, ISysActionService
    {
        #region 依赖注入
        ISysActionDao dao;
        ISysUserDao SysUserDao;
        ISysActionMenuDao SysActionMenuDao;
        ISysActionButtonDao SysActionButtonDao;
        public SysActionService(ISysActionDao dao, ISysUserDao sysUserDao, ISysActionMenuDao sysActionMenuDao, ISysActionButtonDao sysActionButtonDao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            SysUserDao = sysUserDao;
            SysActionMenuDao = sysActionMenuDao;
            SysActionButtonDao = sysActionButtonDao;
            this.AddDisposableObject(this.CurrentDao);
            this.AddDisposableObject(this.SysUserDao);
            this.AddDisposableObject(this.SysActionMenuDao);
            this.AddDisposableObject(this.SysActionButtonDao);
        }
        #endregion


        public List<SysAction> GetPermission(int userId)
        {
            List<SysAction> sysActions = new List<SysAction>();
            //获取用户
            var user = SysUserDao.GetList(x => x.Id == userId && x.EnabledMark == (int)EnabledMarkEnum.Valid).FirstOrDefault();
            if (user == null)
            {
                return sysActions;
            }
            //根据角色查找用户所拥有的权限
            var userRoles = user.SysRoles.Where(x => x.EnabledMark == (int)EnabledMarkEnum.Valid).Select(x => x.SysActions).ToList();
            foreach (var item in userRoles)
            {
                sysActions.AddRange(item);
            }
            //查找用户直接关联的权限
            var userActions = user.SysUserActions;
            if (userActions.Count != 0)
            {
                //允许的权限
                var userActionsByTrue = userActions.Where(x => x.HasPermisssin == true).Select(x => x.SysAction).ToList();
                //禁止的权限
                var userActionsByFalse = userActions.Where(x => x.HasPermisssin == false).Select(x => x.SysAction).ToList();
                if (userActionsByTrue.Count() != 0)
                {
                    sysActions.AddRange(userActionsByTrue);
                }
                if (userActionsByFalse.Count() != 0)
                {
                    foreach (var item in userActionsByFalse)
                    {
                        sysActions.Remove(item);
                    }
                }
            }
            //去重
            sysActions = sysActions.GroupBy(x => x.Id).Select(x => x.First()).ToList();
            return sysActions;
        }

        public List<SysAction> GetPermissionByType(int userId, ActionTypeEnum type)
        {
            return GetPermission(userId).Where(x => x.Type == type).ToList();
        }

        public List<DtreeData> GetDtree(int roleId)
        {
            var sysActions = dao.GetList(x => true).ToList();
            List<DtreeData> list = new List<DtreeData>();
            foreach (var item in sysActions)
            {
                DtreeData dtreeData = new DtreeData();
                dtreeData.Id = item.Id.ToString();
                dtreeData.Title = item.Name;
                dtreeData.ParentId = item.ParentId.ToString();
                var role = item.SysRoles.Where(x => x.Id == roleId).FirstOrDefault();
                if (role != null)
                {
                    dtreeData.CheckArr = "1";
                }
                list.Add(dtreeData);
            }

            List<DtreeData> dtreeDatas = new List<DtreeData>();
            Dictionary<string, DtreeData> dict = new Dictionary<string, DtreeData>();
            foreach (var item in list)
            {
                dict.Add(item.Id.ToString(), item);
                if (item.ParentId == "0")
                {
                    dtreeDatas.Add(item);
                }
            }

            foreach (var item in list)
            {
                if (item.ParentId != "0")
                {
                    dict[item.ParentId].Children.Add(item);
                }
            }

            return dtreeDatas;
        }

        public bool HasPermission(int id, string permission)
        {
            return GetPermission(id).Any(x => x.Code.ToLower() == permission.ToLower());
        }

        public List<DtreeData> GetMenuTree()
        {
            var sysActions = dao.GetList(x => x.Type == ActionTypeEnum.Menu).ToList();
            //节点的名称为菜单管理
            DtreeData node = new DtreeData
            {
                Title = "菜单管理",
                Id = "0",
                //定义树节点的子节点
                Children = new List<DtreeData>()
            };
            //遍历info中的list，目的是找到list中的父对象。list储存所有的对象（包括父对象，子对象，子对象的子对象等等）
            for (var i = 0; i < sysActions.Count(); i++)
            {
                SysAction o = sysActions[i];
                //当对象的父类id为0的时候，说明这个是一个父对象
                if (o.ParentId == 0)
                {
                    //定义一个新的easyUI树节点
                    DtreeData c = new DtreeData();
                    //节点的id就是这个父对象的id
                    c.Id = o.Id.ToString();
                    //节点的名称就是这个父对象的名称
                    c.Title = o.Name;
                    //获取父对象下的子对象
                    GetMenuChild(c, o, sysActions);
                    //将这个父对象放入node之中（node相当于爷爷对象0_0）
                    node.Children.Add(c);
                }
            }
            List<DtreeData> list = new List<DtreeData>
            {
                node
            };
            return list;
        }

        private void GetMenuChild(DtreeData parent, SysAction uparent, List<SysAction> allMenu)
        {
            //遍历所有的对象
            for (var i = 0; i < allMenu.Count; i++)
            {
                SysAction a = allMenu[i];
                //如果这个对象的父id和这个父对象的id是相同的，那么说明这个对象是父对象的子对象
                if (a.ParentId == uparent.Id)
                {
                    //设置一个新的子对象，这里又用一个node来命名新的子对象不是很妥当
                    DtreeData node = new DtreeData();
                    //设置子对象的id
                    node.Id = a.Id.ToString();
                    //设置子对象的名称
                    node.Title = a.Name;
                    //一个递归调用，查找子对象是否还存在子对象
                    GetMenuChild(node, a, allMenu);
                    //将这个子对象放入父easyUI树中
                    parent.Children.Add(node);
                }
            }
        }

        public bool DeleteByIds(int[] ids)
        {
            var actions = dao.GetList(x => ids.Contains(x.Id)).ToList();
            foreach (var item in actions)
            {
                if (item.Type == ActionTypeEnum.Menu)
                {
                    SysActionMenuDao.DeleteBy(x => x.Id == item.Id);
                }
                else if (item.Type == ActionTypeEnum.Button)
                {
                    SysActionButtonDao.DeleteBy(x => x.Id == item.Id);
                }
                else
                {
                    return false;
                }
                dao.Delete(item);
                dao.SaveChanges();
            }
            return true;
        }

    }
}
