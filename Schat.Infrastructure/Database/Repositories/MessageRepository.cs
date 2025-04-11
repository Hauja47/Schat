namespace Schat.Infrastructure.Database.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class MessageRepository(AppDbContext context) : GenericRepository<Message>(context), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetMessagesByChannel(Guid channelId, int take = 10)
        => await this.db
            .Where(m => m.ChannelId == channelId)
            .OrderByDescending(m => m.CreatedDate)
            .Take(take)
            .ToListAsync();
}