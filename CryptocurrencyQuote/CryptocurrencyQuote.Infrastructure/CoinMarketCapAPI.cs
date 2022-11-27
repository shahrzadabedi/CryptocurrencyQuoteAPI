using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Infrastructure
{
    public class CoinMarketCapAPI : ICryptocurrencyAPI
    {
        public Task<List<ExchangeRateDTO>> GetQuotes(CurrencyDTO fromCurrency, List<CurrencyDTO> toCurrencies)
        {
            throw new NotImplementedException();
        }
    }
}