using WebStoreProduct.Application.Common;
using WebStoreProduct.Application.DTOs;
using WebStoreProduct.Application.Models;
using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Application.Interfaces.Services;

public interface IProductService
{
    Task<Result<PaginatedList<ProductDto>>> GetProductsAsync(int page, int size);
    Task<Result<PaginatedList<ProductDto>>> GetProductsByCategoryAsync(uint categoryId, int page, int size);
    Task<Result<Product>> GetDetailedProductByIdAsync(uint id);
}
