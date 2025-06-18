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
        builder.HasKey("CountryId", "RegionId", "DistrictId");
        // Agar Addressni boshqa Entity FK sifatida ishlatayotgan bo‘lsang, bu muhim emas.
        // Aks holda asosiy kalit sifatida alohida property (`Id`) qo‘shgan ma’qul.

        // Orientation - optional string
        builder.Property(a => a.Orientation)
               .HasMaxLength(255);

        // GeoLocation – complex (owned) type sifatida ko‘rib chiqamiz
        builder.OwnsOne(a => a.Location, location =>
        {
            location.Property(l => l.Latitude)
                    .HasColumnName("Latitude")
                    .HasPrecision(9, 6);

            location.Property(l => l.Longitude)
                    .HasColumnName("Longitude")
                    .HasPrecision(9, 6);
        });

        // Country - required
        builder.HasOne(a => a.Country)
               .WithMany()
               .HasForeignKey(a => a.CountryId)
               .OnDelete(DeleteBehavior.Restrict);

        // Region - optional
        builder.HasOne(a => a.Region)
               .WithMany()
               .HasForeignKey(a => a.RegionId)
               .OnDelete(DeleteBehavior.SetNull);

        // District - optional
        builder.HasOne(a => a.District)
               .WithMany()
               .HasForeignKey(a => a.DistrictId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}