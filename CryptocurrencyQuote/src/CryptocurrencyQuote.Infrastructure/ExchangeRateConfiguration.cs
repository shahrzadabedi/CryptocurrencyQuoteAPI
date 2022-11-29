using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Infrastructure
{
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
}
