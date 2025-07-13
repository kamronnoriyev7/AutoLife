using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class FuelHistoryConfiguration : IEntityTypeConfiguration<FuelHistory>
{
    public void Configure(EntityTypeBuilder<FuelHistory> builder)
    {
        builder.HasKey(fh => fh.Id); // BaseEntity'dan kelgan PK

        builder.Property(fh => fh.FuelType)
               .HasConversion<int>() // enumni int sifatida saqlaymiz
               .IsRequired();

        builder.Property(fh => fh.OldPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(fh => fh.NewPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(fh => fh.ChangedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // FuelStation bilan aloqa
        builder.HasOne(fh => fh.FuelStation)
               .WithMany(fs => fs.FuelHistories)
               .HasForeignKey(fh => fh.FuelStationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
