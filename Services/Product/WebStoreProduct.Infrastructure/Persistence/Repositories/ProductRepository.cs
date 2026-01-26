using Microsoft.EntityFrameworkCore;
using WebStoreProduct.Application.Interfaces.Repositories;
using WebStoreProduct.Application.Models;
using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(uint id)
        => await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PaginatedList<Product>?> GetProductsAsync(int page, int size)
    {
        var products = await dbContext.Products
            .Skip((page - 1) * size)
            .Take(size)
            .ToArrayAsync();

        var totalPages = (int)Math.Ceiling(await dbContext.Products.CountAsync() / (double)size);

        return new PaginatedList<Product>(
            products,
            page,
            totalPages
        );
    }

    public async Task<PaginatedList<Product>?> GetProductsByCategoryAsync(uint categoryId, int page, int size)
    {
        var products = await dbContext.Products
            .Where(p => p.CategoryId == categoryId)
            .Skip((page - 1) * size)
            .Take(size)
            .ToArrayAsync();

        var totalPages = (int)Math.Ceiling(await dbContext.Products.Where(p => p.CategoryId == categoryId).CountAsync() / (double)size);

        return new PaginatedList<Product>(
            products,
            page,
            totalPages
        );
    }
}
