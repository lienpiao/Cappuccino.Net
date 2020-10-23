using Cappuccino.Common.Net;
using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public class SysLogLogonService : BaseService<SysLogLogon>, ISysLogLogonService
    {
        #region 依赖注入
        ISysLogLogonDao dao;
        public SysLogLogonService(ISysLogLogonDao dao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion


        /// <summary>
        /// 写入登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int WriteDbLog(SysLogLogon logLogon)
        {
            logLogon.IPAddress = NetHelper.Ip;
            logLogon.IPAddressName = NetHelper.GetLocation(logLogon.IPAddress);
            logLogon.CreateTime = DateTime.Now;
            return Add(logLogon);
        }
    }
}
