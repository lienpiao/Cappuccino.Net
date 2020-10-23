using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class ButtonViewModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string ButtonCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
