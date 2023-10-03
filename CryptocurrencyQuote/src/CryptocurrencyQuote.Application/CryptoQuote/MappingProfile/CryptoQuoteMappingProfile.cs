using AutoMapper;
using CryptocurrencyQuote.Application.CryptoQuote.Dtos;
using CryptocurrencyQuote.Infrastructure.Models;

namespace CryptocurrencyQuote.Application.CryptoQuote.MappingProfile;

public class CryptoQuoteMappingProfile : Profile
{
    public CryptoQuoteMappingProfile()
    {
        CreateMap<ExchangeRateDto, GetCryptoQuotesListResponse>();
        CreateMap<CurrencyDto, GetCurrencyResponse>();
    }
}
