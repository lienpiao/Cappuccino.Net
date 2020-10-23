using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    /// <summary>
    /// 字典分类
    /// </summary>
    public class SysDictTypeViewModel : EntityViewModel
    {
        /// <summary>
        /// 父级
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        public virtual ICollection<SysDictViewModel> SysDicts { get; set; }
    }
}
