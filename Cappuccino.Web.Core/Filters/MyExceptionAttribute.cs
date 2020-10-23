using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cappuccino.Web.Core.Filters
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExecptionQueue = new Queue<Exception>();

        /// <summary>
        /// 可以捕获异常数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {

            Exception ex = filterContext.Exception;
            //写到队列
            ExecptionQueue.Enqueue(ex);

            //获取ex的第一级内部异常
            Exception innerEx = ex.InnerException == null ? ex : ex.InnerException;
            //循环获取内部异常直到获取详细异常信息为止
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResult json = new JsonResult();
                json.Data = new { status = (int)AjaxStateEnum.Error, msg = innerEx.Message };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else
            {
                ViewResult viewResult = new ViewResult();
                viewResult.ViewName = "/Views/Shared/Error.cshtml";
                viewResult.ViewData["url"] = filterContext.HttpContext.Request.RawUrl;
                viewResult.ViewData["ex"] = innerEx.Message;
                filterContext.Result = viewResult;
            }

            //告诉MVC框架异常被处理
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }
    }
}