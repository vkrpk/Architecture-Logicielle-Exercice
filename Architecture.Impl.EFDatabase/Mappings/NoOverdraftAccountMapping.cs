using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase.Mappings
{
    public static class NoOverdraftAccountMapping
    {
        public static EntityTypeBuilder MapNoOverdraftAccount(this ModelBuilder modelbuilder)
        {
            var builder = modelbuilder.GetBaseModelBuilder<NoOverdraftAccount>();

            return builder;
        }
    }
}
