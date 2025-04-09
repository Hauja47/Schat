using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using Schat.Domain.Entities;

namespace Schat.Infrastructure.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(e => e.CreatedDate)
                .HasDefaultValue(SystemClock.Instance.GetCurrentInstant().InUtc())
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.UpdatedDate)
                .HasDefaultValue(null)
                .ValueGeneratedOnUpdate();
        }
    }
}
