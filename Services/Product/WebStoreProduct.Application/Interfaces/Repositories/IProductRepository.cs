using WebStoreProduct.Application.Models;
using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<PaginatedList<Product>?> GetProductsAsync(int page, int size);
    Task<PaginatedList<Product>?> GetProductsByCategoryAsync(uint categoryId, int page, int size);
    Task<Product?> GetProductByIdAsync(uint id);
}
