using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<OverdraftAccount> OverdraftAccount { get; set; }
        public DbSet<NoOverdraftAccount> NoOverdraftAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext() 
        {
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
