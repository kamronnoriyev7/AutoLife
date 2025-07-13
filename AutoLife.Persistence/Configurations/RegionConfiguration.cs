using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("Regions");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UzName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.RuName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.EnName)
            .HasMaxLength(200)
            .IsRequired();

        // Country bilan aloqasi (1 ta Country - ko'p Region)
        builder.HasOne(r => r.Country)
            .WithMany(c => c.Regions)
            .HasForeignKey(r => r.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        // District lar bilan aloqasi
        builder.HasMany(r => r.Districts)
            .WithOne(d => d.Region)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
