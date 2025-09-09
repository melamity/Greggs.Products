using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests.Services;

public class ProductServiceTests
{
    private readonly ICurrencyConversionService _currencyConversionService = new CurrencyConversionService();
    private readonly IProductService _productService;

    public ProductServiceTests()
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
        
        productDataAccess.Setup(m => m.List(
            It.Is<int?>(i => Nullable.Equals(i, 1)),
            It.Is<int?>(i => Nullable.Equals(i, 3))
        )).Returns(new List<Product>()
        {
            new() { Name = "B", PriceInPounds = 1.5m },
            new() { Name = "C", PriceInPounds = 2m },
        });

        _productService = new ProductService(
            _currencyConversionService,
            productDataAccess.Object
        );
    }

    [Theory]
    [InlineData(0, 2, 2, "A", "B")]
    [InlineData(1, 3, 2, "B", "C")]
    public void TestPagination(int pageStart, int pageSize, int expected, string firstItemName, string secondItemName)
    {
        var result = _productService.GetProducts(pageStart, pageSize).ToList();
        
        Assert.Equal(expected, result.Count);
        Assert.Equal(firstItemName, result.First().Name);
        Assert.Equal(secondItemName, result.Last().Name);
    }

    [Fact]
    public void TestCurrencyConversion()
    {
        var result = _productService.GetProducts(0, 2).ToList();

        result.ForEach(i => Assert.Equal(i.PriceInPounds * 1.1m, i.PriceInEuros));
    }
}