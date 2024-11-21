using CatalogApi.Core;
using CatalogApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Web;

public static class AppBuilderExtensions
{
    public static WebApplicationBuilder AddCatalogApi(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        builder.Services.AddCoreLayer();
        builder.Services.AddDataLayer();
        
        return builder;
    }
}