using Cappuccino.IBLL;
using Cappuccino.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web.Areas.System.Controllers
{
    public class SysActionButtonController : BaseController
    {
        public SysActionButtonController(ISysActionButtonService sysActionButtonService)
        {
            base.SysActionButtonService = sysActionButtonService;
            this.AddDisposableObject(SysActionButtonService);
        }

        [CheckPermission("system.action.create")]
        public ActionResult CreateButtonPartial()
        {
            return PartialView("_ActionButtonPartial");
        }
    }
}