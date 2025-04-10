using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schat.Domain.Entities;

namespace Schat.Infrastructure.Database.EntityConfiguration
{
    public class MessageEntityConfiguration : BaseEntityConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder
                .HasKey(m => new { m.UserInfoId, m.ChannelId });

            builder
                .HasIndex(m => new { m.UserInfoId, m.ChannelId });

            builder
                .Property(m => m.Content)
                .IsRequired();
        }
    }
}
