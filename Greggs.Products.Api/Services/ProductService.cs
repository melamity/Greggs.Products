using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Dto;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services;

public class ProductService : IProductService
{
    private readonly ICurrencyConversionService _currencyConversionService;
    
    private readonly IDataAccess<Product> _productDataAccess;
    
    public ProductService(ICurrencyConversionService currencyConversionService, IDataAccess<Product> productDataAccess)
    {
        _currencyConversionService = currencyConversionService;
        _productDataAccess = productDataAccess;
    }
    
    public IEnumerable<ProductDto> GetProducts(int? pageStart, int? pageSize)
    {
        // Pagination logic not needed here - data layer is handling it.
    
        return _productDataAccess.List(pageStart, pageSize).Select(p => new ProductDto {
            Name = p.Name,
            PriceInPounds = p.PriceInPounds,
            PriceInEuros = _currencyConversionService.Convert(Currency.Gbp, Currency.Eur, p.PriceInPounds)
        });
    }
}