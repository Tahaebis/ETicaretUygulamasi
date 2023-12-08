using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETicaretUygulamasi.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Login> Login { get; set; }

        public DbSet<Register> Register { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=ETicaretUygDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}