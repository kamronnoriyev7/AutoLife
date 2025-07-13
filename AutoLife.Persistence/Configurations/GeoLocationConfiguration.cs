using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.HasOne(gl => gl.Address)
         .WithOne(a => a.GeoLocation)
         .HasForeignKey<GeoLocation>(gl => gl.AddressId)
         .OnDelete(DeleteBehavior.Cascade);

    }
}
