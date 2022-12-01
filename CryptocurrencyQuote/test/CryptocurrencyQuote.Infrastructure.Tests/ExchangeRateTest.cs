using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using CryptocurrencyQuote.Domain.Model.Exceptions;
using CryptocurrencyQuote.Infrastructure.ExchangeRates;
using System.Linq;

namespace CryptocurrencyQuote.Infrastructure.Tests
{
    public class ExchangeRateTest
    {
        [Theory]
        [InlineData("BTC")]

        public async void GetQuotes_From_X_To_USD_EUR_GBP_AUD_BRL_Returns_Values_Successfully(string symbol)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = new List<Domain.Model.CurrencyDTO>() {
                new Domain.Model.CurrencyDTO() {Symbol= "USD"} ,
                new Domain.Model.CurrencyDTO { Symbol="EUR"},
                new Domain.Model.CurrencyDTO() { Symbol="GBP"},
                new Domain.Model.CurrencyDTO(){Symbol="AUD"},
                new Domain.Model.CurrencyDTO(){Symbol="BRL"}
            };
            //Act
            var quotes = await api.GetQuotes(new Domain.Model.CurrencyDTO() { Symbol = symbol }, toCurrencies);

            //Assert
            quotes.Count.Should().Be(toCurrencies.Count);

        }

        [Theory]
        [InlineData("ETH")]
        public async void GetQuotes_From_NotExistingSymbol_To_USD_Throws(string symbol)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = new List<Domain.Model.CurrencyDTO>() {
                new Domain.Model.CurrencyDTO() { Symbol= "USD"} ,
            };
            //Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => api.GetQuotes(new Domain.Model.CurrencyDTO() { Symbol = symbol }, toCurrencies));

        }
        
        [Theory]
        [InlineData("TTT")]
        public async void GetQuotes_From_BTC_To_NotExistingSymbol_Throws(string symbol)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = new List<Domain.Model.CurrencyDTO>() {
                new Domain.Model.CurrencyDTO() { Symbol= symbol} ,
            };

            //Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() => api.GetQuotes(new Domain.Model.CurrencyDTO() { Symbol = "BTC" }, toCurrencies));

        }
        
        [Theory]
        [InlineData("TTT,USD,EUR")]
        public async void GetQuotes_From_BTC_To_MixedOfExistingAndNotExistingSymbol_Throws(string toCurrenciesString)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = toCurrenciesString.Split(',').Select(p => new Domain.Model.CurrencyDTO() { Symbol = p }).ToList();

            //Act
            var quotes = await api.GetQuotes(new Domain.Model.CurrencyDTO() { Symbol = "BTC" }, toCurrencies);

            //Assert
           quotes.Count.Should().Be(2);

        }

    }
}