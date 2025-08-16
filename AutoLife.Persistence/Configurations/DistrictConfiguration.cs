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
        builder.ToTable("Districts");

        builder.HasKey(d => d.BasaEntityId);

        builder.Property(d => d.UzName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(d => d.RuName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(d => d.EnName)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasOne(d => d.Region)
            .WithMany(r => r.Districts)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}