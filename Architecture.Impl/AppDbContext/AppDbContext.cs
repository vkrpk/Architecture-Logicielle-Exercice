using Microsoft.EntityFrameworkCore;
using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionString"); // Remplace connectionString par ta propre chaîne de connexion
        }
    }
}
