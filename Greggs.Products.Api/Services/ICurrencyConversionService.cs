using Greggs.Products.Api.Enums;

namespace Greggs.Products.Api.Services;

public interface ICurrencyConversionService
{
    public decimal Convert(Currency from, Currency to, decimal amount);
}