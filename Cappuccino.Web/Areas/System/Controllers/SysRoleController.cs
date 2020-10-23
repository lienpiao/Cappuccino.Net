using Cappuccino.Common;
using Cappuccino.IBLL;
using Cappuccino.Model;
using Cappuccino.ViewModel;
using Cappuccino.Web.Core;
using Cappuccino.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysRoleController : BaseController
    {
        public SysRoleController(ISysRoleService sysRoleService)
        {
            base.SysRoleService = sysRoleService;
            this.AddDisposableObject(SysRoleService);
        }

        [CheckPermission("system.role.list")]
        public override ActionResult Index()
        {
            base.Index();
            return View();
        }

        [CheckPermission("system.role.list")]
        public JsonResult List(SysRoleViewModel viewModel, PageInfo pageInfo)
        {
            QueryCollection queries = new QueryCollection();
            if (!string.IsNullOrEmpty(viewModel.Name))
            {
                queries.Add(new Query { Name = "Name", Operator = Query.Operators.Contains, Value = viewModel.Name });

            }
            else if (!string.IsNullOrEmpty(viewModel.Code))
            {
                queries.Add(new Query { Name = "Code", Operator = Query.Operators.Contains, Value = viewModel.Code });
            }
            var list = SysRoleService.GetListByPage(queries.AsExpression<SysRole>(), x => true, pageInfo.Limit, pageInfo.Page, out int totalCount, true).Select(x => new
            {
                x.Id,
                x.Name,
                x.Code,
                x.EnabledMark,
                x.Remark
            }).ToList();
            return Json(Pager.Paging(list, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.role.create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, CheckPermission("system.role.create")]
        public ActionResult Create(SysRoleViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                SysRole entity = viewModel.EntityMap();
                entity.CreateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                SysRoleService.Add(entity);
                return WriteSuccess();
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpGet, CheckPermission("system.role.edit")]
        public ActionResult Edit(int id)
        {
            var viewModel = SysRoleService.GetList(x => x.Id == id).FirstOrDefault();
            return View(viewModel.EntityMap());
        }

        [HttpPost, CheckPermission("system.role.edit")]
        public ActionResult Edit(int id, SysRoleViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }
            viewModel.Id = id;
            viewModel.UpdateTime = DateTime.Now;
            viewModel.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
            SysRole entity = viewModel.EntityMap();
            SysRoleService.Update(entity, new string[] { "Name", "Code", "EnabledMark", "Remark", "UpdateTime", "UpdateUserId" });
            return WriteSuccess();
        }

        [HttpPost, CheckPermission("system.role.delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                SysRoleService.DeleteBy(x => x.Id == id);
                return WriteSuccess("数据删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpPost, CheckPermission("system.role.batchDel")]
        public ActionResult BatchDel(string idsStr)
        {
            try
            {
                var idsArray = idsStr.Substring(0, idsStr.Length).Split(',');
                int[] ids = Array.ConvertAll<string, int>(idsArray, int.Parse);
                var result = SysRoleService.DeleteBy(x => ids.Contains(x.Id)) > 0 ? WriteSuccess("数据删除成功") : WriteError("数据删除失败");
                return result;
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [CheckPermission("system.role.assign")]
        public ActionResult Assign(int id)
        {
            ViewBag.RoleId = id;
            return View();
        }

        [HttpPost, CheckPermission("system.role.assign")]
        public ActionResult Assign(int id, List<DtreeResponse> dtrees)
        {
            SysRoleService.Add(id, dtrees);
            return WriteSuccess("保存成功");
        }

        [HttpPost, CheckPermission("system.role.edit")]
        public ActionResult UpdateEnabledMark(int id, int enabledMark)
        {
            SysRole entity = new SysRole
            {
                Id = id,
                EnabledMark = enabledMark,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserManager.GetCurrentUserInfo().Id
            };
            SysRoleService.Update(entity, new string[] { "EnabledMark", "UpdateTime", "UpdateUserId" });
            return WriteSuccess("更新成功");
        }

    }
}