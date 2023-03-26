using Distributors.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Distributors.Api.StartUp;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DistributorsDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b=>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("Distributors.Infrastructure");
            });
        });
        return services;
    }
}