using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure;

public interface ICryptocurrencyAPI
{
    Task<List<ExchangeRateDto>> GetQuotesAsync(CurrencyDto fromCurrency, List<CurrencyDto> toCurrencies);
}
