using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Enum
{
    public enum DbLogType
    {
        [Display(Name = "其他")]
        Other = 0,
        [Display(Name = "登录")]
        Login = 1,
        [Display(Name = "退出")]
        Exit = 2,
        [Display(Name = "访问")]
        Visit = 3,
        [Display(Name = "新增")]
        Create = 4,
        [Display(Name = "删除")]
        Delete = 5,
        [Display(Name = "修改")]
        Update = 6,
        [Display(Name = "提交")]
        Submit = 7,
        [Display(Name = "异常")]
        Exception = 8
    }
}
