using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public System.Net.HttpStatusCode statusCode { get; private set; }
        public UnauthorizedException(string message) : base(message) {
            statusCode = System.Net.HttpStatusCode.Unauthorized;
        }
        
    }
}
