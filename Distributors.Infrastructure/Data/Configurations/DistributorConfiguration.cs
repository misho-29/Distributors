using Distributors.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Distributors.Infrastructure.Data.Configurations;
internal class DistributorConfiguration : BaseConfiguration<DistributorEntity>
{ 
    public override void Configure(EntityTypeBuilder<DistributorEntity> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
        builder.Property(x => x.ContactInfo).HasMaxLength(100);
        builder.Property(x => x.AddressInfo).HasMaxLength(100);
    }
}
