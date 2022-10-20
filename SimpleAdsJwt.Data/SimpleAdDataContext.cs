using Microsoft.EntityFrameworkCore;

namespace SimpleAdsJwt.Data
{
    public class SimpleAdDataContext : DbContext
    {
        private readonly string _connectionString;

        public SimpleAdDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ad> Ads { get; set; }
    }
}
