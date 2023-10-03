using CryptocurrencyQuote.Application.Models;
using CryptocurrencyQuote.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyQuote.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (!result.IsError && !result.Errors.Any())
            return new OkObjectResult(result.Data);

        var error = result.Errors.First();
        var errors = string.Join(',', result.Errors.Select(_ => _.Message));
        var apiError = new ErrorResponse
        {
            StatusCode = (int)error.HttpCode,
            StatusPhrase = error.HttpCode.ToString(),
            Timestamp = DateTime.Now
        };
        apiError.Errors.AddRange(result.Errors.Select(_ => new ErrorItem(_.Code, _.Message)));

        return error.HttpCode switch
        {
            HttpErrorCode.BadRequest => new BadRequestObjectResult(apiError),
            HttpErrorCode.NotFound => new NotFoundObjectResult(apiError),
            HttpErrorCode.Forbidden => new UnauthorizedObjectResult(apiError),
            HttpErrorCode.InternalServerError => throw new Exception(errors),
            _ => new ObjectResult(apiError),
        };
    }
}
