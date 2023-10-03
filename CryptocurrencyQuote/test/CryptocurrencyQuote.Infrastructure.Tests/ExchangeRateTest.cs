using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using CryptocurrencyQuote.Domain.Model.Exceptions;
using CryptocurrencyQuote.Infrastructure.ExchangeRates;
using System.Linq;
using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Infrastructure.Tests;

public class ExchangeRateTest
{
    [Theory]
    [InlineData("BTC")]

    public async void WhenGetQuotesFromXToUSD_EUR_GBP_AUD_BRL_ThenShouldSucceed(string symbol)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto>() {
            new CurrencyDto() {Symbol= "USD"} ,
            new CurrencyDto { Symbol="EUR"},
            new CurrencyDto() { Symbol="GBP"},
            new CurrencyDto(){Symbol="AUD"},
            new CurrencyDto(){Symbol="BRL"}
        };
        //Act
        var quotes = await api.GetQuotesAsync(new CurrencyDto() { Symbol = symbol }, toCurrencies);

        //Assert
        quotes.Count.Should().Be(toCurrencies.Count);
    }

    [Theory]
    [InlineData("ETH")]
    public async void WhenGetQuotesFromNotExistingSymbolToUSD_ThenShouldThrow(string symbol)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto>() {
            new CurrencyDto() { Symbol= "USD"} ,
        };

        //Act and Assert
        await Assert.ThrowsAsync<BadRequestException>(() => api.GetQuotesAsync(new CurrencyDto() { Symbol = symbol }, toCurrencies));
    }

    [Theory]
    [InlineData("TTT")]
    public async void WhenGetQuotesFromBTCToNotExistingSymbol_ThenShouldThrow(string symbol)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto>() {
            new CurrencyDto() { Symbol= symbol} ,
        };

        //Act and Assert
        await Assert.ThrowsAsync<BadRequestException>(() => api.GetQuotesAsync(new CurrencyDto() { Symbol = "BTC" }, toCurrencies));
    }

    [Theory]
    [InlineData("TTT,USD,EUR")]
    public async void WhenGetQuotesFromBTCToMixedOfExistingAndNotExistingSymbol_ThenShouldSucceed(string toCurrenciesString)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = toCurrenciesString.Split(',').Select(p => new CurrencyDto() { Symbol = p }).ToList();

        //Act
        var quotes = await api.GetQuotesAsync(new CurrencyDto() { Symbol = "BTC" }, toCurrencies);

        //Assert
        quotes.Count.Should().Be(2);
    }

    [Theory]
    [InlineData("BTC")]
    public async void WhenGetQuotesFromXToNothing_ThenShouldSucceed(string symbol)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto>();

        //Act
        var quotes = await api.GetQuotesAsync(new CurrencyDto() { Symbol = symbol }, toCurrencies);

        //Assert
        quotes.Count.Should().BeGreaterThan(2);
    }

    [Fact]
    public async void WhenGetQuotesFromNothingToNothing_ThenShouldSucceed()
    {
        //Arrange
        var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto>();

        //Act
        var quotes = await api.GetQuotesAsync(null, toCurrencies);

        //Assert
        quotes.Count.Should().BeGreaterThan(2);
    }

    [Theory]
    [InlineData("BTC")]

    public async void WhenGetQuotesFromBTCToUSD_EUR_GBP_AUD_BRLWithWrongCredentials_ThenShouldThrow(string symbol)
    {
        //Arrange
        var api = new ExchangeRatesAPI(new IncorrectMockExchangeRateAPIConfig());
        var toCurrencies = new List<CurrencyDto> {
            new CurrencyDto() {Symbol= "USD"} ,
            new CurrencyDto { Symbol="EUR"},
            new CurrencyDto() { Symbol="GBP"},
            new CurrencyDto(){Symbol="AUD"},
            new CurrencyDto(){Symbol="BRL"}
        };

        //Act and Assert
        await Assert.ThrowsAsync<UnauthorizedException>(() => api.GetQuotesAsync(new CurrencyDto() { Symbol = symbol }, toCurrencies));
    }
}