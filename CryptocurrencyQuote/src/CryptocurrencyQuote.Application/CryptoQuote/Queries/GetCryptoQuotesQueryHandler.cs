using AutoMapper;
using CryptocurrencyQuote.Application.Constants;
using CryptocurrencyQuote.Application.CryptoQuote.Dtos;
using CryptocurrencyQuote.Application.Models;
using CryptocurrencyQuote.Domain.Model.Exceptions;
using CryptocurrencyQuote.Infrastructure;
using CryptocurrencyQuote.Infrastructure.Models;
using MediatR;

namespace CryptocurrencyQuote.Application.CryptoQuote.Queries;

public record GetCryotoQuotesQuery(string FromCurrency, List<string> ToCurrencies) : IRequest<Result<List<GetCryptoQuotesListResponse>>>;

public class GetCryptoQuotesQueryHandler : IRequestHandler<GetCryotoQuotesQuery,
    Result<List<GetCryptoQuotesListResponse>>>
{
    private readonly ICryptocurrencyAPI _cryptocurrencyAPI;
    
    private readonly IMapper _mapper;

    public GetCryptoQuotesQueryHandler(ICryptocurrencyAPI cryptocurrencyAPI,
        IMapper mapper)
    {
        _cryptocurrencyAPI = cryptocurrencyAPI;
        _mapper = mapper;
    }

    public async Task<Result<List<GetCryptoQuotesListResponse>>> Handle(GetCryotoQuotesQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var fromCurrency = request.FromCurrency == null ? null : new CurrencyDto() { Symbol = request.FromCurrency };
            var toCurrencies = request.ToCurrencies.Select(c => new CurrencyDto() { Symbol = c }).ToList();

            var quotes = await _cryptocurrencyAPI.GetQuotesAsync(fromCurrency, toCurrencies);

            var result = quotes.Select(_mapper.Map<GetCryptoQuotesListResponse>).ToList();

            return Result<List<GetCryptoQuotesListResponse>>.Success(result);
        }
        catch(UnauthorizedException ex)
        {
            return Result<List<GetCryptoQuotesListResponse>>.Error(
                new Error()
                { 
                    HttpCode = HttpErrorCode.Unauthorized,
                    Code = ErrorCodeConst.IdentityError,
                    Message = ex.Message,
                });
        }
        catch(TooManyRequestsException ex)
        {
            return Result<List<GetCryptoQuotesListResponse>>.Error(
                new Error()
                {
                    HttpCode = HttpErrorCode.TooManyRequests,
                    Code = ErrorCodeConst.TooManyRequestsError,
                    Message = ex.Message,
                });
        }
        catch (BadRequestException ex)
        {
            return Result<List<GetCryptoQuotesListResponse>>.Error(
                new Error()
                {
                    HttpCode = HttpErrorCode.BadRequest,
                    Code = ErrorCodeConst.ValidationError,
                    Message = ex.Message,
                });
        }
        catch (Exception exception)
        {
            return Result<List<GetCryptoQuotesListResponse>>.Error(exception);
        }
       
    }
}

