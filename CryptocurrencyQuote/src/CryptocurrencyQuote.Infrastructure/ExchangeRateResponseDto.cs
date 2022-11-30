using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Infrastructure
{
    public class ConvertExchangeRateResponseDto
    {
        public DateTime Date { get; set; }
        public Info Info { get; set; }
        public Query  Query { get; set; }
        public bool Success { get; set; }
        public decimal Result { get; set; }

    }
    public class Info
    {
        public decimal Rate { get; set; }
        public int TimeStamp { get; set; }

    }

    public class Query
    {
        public decimal Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }

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


}
