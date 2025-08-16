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

        builder.HasKey(r => r.BasaEntityId);

        builder.Property(r => r.UzName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(r => r.RuName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(r => r.EnName)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasOne(r => r.Country)
            .WithMany(c => c.Regions)
            .HasForeignKey(r => r.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(r => r.Districts)
            .WithOne(d => d.Region)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
