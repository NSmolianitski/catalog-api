using CatalogApi.Contracts.Dto.DomainEntities;

namespace CatalogApi.Contracts.Repositories;

public interface ICatalogRepository
{
    Task<IEnumerable<CatalogDto>> GetAllAsync();
    Task<CatalogDto?> TryGetByIdAsync(long id);
    Task<IEnumerable<CatalogDto>> GetChildrenAsync(long parentId);
    Task<CatalogDto> AddAsync(CatalogDto catalogDto);
    Task<CatalogDto> UpdateAsync(CatalogDto catalogDto);
    Task DeleteAsync(long id);
    Task<bool> CatalogExistsAsync(string name, long? parentId);
    Task<bool> IsCyclicDependency(long parentId, long currentCatalogId);
}