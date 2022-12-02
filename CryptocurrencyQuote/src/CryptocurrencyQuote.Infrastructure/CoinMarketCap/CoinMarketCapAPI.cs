using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Infrastructure
{
    public class CoinMarketCapAPI : ICryptocurrencyAPI
    {
        public Task<List<ExchangeRateDTO>> GetQuotesAsync(CurrencyDTO fromCurrency, List<CurrencyDTO> toCurrencies)
        {
            throw new NotImplementedException();
        }

        public Task<List<CurrencyDTO>> GetSymbolsAsync()
        {
            throw new NotImplementedException();
        }
    }
}