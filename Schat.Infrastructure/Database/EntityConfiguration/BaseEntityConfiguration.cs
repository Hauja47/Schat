using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schat.Domain.Entities;

namespace Schat.Infrastructure.Database.EntityConfiguration
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(e => e.CreatedDate)
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.UpdatedDate)
                .HasDefaultValue(null)
                .ValueGeneratedOnUpdate();
        }
    }
}
