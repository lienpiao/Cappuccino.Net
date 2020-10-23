using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cappuccino.Web.Models
{
    public class PageInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数据量
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }
    }
}