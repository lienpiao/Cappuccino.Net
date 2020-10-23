using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Enum
{
    /// <summary>
    /// 显示位置枚举
    /// </summary>
    public enum PositionEnum
    {
        [Display(Name = "表内")]
        FormInside = 0,

        [Display(Name = "表外")]
        FormRightTop = 1
    }
}
