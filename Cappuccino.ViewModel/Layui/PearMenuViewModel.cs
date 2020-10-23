using Cappuccino.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class PearMenuViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Icon { get; set; }
        public string OpenType { get; set; }
        public string Href { get; set; }
        public List<PearMenuViewModel> Children { get; set; } = new List<PearMenuViewModel>();
        public int ParentId { get; set; }
    }

}
