using Architecture.Domain;
using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Impl.EFDatabase.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(Consts.MaxNameLength).IsRequired();
            builder.Property(c => c.ClientNumber).IsRequired();
            builder.Property(c => c.Address).IsRequired();

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId);

            builder.HasOne(c => c.Bank)
                 .WithMany(a => a.Customers)
                 .HasForeignKey(c => c.BankId);
        }
    }
}
