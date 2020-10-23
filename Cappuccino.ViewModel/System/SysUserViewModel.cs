using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class SysUserViewModel : EntityViewModel
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string HeadIcon { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 可空是用于查询
        /// </summary>
        public int? EnabledMark { get; set; }
        public string RoleIds { get; set; }
        public virtual ICollection<SysRoleViewModel> SysRoles { get; set; }
        public virtual ICollection<SysUserActionViewModel> SysUserActions { get; set; }

    }
}
