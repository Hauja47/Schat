namespace Schat.Domain.Interfaces;

using Entities;

public interface IChannelRepository
{
    Task<IEnumerable<Channel>> GetUserChannels(UserInfo user, int skip, int take);
}