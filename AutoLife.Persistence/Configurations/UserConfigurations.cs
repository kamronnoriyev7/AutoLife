using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.IdentityUserId).IsRequired(); // Navigation yo‘q, faqat ID

        builder.HasMany(u => u.PaymentTransactions)
           .WithOne(pt => pt.User)
           .HasForeignKey(pt => pt.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(u => u.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        // ProfileImage (1:1 optional)
        //builder.HasOne(u => u.ProfileImage)
        //    .WithOne()
        //    .HasForeignKey<User>(u => u.Id) // Ehtiyot bo‘ling: bu ProfileImage uchun alohida FK bo‘lmasa, `WithOne()` yetarli
        //    .OnDelete(DeleteBehavior.SetNull);

        // One-to-many munosabatlar:

        builder.HasMany(u => u.Images)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Vehicles)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Ratings)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AppFeedbacks)
            .WithOne(fb => fb.User)
            .HasForeignKey(fb => fb.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Favorites)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.FuelStations)
            .WithOne(fs => fs.User) // Agar `FuelStation` da `UserId` bo‘lsa
            .HasForeignKey(fs => fs.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Parkings)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.ServiceCenters)
            .WithOne(sc => sc.User)
            .HasForeignKey(sc => sc.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Companies)
            .WithOne(c => c.User) // Agar Company’da `UserId` mavjud bo‘lsa
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.News)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
