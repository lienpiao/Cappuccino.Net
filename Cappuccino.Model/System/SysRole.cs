using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Model
{
    public class SysRole : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int EnabledMark { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<SysUser> SysUsers { get; set; } = new List<SysUser>();
        public virtual ICollection<SysAction> SysActions { get; set; } = new List<SysAction>();
    }
}
