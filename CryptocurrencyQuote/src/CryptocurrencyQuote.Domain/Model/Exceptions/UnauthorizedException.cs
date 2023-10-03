namespace CryptocurrencyQuote.Domain.Model.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) {
       
    }    
}
