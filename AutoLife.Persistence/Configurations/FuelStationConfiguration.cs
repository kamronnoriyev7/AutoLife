using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AutoLife.Persistence.Configurations;

public class FuelStationConfiguration : IEntityTypeConfiguration<FuelStation>
{
    public void Configure(EntityTypeBuilder<FuelStation> builder)
    {
        // Primary Key
        builder.HasKey(fs => fs.Id);

        // Properties
        builder.Property(fs => fs.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(fs => fs.OperatorName)
               .HasMaxLength(100);

        builder.Property(fs => fs.PhoneNumber)
               .HasMaxLength(20);

        // Address (many-to-one)
        builder.HasOne(fs => fs.Address)
               .WithMany(a => a.FuelStations)
               .HasForeignKey(fs => fs.AddressId)
               .OnDelete(DeleteBehavior.Restrict);

        // User (optional, many-to-one)
        builder.HasOne(fs => fs.User)
               .WithMany(u => u.FuelStations)
               .HasForeignKey(fs => fs.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        // Company (optional, many-to-one)
        builder.HasOne(fs => fs.Company)
               .WithMany(c => c.FuelStations)
               .HasForeignKey(fs => fs.CompanyId)
               .OnDelete(DeleteBehavior.SetNull);

        // FuelTypes (one-to-many)
        builder.HasMany(fs => fs.FuelTypes)
               .WithOne(ft => ft.FuelStation)
               .HasForeignKey(ft => ft.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // Images (one-to-many)
        builder.HasMany(fs => fs.Images)
               .WithOne(i => i.FuelStation)
               .HasForeignKey(img => img.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // Ratings (one-to-many)
        builder.HasMany(fs => fs.Ratings)
               .WithOne(r => r.FuelStation)
               .HasForeignKey(r => r.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // Favorites (one-to-many)
        builder.HasMany(fs => fs.Favorites)
               .WithOne(f => f.FuelStation) 
               .HasForeignKey(fav => fav.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);


        // Notifications (one-to-many)
        builder.HasMany(fs => fs.Notifications)
               .WithOne(n => n.FuelStation)
               .HasForeignKey(n => n.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // News (one-to-many)
        builder.HasMany(fs => fs.News)
               .WithOne(n => n.FuelStation)
               .HasForeignKey(news => news.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // ❌ Booking bilan to‘g‘ridan-to‘g‘ri bog‘lamaymiz
        // Chunki Booking TargetId orqali ishlaydi
        builder.Ignore(fs => fs.Bookings);
    }
}