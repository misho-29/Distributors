using Distributors.Core.Entities;
using Distributors.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Distributors.Infrastructure.Data;
public class DistributorsDbContext : DbContext
{
    public DistributorsDbContext(DbContextOptions<DistributorsDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new DistributorConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new SaleConfiguration());
        builder.ApplyConfiguration(new IdentityDocumentConfiguration());

        foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
        base.ConfigureConventions(configurationBuilder);
    }

    public DbSet<DistributorEntity> Distributors { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<SaleEntity> Sales { get; set; } = null!;
    public DbSet<IdentityDocumentEntity> IdentityDocuments { get; set; } = null!;
}
