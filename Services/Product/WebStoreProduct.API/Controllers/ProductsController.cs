using Microsoft.AspNetCore.Mvc;
using WebStoreProduct.Application.Interfaces.Services;

namespace WebStoreProduct.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute] int page = 1, [FromRoute] int size = 12)
    {
        var productsResult = await productService.GetProductsAsync(page, size);
        if (!productsResult.IsSuccess)
        {
            return BadRequest(productsResult.Error);
        }

        return Ok(productsResult.Value);
    }
    [HttpGet("v2")]
    public async Task<IActionResult> GetAllByCategory([FromRoute] uint categoryId, [FromRoute] int page = 1, [FromRoute] int size = 12)
    {
        var productsResult = await productService.GetProductsByCategoryAsync(categoryId, page, size);
        if (!productsResult.IsSuccess)
        {
            return BadRequest(productsResult.Error);
        }

        return Ok(productsResult.Value);
    }
}
