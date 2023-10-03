using Newtonsoft.Json;

namespace CryptocurrencyQuote.Application.Models;

public abstract class Resource
{
    [JsonProperty(Order = -2)]
    public string? Href { get; set; }
}
