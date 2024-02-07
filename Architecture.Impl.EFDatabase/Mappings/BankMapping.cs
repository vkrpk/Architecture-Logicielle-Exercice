using Architecture.Domain.Models;
using Architecture.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase.Mappings
{
    public class BankMapping : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasMany(b => b.Customers)
                .WithOne(c => c.Bank)
                .HasForeignKey(c => c.BankId);

            builder
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Bank)
                .HasForeignKey(a => a.BankId);

        }
    }
}
