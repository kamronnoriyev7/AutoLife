using AutoLife.Identity.Models.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // Jadval nomi
        builder.ToTable("UserRoles");

        // Primary Key
        builder.HasKey(r => r.Id);

        // Property: Name
        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(r => r.Name) // Unique constraint
               .IsUnique();

        // Property: Description
        builder.Property(r => r.Description)
               .HasMaxLength(250);

        // BaseEntity'dan kelgan common property'lar uchun optional defaultlar
        builder.Property(r => r.CreateDate)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.IsDeleted)
               .HasDefaultValue(false);

        // Relationship: 1 Role → N IdentityUsers
        builder.HasMany(r => r.Users)
               .WithOne(u => u.Role)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict); // Rol o‘chsa ham foydalanuvchi saqlanadi
    }
}