using Microsoft.EntityFrameworkCore;

namespace FakeBankAPI.Entities
{
    public class BankDatabaseContext : DbContext
    {
        public BankDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
