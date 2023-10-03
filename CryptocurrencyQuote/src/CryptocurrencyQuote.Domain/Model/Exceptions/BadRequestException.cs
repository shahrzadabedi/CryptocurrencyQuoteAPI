namespace CryptocurrencyQuote.Domain.Model.Exceptions;

public class BadRequestException : Exception
{
    private string _code;
    public BadRequestException(string code, string message) : base(message) {
        _code = code;
    }
}
