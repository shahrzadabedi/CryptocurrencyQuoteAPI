using CryptocurrencyQuote.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolsController : ControllerBase
    {
        private readonly ICryptocurrencyAPI api;
        public SymbolsController(ICryptocurrencyAPI cryptocurrencyApi)
        {
            api = cryptocurrencyApi;
        }
        [HttpGet(Name = nameof(GetSymbols))]        
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetSymbols()
        {
            try
            {
                var symbols = await api.GetSymbols();
                symbols.ForEach(p => p.Href = Url.Link(nameof(GetSymbols),null));
                return Ok(symbols);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); ;
            }

        }
    }
}
