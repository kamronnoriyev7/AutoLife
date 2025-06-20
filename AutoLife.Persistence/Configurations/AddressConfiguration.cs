using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Orientation)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(a => a.Street)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(a => a.HouseNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        // GeoLocation (1:1)
        builder.HasOne(a => a.GeoLocation)
            .WithOne(gl => gl.Address)
            .HasForeignKey<Address>(a => a.GeoLocationId)
            .OnDelete(DeleteBehavior.SetNull);

        // User (1:N optional)
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Country (N:1 required)
        builder.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Region (N:1 optional)
        builder.HasOne(a => a.Region)
            .WithMany()
            .HasForeignKey(a => a.RegionId)
            .OnDelete(DeleteBehavior.SetNull);

        // District (N:1 optional)
        builder.HasOne(a => a.District)
            .WithMany()
            .HasForeignKey(a => a.DistrictId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company (N:1 optional)
        builder.HasOne(a => a.Company)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Parkings
        builder.HasMany(a => a.Parkings)
            .WithOne(p => p.Address)
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        // ServiceCenters
        builder.HasMany(a => a.ServiceCenters)
            .WithOne(sc => sc.Address)
            .HasForeignKey(sc => sc.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        // FuelStations
        builder.HasMany(a => a.FuelStations)
            .WithOne(fs => fs.Address)
            .HasForeignKey(fs => fs.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}