using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model
{
    public class CryptocurrencyAPIConfig
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string APIKey { get; set; }
    }
}
