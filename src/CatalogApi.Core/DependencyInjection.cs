using CatalogApi.Contracts.Services;
using CatalogApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApi.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<ICatalogService, CatalogService>();
        return services;
    }
}