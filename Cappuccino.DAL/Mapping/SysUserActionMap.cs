using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysUserActionMap : EntityTypeConfiguration<SysUserAction>
    {
        public SysUserActionMap()
        {
            this.ToTable("SysUserAction");
            this.HasKey(x => x.Id);
            this.HasRequired(x => x.SysUser).WithMany(x => x.SysUserActions).HasForeignKey(x => x.UserId);
            this.HasRequired(x => x.SysAction).WithMany(x => x.SysUserActions).HasForeignKey(x => x.ActionId);
        }
    }
}
