namespace CatalogApi.Contracts.Dto.DomainEntities;

public class CatalogDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public long? ParentId { get; set; }
}