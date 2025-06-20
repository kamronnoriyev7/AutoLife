using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class FuelStationConfiguration : IEntityTypeConfiguration<FuelStation>
{
    public void Configure(EntityTypeBuilder<FuelStation> builder)
    {
        builder.ToTable("FuelStations");

        builder.HasKey(fs => fs.Id);

        builder.Property(fs => fs.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(fs => fs.OperatorName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(fs => fs.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        // Enumlar uchun
        builder.Property(fs => fs.FuelType)
            .HasConversion<string>()
            .IsRequired(false);

        builder.Property(fs => fs.FuelSubType)
            .HasConversion<string>()
            .IsRequired(false);

        // Address bilan bog‘liq
        builder.HasOne(fs => fs.Address)
            .WithMany(a => a.FuelStations)
            .HasForeignKey(fs => fs.AddressId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog‘liq
        builder.HasOne(fs => fs.Company)
            .WithMany(c => c.FuelStations)
            .HasForeignKey(fs => fs.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Images
        builder.HasMany(fs => fs.Images)
            .WithOne()
            .HasForeignKey("FuelStationId")
            .OnDelete(DeleteBehavior.Cascade);

        // FuelPrices
        builder.HasMany(fs => fs.FuelPrices)
            .WithOne(fp => fp.FuelStation)
            .HasForeignKey(fp => fp.FuelStationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ratings
        builder.HasMany(fs => fs.Ratings)
            .WithOne(r => r.FuelStation)
            .HasForeignKey(r => r.FuelStationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Favorites
        builder.HasMany(fs => fs.Favorites)
            .WithOne(f => f.FuelStation)
            .HasForeignKey(f => f.FuelStationId)
            .OnDelete(DeleteBehavior.Cascade);

        // FuelHistories
        builder.HasMany(fs => fs.FuelHistories)
            .WithOne(fh => fh.FuelStation)
            .HasForeignKey(fh => fh.FuelStationId)
            .OnDelete(DeleteBehavior.Cascade);

        // News
        builder.HasMany(fs => fs.News)
            .WithOne(n => n.FuelStation)
            .HasForeignKey(n => n.FuelStationId)
            .OnDelete(DeleteBehavior.SetNull);

        // Bookings
        builder.HasMany(fs => fs.Bookings)
            .WithOne(b => b.FuelStation)
            .HasForeignKey(b => b.FuelStationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
