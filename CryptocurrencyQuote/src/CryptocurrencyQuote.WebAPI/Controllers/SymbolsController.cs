using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using CryptocurrencyQuote.WebAPI.Models;
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
        [HttpGet(Name = nameof(GetSymbolsAsync))]        
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetSymbolsAsync()
        {
            try
            {
                var symbols = await api.GetSymbolsAsync();
                symbols.ForEach(p => p.Href = Url.Link(nameof(GetSymbolsAsync),null));
                var response = new ListResponse<CurrencyDTO>() { Data = symbols, Success = true };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); ;
            }

        }
    }
}
