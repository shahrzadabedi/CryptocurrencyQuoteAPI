using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Domain

{
    public interface ICryptocurrencyAPI
    {
        Task<List<ExchangeRateDTO>> GetQuotes(CurrencyDTO fromCurrency,List<CurrencyDTO> toCurrencies);
        Task<List<CurrencyDTO>> GetSymbols();
    }
}