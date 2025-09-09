using System.Collections.Generic;
using Greggs.Products.Api.Dto;

namespace Greggs.Products.Api.Services;

public interface IProductService
{
    public IEnumerable<ProductDto> GetProducts(int? pageStart, int? pageSize);
}