
using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class FuelSubTypeConfiguration : IEntityTypeConfiguration<FuelSubType>
{
    public void Configure(EntityTypeBuilder<FuelSubType> builder)
    {
        // Primary Key
        builder.HasKey(fst => fst.Id);

        // Required: Name
        builder.Property(fst => fst.Name)
               .IsRequired()
               .HasMaxLength(50);

        // Required: Description
        builder.Property(fst => fst.Description)
               .IsRequired()
               .HasMaxLength(250);

        // Enum: FuelType
        builder.Property(fst => fst.FuelType)
               .HasConversion<string>()
               .IsRequired();

        // Table name (ixtiyoriy)
        builder.ToTable("FuelSubTypes");
    }
}
