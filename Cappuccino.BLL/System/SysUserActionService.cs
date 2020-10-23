using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using Cappuccino.ViewModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public class SysUserActionService : BaseService<SysUserAction>, ISysUserActionService
    {
        #region 依赖注入
        readonly ISysUserActionDao dao;
        readonly ISysActionDao SysActionDao;
        public SysUserActionService(ISysUserActionDao dao, ISysActionDao sysActionDao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            SysActionDao = sysActionDao;
            this.AddDisposableObject(this.CurrentDao);
            this.AddDisposableObject(SysActionDao);
        }
        #endregion

        public List<UserActionViewModel> GetUserActionList(int userId)
        {
            List<UserActionViewModel> userActions = new List<UserActionViewModel>();
            var actions = SysActionDao.GetList(x => true).OrderBy(x => x.SortCode).ToList();
            var myUserActions = GetList(x => x.UserId == userId).ToList();
            foreach (var item in actions)
            {
                UserActionViewModel viewModel = new UserActionViewModel();
                viewModel.Id = item.Id;
                viewModel.ParentId = item.ParentId;
                viewModel.Code = item.Code;
                viewModel.Name = item.Name;
                var myUserAction = myUserActions.Where(x => x.ActionId == item.Id).FirstOrDefault();
                if (myUserAction != null)
                {
                    if (myUserAction.HasPermisssin)
                    {
                        viewModel.Status = 1;
                    }
                    else
                    {
                        viewModel.Status = 2;
                    }
                }
                userActions.Add(viewModel);
            }
            return userActions;
        }

        public bool SaveUserAction(int userId, List<UserActionViewModel> userActions)
        {
            userActions = userActions.Where(x => x.Status != 0).ToList();
            if (userActions.Count == 0)
            {
                return true;
            }
            dao.DeleteBy(x => x.UserId == userId);
            foreach (var item in userActions)
            {
                SysUserAction userAction = new SysUserAction();
                userAction.UserId = userId;
                userAction.ActionId = item.Id;
                if (item.Status == 1)
                {
                    userAction.HasPermisssin = true;
                }
                else if (item.Status == 2)
                {
                    userAction.HasPermisssin = false;
                }
                dao.Add(userAction);
            }
            return dao.SaveChanges() > 0;
        }
    }
}
