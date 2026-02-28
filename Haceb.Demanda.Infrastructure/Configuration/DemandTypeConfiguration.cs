using Haceb.Demanda.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Infrastructure.Configuration
{
    internal class DemandTypeConfiguration : IEntityTypeConfiguration<DemandTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DemandTypeEntity> builder)
        {
            builder.ToTable("DemandTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Description).HasMaxLength(50).IsRequired(true);
        }
    }
}
