using Autofac;
using Cappuccino.Common;
using Cappuccino.Common.Caching;
using Cappuccino.Common.Enum;
using Cappuccino.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cappuccino.Web.Core
{
    public class BaseController : Controller
    {
        protected IList<IDisposable> DisposableObjects { get; private set; }
        protected const string SuccessText = "操作成功！";
        protected const string ErrorText = "操作失败！";
        public BaseController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        public virtual ActionResult Index()
        {
            string url = Request.Url.AbsolutePath.ToString();
            var container = CacheManager.Get<IContainer>(KeyManager.AutofacContainer);
            ISysActionButtonService sysActionButtonService = container.Resolve<ISysActionButtonService>();
            ViewData["RightButtonList"] = sysActionButtonService.GetButtonListByUserIdAndMenuId(UserManager.GetCurrentUserInfo().Id, url, PositionEnum.FormInside);
            ViewData["TopButtonList"] = sysActionButtonService.GetButtonListByUserIdAndMenuId(UserManager.GetCurrentUserInfo().Id, url, PositionEnum.FormRightTop);
            return View();
        }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

        #region 定义当前系统中所有的业务逻辑层的接口成员
        protected ISysLogLogonService SysLogLogonService;
        protected ISysUserService SysUserService;
        protected ISysActionService SysActionService;
        protected ISysActionButtonService SysActionButtonService;
        protected ISysActionMenuService SysActionMenuService;
        protected ISysDictService SysDictService;
        protected ISysDictTypeService SysDictTypeService;
        protected ISysRoleService SysRoleService;
        protected ISysUserActionService SysUserActionService;
        #endregion

        #region 封装ajax请求的返回方法
        protected ActionResult WriteSuccess(string msg = SuccessText)
        {
            return Json(new { Status = (int)AjaxStateEnum.Sucess, Msg = msg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteSuccess(string msg, object obj)
        {
            return Json(new { Status = (int)AjaxStateEnum.Sucess, Msg = msg, Data = obj }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(string msg = ErrorText)
        {
            return Json(new { Status = (int)AjaxStateEnum.Error, Msg = msg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(Exception ex)
        {
            //获取ex的第一级内部异常
            Exception innerEx = ex.InnerException == null ? ex : ex.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            return Json(new { Status = (int)AjaxStateEnum.Error, Msg = innerEx.Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
