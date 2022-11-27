namespace CryptocurrencyQuote.Domain.Model
{
    public class ExchangeRateDTO
    {
        public decimal Price { get; set; }
       public CurrencyDTO Currency { get; set; }
    }
}
