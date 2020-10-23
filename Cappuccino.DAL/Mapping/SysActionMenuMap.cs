using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.DAL.Mapping
{
    public class SysActionMenuMap : EntityTypeConfiguration<SysActionMenu>
    {
        public SysActionMenuMap()
        {
            ToTable("SysActionMenu");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ActionId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.Icon).HasMaxLength(50).IsOptional();
            this.Property(x => x.Url).HasMaxLength(100).IsOptional();

        }
    }
}
