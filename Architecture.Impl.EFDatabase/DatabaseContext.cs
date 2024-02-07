using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Architecture.Impl.EFDatabase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<OverdraftAccount> OverdraftAccount { get; set; }
        public DbSet<NoOverdraftAccount> NoOverdraftAccounts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Banks = Set<Bank>();
            OverdraftAccount = Set<OverdraftAccount>();
            NoOverdraftAccounts = Set<NoOverdraftAccount>();
            Accounts = Set<Account>();
            Customers = Set<Customer>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BankMapping());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }

    }
}
