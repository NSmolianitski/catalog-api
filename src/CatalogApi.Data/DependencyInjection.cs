using CatalogApi.Contracts.Repositories;
using CatalogApi.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogApi.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        return services;
    }
}