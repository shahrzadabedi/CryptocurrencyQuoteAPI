using CryptocurrencyQuote.Application.Models;

namespace CryptocurrencyQuote.Application.CryptoQuote.Dtos;

public class GetCryptoQuotesListResponse : Resource
{
    public GetCurrencyResponse Currency { get; set; }

    public decimal Price { get; set; } 
}
