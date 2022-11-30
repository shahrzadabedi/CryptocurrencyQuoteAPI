using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model.Exceptions
{
    public class TooManyRequestsException :Exception
    {
        public TooManyRequestsException(string message) : base(message) { }
    }
}
