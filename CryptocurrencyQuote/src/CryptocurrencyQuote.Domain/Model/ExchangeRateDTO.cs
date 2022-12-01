namespace CryptocurrencyQuote.Domain.Model
{
    public class ExchangeRateDTO : Resource
    {
        public decimal Price { get; set; }
       public CurrencyDTO Currency { get; set; }
    }
}
