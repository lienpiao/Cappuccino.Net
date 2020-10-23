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
    public class SysDictTypeService : BaseService<SysDictType>, ISysDictTypeService
    {
        #region 依赖注入
        ISysDictTypeDao dao;
        public SysDictTypeService(ISysDictTypeDao dao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion

    }
}
