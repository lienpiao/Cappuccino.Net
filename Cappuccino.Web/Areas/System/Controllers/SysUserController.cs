using Cappuccino.Common;
using Cappuccino.Common.Util;
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
    public class SysUserController : BaseController
    {
        public SysUserController(ISysUserService sysUserService, ISysRoleService sysRoleService)
        {
            base.SysUserService = sysUserService;
            base.SysRoleService = sysRoleService;
            this.AddDisposableObject(SysUserService);
        }

        public SelectList RoleSelectList { get { return new SelectList(SysRoleService.GetList(x => true).Select(x => new { x.Id, x.Name }), "Id", "Name"); } }

        [CheckPermission("system.user.list")]
        public override ActionResult Index()
        {
            base.Index();
            return View();
        }

        [CheckPermission("system.user.list")]
        public JsonResult List(SysUserViewModel viewModel, PageInfo pageInfo)
        {
            QueryCollection queries = new QueryCollection();
            if (!string.IsNullOrEmpty(viewModel.UserName))
            {
                queries.Add(new Query { Name = "UserName", Operator = Query.Operators.Contains, Value = viewModel.UserName });

            }
            if (!string.IsNullOrEmpty(viewModel.NickName))
            {
                queries.Add(new Query { Name = "NickName", Operator = Query.Operators.Contains, Value = viewModel.NickName });

            }
            var list = SysUserService.GetListByPage(queries.AsExpression<SysUser>(), x => true, pageInfo.Limit, pageInfo.Page, out int totalCount, true).Select(x => new
            {
                x.Id,
                x.UserName,
                x.NickName,
                x.HeadIcon,
                x.MobilePhone,
                x.Email,
                x.EnabledMark
            }).ToList();
            return Json(Pager.Paging(list, totalCount), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, CheckPermission("system.user.create")]
        public ActionResult Create()
        {
            ViewBag.UploadFileSize = ConfigUtils.GetValue("UploadFileByImgSize");
            ViewBag.UploadFileType = ConfigUtils.GetValue("UploadFileByImgType");
            ViewBag.RoleSelectList = RoleSelectList;
            return View();
        }

        [HttpPost, CheckPermission("system.user.create")]
        public ActionResult Create(SysUserViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                var user = SysUserService.GetList(x => x.UserName == viewModel.UserName).FirstOrDefault();
                if (user != null)
                {
                    return WriteError("该账号已存在");
                }
                string salt = VerifyCodeUtils.CreateVerifyCode(5);
                string passwordHash = Md5Utils.EncryptTo32(salt + ConfigUtils.GetValue("InitUserPwd"));
                SysUser entity = viewModel.EntityMap();
                entity.CreateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.PasswordSalt = salt;
                entity.PasswordHash = passwordHash;
                if (!string.IsNullOrEmpty(viewModel.RoleIds))
                {
                    var RoleIdsArray = Array.ConvertAll(viewModel.RoleIds.Split(','), s => int.Parse(s));
                    var roleList = SysRoleService.GetList(x => RoleIdsArray.Contains(x.Id)).ToList();
                    entity.SysRoles = roleList;
                }
                SysUserService.Add(entity);
                return WriteSuccess();
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpGet, CheckPermission("system.user.edit")]
        public ActionResult Edit(int id)
        {
            ViewBag.UploadFileSize = ConfigUtils.GetValue("UploadFileByImgSize");
            ViewBag.UploadFileType = ConfigUtils.GetValue("UploadFileByImgType");
            var entity = SysUserService.GetList(x => x.Id == id).FirstOrDefault();
            ViewBag.RoleSelectList = RoleSelectList;
            var viewModel = entity.EntityMap();
            if (viewModel.SysRoles.Count != 0)
            {
                viewModel.RoleIds = string.Join(",", viewModel.SysRoles.Select(x => x.Id.ToString()).ToArray());
            }
            return View(viewModel);
        }

        [HttpPost, CheckPermission("system.user.edit")]
        public ActionResult Edit(int id, SysUserViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return WriteError("实体验证失败");
            }
            var user = SysUserService.GetList(x => x.UserName == viewModel.UserName && x.Id != id).FirstOrDefault();
            if (user != null)
            {
                return WriteError("该账号已存在");
            }
            //获取角色
            var roleList = new List<SysRole>();
            if (!string.IsNullOrEmpty(viewModel.RoleIds))
            {
                var RoleIdsArray = Array.ConvertAll(viewModel.RoleIds.Split(','), s => int.Parse(s));
                roleList = SysRoleService.GetList(x => RoleIdsArray.Contains(x.Id)).ToList();
            }
            //赋值
            var entity = SysUserService.GetList(x => x.Id == id).FirstOrDefault();
            entity.SysRoles.Clear();
            foreach (var item in roleList)
            {
                entity.SysRoles.Add(item);
            }
            entity.UserName = viewModel.UserName;
            entity.NickName = viewModel.NickName;
            entity.HeadIcon = viewModel.HeadIcon;
            entity.MobilePhone = viewModel.MobilePhone;
            entity.Email = viewModel.Email;
            entity.EnabledMark = (int)viewModel.EnabledMark;
            entity.MobilePhone = viewModel.MobilePhone;
            entity.Email = viewModel.Email;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = UserManager.GetCurrentUserInfo().Id;
            SysUserService.Update(entity);
            return WriteSuccess();
        }

        [HttpPost, CheckPermission("system.user.delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                SysUserService.DeleteBy(x => x.Id == id);
                return WriteSuccess("数据删除成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpPost, CheckPermission("system.user.batchDel")]
        public ActionResult BatchDel(string idsStr)
        {
            try
            {
                var idsArray = idsStr.Substring(0, idsStr.Length).Split(',');
                int[] ids = Array.ConvertAll<string, int>(idsArray, int.Parse);
                var result = SysUserService.DeleteBy(x => ids.Contains(x.Id)) > 0 ? WriteSuccess("数据删除成功") : WriteError("数据删除失败");
                return result;
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }

        [HttpGet, CheckPermission("system.user.assign")]
        public ActionResult Assign(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost, CheckPermission("system.user.edit")]
        public ActionResult UpdateEnabledMark(int id, int enabledMark)
        {
            SysUser entity = new SysUser
            {
                Id = id,
                EnabledMark = enabledMark,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserManager.GetCurrentUserInfo().Id
            };
            SysUserService.Update(entity, new string[] { "EnabledMark", "UpdateTime", "UpdateUserId" });
            return WriteSuccess("更新成功");
        }

        [HttpPost, CheckPermission("system.user.initPwd")]
        public ActionResult InitPwd(int id)
        {
            string salt = VerifyCodeUtils.CreateVerifyCode(5);
            string pwd = ConfigUtils.GetValue("InitUserPwd");
            string passwordHash = Md5Utils.EncryptTo32(salt + pwd);
            SysUser entity = new SysUser
            {
                Id = id,
                PasswordSalt = salt,
                PasswordHash = passwordHash,
                UpdateTime = DateTime.Now,
                UpdateUserId = UserManager.GetCurrentUserInfo().Id
            };
            SysUserService.Update(entity, new string[] { "PasswordSalt", "PasswordHash", "UpdateTime", "UpdateUserId" });
            return WriteSuccess("重置密码成功，新密码:" + pwd);
        }
    }
}