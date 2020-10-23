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
    public class SysActionButtonService : BaseService<SysActionButton>, ISysActionButtonService
    {
        #region 依赖注入
        ISysActionButtonDao dao;
        ISysActionService SysActionService;
        ISysActionMenuDao SysActionMenuDao;
        public SysActionButtonService(ISysActionButtonDao dao, ISysActionService sysActionService, ISysActionMenuDao sysActionMenuDao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            SysActionService = sysActionService;
            SysActionMenuDao = sysActionMenuDao;
            this.AddDisposableObject(this.CurrentDao);
            this.AddDisposableObject(this.SysActionService);
            this.AddDisposableObject(this.SysActionMenuDao);
        }
        #endregion

        public List<ButtonViewModel> GetButtonListByUserIdAndMenuId(int userId, string url, PositionEnum position)
        {
            List<ButtonViewModel> buttonViewModels = new List<ButtonViewModel>();
            var sysActionButtons = dao.GetList(x => true).ToList();
            var menu = SysActionMenuDao.GetList(x => x.Url == url).FirstOrDefault();
            if (menu == null)
            {
                return buttonViewModels;
            }
            var sysActionList = SysActionService.GetPermissionByType(userId, ActionTypeEnum.Button)
                .Where(x => x.ParentId == menu.Id && x.SysActionButton.Location == position).OrderBy(x => x.SortCode).ToList();
            foreach (var item in sysActionList)
            {
                ButtonViewModel buttonViewModel = new ButtonViewModel();
                buttonViewModel.FullName = item.Name;
                buttonViewModel.ButtonCode = item.SysActionButton.ButtonCode;
                buttonViewModel.ClassName = item.SysActionButton.ButtonClass;
                buttonViewModel.Icon = item.SysActionButton.ButtonIcon;
                buttonViewModels.Add(buttonViewModel);
            }
            return buttonViewModels;
        }

    }
}
