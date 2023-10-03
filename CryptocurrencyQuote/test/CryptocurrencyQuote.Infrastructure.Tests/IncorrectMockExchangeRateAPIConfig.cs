using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Infrastructure.Tests;

public class IncorrectMockExchangeRateAPIConfig: IConfigurationAPI
{
    public CryptocurrencyAPIConfig Get()
    {
        return
            new CryptocurrencyAPIConfig()
            {
                BaseUrl = "https://api.apilayer.com/exchangerates_data",
                APIKey = "V7iXzah9qrNfseBoDfEaUVdKR5DU2t",
                Name = "ExchangeRates"

            };
    }
}
