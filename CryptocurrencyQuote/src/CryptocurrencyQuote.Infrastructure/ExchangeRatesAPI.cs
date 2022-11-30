using CryptocurrencyQuote.Domain;
using CryptocurrencyQuote.Domain.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CryptocurrencyQuote.Domain.Model.Exceptions;

namespace CryptocurrencyQuote.Infrastructure
{
    public class ExchangeRatesAPI : ICryptocurrencyAPI
    {
        private IConfigurationAPI _configuration;
        public ExchangeRatesAPI(IConfigurationAPI configuration)
        {
            this._configuration = configuration;

        }
        public async Task<List<ExchangeRateDTO>> GetQuotes(CurrencyDTO fromCurrency, List<CurrencyDTO> toCurrencies)
        {
            var baseUrl = _configuration.Get().BaseUrl;
            var apiKey = _configuration.Get().APIKey;
            var client = new RestClient();
            List<ExchangeRateDTO> result = new List<ExchangeRateDTO>();
            string fromCurrencySymbols = toCurrencies.Select(x => x.Symbol).ToList().Aggregate((a, b) => a + "," + b);
            var request = new RestRequest(String.Format("{0}/latest?symbols={1}&base={2}",
                                   baseUrl, fromCurrencySymbols, fromCurrency.Symbol), Method.Get);
            request.AddHeader("apikey", apiKey);

            RestResponse response = (await client.ExecuteAsync(request));
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonConvert.DeserializeObject<LatestExchangeRateResponse>((response.Content ?? "").ToString());

                if (responseDto != null)
                {
                    var ratesDictionary = responseDto.Rates;
                    var keys = ratesDictionary.Keys;
                    foreach (var key in keys)
                    {
                        result.Add(new ExchangeRateDTO() { Currency = new CurrencyDTO() { IsCrypto = false, Symbol = key }, Price = ratesDictionary[key] });
                    }
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var exchangeRateError = JsonConvert.DeserializeObject<ExchangeRateError>((response.Content ?? "").ToString());
                if (exchangeRateError != null)
                {
                    throw new BadRequestException(exchangeRateError.Error.Code, exchangeRateError.Error.Message);
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var tooManyRequestsError = JsonConvert.DeserializeObject<ExchangeRateError>((response.Content ?? "").ToString());
                if (tooManyRequestsError != null)
                {
                    throw new TooManyRequestsException(tooManyRequestsError.Error.Message);
                }
            }
            return result;
        }
    }
}
