using Microsoft.EntityFrameworkCore;
using Schat.Domain.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Schat.Infrastructure.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Channel> Channels { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
