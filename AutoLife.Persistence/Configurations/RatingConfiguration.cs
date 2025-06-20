using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Stars)
            .IsRequired(false)
            .HasDefaultValue(0);

        builder.Property(r => r.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        // User bilan bog‘lanish
        builder.HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // ServiceCenter bilan bog‘lanish
        builder.HasOne(r => r.ServiceCenter)
            .WithMany(sc => sc.Ratings)
            .HasForeignKey(r => r.ServiceCenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Parking bilan bog‘lanish
        builder.HasOne(r => r.Parking)
            .WithMany(p => p.Ratings)
            .HasForeignKey(r => r.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);

        // FuelStation bilan bog‘lanish
        builder.HasOne(r => r.FuelStation)
            .WithMany(fs => fs.Ratings)
            .HasForeignKey(r => r.FuelStationId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog‘lanish
        builder.HasOne(r => r.Company)
            .WithMany(c => c.Ratings)
            .HasForeignKey(r => r.Companyid)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
