using AutoLife.Identity.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Configuration;

public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.ToTable("IdentityUsers");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(u => u.PhoneNumber)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.PasswordSalt)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Role aloqasi (agar Role entity bo‘lsa)
        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

      

        //builder.Property(u => u.CodeExpireAt);

        builder.Property(u => u.IsEmailConfirmed)
            .HasDefaultValue(false);

        //builder.Property(u => u.IsPhoneConfirmed)
        //    .HasDefaultValue(false);

    }
}
