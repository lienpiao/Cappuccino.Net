using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IBLL
{
    public interface ISysLogLogonService : IBaseService<SysLogLogon>
    {
        /// <summary>
        /// 写入登录日志
        /// </summary>
        /// <param name="logLogon"></param>
        /// <returns></returns>
        int WriteDbLog(SysLogLogon logLogon);
    }
}
