using CatalogApi.Contracts.Dto.DomainEntities;
using CatalogApi.Contracts.Repositories;
using CatalogApi.Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data.Repositories;

public class CatalogRepository(AppDbContext context) : ICatalogRepository
{
    public async Task<IEnumerable<CatalogDto>> GetAllAsync()
    {
        var catalogs = await context.Catalogs
            .AsNoTracking()
            .Include(c => c.Children)
            .ToListAsync();

        return catalogs.Select(c => c.ToCatalogDto());
    }

    public async Task<CatalogDto?> TryGetByIdAsync(long id)
    {
        var catalog = await context.Catalogs
            .AsNoTracking()
            .Include(c => c.Children)
            .FirstOrDefaultAsync(c => c.Id == id);

        return catalog?.ToCatalogDto();
    }

    public async Task<IEnumerable<CatalogDto>> GetChildrenAsync(long parentId)
    {
        var parent = await context.Catalogs.Where(c => c.Id == parentId)
            .AsNoTracking()
            .Include(c => c.Children)
            .FirstOrDefaultAsync();

        return parent?.Children.Select(c => c.ToCatalogDto())
               ?? Enumerable.Empty<CatalogDto>();
    }

    public async Task<CatalogDto> AddAsync(CatalogDto catalogDto)
    {
        var catalog = catalogDto.ToCatalog();
        catalog.CreatedAt = DateTime.UtcNow;
        catalog.UpdatedAt = DateTime.UtcNow;

        context.Catalogs.Add(catalog);
        await context.SaveChangesAsync();

        return catalog.ToCatalogDto();
    }

    public async Task<CatalogDto> UpdateAsync(CatalogDto catalogDto)
    {
        var catalog = catalogDto.ToCatalog();
        catalog.UpdatedAt = DateTime.UtcNow;

        context.Catalogs.Update(catalog);
        await context.SaveChangesAsync();

        return catalog.ToCatalogDto();
    }

    public async Task DeleteAsync(long id)
    {
        await context.Catalogs
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> CatalogExistsAsync(string name, long? parentId)
    {
        return await context.Catalogs
            .AnyAsync(c => c.Name == name && c.ParentId == parentId);
    }

    public async Task<bool> IsCyclicDependency(long parentId, long currentCatalogId)
    {
        var parent = await context.Catalogs
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == parentId);

        if (parent is null)
            return false;

        while (parent.ParentId.HasValue)
        {
            if (parent.ParentId == currentCatalogId)
                return true;

            parent = await context.Catalogs
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == parent.ParentId.Value);
        }

        return false;
    }
}