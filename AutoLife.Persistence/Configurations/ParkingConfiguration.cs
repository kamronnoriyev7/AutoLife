using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class ParkingConfiguration : IEntityTypeConfiguration<Parking>
{
    public void Configure(EntityTypeBuilder<Parking> builder)
    {
        builder.ToTable("Parkings");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.HourlyRate)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.DailyRate)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.IsFree)
            .IsRequired();

        builder.Property(p => p.HasCameras)
            .IsRequired();

        builder.Property(p => p.IsCovered)
            .IsRequired();

        builder.Property(p => p.TotalSpaces)
            .IsRequired();

        builder.Property(p => p.AvailableSpaces)
            .IsRequired();

        builder.Property(p => p.OpeningTime)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.ClosingTime)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.IsPreBookingAllowed)
            .IsRequired(false);

        builder.Property(p => p.AverageRating)
            .HasColumnType("float")
            .HasDefaultValue(0);

        // Address - required
        builder.HasOne(p => p.Address)
            .WithMany(a => a.Parkings)
            .HasForeignKey(p => p.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        // User - optional
        builder.HasOne(p => p.User)
            .WithMany(u => u.Parkings)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company - optional
        builder.HasOne(p => p.Company)
            .WithMany(c => c.Parkings)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Ratings
        builder.HasMany(p => p.Ratings)
            .WithOne(r => r.Parking)
            .HasForeignKey(r => r.ParkingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Images
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Parking)
            .HasForeignKey(i => i.ParkingId)
            .OnDelete(DeleteBehavior.Cascade);

        // News
        builder.HasMany(p => p.News)
            .WithOne(n => n.Parking)
            .HasForeignKey(n => n.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);

        // Bookings
        builder.HasMany(p => p.Bookings)
            .WithOne(b => b.Parking)
            .HasForeignKey(b => b.ParkingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Favorites
        builder.HasMany(p => p.Favorites)
            .WithOne(f => f.Parking)
            .HasForeignKey(f => f.ParkingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Notifications
        builder.HasMany(p => p.Notifications)
            .WithOne(n => n.Parking)
            .HasForeignKey(n => n.ParkingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
