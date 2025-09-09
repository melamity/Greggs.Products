using System;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Services;
using Xunit;

namespace Greggs.Products.UnitTests.Services;

public class CurrencyConversionServiceTests
{
    private readonly ICurrencyConversionService _currencyConversionService = new CurrencyConversionService();

    [Theory]
    [InlineData(Currency.Gbp, Currency.Eur, 1, 1.1)]
    public void ConvertValidCurrencyPairs(Currency from, Currency to, decimal amount, decimal expected)
    {
        var result = _currencyConversionService.Convert(from, to, amount);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Currency.Eur, Currency.Gbp, 1.1)]
    public void ConvertInvalidCurrencyPairs(Currency from, Currency to, decimal amount)
    {
        Assert.Throws<NotImplementedException>(() => _currencyConversionService.Convert(from, to, amount));
    }
}