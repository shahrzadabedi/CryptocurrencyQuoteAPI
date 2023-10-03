using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure.Tests;

public class MockExchangeRateAPIConfig : IConfigurationAPI
{
    public CryptocurrencyAPIConfig Get()
    {
        return
            new CryptocurrencyAPIConfig()
            {
                BaseUrl = "https://api.apilayer.com/exchangerates_data",
                APIKey = "V7iXzah9qrNfseBoDfEaUVdKR5DU2tAJ",
                Name = "ExchangeRates"

            };
    }
}
