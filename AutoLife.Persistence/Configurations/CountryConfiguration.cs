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
        builder.HasKey(c => c.Id); // BaseEntity'dan meros

        builder.Property(c => c.UzName)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(c => c.RuName)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(c => c.EnName)
               .IsRequired()
               .HasMaxLength(128);

        builder.HasMany(c => c.Regions)
               .WithOne(r => r.Country)
               .HasForeignKey(r => r.CountryId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
