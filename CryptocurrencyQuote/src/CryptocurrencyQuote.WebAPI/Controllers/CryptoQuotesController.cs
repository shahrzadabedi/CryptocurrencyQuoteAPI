using CryptocurrencyQuote.Application.CryptoQuote.Dtos;
using CryptocurrencyQuote.Application.CryptoQuote.Queries;
using CryptocurrencyQuote.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Controllers;

public class CryptoQuotesController : BaseController
{
    [HttpGet(Name = nameof(Get))]

    public async Task<ActionResult<List<GetCryptoQuotesListResponse>>> Get([FromQuery] string fromCurrency,
        string toCurrencies)
    {
        try
        {
            var toCurrenciesList = toCurrencies?.Split(',').ToList();

            var query = new GetCryotoQuotesQuery(fromCurrency, toCurrenciesList);

            var result = await Mediator.Send(query);

            if (result.Data != null)
                MakeUrlLink(result.Data, fromCurrency);

            return result.ToActionResult();
        }
        catch (Exception ex)
        {
            return StatusCode((int)((StatusCodeHelper)ex).statusCode, ex.Message);                 
        }            
    } 
    
    private void MakeUrlLink(List<GetCryptoQuotesListResponse> data, string fromCurrency)
    {
        data.ForEach(p => p.Href = Url.Link(nameof(Get), new { fromCurrency, p.Currency.Symbol }));
    }
}
