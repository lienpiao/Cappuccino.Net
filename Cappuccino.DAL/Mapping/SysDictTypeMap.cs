using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysDictTypeMap : EntityTypeConfiguration<SysDictType>
    {
        public SysDictTypeMap()
        {
            this.ToTable("SysDictType");
            this.HasKey(x => x.Id);
            this.Property(x => x.Code).HasMaxLength(50).IsRequired();
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();
            this.Property(x => x.SortCode);
        }
    }
}
