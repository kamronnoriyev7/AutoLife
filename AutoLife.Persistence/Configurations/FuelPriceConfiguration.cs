using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configuration;

public class FuelPriceConfiguration : IEntityTypeConfiguration<FuelPrice>
{
    public void Configure(EntityTypeBuilder<FuelPrice> builder)
    {
        // Primary Key
        builder.HasKey(fp => fp.Id);

        // Price property
        builder.Property(fp => fp.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // Date property
        builder.Property(fp => fp.Date)
               .IsRequired()
               .HasColumnType("date"); // SQL date turiga mos

        // Relationship: FuelPrice -> FuelSubType (many-to-one)
        builder.HasOne(fp => fp.FuelSubType)
               .WithMany() // FuelSubType da FuelPrices kolleksiyasi bo'lmasa shunday yoziladi
               .HasForeignKey(fp => fp.FuelSubTypeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
