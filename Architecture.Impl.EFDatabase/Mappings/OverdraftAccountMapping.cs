using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.EFDatabase.Mappings
{
    public static class OverdraftAccountMapping
    {
        public static EntityTypeBuilder MapOverdraftAccount(this ModelBuilder modelbuilder)
        {
            var builder = modelbuilder.GetBaseModelBuilder<OverdraftAccount>();

            return builder;
        }
    }
}
