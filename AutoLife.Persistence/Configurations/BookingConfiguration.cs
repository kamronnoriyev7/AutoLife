using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        new BaseEntityConfiguration<Booking>().Configure(builder);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingType)
               .HasConversion<int>() // Enum -> int
               .IsRequired();

        builder.Property(b => b.Description)
               .HasMaxLength(500);

        builder.Property(b => b.TotalPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(b => b.Status)
               .HasConversion<int>(); // Nullable enum

        // User bilan bog'lanish
        builder.HasOne(b => b.User)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Vehicle bilan bog'lanish
        builder.HasOne(b => b.Vehicle)
               .WithMany(v => v.Bookings)
               .HasForeignKey(b => b.VehicleId)
               .OnDelete(DeleteBehavior.SetNull);

        // Address bilan bog'lanish
        builder.HasOne(b => b.Address)
               .WithMany(a => a.Bookings)
               .HasForeignKey(b => b.AddressId)
               .OnDelete(DeleteBehavior.SetNull);

        // Notifications va Ratings — ICollection bo‘lgani uchun EF Core avtomatik 1:N qiladi
    }
}
