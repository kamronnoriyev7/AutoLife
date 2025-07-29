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

       
        // User (1:N optional)
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Country (N:1 required)
        builder.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        // Region (N:1 optional)
        builder.HasOne(a => a.Region)
            .WithMany()
            .HasForeignKey(a => a.RegionId)
            .OnDelete(DeleteBehavior.NoAction);

        // District (N:1 optional)
        builder.HasOne(a => a.District)
            .WithMany()
            .HasForeignKey(a => a.DistrictId)
            .OnDelete(DeleteBehavior.NoAction);

        // Company (N:1 optional)
        builder.HasOne(a => a.Company)
           .WithOne(c => c.Address)
           .HasForeignKey<Address>(a => a.CompanyId)
           .OnDelete(DeleteBehavior.NoAction);


        // Parkings
        builder.HasMany(a => a.Parkings)
            .WithOne(p => p.Address)
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.NoAction);

        // ServiceCenters
        builder.HasMany(a => a.ServiceCenters)
            .WithOne(sc => sc.Address)
            .HasForeignKey(sc => sc.AddressId)
            .OnDelete(DeleteBehavior.NoAction);

        // FuelStations
        builder.HasMany(a => a.FuelStations)
            .WithOne(fs => fs.Address)
            .HasForeignKey(fs => fs.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}