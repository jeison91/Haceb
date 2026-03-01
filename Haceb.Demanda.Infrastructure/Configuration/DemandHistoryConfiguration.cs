using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haceb.Demanda.Domain.Entities;

namespace Haceb.Demanda.Infrastructure.Configuration
{
    public class DemandHistoryConfiguration : IEntityTypeConfiguration<DemandHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<DemandHistoryEntity> builder)
        {
            builder.ToTable("DemandHistories");
            builder.HasKey(x => x.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Action).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Comments).HasMaxLength(1000);
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.DemandEntity)
                .WithMany(d => d.HistoryEntities)
                .HasForeignKey(x => x.DemandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UserEntity)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
