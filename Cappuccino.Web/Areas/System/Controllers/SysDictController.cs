using Cappuccino.Common;
using Cappuccino.Common.Enum;
using Cappuccino.Common.Helper;
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
    public class SysDictController : BaseController
    {
        public SysDictController(ISysDictService sysDictService, ISysActionButtonService sysActionButtonService)
        {
            base.SysDictService = sysDictService;
            base.SysActionButtonService = sysActionButtonService;
            this.AddDisposableObject(SysDictService);
            this.AddDisposableObject(SysActionButtonService);
        }

        [CheckPermission("system.dict.list")]
        public override ActionResult Index()
        {
            base.Index();
            return View();
        }

        [CheckPermission("system.dict.list")]
        public JsonResult List(SysDictViewModel viewModel, PageInfo pageInfo)
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
            else if (viewModel.TypeId != 0)
            {
                queries.Add(new Query { Name = "TypeId", Operator = Query.Operators.Equal, Value = viewModel.TypeId });
            }
            var list = SysDictService.GetListByPage(queries.AsExpression<SysDict>(), x => true, pageInfo.Limit, pageInfo.Page, out int totalCount, true).Select(x => new
            {
                x.Id,
                x.Name,
                x.Code,
                x.SortCode
            }).ToList();
            return Json(Pager.Paging(list, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.dict.create")]
        public ActionResult Create(int TypeId)
        {
            ViewBag.TypeId = TypeId;
            return View();
        }

        [HttpPost, CheckPermission("system.dict.create")]
        public ActionResult Create(SysDictViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                SysDict entity = viewModel.EntityMap();
                entity.CreateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                SysDictService.Add(entity);
                return WriteSuccess();
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpGet, CheckPermission("system.dict.edit")]
        public ActionResult Edit(int id)
        {
            var viewModel = SysDictService.GetList(x => x.Id == id).FirstOrDefault();
            return View(viewModel.EntityMap());
        }

        [HttpPost, CheckPermission("system.dict.edit")]
        public ActionResult Edit(SysDictViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }
            viewModel.Id = viewModel.Id;
            viewModel.UpdateTime = DateTime.Now;
            viewModel.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
            SysDict entity = viewModel.EntityMap();
            SysDictService.Update(entity, new string[] { "Name", "Code", "SortCode", "UpdateTime", "UpdateUserId" });
            return WriteSuccess();
        }

        [HttpPost, CheckPermission("system.dict.delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                SysDictService.DeleteBy(x => x.Id == id);
                return WriteSuccess("数据删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpPost, CheckPermission("system.dict.batchDel")]
        public ActionResult BatchDel(string idsStr)
        {
            try
            {
                var idsArray = idsStr.Substring(0, idsStr.Length).Split(',');
                int[] ids = Array.ConvertAll<string, int>(idsArray, int.Parse);
                var result = SysDictService.DeleteBy(x => ids.Contains(x.Id)) > 0 ? WriteSuccess("数据删除成功") : WriteError("数据删除失败");
                return result;
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }
    }
}