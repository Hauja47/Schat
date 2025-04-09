using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schat.Domain.Entities;

namespace Schat.Infrastructure.EntityConfigurations
{
    public class ChannelEntityConfiguration : BaseEntityConfiguration<Channel>
    {
        public new void Configure(EntityTypeBuilder<Channel> builder)
        {
            base.Configure(builder);

            builder
                .Property(c => c.Name)
                .IsRequired();

            builder
                .HasIndex(c => c.Name);
        }
    }
}
