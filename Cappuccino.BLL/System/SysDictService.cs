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
    public class SysDictService : BaseService<SysDict>, ISysDictService
    {
        #region 依赖注入
        ISysDictDao dao;
        public SysDictService(ISysDictDao dao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion

    }
}
