using Microsoft.EntityFrameworkCore;

namespace ETicaretUygulamasi.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=ETicaretUygDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");

            optionsBuilder.UseLazyLoadingProxies();
        }

        public static string LoggedUser { get; set; }

    }
    

}