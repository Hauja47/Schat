using Microsoft.EntityFrameworkCore;
using Schat.Domain.Entities;
using System.Reflection;

namespace Schat.Infrastructure.Databases
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Domain.Entities.Channel> Channels { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
