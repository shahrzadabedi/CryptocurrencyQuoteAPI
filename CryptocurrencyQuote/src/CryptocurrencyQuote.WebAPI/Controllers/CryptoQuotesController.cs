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
        private ICryptocurrencyAPI api;
        public CryptoQuotesController(ICryptocurrencyAPI cryptocurrencyApi)
        {
            this.api = cryptocurrencyApi;
        }
        [HttpGet(Name = "GetCryptoQuotes")]
        public async Task<IActionResult> GetQuotes([FromQuery]string fromCurrency)
        {
            //var quotes = await api.GetQuotes(new Domain.Model.CurrencyDTO() { IsCrypto = true, Symbol = symbol }, toCurrencies);
            return Ok();
        }
    }
}
