using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class AppFeedbackConfiguration : IEntityTypeConfiguration<AppFeedback>
{
    public void Configure(EntityTypeBuilder<AppFeedback> builder)
    {
        new BaseEntityConfiguration<AppFeedback>().Configure(builder);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Message)
               .IsRequired()
               .HasMaxLength(1000); // maksimal uzunlik qo‘shdik

        builder.Property(f => f.SentAt)
               .IsRequired();

        builder.Property(f => f.Type)
               .HasConversion<int>() // Enumni integer sifatida saqlaymiz
               .IsRequired();

        // 1:N - User → AppFeedback
        builder.HasOne(f => f.User)
               .WithMany(u => u.AppFeedbacks) // 🟡 User.cs da ICollection<AppFeedback> Feedbacks bo'lishi kerak
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade); // User o‘chsa, feedbacklar ham o‘chadi
    }
}
