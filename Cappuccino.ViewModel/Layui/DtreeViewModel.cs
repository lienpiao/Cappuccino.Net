using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class DtreeViewModel
    {
        public DtreeStatus Status { get; set; }
        public List<DtreeData> Data { get; set; }
    }

    public class DtreeData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
        public List<DtreeData> Children { get; set; } = new List<DtreeData>();
        public string CheckArr = "0";
    }

    public class DtreeStatus
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "操作成功";
    }

    public class DtreeResponse
    {
        public string NodeId { get; set; }
        public string ParntId { get; set; }
        public string Context { get; set; }
        public bool Leaf { get; set; }
        public string Level { get; set; }
        public string Spread { get; set; }
        public string DataType { get; set; }
        public string Checked { get; set; }
        public string Initcheked { get; set; }
    }
}
