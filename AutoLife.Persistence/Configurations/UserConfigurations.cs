using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(u => u.UserName)
               .IsUnique(); // UserName unikal bo'lishi kerak

        builder.Property(u => u.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
               .HasMaxLength(255);

        builder.Property(u => u.PasswordSalt)
               .HasMaxLength(255);

        builder.Property(u => u.DateOfBirth)
               .IsRequired();

        builder.Property(u => u.CreatedAt)
               .IsRequired();

        builder.Property(u => u.Role)
               .HasConversion<int>() // Enum int sifatida saqlanadi
               .IsRequired();

        // 1:N - User → Vehicles
        builder.HasMany(u => u.Vehicles)
               .WithOne(v => v.User)
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // 1:N - User → Bookings
        builder.HasMany(u => u.Bookings)
               .WithOne(b => b.User)
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // 1:N - User → Notifications
        builder.HasMany(u => u.Notifications)
               .WithOne(n => n.User)
               .HasForeignKey(n => n.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // 1:N - User → Ratings
        builder.HasMany(u => u.Ratings)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // 1:N - User → RefreshTokens
        builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // 1:1 - User → ProfileImage (nullable)
        builder.HasOne(u => u.ProfileImage)
               .WithOne()
               .HasForeignKey<Image>(img => img.Id)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
