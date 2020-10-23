using Cappuccino.IBLL;
using Cappuccino.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysActionMenuController : BaseController
    {
        public SysActionMenuController(ISysActionMenuService sysActionMenuService)
        {
            base.SysActionMenuService = sysActionMenuService;
            this.AddDisposableObject(SysActionMenuService);
        }

        [CheckPermission("system.action.create")]
        public ActionResult CreateMenuPartial()
        {
            return PartialView("_ActionMenuPartial");
        }
    }
}