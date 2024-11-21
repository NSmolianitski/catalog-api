using CatalogApi.Contracts.Dto.WebEntities.Catalog;

namespace CatalogApi.Contracts.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogResponseDto>> GetAllAsync();
    Task<CatalogResponseDto> GetByIdAsync(long id);
    Task<IEnumerable<CatalogResponseDto>> GetChildrenAsync(long parentId);
    Task<CatalogResponseDto> AddAsync(CreateCatalogRequestDto catalog);
    Task<CatalogResponseDto> UpdateAsync(long id, UpdateCatalogRequestDto requestDto);
    Task DeleteAsync(long id);
}