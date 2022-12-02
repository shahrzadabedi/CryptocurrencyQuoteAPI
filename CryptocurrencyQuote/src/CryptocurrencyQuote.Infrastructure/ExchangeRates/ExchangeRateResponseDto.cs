using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Infrastructure.ExchangeRates
{
    public class LatestExchangeRateResponse
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public bool Success { set; get; }
        public Dictionary<string,decimal> Rates {get; set; }


    }

    public class ExchangeRateError
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }

    public class TooManyRequestsError
    {
        public string Message { get; set; }
    }

    public class UnauthorizedRequestError
    {
        public string Message { get; set; }
    }


}
