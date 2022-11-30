using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System;
using CryptocurrencyQuote.Domain.Model.Exceptions;

namespace CryptocurrencyQuote.Infrastructure.Tests
{
    public class ExchangeRateTest
    {
        [Theory]
        [InlineData("BTC")]
        [InlineData("GMD")]

        public async void GetQuotes_From_X_To_USD_EUR_GBP_AUD_BRL_Returns_Values_Successfully(string symbol)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = new List<Domain.Model.CurrencyDTO>() {
                new Domain.Model.CurrencyDTO() { IsCrypto= false,Symbol= "USD"} ,
                new Domain.Model.CurrencyDTO { IsCrypto= false,Symbol="EUR"},
                new Domain.Model.CurrencyDTO() {IsCrypto= false, Symbol="GBP"},
                new Domain.Model.CurrencyDTO(){IsCrypto= false,Symbol="AUD"},
                new Domain.Model.CurrencyDTO(){IsCrypto=! false,Symbol="BRL"}
            };
            //Act
            var quotes = await api.GetQuotes(new Domain.Model.CurrencyDTO() { IsCrypto = true, Symbol = symbol },toCurrencies);
            
            //Assert
            quotes.Count.Should().Equals(toCurrencies.Count);
            
        }

        [Theory]
        [InlineData("ETH")]
        [InlineData("BNB")]

        public async void GetQuotes_From_NotExistingSymbol_To_USD_Throws(string symbol)
        {
            //Arrange
            var api = new ExchangeRatesAPI(new MockExchangeRateAPIConfig());
            var toCurrencies = new List<Domain.Model.CurrencyDTO>() {
                new Domain.Model.CurrencyDTO() { IsCrypto= false,Symbol= "USD"} ,
            };
            //Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>  api.GetQuotes(new Domain.Model.CurrencyDTO() { IsCrypto = true, Symbol = symbol }, toCurrencies));

        }


    }
}