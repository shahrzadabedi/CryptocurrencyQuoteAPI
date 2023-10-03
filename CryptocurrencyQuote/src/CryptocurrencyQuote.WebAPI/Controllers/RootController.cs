using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Controllers;

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
            CryptoQuotes = new
            {
                Href = Url.Link(nameof(CryptoQuotesController.Get),null),
            }
        };

        return Ok(response);
    }
}

