using CatalogApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Catalog> Catalogs { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalog>()
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Catalog>()
            .HasData([
                new Catalog {Id = 1, Name = "_.1", ParentId = null},
                new Catalog {Id = 2, Name = "1.2", ParentId = 1},
                new Catalog {Id = 3, Name = "1.3", ParentId = 1},
                new Catalog {Id = 4, Name = "1.2.4", ParentId = 2},
                new Catalog {Id = 5, Name = "_.5", ParentId = null},
                new Catalog {Id = 6, Name = "5.6", ParentId = 5},
            ]);
    }
}