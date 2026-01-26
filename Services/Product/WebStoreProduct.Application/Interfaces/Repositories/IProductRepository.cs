using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(int page, int size);
    Task<List<Product>> GetProductsByCategoryAsync(uint categoryId, int page, int size);
    Task<Product> GetProductByIdAsync(uint id);
}
