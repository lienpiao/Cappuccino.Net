using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Model
{
    public class SysUser : Entity
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string HeadIcon { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public int EnabledMark { get; set; }

        public virtual ICollection<SysRole> SysRoles { get; set; } = new List<SysRole>();
        public virtual ICollection<SysUserAction> SysUserActions { get; set; } = new List<SysUserAction>();

    }
}
