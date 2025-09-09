namespace Greggs.Products.Api.Dto;

/// <summary>
/// Reviewer note: What I'd probably want to see is for us to keep using PriceInPounds in the interim, while we move to
/// a Price object that then does the currency converted amounts on the inside, something akin to:
/// {
///     "name": "Vegan Sausage Roll",
///     "prices": [
///         {
///             "currency": "GBP",
///             "price": 1.1
///         },
///         {
///             "currency": "EUR",
///             "price": 1.21
///         }
///     ]
/// }
///
/// That said, since this is just to gauge my thinking technically, I am going to add PriceInEuros, but I would advocate
/// for the above so that we can expand nicely in the future, but I'd also understand if that was deemed unneccesary.
/// </summary>
public class ProductDto
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
    public decimal PriceInEuros { get; set; }
}