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

        builder.Property(b => b.From)
               .IsRequired();

        builder.Property(b => b.To)
               .IsRequired();

        builder.Property(b => b.SpotCount)
               .IsRequired();

        builder.Property(b => b.TotalPrice)
               .HasColumnType("decimal(18,2)") // Pul uchun aniq format
               .IsRequired();

        builder.Property(b => b.Status)
               .HasConversion<int>() // enumni int qilib saqlaymiz
               .IsRequired();

        // User bilan aloqasi (1:N)
        builder.HasOne(b => b.User)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Hozir ParkingId bor, lekin Parking entitiy yo‘q. Bo‘lsa, mana bunday yoziladi:
        // builder.HasOne(b => b.Parking)
        //        .WithMany(p => p.Bookings)
        //        .HasForeignKey(b => b.ParkingId)
        //        .OnDelete(DeleteBehavior.Cascade);
    }
}
