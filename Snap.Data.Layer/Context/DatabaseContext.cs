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
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<MonthType> MonthTypes { get; set; }
        public DbSet<Humidity> Humidities { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Factor> Factors { get; set; }
    }
}
