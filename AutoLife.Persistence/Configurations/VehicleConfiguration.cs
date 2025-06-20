using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Brand)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.NumberPlate)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(v => v.FuelType)
            .IsRequired();

        // User bilan aloqasi (majburiy)
        builder.HasOne(v => v.User)
            .WithMany(u => u.Vehicles)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Notification bilan optional one-to-one (birinchi notificationga ulanadi, lekin ko'p notificationlar collection’da ham bor)
        builder.HasOne(v => v.Notification)
            .WithOne(n => n.Vehicle)
            .HasForeignKey<Vehicle>(v => v.NotificationId)
            .OnDelete(DeleteBehavior.SetNull);

        // Images
        builder.HasMany(v => v.Images)
            .WithOne(i => i.Vehicle)
            .HasForeignKey(i => i.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Bookings
        builder.HasMany(v => v.Bookings)
            .WithOne(b => b.Vehicle)
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ratings
        builder.HasMany(v => v.Ratings)
            .WithOne(r => r.Vehicle)
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // News
        builder.HasMany(v => v.News)
            .WithOne(n => n.Vehicle)
            .HasForeignKey(n => n.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Favorites
        builder.HasMany(v => v.Favorites)
            .WithOne(f => f.Vehicle)
            .HasForeignKey(f => f.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Notifications
        builder.HasMany(v => v.Notifications)
            .WithOne(n => n.Vehicle)
            .HasForeignKey(n => n.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}