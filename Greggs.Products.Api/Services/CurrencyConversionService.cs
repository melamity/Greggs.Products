using System;
using Greggs.Products.Api.Enums;

namespace Greggs.Products.Api.Services;

/// <summary>
/// Reviewer note: There's a couple ways we could make this nicer:
///  - We really probably ought to get the currency conversion information from a third-party.
///  - Where we do the above, we should cache it and make this class a singleton so that this remains in-memory cached
/// through the life of the application.
///  - Where we also do the above, the currency conversion stuff should be done using async functions where possible.
/// </summary>
public class CurrencyConversionService : ICurrencyConversionService
{
    const decimal GbpToEur = 1.1m;
    
    public decimal Convert(Currency from, Currency to, decimal amount)
    {
        if (from == Currency.Gbp && to == Currency.Eur)
        {
            return amount * GbpToEur;
        }

        throw new NotImplementedException();
    }
}