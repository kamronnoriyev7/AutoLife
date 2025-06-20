using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class FuelPriceConfiguration : IEntityTypeConfiguration<FuelPrice>
{
    public void Configure(EntityTypeBuilder<FuelPrice> builder)
    {
        // Primary Key
        builder.HasKey(fp => fp.Id);

        // Foreign Key: FuelStation
        builder.HasOne(fp => fp.FuelStation)
               .WithMany(fs => fs.FuelPrices)
               .HasForeignKey(fp => fp.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // Foreign Key: FuelSubType
        builder.HasOne(fp => fp.FuelSubType)
               .WithMany()
               .HasForeignKey(fp => fp.FuelSubTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        // Enum: FuelType — int sifatida saqlanadi
        builder.Property(fp => fp.FuelType)
               .HasConversion<string>()
               .IsRequired();

        // Price field
        builder.Property(fp => fp.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        // Date
        builder.Property(fp => fp.Date)
               .HasDefaultValueSql("NOW()");

        // Jadval nomi
        builder.ToTable("FuelPrices");
    }
}