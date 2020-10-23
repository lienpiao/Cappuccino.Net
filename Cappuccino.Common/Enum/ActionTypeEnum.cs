using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Enum
{
    public enum ActionTypeEnum
    {
        [Display(Name = "菜单")]
        Menu = 0,
        [Display(Name = "按钮")]
        Button = 1,
    }
}
