using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Configurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreateDate)
               .IsRequired();

        builder.Property(e => e.UpdateDate)
               .IsRequired(false);

        builder.Property(e => e.DeleteDate)
               .IsRequired(false);

        builder.Property(e => e.IsDeleted)
               .HasDefaultValue(false);
    }
}
