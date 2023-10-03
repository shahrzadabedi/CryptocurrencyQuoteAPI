using CryptocurrencyQuote.Domain;
using RestSharp;
using Newtonsoft.Json;
using CryptocurrencyQuote.Domain.Model.Exceptions;
using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure.ExchangeRates;

public class ExchangeRatesAPI : ICryptocurrencyAPI
{
    private IConfigurationAPI _configuration;

    private string _basUrl;

    private string _apiKey;

    public ExchangeRatesAPI(IConfigurationAPI configuration)
    {
        _configuration = configuration;
        var config = _configuration.Get();
        _basUrl = config.BaseUrl;
        _apiKey = config.APIKey;
    }

    public async Task<List<ExchangeRateDto>> GetQuotesAsync(CurrencyDto fromCurrency, List<CurrencyDto> toCurrencies)
    {            
        var request = PrepareQuotesRequest(fromCurrency,toCurrencies);

        var result = new List<ExchangeRateDto>();

        var response = await new RestClient().ExecuteAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            result = PrepareQuotesResult(response);
        }
        else
        {
            HandleErrorResponse(response);
        } 

        return result;
    }

    private RestRequest PrepareQuotesRequest(CurrencyDto fromCurrency, List<CurrencyDto> toCurrencies)
        {
            var toCurrencySymbols = toCurrencies.Any() ? toCurrencies.Select(x => x.Symbol).ToList().Aggregate((a, b) => a + "," + b) : "";
            
            var request = new RestRequest(string.Format("{0}/latest?symbols={1}&base={2}",
                                   _basUrl, toCurrencySymbols, fromCurrency?.Symbol), Method.Get);
            request.AddHeader("apikey", _apiKey);
            
            return request;
        }
    
    private List<ExchangeRateDto> PrepareQuotesResult(RestResponse response)
    {
        var responseDto = JsonConvert.DeserializeObject<LatestExchangeRateResponse>((response.Content ?? "").ToString());
        
        var result = new List<ExchangeRateDto>();
        if (responseDto != null)
        {
            var ratesDictionary = responseDto.Rates;
            var keys = ratesDictionary.Keys;
            foreach (var key in keys)
            {
                result.Add(new ExchangeRateDto() 
                {
                    Currency = new CurrencyDto() { Symbol = key },
                    Price = ratesDictionary[key] 
                });
            }
        }
        
        return result;
    }

    private void HandleErrorResponse(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
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
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var exchangeRateError = JsonConvert.DeserializeObject<UnauthorizedRequestError>((response.Content ?? "").ToString());
                if (exchangeRateError != null)
                {
                    throw new UnauthorizedException(exchangeRateError.Message);
                }
            }
        }
}
