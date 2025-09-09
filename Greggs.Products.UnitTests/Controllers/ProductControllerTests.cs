using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Dto;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests.Controllers;

public class ProductControllerTests
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    
    public ProductControllerTests()
    {
        var productDataAccess = new Mock<IDataAccess<Product>>();
        productDataAccess.Setup(m => m.List(
            It.Is<int?>(i => Nullable.Equals(i, 0)),
            It.Is<int?>(i => Nullable.Equals(i, 2))
        )).Returns(new List<Product>()
        {
            new() { Name = "A", PriceInPounds = 1m },
            new() { Name = "B", PriceInPounds = 1.5m },
        });
        
        _logger = Mock.Of<ILogger<ProductController>>();
        _productService = new ProductService(new CurrencyConversionService(), productDataAccess.Object);
    }
    
    [Fact]
    public void TestControllerResponse()
    {
        var controller = new ProductController(_logger, _productService);
        var response = controller.Get(0, 2);
        
        var ok = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

        var body = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(ok.Value);
        Assert.Equal(2, body.Count());
    }
}