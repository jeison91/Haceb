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
    public class DemandConfiguration : IEntityTypeConfiguration<DemandEntity>
    {
        public void Configure(EntityTypeBuilder<DemandEntity> builder)
        {
            builder.ToTable("Demands");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.CaseNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.PlaintiffName).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(1000);
            builder.Property(d => d.DateRegistry).IsRequired();
            builder.Property(d => d.Prioritize).IsRequired().HasConversion<int>();
            builder.Property(d => d.Status).IsRequired().HasConversion<int>();

            builder.HasOne(d => d.TypeEntity)
               .WithMany(x=> x.Demands)
               .HasForeignKey(x => x.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Rating)
               .WithMany()
               .HasForeignKey(x=> x.RatingId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UserEntity)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AssignmentEntities)
                .WithOne(x => x.DemandEntity)
                .HasForeignKey(x=> x.DemandId);

            builder.HasMany(x => x.HistoryEntities)
                .WithOne(x => x.DemandEntity)
                .HasForeignKey(x => x.DemandId);
        }
    }
}
