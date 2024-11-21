using CatalogApi.Contracts.Dto.DomainEntities;
using CatalogApi.Contracts.Dto.WebEntities.Catalog;

namespace CatalogApi.Core.Mappers;

public static class CatalogMapper
{
    public static CatalogDto ToCatalogDto(this CreateCatalogRequestDto catalogResponseDto)
    {
        return new CatalogDto
        {
            Name = catalogResponseDto.Name,
            ParentId = catalogResponseDto.ParentId
        };
    }
    
    public static CatalogResponseDto ToCatalogResponseDto(this CatalogDto catalog)
    {
        return new CatalogResponseDto
        (
            catalog.Id,
            catalog.CreatedAt,
            catalog.UpdatedAt,
            catalog.Name,
            catalog.ParentId
        );
    }
}