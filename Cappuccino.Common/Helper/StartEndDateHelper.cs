using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Helper
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class StartEndDateHelper
    {
        public static DateTime GteStartDate(string startEndDate)
        {
            if (!string.IsNullOrEmpty(startEndDate) && startEndDate != " ~ ")
            {
                if (startEndDate.Contains("~"))
                {
                    if (startEndDate.Contains("+"))
                    {
                        startEndDate = startEndDate.Replace("+", "");
                    }
                    var dts = startEndDate.Split('~');
                    DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                    dtFormat.ShortDatePattern = "yyyy-MM-dd";
                    DateTime startDt = Convert.ToDateTime(dts[0].Trim(), dtFormat);
                    return startDt;
                }
            }
            return new DateTime();
        }

        public static DateTime GteEndDate(string startEndDate)
        {
            if (!string.IsNullOrEmpty(startEndDate) && startEndDate != " ~ ")
            {
                if (startEndDate.Contains("~"))
                {
                    if (startEndDate.Contains("+"))
                    {
                        startEndDate = startEndDate.Replace("+", "");
                    }
                    var dts = startEndDate.Split('~');
                    DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                    dtFormat.ShortDatePattern = "yyyy-MM-dd";
                    DateTime startDt = Convert.ToDateTime(dts[1].Trim(), dtFormat);
                    return startDt;
                }
            }
            return new DateTime();
        }

    }
}

