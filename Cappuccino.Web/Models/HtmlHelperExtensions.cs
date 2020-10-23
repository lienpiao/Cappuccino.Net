using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Cappuccino.ViewModel;

namespace Cappuccino.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 查询按钮
        /// </summary>
        public static HtmlString SearchBtnHtml(this HtmlHelper helper, string title = "查询", string _class = "pear-btn-primary")
        {
            return new HtmlString(string.Format(@"<button class='pear-btn pear-btn-md {1}' lay-submit lay-filter='search'>
                                                   <i class='layui-icon layui-icon-search'></i>{0}
                                                </button>", title, _class));
        }
        /// <summary>
        /// 重置按钮
        /// </summary>
        public static HtmlString ResetBtnHtml(this HtmlHelper helper, string title = "重置", string _class = "")
        {
            return new HtmlString(string.Format(@"<button type='reset' class='pear-btn pear-btn-md {1}'><i class='layui-icon layui-icon-refresh'></i>{0}</button>", title, _class));
        }

        /// <summary>
        /// 表格内按钮组
        /// </summary>]
        public static HtmlString RightToolBarHtml(this HtmlHelper helper, dynamic _list = null)
        {
            StringBuilder sb = new StringBuilder();
            List<ButtonViewModel> list = _list as List<ButtonViewModel>;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.AppendLine(string.Format(@"<button class='{0}' lay-event='{1}'><i class='layui-icon {2}'></i></button>", item.ClassName, item.ButtonCode, item.Icon));
                }
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// 表格外按钮组
        /// </summary>
        public static HtmlString TopToolBarHtml(this HtmlHelper helper, dynamic _list = null)
        {
            StringBuilder sb = new StringBuilder();
            List<ButtonViewModel> list = _list as List<ButtonViewModel>;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    sb.AppendLine(string.Format(@"<button class='{0}' lay-event='{1}'><i class='layui-icon {3}'></i>{2}</button>", item.ClassName, item.ButtonCode, item.FullName, item.Icon));
                }
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// 状态单选框
        /// </summary>
        public static HtmlString EnabledMarkRadioHtml(this HtmlHelper helper, int defaultVal = 0)
        {
            var enabled = defaultVal == 1 ? "checked" : "";
            var disabled = defaultVal == 0 ? "checked" : "";
            return new HtmlString(string.Format(@"<div class='layui-form-item'>
                                        <label class='layui-form-label'>状态</label>
                                        <div class='layui-input-block'>
                                            <input type='radio' name='enabledMark' value='1' title='开启' {0}>
                                            <input type='radio' name='enabledMark' value='0' title='禁用' {1}>
                                        </div>
                                    </div>", enabled, disabled));
        }
    }
}
