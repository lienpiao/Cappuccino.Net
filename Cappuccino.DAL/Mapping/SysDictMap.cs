using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysDictMap : EntityTypeConfiguration<SysDict>
    {
        public SysDictMap()
        {
            this.ToTable("SysDict");
            this.HasKey(x => x.Id);
            this.HasRequired(x => x.SysDictType).WithMany(x => x.SysDicts).HasForeignKey(x => x.TypeId);

            this.Property(x => x.Code).HasMaxLength(50).IsRequired();
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
