using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace CryptocurrencyQuote.Infrastructure.ExchangeRates;

public class ExchangeRateConfiguration : IConfigurationAPI
{
    private IConfiguration _configuration;
    public ExchangeRateConfiguration(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public CryptocurrencyAPIConfig Get()
    {
        var configList = _configuration.GetSection("CryptocurrencyAPIList")
            .Get<List<CryptocurrencyAPIConfig>>()
            .ToList();
        return configList.Any()?configList.FirstOrDefault(p => p.Name == "ExchangeRates"):null;
    }
}
