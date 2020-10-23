using Cappuccino.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class ActionViewModel
    {
        /// 权限名
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 按钮编码
        /// </summary>
        public string ButtonCode { get; set; }

        /// <summary>
        /// 位置 0：表内 1：表外
        /// </summary>
        public PositionEnum Location { get; set; }

        /// <summary>
        /// 按钮样式
        /// </summary>
        public string ButtonClass { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ButtonIcon { get; set; }

    }
}
