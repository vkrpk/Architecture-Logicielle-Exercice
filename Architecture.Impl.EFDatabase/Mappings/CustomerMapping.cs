using Architecture.Domain;
using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Impl.EFDatabase.Mappings
{
    internal static class CustomerMapping
    {
        public static EntityTypeBuilder MapCustomer(this ModelBuilder modelbuilder)
        {
            var builder = modelbuilder.GetBaseModelBuilder<Customer>();

            builder.Property(c => c.Name).HasMaxLength(Consts.MaxNameLength).IsRequired();
            builder.Property(c => c.ClientNumber).IsRequired();
            builder.Property(c => c.Address).IsRequired();

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(c => c.Bank)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.NoAction);


            return builder;
        }
    }
}