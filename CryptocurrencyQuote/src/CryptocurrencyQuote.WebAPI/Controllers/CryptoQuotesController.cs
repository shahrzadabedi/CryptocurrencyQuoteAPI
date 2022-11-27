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
        private ICryptocurrencyAPI cryptocurrencyApi;
        public CryptoQuotesController(ICryptocurrencyAPI cryptocurrencyApi)
        {
            this.cryptocurrencyApi = cryptocurrencyApi;
        }
        [HttpGet(Name = "GetCryptoQuotes")]
        public async Task<IActionResult> GetQuotes(string fromCurrency)
        {
            throw new NotImplementedException();
//            cryptocurrencyApi.GetQuotes(fromCurrency, new List<CurrencyDTO>);
        }
    }
}
