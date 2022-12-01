using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using Microsoft.AspNetCore.Http;
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
        [HttpGet(Name = nameof(GetQuotes))]
        [ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]

        public async Task<IActionResult> GetQuotes(string fromCurrency, string toCurrencies)
        {
            try
            {
                var toCurrenciesDto = toCurrencies.Split(',').Select(c => new CurrencyDTO() { Symbol = c }).ToList();
                var quotes = await api.GetQuotes(new CurrencyDTO() { Symbol = fromCurrency }, toCurrenciesDto);
                quotes.ForEach(p => p.Href = Url.Link(nameof(GetQuotes), new { fromCurrency, toCurrencies }));
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message); ;
            }
            
        }

        
    }
}
