using Microsoft.EntityFrameworkCore;
using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ImageLink> ImageLinks { get; set; }
}
