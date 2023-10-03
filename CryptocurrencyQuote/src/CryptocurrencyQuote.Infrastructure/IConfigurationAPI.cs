using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure;

public interface IConfigurationAPI
{
    CryptocurrencyAPIConfig Get();
}
