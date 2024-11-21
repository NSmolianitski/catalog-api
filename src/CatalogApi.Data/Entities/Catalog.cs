namespace CatalogApi.Data.Entities;

public class Catalog
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    
    public Catalog? Parent { get; set; }
    public ICollection<Catalog> Children { get; set; } = [];
}