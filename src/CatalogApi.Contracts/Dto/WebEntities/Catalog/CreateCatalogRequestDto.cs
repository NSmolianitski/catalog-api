using System.ComponentModel.DataAnnotations;

namespace CatalogApi.Contracts.Dto.WebEntities.Catalog;

public record CreateCatalogRequestDto(
    [MinLength(1, ErrorMessage = "Name must be at least one character long.")]
    [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long.")]
    string Name,
    
    [Range(1, long.MaxValue, ErrorMessage = "ParentId must be greater than zero if provided.")]
    long? ParentId
);