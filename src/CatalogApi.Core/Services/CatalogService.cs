using CatalogApi.Contracts.Dto.WebEntities.Catalog;
using CatalogApi.Contracts.Repositories;
using CatalogApi.Contracts.Services;
using CatalogApi.Core.Exceptions;
using CatalogApi.Core.Mappers;

namespace CatalogApi.Core.Services;

public class CatalogService(ICatalogRepository catalogRepository) : ICatalogService
{
    public async Task<IEnumerable<CatalogResponseDto>> GetAllAsync()
    {
        var catalogs = await catalogRepository.GetAllAsync();
        return catalogs.Select(c => c.ToCatalogResponseDto());
    }

    public async Task<CatalogResponseDto> GetByIdAsync(long id)
    {
        var catalog = await catalogRepository.TryGetByIdAsync(id);
        if (catalog is null)
            throw new NotFoundException($"Catalog with {id} not found.");

        return catalog.ToCatalogResponseDto();
    }

    public async Task<IEnumerable<CatalogResponseDto>> GetChildrenAsync(long parentId)
    {
        var children = await catalogRepository.GetChildrenAsync(parentId);
        return children.Select(c => c.ToCatalogResponseDto());
    }

    public async Task<CatalogResponseDto> AddAsync(CreateCatalogRequestDto catalogDto)
    {
        if (catalogDto.ParentId != null)
        {
            var parent = await catalogRepository.TryGetByIdAsync(catalogDto.ParentId.Value);
            if (parent is null)
                throw new NotFoundException($"Parent catalog with {catalogDto.ParentId} not found.");
        }

        var catalog = catalogDto.ToCatalogDto();
        if (await catalogRepository.CatalogExistsAsync(catalog.Name, catalog.ParentId))
            throw new ConflictException(
                $"Catalog with name {catalog.Name} and parent id {catalog.ParentId} already exists.");

        catalog = await catalogRepository.AddAsync(catalog);
        return catalog.ToCatalogResponseDto();
    }

    public async Task<CatalogResponseDto> UpdateAsync(long id, UpdateCatalogRequestDto requestDto)
    {
        var catalogToUpdate = await catalogRepository.TryGetByIdAsync(id);
        if (catalogToUpdate is null)
            throw new NotFoundException($"Catalog with {id} not found.");

        if (!string.IsNullOrEmpty(requestDto.Name))
        {
            if (await catalogRepository.CatalogExistsAsync(requestDto.Name, catalogToUpdate.ParentId))
            {
                throw new ConflictException(
                    $"Catalog with name {catalogToUpdate.Name} and parent id {catalogToUpdate.ParentId} already exists.");
            }

            catalogToUpdate.Name = requestDto.Name;
        }

        if (requestDto.ParentId.HasValue)
        {
            if (await catalogRepository.IsCyclicDependency(requestDto.ParentId.Value, id))
            {
                throw new ConflictException(
                    $"Cyclic dependency detected for catalog id: {id} and it's new parent id: {requestDto.ParentId.Value}.");
            }
        }
        
        catalogToUpdate.ParentId = requestDto.ParentId;

        catalogToUpdate = await catalogRepository.UpdateAsync(catalogToUpdate);
        return catalogToUpdate.ToCatalogResponseDto();
    }

    public async Task DeleteAsync(long id)
    {
        await catalogRepository.DeleteAsync(id);
    }
}