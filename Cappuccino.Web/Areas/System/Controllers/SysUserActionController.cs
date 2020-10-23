using Cappuccino.IBLL;
using Cappuccino.ViewModel.System;
using Cappuccino.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysUserActionController : BaseController
    {
        public SysUserActionController(ISysUserActionService sysUserActionService)
        {
            base.SysUserActionService = sysUserActionService;
            this.AddDisposableObject(SysUserActionService);
        }

        [HttpGet, CheckPermission("system.user.assign")]
        public ActionResult List(int id)
        {
            var list = SysUserActionService.GetUserActionList(id);
            var result = new { code = 0, count = list.Count(), data = list };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, CheckPermission("system.user.assign")]
        public ActionResult Save(int userId, List<UserActionViewModel> list)
        {
            var result = SysUserActionService.SaveUserAction(userId, list) ? WriteSuccess("保存成功") : WriteError("保存失败");
            return result;
        }
    }
}