namespace CatalogApi.Contracts.Dto.WebEntities.Catalog;

public record CatalogResponseDto(long Id,  DateTime CreatedAt, DateTime UpdatedAt, string Name, long? ParentId);