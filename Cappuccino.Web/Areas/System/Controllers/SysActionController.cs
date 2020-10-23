using Cappuccino.Common;
using Cappuccino.Common.Enum;
using Cappuccino.IBLL;
using Cappuccino.Model;
using Cappuccino.ViewModel;
using Cappuccino.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysActionController : BaseController
    {
        public SysActionController(ISysActionService sysActionService, ISysActionButtonService sysActionButtonService, ISysActionMenuService sysActionMenuService)
        {
            base.SysActionService = sysActionService;
            base.SysActionButtonService = sysActionButtonService;
            base.SysActionMenuService = sysActionMenuService;
            this.AddDisposableObject(SysActionService);
            this.AddDisposableObject(SysActionButtonService);
            this.AddDisposableObject(SysActionMenuService);
        }

        [CheckPermission("system.action.list")]
        public override ActionResult Index()
        {
            base.Index();
            return View();
        }

        [HttpGet, CheckPermission("system.action.list")]
        public ActionResult List()
        {
            var list = SysActionService.GetList(x => true).OrderBy(x => x.SortCode).ToList();
            var result = new { code = 0, count = list.Count(), data = list };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.role.assign")]
        public ActionResult Assign(int id)
        {
            var data = SysActionService.GetDtree(id);
            var result = new DtreeViewModel { Data = data, Status = new DtreeStatus() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [CheckPermission("system.action.create")]
        public ActionResult GetMenuTree()
        {
            var data = SysActionService.GetMenuTree();
            var result = new DtreeViewModel { Data = data, Status = new DtreeStatus() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.action.create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, CheckPermission("system.action.create")]
        public ActionResult Create(ActionViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }

                SysAction sysAction = new SysAction
                {
                    Name = viewModel.Name,
                    ParentId = viewModel.ParentId,
                    Code = viewModel.Code,
                    Type = viewModel.Type,
                    SortCode = viewModel.SortCode,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUserId = UserManager.GetCurrentUserInfo().Id,
                    UpdateUserId = UserManager.GetCurrentUserInfo().Id
                };

                if (viewModel.Type == ActionTypeEnum.Menu)
                {
                    sysAction.SysActionMenu = new SysActionMenu { Icon = viewModel.Icon, Url = viewModel.Url };
                }
                else if (viewModel.Type == ActionTypeEnum.Button)
                {
                    sysAction.SysActionButton = new SysActionButton
                    {
                        ButtonCode = viewModel.ButtonCode,
                        Location = viewModel.Location,
                        ButtonClass = viewModel.ButtonClass,
                        ButtonIcon = viewModel.ButtonIcon
                    };
                }
                else
                {
                    return WriteError("类型不正确");
                }
                SysActionService.Add(sysAction);
                return WriteSuccess();
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpGet, CheckPermission("system.action.edit")]
        public ActionResult Edit(int id)
        {
            var entity = SysActionService.GetList(x => x.Id == id).FirstOrDefault();
            SysActionMenuService.GetList(x => x.Id == id).FirstOrDefault();
            SysActionButtonService.GetList(x => x.Id == id).FirstOrDefault();
            var viewModel = entity.EntityMap();
            if (viewModel.Type == ActionTypeEnum.Menu)
            {
                return View("EditMenu", viewModel);
            }
            else if (viewModel.Type == ActionTypeEnum.Button)
            {
                return View("EditButton", viewModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost, CheckPermission("system.action.edit")]
        public ActionResult Edit(ActionViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }
            var action = SysActionService.GetList(x => x.Id == viewModel.Id).FirstOrDefault();
            SysActionMenuService.GetList(x => x.Id == viewModel.Id).FirstOrDefault();
            SysActionButtonService.GetList(x => x.Id == viewModel.Id).FirstOrDefault();
            if (action != null)
            {
                action.Name = viewModel.Name;
                action.ParentId = viewModel.ParentId;
                action.Code = viewModel.Code;
                action.Type = viewModel.Type;
                action.SortCode = viewModel.SortCode;
                action.UpdateTime = DateTime.Now;
                action.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
                if (viewModel.Type == ActionTypeEnum.Menu)
                {
                    action.SysActionMenu.Icon = viewModel.Icon;
                    action.SysActionMenu.Url = viewModel.Url;
                }
                else if (viewModel.Type == ActionTypeEnum.Button)
                {
                    action.SysActionButton.ButtonCode = viewModel.Code;
                    action.SysActionButton.Location = viewModel.Location;
                    action.SysActionButton.ButtonClass = viewModel.ButtonClass;
                    action.SysActionButton.ButtonIcon = viewModel.ButtonIcon;
                }
                else
                {
                    return WriteError("类型不正确");
                }
                SysActionService.Update(action);
                return WriteSuccess();
            }
            return WriteError();
        }

        [HttpPost, CheckPermission("system.action.delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var action = SysActionService.GetList(x => x.Id == id).FirstOrDefault();
                if (action.Type == ActionTypeEnum.Menu)
                {
                    SysActionMenuService.DeleteBy(x => x.Id == id);
                    SysActionService.DeleteBy(x => x.Id == id);
                }
                else if (action.Type == ActionTypeEnum.Button)
                {
                    SysActionButtonService.DeleteBy(x => x.Id == id);
                    SysActionService.DeleteBy(x => x.Id == id);
                }
                return WriteSuccess("数据删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpPost, CheckPermission("system.action.batchDel")]
        public ActionResult BatchDel(string idsStr)
        {
            try
            {
                var idsArray = idsStr.Substring(0, idsStr.Length).Split(',');
                int[] ids = Array.ConvertAll<string, int>(idsArray, int.Parse);
                var result = SysActionService.DeleteByIds(ids) ? WriteSuccess("数据删除成功") : WriteError("数据删除失败");
                return result;
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }
    }
}