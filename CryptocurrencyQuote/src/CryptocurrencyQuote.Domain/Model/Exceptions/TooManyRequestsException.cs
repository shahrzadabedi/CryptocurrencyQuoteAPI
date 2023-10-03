namespace CryptocurrencyQuote.Domain.Model.Exceptions;

public class TooManyRequestsException :Exception
{
    public TooManyRequestsException(string message) : base(message) {           
    }
}
