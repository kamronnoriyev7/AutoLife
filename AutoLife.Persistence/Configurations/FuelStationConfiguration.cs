using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class FuelStationConfiguration : IEntityTypeConfiguration<FuelStation>
{
    public void Configure(EntityTypeBuilder<FuelStation> builder)
    {
        builder.HasKey(fs => fs.Id);

        builder.Property(fs => fs.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(fs => fs.OperatorName)
               .HasMaxLength(100);

        builder.Property(fs => fs.PhoneNumber)
               .HasMaxLength(30);

        // Address bilan bog‘lanish (nullable)
        builder.HasOne(fs => fs.Address)
           .WithMany(a => a.FuelStations)
           .HasForeignKey(fs => fs.AddressId)
           .OnDelete(DeleteBehavior.NoAction);


        // User bilan bog‘lanish (nullable)
        builder.HasOne(fs => fs.User)
               .WithMany()
               .HasForeignKey(fs => fs.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        // Company bilan bog‘lanish (nullable)
        builder.HasOne(fs => fs.Company)
               .WithMany()
               .HasForeignKey(fs => fs.CompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        // FuelType bilan bog‘lanish (required)
        builder.HasOne(fs => fs.FuelType)
               .WithMany()
               .HasForeignKey(fs => fs.FuelTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        // FuelSubType bilan bog‘lanish (nullable)
        builder.HasOne(fs => fs.FuelSubType)
               .WithMany()
               .HasForeignKey(fs => fs.FuelSubTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        // FuelPrices, Ratings, etc. - bu ICollection navigatsiyalar EF Core tomonidan avtomatik aniqlanadi,
        // ammo kerak bo‘lsa, .WithOne().HasForeignKey() tarzida ularga ham alohida konfiguratsiya yozish mumkin.
    }
}
