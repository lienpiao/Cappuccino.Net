using Autofac;
using Cappuccino.Common;
using Cappuccino.Common.Caching;
using Cappuccino.Common.Enum;
using Cappuccino.Common.Helper;
using Cappuccino.Common.Util;
using Cappuccino.IBLL;
using Cappuccino.Model;
using Cappuccino.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Core
{
    public class CheckPermissionFilterAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            //判断是否有贴跳过登录检查的特性标签(控制器)
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }

            //方法
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }

            if (!string.IsNullOrEmpty(CookieHelper.Get(KeyManager.IsMember)))
            {
                List<string> list = DESUtils.Decrypt(CookieHelper.Get(KeyManager.IsMember)).ToList<string>();
                if (list == null || list.Count() != 2)
                {
                    ToLogin(filterContext);
                    return;
                }
                SysUser userinfo = CacheManager.Get<SysUser>(list[0]);
                if (userinfo != null)
                {
                    // 0为永久key
                    if (list[1] == "0")
                    {
                        CacheManager.Set(list[0], userinfo, new TimeSpan(10, 0, 0, 0));
                    }
                    // 1为滑动key
                    else if (list[1] == "1")
                    {
                        CacheManager.Set(list[0], userinfo, new TimeSpan(0, 30, 0));
                        CookieHelper.Set(KeyManager.IsMember, DESUtils.Encrypt(list.ToJson()), 30);
                    }
                    else
                    {
                        ToLogin(filterContext);
                        return;
                    }
                }
                else
                {
                    ToLogin(filterContext);
                    return;
                }
            }
            else
            {
                ToLogin(filterContext);
                return;
            }

            //获得当前要执行的Action上标注的CheckPermissionAttribute实例对象
            CheckPermission[] permAtts = (CheckPermission[])filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(CheckPermission), false);
            if (permAtts.Length <= 0)
            {
                return;
            }

            var container = CacheManager.Get<IContainer>(KeyManager.AutofacContainer);
            ISysActionService sysActionService = container.Resolve<ISysActionService>();

            //检查是否有权限
            foreach (var permAtt in permAtts)
            {
                //判断当前登录用户是否具有permAtt.Permission权限
                if (!sysActionService.HasPermission(UserManager.GetCurrentUserInfo().Id, permAtt.Permission))
                {
                    NoPermission(filterContext);
                    return;
                }
            }
        }

        private static void ToLogin(AuthorizationContext filterContext)
        {
            bool isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjaxRequst)
            {
                JsonResult json = new JsonResult();
                json.Data = new { status = (int)AjaxStateEnum.NoLogin, msg = "您未登录或登录已失效，请重新登录" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else
            {
                ViewResult view = new ViewResult();
                view.ViewName = "/Views/Shared/Tip.cshtml";
                filterContext.Result = view;
            }
        }

        private static void NoPermission(AuthorizationContext filterContext)
        {
            bool isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjaxRequst)
            {
                JsonResult json = new JsonResult
                {
                    Data = new { status = (int)AjaxStateEnum.NoPermission, msg = "您没有执行此操作的权限" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.Result = json;
            }
            else
            {
                ViewResult view = new ViewResult
                {
                    ViewName = "/Views/Shared/Error403.cshtml"
                };
                filterContext.Result = view;
            }
        }

    }
}