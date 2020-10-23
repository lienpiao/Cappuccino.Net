using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Model
{
    public class SysUserAction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual SysUser SysUser { get; set; }
        public int ActionId { get; set; }
        public virtual SysAction SysAction { get; set; }
        public bool HasPermisssin { get; set; }

    }
}
