using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Enum
{
    /// <summary>
    ///  启用标志枚举
    /// </summary>
    public enum EnabledMarkEnum
    {
        [Description("有效")]
        Valid = 1,
        [Description("无效")]
        Invalid = 0,
    }
}
