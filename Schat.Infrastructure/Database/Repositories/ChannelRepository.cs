namespace Schat.Infrastructure.Database.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ChannelRepository(AppDbContext context) : GenericRepository<Channel>(context), IChannelRepository
{
    public async Task<IEnumerable<Channel>> GetUserChannels(UserInfo user, int skip = 0, int take = 10) 
        => await this.db
            .Where(c => c.Members.Contains(user))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
}