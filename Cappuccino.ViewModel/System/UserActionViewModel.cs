using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel.System
{
    public class UserActionViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// 状态 0.默认 1.启用 2.禁用
        /// </summary>
        public int Status { get; set; }

    }

    public class UserActionSaveViewModel : UserActionViewModel
    {
        public string LAY_TABLE_INDEX { get; set; }
        public string pid { get; set; }
        public bool isParent { get; set; }
    }
}
