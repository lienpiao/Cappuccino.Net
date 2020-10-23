using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Web.Core
{
    //这个Attribute可以应用到方法上，而且可以添加多个
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermission : Attribute
    {
        public string Permission { get; set; }
        public CheckPermission(string permission)
        {
            this.Permission = permission;
        }
    }
}
