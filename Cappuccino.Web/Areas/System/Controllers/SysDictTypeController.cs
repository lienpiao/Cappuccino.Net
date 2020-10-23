using Cappuccino.Common;
using Cappuccino.ViewModel;
using Cappuccino.IBLL;
using Cappuccino.Model;
using Cappuccino.Web.Core;
using Cappuccino.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysDictTypeController : BaseController
    {
        public SysDictTypeController(ISysDictTypeService sysDictTypeService)
        {
            base.SysDictTypeService = sysDictTypeService;
            this.AddDisposableObject(SysDictTypeService);
        }

        [CheckPermission("system.dict.list")]
        public JsonResult List(SysDictTypeViewModel viewModel, PageInfo pageInfo)
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
            var list = SysDictTypeService.GetListByPage(queries.AsExpression<SysDictType>(), x => true, pageInfo.Limit, pageInfo.Page, out int totalCount, true).Select(x => new
            {
                x.Id,
                x.Name,
                x.Code,
                x.SortCode
            }).ToList();
            return Json(Pager.Paging(list, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.dict.create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost,CheckPermission("system.dict.create")]
        public ActionResult Create(SysDictTypeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                SysDictType entity = viewModel.EntityMap();
                entity.CreateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                SysDictTypeService.Add(entity);
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
            var viewModel = SysDictTypeService.GetList(x => x.Id == id).FirstOrDefault();
            return View(viewModel.EntityMap());
        }

        [HttpPost, CheckPermission("system.dict.edit")]
        public ActionResult Edit(SysDictTypeViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }
            viewModel.Id = viewModel.Id;
            viewModel.UpdateTime = DateTime.Now;
            viewModel.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
            SysDictType entity = viewModel.EntityMap();
            SysDictTypeService.Update(entity, new string[] { "Name", "Code", "SortCode", "UpdateTime", "UpdateUserId" });
            return WriteSuccess();
        }

        [HttpPost, CheckPermission("system.dict.delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                SysDictTypeService.DeleteBy(x => x.Id == id);
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
                var result = SysDictTypeService.DeleteBy(x => ids.Contains(x.Id)) > 0 ? WriteSuccess("数据删除成功") : WriteError("数据删除失败");
                return result;
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }
    }
}