using Distributors.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Distributors.Infrastructure.Data.Configurations;
internal class IdentityDocumentConfiguration : BaseConfiguration<IdentityDocumentEntity>
{
    public override void Configure(EntityTypeBuilder<IdentityDocumentEntity> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.DocumentNo).HasMaxLength(10);
        builder.Property(x => x.SerialNo).HasMaxLength(10);
        builder.Property(x => x.PersonalNo).HasMaxLength(50);
        builder.Property(x => x.IssuingAuthority).HasMaxLength(100);
    }
}
