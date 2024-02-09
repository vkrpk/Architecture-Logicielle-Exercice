using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public DbSet<OverdraftAccount> OverdraftAccounts { get; set; }
        public DbSet<NoOverdraftAccount> NoOverdraftAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext() 
        {
            Banks = Set<Bank>();
            Accounts = Set<Account>();
            OverdraftAccounts = Set<OverdraftAccount>();
            NoOverdraftAccounts = Set<NoOverdraftAccount>();
            Customers = Set<Customer>();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost;Database=ArchitectureDB;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.MapBank();
            builder.MapAccount();
            builder.MapCustomer();
        }
    }
}
