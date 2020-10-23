using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysUserMap : EntityTypeConfiguration<SysUser>
    {
        public SysUserMap()
        {
            this.ToTable("SysUser");
            this.HasKey(x => x.Id);
            this.HasMany(x => x.SysRoles).WithMany(x => x.SysUsers).Map(x => x.ToTable("SysUserRole").MapLeftKey("UserId").MapRightKey("RoleId"));
            this.HasMany(x => x.SysUserActions).WithRequired(x => x.SysUser).HasForeignKey(x => x.UserId);
            //指定索引，不能重复
            this.HasIndex(c => c.UserName).IsUnique();
            this.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            this.Property(x => x.NickName).HasMaxLength(50).IsRequired();
            this.Property(x => x.PasswordHash).HasMaxLength(50).IsRequired();
            this.Property(x => x.PasswordSalt).HasMaxLength(50).IsRequired();
            this.Property(x => x.HeadIcon).HasMaxLength(50).IsRequired();
            this.Property(x => x.MobilePhone).HasMaxLength(11).IsOptional();
            this.Property(x => x.Email).HasMaxLength(50).IsRequired();
            this.Property(x => x.EnabledMark).IsRequired();
        }
    }
}
