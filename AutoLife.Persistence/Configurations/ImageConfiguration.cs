using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImageUrl)
            .IsRequired()
            .HasMaxLength(500);

        // Vehicle bilan bog'lanish
        builder.HasOne(i => i.Vehicle)
            .WithMany(v => v.Images)
            .HasForeignKey(i => i.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        // User bilan bog'lanish
        builder.HasOne(i => i.User)
            .WithMany(u => u.Images)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // FuelStation bilan bog'lanish
        builder.HasOne(i => i.FuelStation)
            .WithMany(fs => fs.Images)
            .HasForeignKey(i => i.FuelStationId)
            .OnDelete(DeleteBehavior.NoAction);

        // Parking bilan bog'lanish
        builder.HasOne(i => i.Parking)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);

        // ServiceCenter bilan bog'lanish
        builder.HasOne(i => i.ServiceCenter)
            .WithMany(sc => sc.Images)
            .HasForeignKey(i => i.ServiceCenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // News bilan bog'lanish
        builder.HasOne(i => i.News)
            .WithMany(n => n.Images)
            .HasForeignKey(i => i.NewsId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog'lanish
        builder.HasOne(i => i.Company)
            .WithMany(c => c.Images)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}