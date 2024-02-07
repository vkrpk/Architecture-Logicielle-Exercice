using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Architecture.Impl.EFDatabase
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<OverdraftAccount> OverdraftAccount { get; set; }
        public DbSet<NoOverdraftAccount> NoOverdraftAccounts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public  DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BankMapping());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }

    }
}
