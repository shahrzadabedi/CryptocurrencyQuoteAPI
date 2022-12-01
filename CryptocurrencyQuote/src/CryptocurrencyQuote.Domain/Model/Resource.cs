using Newtonsoft.Json;

namespace CryptocurrencyQuote.Domain.Model
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        public string Href { get; set; }
    }
}