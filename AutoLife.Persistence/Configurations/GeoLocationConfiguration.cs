using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class GeoLocationConfiguration : IEntityTypeConfiguration<GeoLocation>
{
    public void Configure(EntityTypeBuilder<GeoLocation> builder)
    {
        builder.ToTable("GeoLocations");

        builder.HasKey(gl => gl.Id);

        builder.Property(gl => gl.Latitude)
            .IsRequired();

        builder.Property(gl => gl.Longitude)
            .IsRequired();

        builder.Property(gl => gl.Description)
            .HasMaxLength(300)
            .IsRequired(false);

        // Address bilan 1:1 bog‘lanish
        builder.HasOne(gl => gl.Address)
            .WithOne(a => a.GeoLocation)
            .HasForeignKey<Address>(a => a.GeoLocationId)
            .OnDelete(DeleteBehavior.SetNull); // GeoLocation o‘chsa, Address.GeoLocationId null bo‘ladi
    }
}
