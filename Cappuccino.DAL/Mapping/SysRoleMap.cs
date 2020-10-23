using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysRoleMap : EntityTypeConfiguration<SysRole>
    {
        public SysRoleMap()
        {
            this.ToTable("SysRole");
            this.HasKey(x => x.Id);
            this.HasMany(x => x.SysActions).WithMany(x => x.SysRoles).Map(x => x.ToTable("SysRoleAction").MapLeftKey("RoleId").MapRightKey("ActionId"));
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.Property(x => x.Code).HasMaxLength(50).IsRequired();
            this.Property(x => x.EnabledMark).IsRequired();
            this.Property(x => x.Remark).HasMaxLength(250).IsOptional();
        }
    }
}
