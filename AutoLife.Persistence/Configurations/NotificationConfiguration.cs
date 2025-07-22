using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(n => n.Body)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(n => n.IsRead)
            .HasDefaultValue(false)
            .IsRequired();

        // User bilan bog'lanish (majburiy)
        builder.HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Vehicle bilan bog‘lanish
        builder.HasOne(n => n.Vehicle)
            .WithMany(v => v.Notifications)
            .HasForeignKey(n => n.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        // ServiceCenter bilan bog‘lanish
        builder.HasOne(n => n.Service)
            .WithMany(sc => sc.Notifications)
            .HasForeignKey(n => n.ServiceCenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Booking bilan bog‘lanish
        builder.HasOne(n => n.Booking)
            .WithMany(b => b.Notifications)
            .HasForeignKey(n => n.BookingId)
            .OnDelete(DeleteBehavior.SetNull);

        // FuelStation bilan bog‘lanish
        builder.HasOne(n => n.FuelStation)
            .WithMany(fs => fs.Notifications)
            .HasForeignKey(n => n.FuelStationId)
            .OnDelete(DeleteBehavior.NoAction);

        // Parking bilan bog‘lanish
        builder.HasOne(n => n.Parking)
            .WithMany(p => p.Notifications)
            .HasForeignKey(n => n.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog‘lanish
        builder.HasOne(n => n.Company)
            .WithMany(c => c.Notifications)
            .HasForeignKey(n => n.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}