using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configurations;

public class FuelSubTypeConfiguration : IEntityTypeConfiguration<FuelSubType>
{
    public void Configure(EntityTypeBuilder<FuelSubType> builder)
    {
        builder.HasKey(fst => fst.Id);

        builder.Property(fst => fst.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(fst => fst.Description)
               .HasMaxLength(500);

        // FuelType bilan bog‘lanish allaqachon FuelTypeConfiguration ichida bor,
        // bu yerda faqat mavjudligini ko‘rsatish kifoya
    }
}
