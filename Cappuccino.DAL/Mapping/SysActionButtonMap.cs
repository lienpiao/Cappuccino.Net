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
    public class SysActionButtonMap : EntityTypeConfiguration<SysActionButton>
    {
        public SysActionButtonMap()
        {
            ToTable("SysActionButton");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ActionId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(x => x.ButtonCode).HasMaxLength(50).IsRequired();
            this.Property(x => x.ButtonClass).HasMaxLength(50).IsRequired();
            this.Property(x => x.ButtonIcon).HasMaxLength(50).IsRequired();

        }
    }
}
