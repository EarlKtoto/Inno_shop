using Inno_Shop.Domain.Entities;
namespace Inno_Shop.Api.Controllers;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(Product product)
    {
        await _productService.AddProduct(product);
        return Ok("Product added");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _productService.GetProductInformation(id));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _productService.GetAllProductsInformationAsync());
}
