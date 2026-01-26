using WebStoreProduct.Application.DTOs;
using WebStoreProduct.Application.Models;

namespace WebStoreProduct.Application.Interfaces.Services;

public interface IProductService
{
    Task<PaginatedList<ProductDto>> GetProductsAsync(int page, int size);
    Task<PaginatedList<ProductDto>> GetProductsByCategoryAsync(uint categoryId, int page, int size);
    Task<ProductDto> GetDetailedProductByIdAsync(uint id);
}
