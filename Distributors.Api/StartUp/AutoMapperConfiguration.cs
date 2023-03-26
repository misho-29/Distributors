using AutoMapper;
using Distributors.Application.Mappings;

namespace Distributors.Api.StartUp;

public static class AutoMapperConfiguration
{
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DistributorProfile());
            mc.AddProfile(new IdentityDocumentProfile());
            mc.AddProfile(new ProductProfile());
            mc.AddProfile(new SaleProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
