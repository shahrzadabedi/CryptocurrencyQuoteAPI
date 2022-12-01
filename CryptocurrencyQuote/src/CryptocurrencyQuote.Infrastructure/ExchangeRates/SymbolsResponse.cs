namespace CryptocurrencyQuote.Infrastructure.ExchangeRates
{
    public class SymbolsResponse
    {
        public bool Success { set; get; }
        public Dictionary<string,string> Symbols { get; set; }
    }


}
