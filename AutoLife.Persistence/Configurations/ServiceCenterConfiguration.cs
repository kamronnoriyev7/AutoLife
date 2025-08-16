using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class ServiceCenterConfiguration : IEntityTypeConfiguration<ServiceCenter>
{
    public void Configure(EntityTypeBuilder<ServiceCenter> builder)
    {
        builder.ToTable("ServiceCenters");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(sc => sc.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(sc => sc.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(sc => sc.ServiceType)
            .IsRequired(); // Enum (ServiceType)

        // User bilan bog'lanish
        builder.HasOne(sc => sc.User)
            .WithMany(u => u.ServiceCenters)
            .HasForeignKey(sc => sc.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Address bilan bog'lanish
        builder.HasOne(sc => sc.Address)
            .WithMany(a => a.ServiceCenters)
            .HasForeignKey(sc => sc.AddressId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog'lanish
        builder.HasOne(sc => sc.Company)
            .WithMany(c => c.ServiceCenters)
            .HasForeignKey(sc => sc.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Images
        builder.HasMany(sc => sc.Images)
            .WithOne(i => i.ServiceCenter)
            .HasForeignKey(i => i.ServiceCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ratings
        builder.HasMany(sc => sc.Ratings)
            .WithOne(r => r.ServiceCenter)
            .HasForeignKey(r => r.ServiceCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // News
        builder.HasMany(sc => sc.News)
            .WithOne(n => n.ServiceCenter)
            .HasForeignKey(n => n.ServiceCenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Favorites
        builder.HasMany(sc => sc.Favorites)
            .WithOne(f => f.ServiceCenter)
            .HasForeignKey(f => f.ServiceCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // Notifications
        builder.HasMany(sc => sc.Notifications)
            .WithOne(n => n.Service)
            .HasForeignKey(n => n.ServiceCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // ServiceCenter ichida Booking mappingni umuman qo‘ymaymiz
        builder.Ignore(sc => sc.Bookings);

    }
}
