using AutoLife.Identity.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLife.Identity.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCodes");

        builder.HasKey(vc => vc.Id);

        builder.Property(vc => vc.Email)
            .HasMaxLength(256);

        builder.Property(vc => vc.Code)
            .HasMaxLength(20);

        builder.Property(vc => vc.ExpireAt)
            .IsRequired();

        builder.Property(vc => vc.IsUsed)
            .HasColumnType("bit") // SQLda bool = bit
            .HasDefaultValue(false);

        // Agar kerak bo‘lsa, unique constraint yoki indeks
        builder.HasIndex(vc => vc.Code);
    }
}
