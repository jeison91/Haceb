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
    public class DemandAssignmentConfiguration : IEntityTypeConfiguration<DemandAssignmentEntity>
    {
        public void Configure(EntityTypeBuilder<DemandAssignmentEntity> builder)
        {
            builder.ToTable("DemandAssignments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.DemandId, x.UserId }).IsUnique();

            builder.HasOne(x => x.DemandEntity)
                .WithMany(d => d.AssignmentEntities)
                .HasForeignKey(x => x.DemandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UserEntity)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
