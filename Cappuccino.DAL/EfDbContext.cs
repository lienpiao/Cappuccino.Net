using Cappuccino.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL
{
    /// <summary>
    /// EF上下文类
    /// </summary>
    public class EfDbContext : DbContext
    {
        public EfDbContext()
          : base("sqlconn")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
