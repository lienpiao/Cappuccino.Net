using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class SysRoleViewModel : EntityViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 可空是用于查询
        /// </summary>
        public int? EnabledMark { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<SysUserViewModel> SysUsers { get; set; }
        public virtual ICollection<SysActionViewModel> SysActions { get; set; }
    }
}
