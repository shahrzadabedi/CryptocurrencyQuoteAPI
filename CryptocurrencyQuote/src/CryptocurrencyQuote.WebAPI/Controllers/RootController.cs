using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name =nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                Href = Url.Link(nameof(GetRoot), null),
                Symbols = new 
                {
                    Href = Url.Link(nameof(SymbolsController.GetSymbolsAsync),null)
                    
                },
                CryptoQuotes = new
                {
                    Href = Url.Link(nameof(CryptoQuotesController.GetQuotesAsync),null),
                }
                
            };
            return Ok(response);
        }
    }
}
