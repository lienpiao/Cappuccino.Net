using Cappuccino.IDAL;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace Cappuccino.DAL
{
    /// <summary>
    /// 当前EF上下文的抽象工厂
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// 获取当前EF上下文的唯一实例
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentThreadInstance()
        {
            DbContext obj = CallContext.GetData(typeof(EfDbContext).FullName) as DbContext;
            if (obj == null)
            {
                obj = new EfDbContext();
                CallContext.SetData(typeof(EfDbContext).FullName, obj);
            }
            return obj;
        }
    }
}
