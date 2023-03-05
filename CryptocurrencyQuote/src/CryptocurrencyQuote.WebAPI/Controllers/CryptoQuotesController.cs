using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using CryptocurrencyQuote.WebAPI.Filters;
using CryptocurrencyQuote.WebAPI.Models;
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
        [HttpGet(Name = nameof(GetQuotesAsync))]
        [ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        [CustomExceptionFilter]
        public async Task<IActionResult> GetQuotesAsync(string fromCurrency, string toCurrencies)
        {
            var toCurrenciesDto = toCurrencies.Split(',').Select(c => new CurrencyDTO() { Symbol = c }).ToList();
            var quotes = await api.GetQuotesAsync(new CurrencyDTO() { Symbol = fromCurrency }, toCurrenciesDto);
            quotes.ForEach(p => p.Href = Url.Link(nameof(GetQuotesAsync), new { fromCurrency, toCurrencies }));
            var response = new ListResponse<ExchangeRateDTO>() { Data = quotes, Success = true };
            return Ok(response);

        }

    }



}
