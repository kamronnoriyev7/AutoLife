using AutoLife.Domain.Entities.PaymentTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        // Jadval nomi
        builder.ToTable("PaymentTransactions");

        // Primary Key
        builder.HasKey(pt => pt.Id);

        // Property konfiguratsiyasi
        builder.Property(pt => pt.Provider)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pt => pt.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(pt => pt.Status)
            .IsRequired()
            .HasConversion<int>(); // Enum -> int sifatida saqlanadi

        // TransactionId unique bo‘lishi mumkin
        builder.HasIndex(pt => pt.TransactionId)
            .IsUnique();

        // Foydalanuvchi bilan bog‘lash
        builder.HasOne(pt => pt.User)
            .WithMany(u => u.PaymentTransactions)
            .HasForeignKey(pt => pt.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
