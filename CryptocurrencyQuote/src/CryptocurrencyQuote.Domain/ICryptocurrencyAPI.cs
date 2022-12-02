using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Domain

{
    public interface ICryptocurrencyAPI
    {
        Task<List<ExchangeRateDTO>> GetQuotesAsync(CurrencyDTO fromCurrency,List<CurrencyDTO> toCurrencies);
        Task<List<CurrencyDTO>> GetSymbolsAsync();
    }
}