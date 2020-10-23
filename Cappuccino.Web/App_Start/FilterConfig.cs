using Cappuccino.Web.Core;
using Cappuccino.Web.Core.Filters;
using Cappuccino.Web.Core.Json;
using System.Web;
using System.Web.Mvc;

namespace Cappuccino.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //自定义异常
            filters.Add(new MyExceptionAttribute());
            //权限验证过滤器
            filters.Add(new CheckPermissionFilterAttribute());           
            //Json.Net(Newtonsoft.Json)和 ASP.net MVC 的结合
            filters.Add(new JsonNetActionFilter());
        }
    }
}
