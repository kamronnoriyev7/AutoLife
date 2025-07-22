using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Brand)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(v => v.Model)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(v => v.NumberPlate)
               .IsRequired()
               .HasMaxLength(20);

        // User bilan bog'lanish (one-to-many)
        builder.HasOne(v => v.User)
               .WithMany(u => u.Vehicles)
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // FuelType bilan bog'lanish (one-to-many)
        builder.HasOne(v => v.FuelType)
               .WithMany()
               .HasForeignKey(v => v.FuelTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        // Images (optional) - agar kerak bo‘lsa alohida ImageConfiguration yoziladi
        builder.HasMany(v => v.Images)
               .WithOne(i => i.Vehicle)
               .HasForeignKey(i => i.VehicleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
