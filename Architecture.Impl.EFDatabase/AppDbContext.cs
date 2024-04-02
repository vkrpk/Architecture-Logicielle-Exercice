using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<OverdraftAccount> OverdraftAccounts { get; set; }
        public virtual DbSet<NoOverdraftAccount> NoOverdraftAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Banks = Set<Bank>();
            Accounts = Set<Account>();
            OverdraftAccounts = Set<OverdraftAccount>();
            NoOverdraftAccounts = Set<NoOverdraftAccount>();
            Customers = Set<Customer>();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.MapBank();
            builder.MapAccount();
            builder.MapCustomer();
        }
    }
}