using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(c => c.BasaEntityId);

        builder.Property(c => c.UzName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.RuName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.EnName)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasMany(c => c.Regions)
            .WithOne(r => r.Country)
            .HasForeignKey(r => r.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
