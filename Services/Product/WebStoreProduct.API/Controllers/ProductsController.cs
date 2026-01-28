using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStoreProduct.Application.Interfaces.Services;

namespace WebStoreProduct.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 12)
    {
        var productsResult = await productService.GetProductsAsync(page, size);
        if (!productsResult.IsSuccess)
        {
            return BadRequest(productsResult.Error);
        }

        return Ok(productsResult.Value);
    }

    [HttpGet("v2")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllByCategory([FromQuery] uint categoryId, [FromQuery] int page = 1, [FromQuery] int size = 12)
    {
        var productsResult = await productService.GetProductsByCategoryAsync(categoryId, page, size);
        if (!productsResult.IsSuccess)
        {
            return BadRequest(productsResult.Error);
        }

        return Ok(productsResult.Value);
    }
}
