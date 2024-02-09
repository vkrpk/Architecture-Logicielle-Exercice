using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Impl.EFDatabase.Mappings
{
    internal static class AccountMapping 
    {
        public static EntityTypeBuilder MapAccount(this ModelBuilder modelbuilder)
        {
            var builder = modelbuilder.GetBaseModelBuilder<Account>();

            builder.Property(e => e.AccountNumber)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasOne(a => a.Bank)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BankId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            return builder;
        }
    }
}
