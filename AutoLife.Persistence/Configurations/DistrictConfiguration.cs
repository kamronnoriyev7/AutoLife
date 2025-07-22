using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.BasaEntityId); // BaseEntity'dan olingan PK

        builder.Property(d => d.UzName)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(d => d.RuName)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(d => d.EnName)
               .IsRequired()
               .HasMaxLength(128);

        builder.HasOne(d => d.Region)
               .WithMany(r => r.Districts)
               .HasForeignKey(d => d.RegionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
