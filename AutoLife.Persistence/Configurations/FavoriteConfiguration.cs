using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        // Primary key
        builder.HasKey(f => f.Id);

        // User FK
        builder.HasOne(f => f.User)
               .WithMany(u => u.Favorites)
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Enum saqlanishi uchun int sifatida
        builder.Property(f => f.Type)
               .HasConversion<string>()
               .IsRequired();


        // Jadval nomi (agar kerak bo‘lsa)
        builder.ToTable("Favorites");
    }
}
