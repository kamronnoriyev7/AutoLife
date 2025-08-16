using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class FuelSubTypeConfiguration : IEntityTypeConfiguration<FuelSubType>
{
    public void Configure(EntityTypeBuilder<FuelSubType> builder)
    {
        // Primary Key
        builder.HasKey(fst => fst.Id);

        // Properties
        builder.Property(fst => fst.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(fst => fst.Description)
               .HasMaxLength(500);

        // Relationship: FuelSubType -> FuelType (many-to-one)
        builder.HasOne(fst => fst.FuelType)
               .WithMany(ft => ft.FuelSubTypes)
               .HasForeignKey(fst => fst.FuelTypeId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship: FuelSubType -> FuelPrices (one-to-many)
        builder.HasMany(fst => fst.FuelPrices)
               .WithOne(fp => fp.FuelSubType)
               .HasForeignKey(fp => fp.FuelSubTypeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
