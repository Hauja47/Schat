namespace Schat.Infrastructure.Database.Repositories;

using Domain.Entities;
using Domain.Interfaces;

public class UserInfoRepository(AppDbContext context) : GenericRepository<UserInfo>(context), IUserInfoRepository
{
    
}