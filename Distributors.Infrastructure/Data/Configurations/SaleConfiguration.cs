using Distributors.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Distributors.Infrastructure.Data.Configurations;
internal class SaleConfiguration : BaseConfiguration<SaleEntity>
{
    public override void Configure(EntityTypeBuilder<SaleEntity> builder)
    {
        base.Configure(builder);
    }
}
