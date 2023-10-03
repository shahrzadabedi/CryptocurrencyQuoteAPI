using CryptocurrencyQuote.Domain.Model;

namespace CryptocurrencyQuote.Domain;

public interface IConfigurationAPI
{
    CryptocurrencyAPIConfig Get();
}
