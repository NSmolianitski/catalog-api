using CatalogApi.Contracts.Dto.DomainEntities;
using CatalogApi.Data.Entities;

namespace CatalogApi.Data.Mappers;

public static class CatalogMapper
{
    public static Catalog ToCatalog(this CatalogDto catalogDto)
    {
        return new Catalog
        {
            Id = catalogDto.Id,
            Name = catalogDto.Name,
            ParentId = catalogDto.ParentId
        };
    }

    public static CatalogDto ToCatalogDto(this Catalog catalog)
    {
        return new CatalogDto
        {
            Id = catalog.Id,
            CreatedAt = catalog.CreatedAt,
            UpdatedAt = catalog.UpdatedAt,
            Name = catalog.Name,
            ParentId = catalog.ParentId
        };
    }
}