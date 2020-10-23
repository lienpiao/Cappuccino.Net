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
    public class SysActionMap : EntityTypeConfiguration<SysAction>
    {
        public SysActionMap()
        {
            ToTable("SysAction");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasOptional(x => x.SysActionMenu).WithRequired(x => x.SysAction);
            HasOptional(x => x.SysActionButton).WithRequired(x => x.SysAction);

            this.Property(x => x.Code).HasMaxLength(50).IsRequired();
            this.Property(x => x.Name).HasMaxLength(50).IsRequired();

        }
    }
}
