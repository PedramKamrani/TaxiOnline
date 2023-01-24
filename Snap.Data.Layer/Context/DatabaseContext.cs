using Microsoft.EntityFrameworkCore;
using Snap.Data.Layer.Entities;

namespace Snap.Data.Layer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Driver>Drivers { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Color?> Colors { get; set; }
        public DbSet<RateType?> RateTypes { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}
