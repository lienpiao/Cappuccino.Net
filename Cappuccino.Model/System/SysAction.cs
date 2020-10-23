using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Model
{
    public class SysAction : Entity
    {

        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 权限类型 0为菜单 1为功能
        /// </summary>
        public ActionTypeEnum Type { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        public SysActionMenu SysActionMenu { get; set; }

        public SysActionButton SysActionButton { get; set; }

        public virtual ICollection<SysRole> SysRoles { get; set; } = new List<SysRole>();

        public virtual ICollection<SysUserAction> SysUserActions { get; set; } = new List<SysUserAction>();
    }
}
