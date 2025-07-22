using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("News");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(n => n.Body)
            .HasColumnType("text")
            .IsRequired(false);

        // User bilan bog‘lanish
        builder.HasOne(n => n.User)
            .WithMany(u => u.News)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        // Company bilan bog‘lanish
        builder.HasOne(n => n.Company)
            .WithMany(c => c.NewsList)
            .HasForeignKey(n => n.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        // FuelStation bilan bog‘lanish
        builder.HasOne(n => n.FuelStation)
            .WithMany(fs => fs.News)
            .HasForeignKey(n => n.FuelStationId)
            .OnDelete(DeleteBehavior.NoAction);

        // ServiceCenter bilan bog‘lanish
        builder.HasOne(n => n.ServiceCenter)
            .WithMany(sc => sc.News)
            .HasForeignKey(n => n.ServiceCenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // Parking bilan bog‘lanish
        builder.HasOne(n => n.Parking)
            .WithMany(p => p.News)
            .HasForeignKey(n => n.ParkingId)
            .OnDelete(DeleteBehavior.SetNull);

        // Images bilan bog‘lanish
        builder.HasMany(n => n.Images)
            .WithOne(i => i.News)
            .HasForeignKey(i => i.NewsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}