using MapsterMapper;
using WebStoreProduct.Application.Common;
using WebStoreProduct.Application.DTOs;
using WebStoreProduct.Application.Interfaces.Repositories;
using WebStoreProduct.Application.Interfaces.Services;
using WebStoreProduct.Application.Models;
using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Application.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
    public async Task<Result<Product>> GetDetailedProductByIdAsync(uint id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        if (product is null)
        {
            return Result<Product>.Failure(new Error(ErrorCode.NotFound, $"Product with ID {id} was not found."));
        }

        return Result<Product>.Success(product);
    }

    public async Task<Result<PaginatedList<ProductDto>>> GetProductsAsync(int page, int size)
    {
        var products = await productRepository.GetProductsAsync(page, size);
        if (products is null)
        {
            return Result<PaginatedList<ProductDto>>.Failure(new Error(ErrorCode.NotFound, "No products were found."));
        }

        return Result<PaginatedList<ProductDto>>.Success(mapper.Map<PaginatedList<ProductDto>>(products));
    }

    public async Task<Result<PaginatedList<ProductDto>>> GetProductsByCategoryAsync(uint categoryId, int page, int size)
    {
        var products = await productRepository.GetProductsByCategoryAsync(categoryId, page, size);
        if (products is null)
        {
            return Result<PaginatedList<ProductDto>>.Failure(new Error(ErrorCode.NotFound, "No products were found."));
        }

        return Result<PaginatedList<ProductDto>>.Success(mapper.Map<PaginatedList<ProductDto>>(products));
    }
}
