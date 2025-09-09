using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    private readonly ILogger<ProductController> _logger;
    
    public ProductController(ILogger<ProductController> logger, IProductService products)
    {
        _logger = logger;
        _productService = products;
    }

    [HttpGet]
    public IActionResult Get(int pageStart = 0, int pageSize = 5)
    {
        // I'd catch the NotImplException usually, but since that's impossible here, and it's a tech demo, I am not and
        // just putting it here that I am aware of it.
        return Ok(_productService.GetProducts(pageStart, pageSize));
    }
}