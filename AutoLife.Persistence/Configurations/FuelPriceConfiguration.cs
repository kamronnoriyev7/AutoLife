using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Persistence.Configuration;

public class FuelPriceConfiguration : IEntityTypeConfiguration<FuelPrice>
{
    public void Configure(EntityTypeBuilder<FuelPrice> builder)
    {
        builder.HasKey(fp => fp.Id);

        builder.Property(fp => fp.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(fp => fp.Date)
            .IsRequired();



        builder.HasOne(fp => fp.FuelStation)
    .WithMany(fs => fs.FuelPrices)
    .HasForeignKey(fp => fp.FuelStationId)
    .OnDelete(DeleteBehavior.Restrict); // BU JOY TO‘G‘RI

        builder.HasOne(fp => fp.FuelSubType)
            .WithMany(fst => fst.FuelPrices)
            .HasForeignKey(fp => fp.FuelSubTypeId)
            .OnDelete(DeleteBehavior.Restrict); // BUNISI HAM RESTRICT BO‘LSIN

    }

}
