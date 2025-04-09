using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schat.Domain.Entities;

namespace Schat.Infrastructure.EntityConfigurations
{
    public class MessageEntityConfiguration : BaseEntityConfiguration<Message>
    {
        public new void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder
                .HasKey(m => new { m.UserId, m.ChannelId });

            builder
                .HasIndex(m => new { m.UserId, m.ChannelId });
        }
    }
}
