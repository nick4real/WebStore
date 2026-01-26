using WebStoreProduct.Domain.Entities;

namespace WebStoreProduct.Application.DTOs;

public record ProductDto(
    uint Id,
    string Title,
    string Description,
    uint CategoryId,
    Category Category,
    DateTime CreatedAt,
    decimal Price,
    int StockQuantity,
    List<ImageLink> Images);