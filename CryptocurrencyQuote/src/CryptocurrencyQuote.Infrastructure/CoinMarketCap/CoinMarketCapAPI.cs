using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure;

public class CoinMarketCapAPI : ICryptocurrencyAPI
{
    public Task<List<ExchangeRateDto>> GetQuotesAsync(CurrencyDto fromCurrency, List<CurrencyDto> toCurrencies)
    {
        throw new NotImplementedException();
    }
}