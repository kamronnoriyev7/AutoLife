using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.HasKey(ft => ft.Id);

        builder.Property(ft => ft.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(ft => ft.Description)
               .HasMaxLength(500);

        builder.HasMany(ft => ft.FuelSubTypes)
               .WithOne(fst => fst.FuelType)
               .HasForeignKey(fst => fst.FuelTypeId)
               .OnDelete(DeleteBehavior.NoAction); // ixtiyoriy: FuelType o‘chsa SubType ham o‘chadi
    }
}
