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

namespace CryptocurrencyQuote.Infrastructure.ExchangeRates
{
    public class ExchangeRatesAPI : ICryptocurrencyAPI
    {
        private IConfigurationAPI _configuration;
        public ExchangeRatesAPI(IConfigurationAPI configuration)
        {
            this._configuration = configuration;

        }
        public async Task<List<ExchangeRateDTO>> GetQuotesAsync(CurrencyDTO fromCurrency, List<CurrencyDTO> toCurrencies)
        {
            var configuration = _configuration.Get();
            var baseUrl = configuration.BaseUrl;
            var apiKey = configuration.APIKey;
            var client = new RestClient();
            List<ExchangeRateDTO> result = new List<ExchangeRateDTO>();
            string toCurrencySymbols = toCurrencies.Any()? toCurrencies.Select(x => x.Symbol).ToList().Aggregate((a, b) => a + "," + b):"";
            var request = new RestRequest(String.Format("{0}/latest?symbols={1}&base={2}",
                                   baseUrl, toCurrencySymbols, fromCurrency?.Symbol), Method.Get);
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
                        result.Add(new ExchangeRateDTO() { Currency = new CurrencyDTO() { Symbol = key }, Price = ratesDictionary[key] });
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
                var tooManyRequestsError = JsonConvert.DeserializeObject<TooManyRequestsError>((response.Content ?? "").ToString());
                if (tooManyRequestsError != null)
                {
                    throw new TooManyRequestsException(tooManyRequestsError.Message);
                }
            }else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var exchangeRateError = JsonConvert.DeserializeObject<UnauthorizedRequestError>((response.Content ?? "").ToString());
                if (exchangeRateError != null)
                {
                    throw new UnauthorizedException(exchangeRateError.Message);
                }

            }
            return result;
        }
        public async Task<List<CurrencyDTO>> GetSymbolsAsync()
        {
            var configuration = _configuration.Get();
            var baseUrl = configuration.BaseUrl;
            var apiKey = configuration.APIKey;
            var client = new RestClient();
            List<CurrencyDTO> result = new List<CurrencyDTO>();

            var request = new RestRequest(String.Format("{0}/symbols",
                                   baseUrl), Method.Get);
            request.AddHeader("apikey", apiKey);

            RestResponse response = (await client.ExecuteAsync(request));
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonConvert.DeserializeObject<SymbolsResponse>((response.Content ?? "").ToString());

                if (responseDto != null)
                {
                    var ratesDictionary = responseDto.Symbols;
                    var keys = ratesDictionary.Keys;
                    foreach (var key in keys)
                    {
                        result.Add(new CurrencyDTO(){ Symbol=key ,Description = ratesDictionary[key]});
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
