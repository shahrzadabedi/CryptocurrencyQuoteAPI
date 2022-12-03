using CryptocurrencyQuote.Domain.Model.Exceptions;
namespace CryptocurrencyQuote.WebAPI
{
    public   class StatusCodeHelper 
    {
       public System.Net.HttpStatusCode statusCode { get; private set; }
        public static explicit operator StatusCodeHelper( Exception exception)
        {
            StatusCodeHelper result = new StatusCodeHelper();
            switch (exception)
            {
                case BadRequestException:                    
                    result.statusCode= System.Net.HttpStatusCode.BadRequest;
                    break;
               
                case UnauthorizedException:
                    result.statusCode = System.Net.HttpStatusCode.Unauthorized;
                    break;
                case TooManyRequestsException:
                    result.statusCode = System.Net.HttpStatusCode.TooManyRequests;
                    break;
                default:
                    result.statusCode = System.Net.HttpStatusCode.InternalServerError;
                    break;

            }
            return result;
        }

    }
}
