using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Architecture.Domain.Models;

namespace Architecture.Impl.EFDatabase.Mappings
{
    internal static class BaseModelMapping
    {
        public static EntityTypeBuilder<TEntity> GetBaseModelBuilder<TEntity>(this ModelBuilder modelBuilder)
                  where TEntity : BaseModel
        {
            EntityTypeBuilder<TEntity> builder = modelBuilder.Entity<TEntity>();

            builder
                .HasKey(e => e.Id);

            return builder;
        }
    }
}
