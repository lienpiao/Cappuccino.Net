using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IDAL
{
    /// <summary>
    /// EF上下文的抽象工厂
    /// </summary>
    public interface IDbContextFactory
    {
        //获取当前上下文的唯一实例
        DbContext GetCurrentThreadInstance();
    }
}
