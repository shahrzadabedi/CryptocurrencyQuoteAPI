namespace CryptocurrencyQuote.Application.Models;

public enum HttpErrorCode
{
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    Forbidden = 403,
    InternalServerError = 500,
    TooManyRequests = 429
}
