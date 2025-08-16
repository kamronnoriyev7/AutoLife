using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        // Primary Key
        builder.HasKey(ft => ft.Id);

        // Properties
        builder.Property(ft => ft.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(ft => ft.Description)
               .HasMaxLength(500);

        // Relationship: FuelType -> FuelStation (many-to-one)
        builder.HasOne(ft => ft.FuelStation)
               .WithMany(fs => fs.FuelTypes)
               .HasForeignKey(ft => ft.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship: FuelType -> FuelSubTypes (one-to-many)
        builder.HasMany(ft => ft.FuelSubTypes)
               .WithOne(fst => fst.FuelType)
               .HasForeignKey(fst => fst.FuelTypeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
