namespace CryptocurrencyQuote.Infrastructure.Models;

public class ExchangeRateDto
{
    public decimal Price { get; set; }

    public CurrencyDto Currency { get; set; }
}
