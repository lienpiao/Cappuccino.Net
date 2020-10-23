using Cappuccino.Common.Enum;
using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cappuccino.ViewModel;

namespace Cappuccino.BLL
{
    public class SysActionMenuService : BaseService<SysActionMenu>, ISysActionMenuService
    {
        #region 依赖注入
        ISysActionMenuDao dao;
        ISysActionService SysActionService;
        public SysActionMenuService(ISysActionMenuDao dao, ISysActionService sysActionService)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            SysActionService = sysActionService;
            this.AddDisposableObject(this.CurrentDao);
            this.AddDisposableObject(this.SysActionService);
        }
        #endregion

        public List<PearMenuViewModel> GetMenu(int userId)
        {
            var sysActionList = SysActionService.GetPermissionByType(userId, ActionTypeEnum.Menu).OrderBy(x => x.SortCode).ToList();
            var sysActionMenus = dao.GetList(x => true).ToList();
            if (sysActionList == null)
            {
                return null;
            }
            //actionMenu转PearMenuData
            List<PearMenuViewModel> list = new List<PearMenuViewModel>();
            //返回list
            List<PearMenuViewModel> pearMenuDatas = new List<PearMenuViewModel>();
            Dictionary<int, PearMenuViewModel> dict = new Dictionary<int, PearMenuViewModel>();
            foreach (var item in sysActionList)
            {
                PearMenuViewModel pearMenuData = new PearMenuViewModel();
                pearMenuData.Id = item.Id;
                pearMenuData.Title = item.Name;
                pearMenuData.Icon = "layui-icon " + item.SysActionMenu.Icon;
                pearMenuData.Href = item.SysActionMenu.Url;
                pearMenuData.ParentId = item.ParentId;
                list.Add(pearMenuData);
                dict.Add(item.Id, pearMenuData);
                if (item.ParentId == 0)
                {
                    pearMenuDatas.Add(pearMenuData);
                }
            }
            foreach (var item in list)
            {
                if (item.ParentId != 0 && dict.ContainsKey(item.ParentId))
                {
                    dict[item.ParentId].OpenType = "";
                    dict[item.ParentId].Type = 0;
                    dict[item.ParentId].Children.Add(item);
                    if (dict[item.Id].Children.Count == 0)
                    {
                        item.Type = 1;
                        item.OpenType = "_iframe";
                    }
                    else
                    {
                        item.Type = 0;

                    }
                }
            }
            return pearMenuDatas;
        }

    }
}
