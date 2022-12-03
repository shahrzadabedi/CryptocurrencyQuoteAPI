using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using CryptocurrencyQuote.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoQuotesController : ControllerBase
    {
        private readonly ICryptocurrencyAPI api;
        public CryptoQuotesController(ICryptocurrencyAPI cryptocurrencyApi)
        {
            api = cryptocurrencyApi;
        }
        [HttpGet(Name = nameof(GetQuotesAsync))]
        [ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.TooManyRequests)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.Unauthorized)]

        public async Task<IActionResult> GetQuotesAsync(string fromCurrency, string toCurrencies)
        {
            try
            {
                var toCurrenciesDto = toCurrencies?.Split(',').Select(c => new CurrencyDTO() { Symbol = c }).ToList();
                var quotes = await api.GetQuotesAsync(fromCurrency==null ? null:new CurrencyDTO() { Symbol = fromCurrency }, toCurrenciesDto?? new List<CurrencyDTO>());
                quotes.ForEach(p => p.Href = Url.Link(nameof(GetQuotesAsync), new { fromCurrency, toCurrencies }));
                var response = new ListResponse<ExchangeRateDTO>() { Data = quotes, Success = true };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)((StatusCodeHelper)ex).statusCode, ex.Message);
                    
            }
            
        }


        
    }
}
