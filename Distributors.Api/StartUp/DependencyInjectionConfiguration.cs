using Distributors.Application.Services;
using Distributors.Core;
using Distributors.Infrastructure;

namespace Distributors.Api.StartUp;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IDistributorService, DistributorService>();
        services.AddTransient<IBonusService, BonusService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ISaleService, SaleService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
