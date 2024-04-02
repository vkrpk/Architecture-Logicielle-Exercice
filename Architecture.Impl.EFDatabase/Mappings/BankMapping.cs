using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Impl.EFDatabase.Mappings
{
    internal static class BankMapping
    {
        public static EntityTypeBuilder MapBank(this ModelBuilder modelbuilder)
        {
            var builder = modelbuilder.GetBaseModelBuilder<Bank>();

            builder
                .HasMany(b => b.Customers)
                .WithOne(c => c.Bank)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Bank)
                .HasForeignKey(a => a.BankId)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;

        }
    }
}