using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Infrastructure
{
    public class ExchangeRatesAPI : ICryptocurrencyAPI
    {
        private CryptocurrencyAPIConfig _apiConfig;
        private readonly IConfiguration _configuration;
        public ExchangeRatesAPI(IConfiguration configuration)
        {
            this._configuration = configuration;
            var cryptoCurrencyAPIConfigs = _configuration.GetSection("CryptocurrencyAPIList")
                .Get<List<CryptocurrencyAPIConfig>>()
                .ToList();
            var exchangeRates = cryptoCurrencyAPIConfigs.Where(p => p.Name == "ExchangeRates").FirstOrDefault();
            _apiConfig = new CryptocurrencyAPIConfig() { 
                BaseUrl = exchangeRates.BaseUrl,
                APIKey = exchangeRates.APIKey,
                Name = exchangeRates.Name
            };

        }
        public Task<List<ExchangeRateDTO>> GetQuotes(CurrencyDTO fromCurrency, List<CurrencyDTO> toCurrencies)
        {
            throw new NotImplementedException();
        }
    }
}
