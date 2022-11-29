using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyQuote.Domain.Model
{
    public class CurrencyDTO
    {
        public string Symbol { get; set; }
        public bool IsCrypto { get; set; }
    }
}
